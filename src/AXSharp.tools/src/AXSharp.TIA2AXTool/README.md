# TIA2AXTool

## Installation

To install the tool run the following command. 
~~~
dotnet tool install AXSharp.TIA2AXTool --global
~~~

Or copy `dotnet-tools.json` in your local folder

~~~~
{
  "version": 1,
  "isRoot": true,
  "tools": {
    "axsharp.tia2axtool": {
      "version": YOUR_VERSION,
      "commands": [
        "tia2ax"
      ]
    }
  }
}
~~~~

and  run

~~~
dotnet tool restore
~~~

## How to prepare TIA portal project

[!Video https://youtu.be/d9EX2FixY1A?t=151]


TIA2AXTool is simple CLI program, which is used for generation of AX# TwinObjects in form of Json from TIA datablocks. Output of CLI command is .json file which contains list of TIABrowseElements. This .json file can be deserialized into `TIARootObject` which than can be used for data exchange operations between TIA based plc and .NET application.



## Parameters 

- -o --output : output .json file with serialized datablocks structures (required)
- -i --ipaddress : ip address for connecting to PLC (required)
- -u --username : username for connection (default value: *Everybody*)
- -p --password : password for connection (default value: "")
- -d --datablocks : list of datablocks names, which should be scanned (if not provided, all datablocks will be serialized)
- -s --symbol : name of symbol, which only his children will be deserialized

Notes:
  - -d and -s parameters shouldn't be used together
  - two dimensional arrays are not supported in -s parameter

## Examples

Get elements by in `dbtest` datablock:

`tia2ax -i 10.10.10.180 -o D:\\testdata.json -d dbtest`

Output is following .json file:

```json
{
  "TIABrowseElements": [
    {
      "Symbol": "TGlobalVariablesDB",
      "Datatype": 1,
      "IsNested": true,
      "Children": [
        {
          "Symbol": "myArrayComplexNested[0]",
          "Datatype": 81,
          "IsNested": true,
          "Children": [
            {
              "Symbol": "nesteditemBOOL",
              "Datatype": 2,
              "IsNested": false,
              "Children": []
            },
            //truncated...
```
Get elements only from symbol `TGlobalVariablesDB.mins` downwards datablock:

`tia2ax -i 172.20.30.110 -o D:\\test.json -s TGlobalVariablesDB.mins`

```json
{
  "TIABrowseElements": [
    {
      "Symbol": "myBOOL",
      "Datatype": 2,
      "IsNested": true,
      "Children": []
    },
    {
      "Symbol": "myBYTE",
      "Datatype": 3,
      "IsNested": true,
      "Children": []
    },
    //truncated...
```
## Usage in application
The generated file can be deserialized and used in following way:

```C#
  // connect to your plc
  var connector = new WebApiConnector("10.10.10.180", "Everybody", "", true, string.Empty);
  //creates adapter from deserialized root object from test.json file
  var adapter = await TIA2AXSharpAdapter.CreateAdapter(connector, "test.json");
  //retrieve all primitive variables
  var allVariables = adapter.RetrievePrimitives();
  // read variables  
  await connector.ReadBatchAsync(allVariables);

```

Note: Make sure, your application reference `AxSharp.TIA2AXSharp` project