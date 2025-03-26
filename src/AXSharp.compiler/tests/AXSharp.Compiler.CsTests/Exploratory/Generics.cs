using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector;
using AXSharp.Connector.Localizations;

using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using System.Globalization;
using AXSharp.Connector.Localizations;



namespace AXSharp.Compiler.CsTests.Exploratory
{
    public class GenericsBase<TO, TP> where TP : IPlain where TO : ITwinObject
    {
        public async Task<TP> CopyFromOnlineToPlain(TO online)
        {
            return await online.OnlineToPlain<TP>();
        }
    }

    public class GenericInstance : GenericsBase<GenericMemberA, GenericMemberB>
    {
        void Some()
        {
            //this.CopyFromOnlineToPlain();
        }
    }


    

    public class GenericMemberA : ITwinObject
    {
        public string Symbol { get; }
        public string AttributeName { get; }
        public string HumanReadable { get; }
        public ITwinObject GetParent()
        {
            throw new NotImplementedException();
        }

        public string GetSymbolTail()
        {
            throw new NotImplementedException();
        }

        public void Poll()
        {
            throw new NotImplementedException();
        }

        public string GetAttributeName(CultureInfo culture)
        {
            return this.Translate(this.AttributeName, culture);
        }

        public string GetHumanReadable(CultureInfo culture)
        {
            return this.Translate(this.HumanReadable, culture);
        }

        public Translator Interpreter { get; }
        public IEnumerable<ITwinObject> GetChildren()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITwinElement> GetKids()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITwinPrimitive> GetValueTags()
        {
            throw new NotImplementedException();
        }

        public void AddChild(ITwinObject twinObject)
        {
            throw new NotImplementedException();
        }

        public void AddValueTag(ITwinPrimitive twinPrimitive)
        {
            throw new NotImplementedException();
        }

        public void AddKid(ITwinElement kid)
        {
            throw new NotImplementedException();
        }

        public Connector.Connector GetConnector()
        {
            throw new NotImplementedException();
        }

        public Task<T> OnlineToPlain<T>()
        {
            throw new NotImplementedException();
        }

        public Task PlainToOnline<T>(T plain)
        {
            throw new NotImplementedException();
        }

        public Task<T> ShadowToPlain<T>()
        {
            throw new NotImplementedException();
        }

        public Task PlainToShadow<T>(T plain)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyChangeAsync<T>(T plain)
        {
            throw new NotImplementedException();
        }
    }
    public class GenericMemberB : IPlain
    {

    }

}
