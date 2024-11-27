// AXSharp.Connector
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector;

/// <summary>
///     Extensions methods for <see cref="AXSharp.Connector.ITwinObject" />.
/// </summary>
public static class TwinObjectExtensions
{

    public static readonly string OnlineToPlainMethodName = nameof(ITwinObject.OnlineToPlain);

    public static readonly string PlainToOnlineMethodName = nameof(ITwinObject.PlainToOnline);

    public static readonly string ShadowToPlainMethodName = nameof(ITwinObject.ShadowToPlain);

    public static readonly string PlainToShadowMethodName = nameof(ITwinObject.PlainToShadow);

    public static readonly string HasChangedMethodName = nameof(ITwinObject.AnyChangeAsync);
    
    /// <summary>
    ///     Makes <see cref="AXSharp.Connector.ITwinObject" /> readonly for this application.
    /// </summary>
    /// <example>
    ///     <code>
    ///     // Renders whole structure value tag readonly
    ///     ReadonlyStructure.MakeReadOnly();
    ///     // Renders element [0,0] of the array readonly.
    ///     roArray[0, 0].MakeReadOnly();
    /// </code>
    /// </example>
    /// <param name="structure">Object to be rendered readonly</param>
    public static void MakeReadOnly(this ITwinObject structure)
    {
        ArgumentNullException.ThrowIfNull(structure);
        foreach (var item in structure.GetValueTags()) item.MakeReadOnly();

        foreach (var item in structure.GetChildren()) item.MakeReadOnly();
    }

    /// <summary>
    ///     Makes <see cref="AXSharp.Connector.ITwinObject" /> access only once during the lifetime
    ///     of the application.
    /// </summary>
    /// <example>
    ///     <code>
    ///     // Renders whole complex type access only once.
    ///     ReadonlyStructure.MakeReadOnce();
    ///     // Renders element [0,0] of the array readonly.
    ///     roArray[0, 0].MakeReadOnce();
    /// </code>
    /// </example>
    /// <param name="structure">Object to be rendered readonly</param>
    public static void MakeReadOnce(this ITwinObject structure)
    {
        ArgumentNullException.ThrowIfNull(structure);
        foreach (var item in structure.GetValueTags()) item.MakeReadOnce();

        foreach (var item in structure.GetChildren()) item.MakeReadOnce();
    }

    /// <summary>
    ///     Reads all value tags of instance <see cref="ITwinOnlineObject" />.
    /// </summary>
    /// <example>
    ///     <code>
    /// // Reads all value tags of the MAIN PRG. The value is stored in property 'Cyclic' and 'LastValue' of the respective value tag.
    /// Connector.MAIN.ReadAsync();
    /// </code>
    /// </example>
    /// <param name="structure"></param>
    public static async Task<IEnumerable<ITwinPrimitive>> ReadAsync(this ITwinObject structure)
    {
        ArgumentNullException.ThrowIfNull(structure);
        var primitives = RetrievePrimitives(structure);
        var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();
        await structure.GetConnector().ReadBatchAsync(twinPrimitives);
        return twinPrimitives;
    }

    /// <summary>
    /// Reads all value tags of instance <see cref="ITwinOnlineObject"/>, but ignores members that are annotated with attribute as generic parameter.
    /// > [!IMPORTANT]
    /// > This method is used for specific framework purposed and there is no specific reason to use it in your application.
    /// > Use non generic version of this method <see cref="ReadAsync"/> instead.
    /// </summary>
    /// <typeparam name="T">Attribute parameter to be ignored</typeparam>
    /// <param name="structure">Structure to be read</param>
    /// <returns>List of read items.</returns>
    public static async Task<IEnumerable<ITwinPrimitive>> ReadAsync<T>(this ITwinObject structure) where T : Attribute
    {
        ArgumentNullException.ThrowIfNull(structure);
        var primitives = RetrievePrimitives<T>(structure);
        var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();
        await structure.GetConnector().ReadBatchAsync(twinPrimitives);
        return twinPrimitives;
    }

