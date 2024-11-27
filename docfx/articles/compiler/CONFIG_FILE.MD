# Ixc config file

**AXSharp.config.json** can contain some settings for the compiler. Upon the first run of the ixc the default `AXSharp.config.json` file is created if not previoulsy created manually.

Some compilation options can be overrided by CLI, such as `OutputProjectFolder` (-o) will override `OutputProjectFolder` setting in the config file.


**Default config file**

~~~json
{
    "OutputProjectFolder":"ix",   
}
~~~

**Config file example**

~~~json
{
    "OutputProjectFolder":"..\\ix-app"
}
~~~

|      Parameter      |                                                Meaning                                                 | Default |
| ------------------- | ------------------------------------------------------------------------------------------------------ | ------- |
| OutputProjectFolder | Sets the directory where the ixc will emmit the twin project.Use path relative to apax project folder. | 'ix'    |
|                     | The value is overridable from the CLI                                                                  |         |


