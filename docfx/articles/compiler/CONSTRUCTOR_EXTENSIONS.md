# Constructor extension methods

Compiler generates partial methods `PreConstruct` and `PostConstruct`, which are used within constructors of compiler generated classes.

> [!CAUTION]
> These methods are called with every construction of object of given type.

These partial methods can be overrode and are used to implemented custom logic, which alter logic of object whether before or after construction.


## PreConstruct partial method
The PreConstruct partial method have following declaration:

```C#
partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
```

## PostConstruct partial method

The PostConstruct partial method have following declaration:

```C#
partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
```


## Usage and example

> [!CAUTION]
> These methods can be overridden only within the same assembly.

Within project, where compiled objects are generated create your partial class, which constructor you want to override and implement `PreConstruct` and `PostConstruct` partial methods.

```C#
namespace MonsterData
{
    public partial class MonsterBase 
    {
        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            // implement your pre construct logic
        }
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            // implement your post construct logic
        }
    }

}
```





