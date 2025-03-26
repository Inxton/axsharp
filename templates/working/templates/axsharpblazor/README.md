# AXSharp Blazor template 

**IMPORTANT!!! When you create the project from Visual Studio, you will need to run `install.ps1` manually to finish creating the project.**



## Setting up the connection

### .NET


Go to [Entry.[cs](axsharpblazor/Entry.cs) and set the following parameters

~~~C#
private const string TargetIp = "192.168.0.4"; // <- replace by IP of your target PLC
private const string UserName = "Everybody"; //<- replace by user name you have set up in your WebAPI settings
private const string Pass = ""; // <- Pass in the password that you have set up for the user. NOT AS PLAIN TEXT! Use user secrets instead.
private const bool IgnoreSslErrors = true; // <- When you have your certificates in order set this to false.
~~~

You will need to use TIA Portal to enable WebAPI interface [see here](https://console.simatic-ax.siemens.io/docs/hwld/PlcWebServer) and [here](https://youtu.be/d9EX2FixY1A?t=151) is a very informative youtube video.


### AX

Go to [apax.yml](ax/apax.yml) file and adjust the parameters

~~~yml
.
.
.
variables:
  AXTARGET: 192.168.0.4 # Replace with the IP of your target system.
  AXTARGETPLATFORMINPUT: './bin/plcsim/'  #Replace with respective target platform.
.
.
.
.
~~~

## Download the project to the PLC

Navigate to your ax folder and run the script command:

~~~
PS [your_root_folder]\ax>apax download
~~~

## To quickly run the hmi

~~~
dotnet run --project .\[your_project_name].app\[your_project_name].hmi.csproj
~~~

~~~
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5262
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
.
.      
~~~

Navigate to the address indicated in "Now listening on:".

> NOTE!
> Your browser may redirect to https. In that case, temporarily disable the redirection. 
> (Opening the page in incognito mode should not redirect.)

## Modifying your HMI project

In Visual Studio (VS2022), open the solution file from the project folder `[your_project_name].sln`. You can then run the solution directly from Visual Studio.

> **NOTE: Security is set to a minimal level for a speedy start. Make sure you set the security appropriately**.


## Resources

Documentation sources: https://github.com/inxton/axsharp/tree/dev/docs