    /// <summary>
    ///     Writes all value tags of instance <see cref="ITwinOnlineObject" />
    /// </summary>
    /// <example>
    ///     <code>
    /// // Writes all modified values of the MAIN PRG.
    /// Connector.Online.MAIN.GrowingLayoutStructure.Write()
    /// </code>
    /// </example>
    /// <param name="structure">Structure to be written</param>
    /// <returns>Primitives that have been written.</returns>
    public static async Task<IEnumerable<ITwinPrimitive>> WriteAsync(this ITwinObject structure)
    {
        var primitives = RetrievePrimitives(structure);
        var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();
        await structure.GetConnector().WriteBatchAsync(twinPrimitives);
        return twinPrimitives;
    }

    /// <summary>
    /// Writes all value tags of instance <see cref="ITwinOnlineObject" />,
    /// but ignores members that are annotated with attribute as generic parameter.
    /// > [!IMPORTANT]
    /// > This method is used for specific framework purposed and there is no specific reason to use it in your application.
    /// > Use non generic version of this method <see cref="WriteAsync"/> instead.
    /// </summary>
    /// <typeparam name="T">Attribute parameter to be ignored</typeparam>
    /// <param name="structure">Structure to be written.</param>
    /// <returns>List of written items.</returns>
    public static async Task<IEnumerable<ITwinPrimitive>> WriteAsync<T>(this ITwinObject structure) where T : Attribute
    {
        var primitives = structure.RetrievePrimitives<T>();
        var twinPrimitives = primitives as ITwinPrimitive[] ?? primitives.ToArray();
        await structure.GetConnector().WriteBatchAsync(twinPrimitives);
        return twinPrimitives;
    }

    /// <summary>
    ///     Retrieves all value tags of given object recursively.
    /// </summary>
    /// <param name="onlineObject">Object from which the value tags are to be retrieved.</param>
    /// <param name="valueTags">Pre-existing value tags.</param>
    /// <returns>Value tags of given object.</returns>
    /// <example>
    ///     This example demonstrates how to get all value tags of the MAIN PRG object.
    ///     <code>
    ///     var mainProgramTags = Connector.MAIN.RetrievePrimitives();
    /// </code>
    /// </example>
    public static IEnumerable<ITwinPrimitive> RetrievePrimitives(this ITwinObject onlineObject,
        List<ITwinPrimitive> valueTags = null)
    {
        ArgumentNullException.ThrowIfNull(onlineObject);

        valueTags ??= new List<ITwinPrimitive>();

        var children = onlineObject.GetChildren();

        if (children != null)
            foreach (var child in children)
                RetrievePrimitives(child, valueTags);

        var tags = onlineObject.GetValueTags();

        if (tags != null)
            foreach (var valueTag in tags)
                valueTags.Add(valueTag);

        return valueTags;
    }

    /// <summary>
    ///     Retrieves all value tags of given object recursively.
    /// </summary>
    /// <param name="onlineObject">Object from which the value tags are to be retrieved.</param>
    /// <param name="valueTags">Pre-existing value tags.</param>
    /// <returns>Value tags of given object.</returns>
    /// <example>
    ///     This example demonstrates how to get all value tags of the MAIN PRG object.
    ///     <code>
    ///     var mainProgramTags = Connector.MAIN.RetrievePrimitives();
    /// </code>
    /// </example>
    private static IEnumerable<ITwinPrimitive> RetrievePrimitives<T>(this ITwinObject onlineObject,
        List<ITwinPrimitive> valueTags = null) where T : Attribute
    {
        ArgumentNullException.ThrowIfNull(onlineObject);

        valueTags ??= new List<ITwinPrimitive>();

        var children = onlineObject.GetChildren().Where(p => !p.HasAttribute<T>());

        if (children != null)
            foreach (var child in children)
                RetrievePrimitives<T>(child, valueTags);

        var tags = onlineObject.GetValueTags().Where(p => !p.HasAttribute<T>());

        if (tags != null)
            foreach (var valueTag in tags)
                valueTags.Add(valueTag);

        return valueTags;
    }

