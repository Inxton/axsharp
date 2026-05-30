using System;
using AXSharp.Abstractions.Presentation;
using AXSharp.Connector;

namespace Pocos
{
    using SimpleFirstLevelNamespace;
    using SimpleQualifiedNamespace.Qualified;
    using HelloLevelOne.HelloLevelTwo;

    [AXSharp.Connector.SourceFileAttribute(@"class_with_using_directives.st")]
    internal partial class ClassWithUsingDirectives : AXSharp.Connector.IPlain
    {
        public ClassWithUsingDirectives()
        {
        }

     using  SimpleFirstLevelNamespace ;  using  SimpleQualifiedNamespace . Qualified ;  using  HelloLevelOne . HelloLevelTwo ; 

}

namespace SimpleFirstLevelNamespace
{
}

namespace SimpleQualifiedNamespace.Qualified
{
}

namespace HelloLevelOne
{
    namespace HelloLevelTwo
    {
    }
} }
