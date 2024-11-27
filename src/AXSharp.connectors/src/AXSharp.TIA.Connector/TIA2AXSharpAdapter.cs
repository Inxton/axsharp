// AXSharp.TIA2AXSharp
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/dev/notices.md

using AXSharp.Connector;
using AXSharp.Connector.S71500.WebApi;
using AXSharp.TIA2AXSharp;
using Newtonsoft.Json;
using Siemens.Simatic.S7.Webserver.API.Enums;
using Siemens.Simatic.S7.Webserver.API.Models;
using Siemens.Simatic.S7.Webserver.API.Services;
using Siemens.Simatic.S7.Webserver.API.Services.RequestHandling;

namespace AXSharp.TIA2AXSharp;

/// <summary>
/// Class containing methods for creating a TIA2AX adapter
/// </summary>
public class TIA2AXSharpAdapter
{
    private readonly ApiStandardServiceFactory serviceFactory = new();

    private TIA2AXSharpAdapter()
    {
    }
    /// <summary>
    /// Creates TIARootObject by scanning provided datablock with WebApi
    /// </summary>
    /// <param name="connector">Instance of WebApiConnector</param>
    /// <param name="dataBlockNames">Datablock names</param>
    /// <returns>TIARootObject</returns>
    public static async Task<TIARootObject> CreateTIARootObject(WebApiConnector connector, string[] dataBlockNames)
    {
        var browseElements = new List<TIABrowseElement>();
        var adapter = new TIA2AXSharpAdapter();
        var requestHandler = await adapter.serviceFactory.GetApiHttpClientRequestHandlerAsync(connector.IPAddress, "Everybody", "");

        List<ApiPlcProgramData> programBlocks = (await requestHandler.PlcProgramBrowseAsync(ApiPlcProgramBrowseMode.Children)).Result;

        if (dataBlockNames != null && dataBlockNames.Length > 0)
        {
            programBlocks = programBlocks.Where(x => dataBlockNames.Any(name => name == x.Name)).ToList();
        }

        foreach (var dataBlockNode in programBlocks)
        {
            var dbe = new TIABrowseElement(dataBlockNode.Name, dataBlockNode.Datatype, true);
            await BrowseParent(dataBlockNode, requestHandler, dbe, 1);
            browseElements.Add(dbe);
        }

        return new TIARootObject { TIABrowseElements = browseElements };
    }
    /// <summary>
    /// Creates TIARootObject from provided connector
    /// </summary>
    /// <param name="connector">Instance of WebApiConnector</param>
    /// <returns>TIARootObject</returns>
    public static async Task<TIARootObject> CreateTIARootObject(WebApiConnector connector)
        => await CreateTIARootObject(connector, new string[]{});

    /// <summary>
    /// Creates TIARootObject from provided connector and from symbol downwards
    /// </summary>
    /// <param name="connector">Instance of WebApiConnector</param>
    /// <param name="symbol">Symbol from which browsing should start.</param>
    /// <returns>TIARootObject</returns>
    public static async Task<TIARootObject> CreateTIARootObject(WebApiConnector connector, string symbol)
    {
        var browseElements = new List<TIABrowseElement>();
        var adapter = new TIA2AXSharpAdapter();
        var requestHandler = await adapter.serviceFactory.GetApiHttpClientRequestHandlerAsync(connector.IPAddress, "Everybody", "");


        var programBlocksVar = (await requestHandler.PlcProgramBrowseAsync(ApiPlcProgramBrowseMode.Var, symbol)).Result;
 
        // if it object with no children, just create it 
        if (programBlocksVar.Count == 1 && programBlocksVar.First().Has_children.GetValueOrDefault() == false)
        {
            var dbe = new TIABrowseElement(programBlocksVar.First().Name, programBlocksVar.First().Datatype, false);
            browseElements.Add(dbe);
        }
        else
        {
            // otherwise, we have to go recursively through his children
            List<ApiPlcProgramData> programBlocks = (await requestHandler.PlcProgramBrowseAsync(ApiPlcProgramBrowseMode.Children, symbol)).Result;

            foreach (var dataBlockNode in programBlocks)
            {
                var dbe = new TIABrowseElement(dataBlockNode.Name, dataBlockNode.Datatype, true);
                await CreateTwin(dataBlockNode, requestHandler, dbe, 1);
                browseElements.Add(dbe);
            }
        }

        return new TIARootObject { TIABrowseElements = browseElements };
    }


