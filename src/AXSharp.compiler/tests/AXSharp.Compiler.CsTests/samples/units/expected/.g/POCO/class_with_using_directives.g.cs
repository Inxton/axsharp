using System;

namespace Pocos
{
    using SimpleFirstLevelNamespace;
    using SimpleQualifiedNamespace.Qualified;
    using HelloLevelOne.HelloLevelTwo;

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