    private static PropertyInfo GetPropertyViaSymbol(ITwinElement twinObject)
    {
        if (twinObject == null) return null;
        var propertyName = twinObject.GetSymbolTail();

        if (twinObject.Symbol == null)
            return null;

        if (twinObject.Symbol.EndsWith("]"))
        {
            propertyName = propertyName?.Substring(0, propertyName.IndexOf('[') - 1);
        }

        var propertyInfo = twinObject?.GetParent()?.GetType().GetProperty(propertyName);

        return propertyInfo;
    }

    public static bool HasAttribute<T>(this ITwinElement twinElement) where T : Attribute
    {
        var retVal = false;
        if (twinElement == null) return false;
        try
        {
            var propertyInfo = GetPropertyViaSymbol(twinElement);
            if (propertyInfo != null)
            {
                if (propertyInfo
                        .GetCustomAttributes().FirstOrDefault(p => p is T) is T propertyAttribute)
                {
                    retVal = true;
                }
            }

            if (twinElement
                    .GetType()
                    .GetCustomAttributes(true)
                    .FirstOrDefault(p => p is T) != null)
            {
                retVal = true;
            }

            
        }
        catch (Exception)
        {
            //throw;
        }

        return retVal;
    }


    /// <summary>
    ///     Subscribes a delegate to be invoked when any <see cref="OnlinerBase{T}.Shadow" /> value on given object changes its
    ///     value.<see cref="AXSharp.Connector.ITwinObject" />
    /// </summary>
    /// <example>
    ///     <code>
    /// class ShadowValueChangeObserver
    /// {
    ///     public ShadowValueChangeObserver()
    ///     {
    ///         Connector.MAIN.SubscribeShadowValueChange(DetectShadowValueChange);                 
    ///     }
    ///     
    ///     private void DetectShadowValueChange(IValueTag valueTag, dynamic original, dynamic newValue)
    ///     {
    ///         Console.WriteLine($"Value '{valueTag.Symbol}' has changed form {original} to {newValue}.")
    ///     }
    ///      
    /// }
    /// </code>
    /// </example>
    /// <note>
    ///     Notice that the value change observer uses is not an event but simple delegate. Any previous delegate assignment
    ///     will be overwritten by the subscription of the new
    ///     delegate.
    /// </note>
    /// <param name="obj">Observed object.</param>
    /// <param name="valueChangeDelegate">Delegate to be invoked on shadow value change.</param>
    /// <returns>Primitives that have been subscribed for shadow value change.</returns>
    public static IEnumerable<ITwinPrimitive> SubscribeShadowValueChange(this ITwinObject obj,
        OnlinerBase.ValueChangeDelegate valueChangeDelegate)
    {
        ArgumentNullException.ThrowIfNull(valueChangeDelegate);
        var valueTags = obj.RetrievePrimitives();
        var subscribeShadowValueChange = valueTags as ITwinPrimitive[] ?? valueTags.ToArray();
        foreach (var tag in subscribeShadowValueChange) tag.ShadowValueChange = valueChangeDelegate;

        return subscribeShadowValueChange;
    }

    /// <summary>
    ///     Un-subscribes <see cref="OnlinerBase{T}.Shadow" /> value change delegate from the value tags of given object.
    /// </summary>
    /// <param name="obj">Object from which the shadow value change delegate has to be removed.</param>
    public static void UnSubscribeShadowValueChange(this ITwinObject obj)
    {
        var valueTags = obj.RetrievePrimitives();
        foreach (var tag in valueTags) tag.ShadowValueChange = null;
    }