    /// <summary>
    /// Creates adapter to Connector in form of list of generated TwinObjects from TIARootObject
    /// </summary>
    /// <param name="connector">Connector instance</param>
    /// <param name="rootObject">Input TIARootObject</param>
    /// <returns>List of ITwinObjects</returns>
    public static async Task<IEnumerable<ITwinObject>> CreateAdapter(AXSharp.Connector.Connector connector, TIARootObject rootObject)
    {
        var twins = new List<ITwinObject>();

        foreach (var dataBlockNode in rootObject.TIABrowseElements)
        {
            var db = new TIATwinObject(connector, $"\"{dataBlockNode.Symbol}\"", $"\"{dataBlockNode.Symbol}\"");
            await CreateTwinFromSerialized(dataBlockNode, db, 1);
            twins.Add(db);
        }
        return twins;
    }

    /// <summary>
    /// Creates adapter to Connector in form of list of generated TwinObjects from TIARootObject
    /// </summary>
    /// <param name="connector">Connector instance</param>
    /// <param name="path">Path to json containing TIARootObject</param>
    /// <returns>List of ITwinObjects</returns>
    public static async Task<IEnumerable<ITwinObject>> CreateAdapter(AXSharp.Connector.Connector connector, string path)
    {
        var rootObject = TIA2AXSharpSerializer.Deserialize(path);
        if(rootObject == null)
        {
            throw new Exception("Deserialization error, check your path or rootobject.");
        }

        return await CreateAdapter(connector, rootObject);

    }
    /// <summary>
    /// Creates adapter to WebApiConnector by scanning it with PlcBrowse
    /// </summary>
    /// <param name="connector">WepApiConnector</param>
    /// <returns>List of ITwinObjects</returns>
    public static async Task<IEnumerable<ITwinObject>> CreateAdapter(WebApiConnector connector)
    {
        var twins = new List<ITwinObject>();
        var adapter = new TIA2AXSharpAdapter();
        var rootObject = new TIARootObject();

        var requestHandler =
            await adapter.serviceFactory.GetApiHttpClientRequestHandlerAsync(connector.IPAddress, "Everybody", "");


        List<ApiPlcProgramData> programBlocks =
        (await requestHandler.PlcProgramBrowseAsync(ApiPlcProgramBrowseMode.Children)).Result;

        var dataBlockNodes = programBlocks.Where(el => el.Datatype == ApiPlcProgramDataType.DataBlock);

        foreach (var dataBlockNode in dataBlockNodes)
        {
            var db = new TIATwinObject(connector, $"\"{dataBlockNode.Name}\"", $"\"{dataBlockNode.Name}\"");
            await BrowseParent(dataBlockNode, requestHandler, db, 1);
            twins.Add(db);
        }

        return twins;
    }

    private static async Task CreateTwinFromSerialized(TIABrowseElement parentNode,
        ITwinObject parentTwinObject, int dept)
    {

        foreach (var child in parentNode.Children)
        {
            if (child.IsNested)
            {
                var nested = new TIATwinObject(parentTwinObject, child.Symbol, child.Symbol);
                parentTwinObject.AddChild(nested);
                parentTwinObject.AddKid(nested);
                await CreateTwinFromSerialized(child, nested, dept);
            }
            else
            {
                CreatePrimitiveFromSerialized(child, parentTwinObject);
            }
        }

    }


    private static async Task BrowseParent(ApiPlcProgramData parentNode, IApiRequestHandler requestHandler,
    ITIAGenericObject parent, int dept)
    {
        dept++;

        if (dept > 9)
            return;

        var children = requestHandler.PlcProgramBrowseAsync(ApiPlcProgramBrowseMode.Children, parentNode).Result.Result;
        parentNode.Children = children;


        await CreateTwin(parentNode, requestHandler, parent, dept);

    }