    /// <summary>
    ///     Subscribes a delegate to be invoked when any <see cref="OnlinerBase{T}.Edit" /> value on given object changes its
    ///     value.<see cref="AXSharp.Connector.ITwinObject" />
    /// </summary>
    /// <example>
    ///     <code>
    /// class EditValueChangeObserver
    /// {
    ///     public EditValueChangeObserver()
    ///     {
    ///         Connector.MAIN.SubscribeEditValueChange(DetectEditValueChange);                 
    ///     }
    ///     
    ///     private void DetectEditValueChange(IValueTag valueTag, dynamic original, dynamic newValue)
    ///     {
    ///         Console.WriteLine($"Value '{valueTag.Symbol}' has changed form {original} to {newValue}.")
    ///     }
    ///      
    /// }
    /// </code>
    /// </example>
    /// <note>
    ///     Notice that the value change observer uses is not an event but simple delegate. Any previous delegate assignment
    ///     will be overwritten by the subscription of the new
    ///     delegate.
    /// </note>
    /// <param name="obj">Observed object.</param>
    /// <param name="valueChangeDelegate">Delegate to be invoked on Edit value change.</param>
    /// <returns></returns>
    public static IEnumerable<ITwinPrimitive> SubscribeEditValueChange(this ITwinObject obj,
        OnlinerBase.ValueChangeDelegate valueChangeDelegate)
    {
        ArgumentNullException.ThrowIfNull(valueChangeDelegate);
        var valueTags = obj.RetrievePrimitives();
        foreach (var tag in valueTags) tag.EditValueChange = valueChangeDelegate;

        return valueTags;
    }

    /// <summary>
    ///     Un-subscribes <see cref="OnlinerBase{T}.Edit" /> value change delegate from the value tags of given object.
    /// </summary>
    /// <param name="obj">Object from which the <see cref="OnlinerBase{T}.Edit" /> value change delegate has to be removed.</param>
    public static void UnSubscribeEditValueChange(this ITwinObject obj)
    {
        var valueTags = obj.RetrievePrimitives();
        foreach (var tag in valueTags) tag.EditValueChange = null;
    }

    /// <summary>
    /// Copies the data from Online primitive items (PLC) of an <see cref="ITwinObject"/> to shadow value holders.
    /// </summary>
    /// <param name="obj">Twin object to copy.</param>
    public static async Task OnlineToShadowAsync(this ITwinObject obj)
    {
        await obj.ReadAsync();
        obj.RetrievePrimitives().ToList().ForEach(p => p.FromOnlineToShadow());
    }

    /// <summary>
    /// Copies the data from Shadow value holder to online primitive items (PLC) of an <see cref="ITwinObject"/>.
    /// </summary>
    /// <param name="obj">Twin object to copy.</param>
    public static async Task ShadowToOnlineAsync(this ITwinObject obj)
    {
        obj.RetrievePrimitives().ToList().ForEach(p => p.FromShadowToOnline());
        await obj.WriteAsync();
    }

    /// <summary>
    /// Starts polling data from a <see cref="ITwinElement"/> at given interval.
    /// </summary>
    /// <param name="obj">Object to be polled.</param>
    /// <param name="interval">Polling interval in ms.</param>
    /// <param name="holder">Object which owns this polling.</param>
    public static void StartPolling(this ITwinElement obj, int interval, object holder)
    {
        Polling.Add(obj, interval, holder);
    }

    /// <summary>
    /// Stop polling data from a <see cref="ITwinElement"/>.
    /// Polling mechanism checks whether there is another polling instance active for the given object.
    /// If there is any remaining instance active the polling will continue until the last instance is stopped.
    /// </summary>
    /// <param name="obj">Object for which the polling should be stopped.</param>
    /// <param name="holder">Object which owns this polling.</param>
    public static void StopPolling(this ITwinElement obj, object holder)
    {
        Polling.Remove(obj, holder);
    }

    /// <summary>
    /// Creates new unpopulated instance of POCO object for this twin object.
    /// <remarks type="important">
    /// This method uses reflections. It is not suitable for operations where performance matters.
    /// </remarks>
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>New empty instance of POCO object for this twin object</returns>
    public static object CreatePoco(this ITwinObject obj)
    {
        var createEmptyPocoMethod = obj.GetType().GetMethod("CreateEmptyPoco");
        return createEmptyPocoMethod.Invoke(obj, null);
    }
}