    private static async Task CreateTwin(ApiPlcProgramData parentNode, IApiRequestHandler requestHandler,
        ITIAGenericObject parentObject, int dept)
    {

        foreach (var child in parentNode.Children)
        {
            child.Parents.AddRange(parentNode.Parents);
            child.Parents.Add(parentNode);

            if (child.ArrayElements.Any())
            {
                if (child.Array_dimensions.Count > 1)
                    continue;

                foreach (var arrayElement in child.ArrayElements)
                {
                    if (arrayElement.Has_children == true)
                    {

                        var tiaObject = CreateTIAObject(parentObject, arrayElement.Name, arrayElement.Datatype, true);

                        await BrowseParent(arrayElement, requestHandler, tiaObject, dept);
                    }
                    else
                    {
                        //this method automaticcaly adds valuetag and kid
                        CreateTIAPrimitive(parentObject, arrayElement);
                    }
                }

            }
            else if (child.Has_children == true)
            {

                if (string.IsNullOrEmpty(child.Name))
                {
                    throw new Exception($"Inheritance error, not supported. Parent is {parentNode.Name}");
                }
                else
                {
                    var tiaObject = CreateTIAObject(parentObject, child.Name, child.Datatype, true);

                    await BrowseParent(child, requestHandler, tiaObject, dept);
                }
            }
            else
            {
                //this method automaticcaly adds value tags and kids
                CreateTIAPrimitive(parentObject, child);
            }
        }

    }


    private static ITIAGenericObject CreateTIAObject(ITIAGenericObject parent, string name, ApiPlcProgramDataType dataType, bool isNested) 
    {
        ITIAGenericObject tIAGenericObject = null;

        if (parent is TIATwinObject tiaTwin)
        {
            var nested = new TIATwinObject(tiaTwin, name, name);
            tiaTwin.AddChild(nested);
            tiaTwin.AddKid(nested);
            tIAGenericObject = nested;
        }

        if (parent is TIABrowseElement tiaBrowse)
        {
            var browse = new TIABrowseElement(name, dataType, isNested);
            tiaBrowse.Children.Add(browse);
            tIAGenericObject = browse;
        }

        if(tIAGenericObject == null) 
        {
            throw new Exception("Parent object cannot be null! Check your input.");
        }
        return tIAGenericObject;
    }

    private static void CreateTIAPrimitive(ITIAGenericObject parent, ApiPlcProgramData child)
    {

        if (parent is TIATwinObject tiaTwin)
        {
            CreatePrimitive(child, tiaTwin);
        }

        if (parent is TIABrowseElement tiaBrowse)
        {
            var primitive = new TIABrowseElement(child.Name, child.Datatype, false);
            tiaBrowse.Children.Add(primitive);
        }
    }

    private static ITwinPrimitive CreatePrimitive(ApiPlcProgramData node, ITwinObject parent)
    {
        string symbolTail = node.Name;
        string nameTail = symbolTail;
        return GetDatatype(node.Datatype, parent, symbolTail, nameTail);
    }

    private static ITwinPrimitive CreatePrimitiveFromSerialized(TIABrowseElement node, ITwinObject parent)
    {
        string symbolTail = node.Symbol;
        string nameTail = symbolTail;
        return GetDatatype(node.Datatype, parent, symbolTail, nameTail);
    }

    private static ITwinPrimitive GetDatatype(ApiPlcProgramDataType datatype, ITwinObject parent, string symbolTail, string nameTail)
    {
        switch (datatype)
        {
            case ApiPlcProgramDataType.Bool:
                return new WebApiBool(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Byte:
                return new WebApiByte(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Usint:
                return new WebApiUSInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Sint:
                return new WebApiSInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Char:
                return new WebApiChar(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Wchar:
                return new WebApiWChar(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Int:
                return new WebApiInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Date:
                return new WebApiDate(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Uint:
                return new WebApiUInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Word:
                return new WebApiWord(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Date_and_time:
                return new WebApiDateTime(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Dint:
                return new WebApiDInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Time:
                return new WebApiTime(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Dword:
                return new WebApiDWord(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Time_of_day:
                return new WebApiTimeOfDay(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Udint:
                return new WebApiUdInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Real:
                return new WebApiReal(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Lreal:
                return new WebApiLReal(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Ldt:
                return new WebApiLDateTime(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Lint:
                return new WebApiLInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Ltime:
                return new WebApiLTime(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Ulint:
                return new WebApiULInt(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Lword:
                return new WebApiLWord(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Ltime_of_day:
                return new WebApiLTimeOfDay(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.String:
                return new WebApiString(parent, symbolTail, nameTail);
            case ApiPlcProgramDataType.Wstring:
                return new WebApiWString(parent, symbolTail, nameTail);
            default:
                return null;
        }
    }



}