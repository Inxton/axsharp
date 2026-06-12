using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

namespace StringCapacityNamespace
{
    [AXSharp.Connector.SourceFileAttribute(@"string_capacity.st")]
    public partial class ClassWithDeclaredStringCapacities : AXSharp.Connector.ITwinObject
    {
        public OnlinerString myString10 { get; }
        public OnlinerWString myWString10 { get; }
        public OnlinerString myString { get; }
        public OnlinerWString myWString { get; }
        public OnlinerWString myWString1000 { get; }
        public OnlinerString[] myString10Array { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ClassWithDeclaredStringCapacities(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            myString10 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "myString10", "myString10");
            myString10.Capacity = 10;
            myWString10 = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this, "myWString10", "myWString10");
            myWString10.Capacity = 10;
            myString = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "myString", "myString");
            myString.Capacity = 254;
            myWString = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this, "myWString", "myWString");
            myWString.Capacity = 254;
            myWString1000 = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this, "myWString1000", "myWString1000");
            myWString1000.Capacity = 1000;
            myString10Array = new OnlinerString[4];
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(myString10Array, this, "myString10Array", "myString10Array", (p, rt, st) => @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(p, rt, st), new[] { (0, 3) });
            foreach (var stringItem in myString10Array)
                stringItem.Capacity = 10;
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities plain = new global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            plain.myString10 = myString10.LastValue;
            plain.myWString10 = myWString10.LastValue;
            plain.myString = myString.LastValue;
            plain.myWString = myWString.LastValue;
            plain.myWString1000 = myWString1000.LastValue;
            plain.myString10Array = myString10Array.Select(p => p.LastValue).ToArray();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities> _OnlineToPlainNoacAsync()
        {
            global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities plain = new global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities();
            plain.myString10 = myString10.LastValue;
            plain.myWString10 = myWString10.LastValue;
            plain.myString = myString.LastValue;
            plain.myWString = myWString.LastValue;
            plain.myWString1000 = myWString1000.LastValue;
            plain.myString10Array = myString10Array.Select(p => p.LastValue).ToArray();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities> _OnlineToPlainNoacAsync(global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities plain)
        {
            plain.myString10 = myString10.LastValue;
            plain.myWString10 = myWString10.LastValue;
            plain.myString = myString.LastValue;
            plain.myWString = myWString.LastValue;
            plain.myWString1000 = myWString1000.LastValue;
            plain.myString10Array = myString10Array.Select(p => p.LastValue).ToArray();
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities plain, eAccessPriority priority = eAccessPriority.Normal)
        {
#pragma warning disable CS0612
            myString10.LethargicWrite(plain.myString10);
#pragma warning restore CS0612
#pragma warning disable CS0612
            myWString10.LethargicWrite(plain.myWString10);
#pragma warning restore CS0612
#pragma warning disable CS0612
            myString.LethargicWrite(plain.myString);
#pragma warning restore CS0612
#pragma warning disable CS0612
            myWString.LethargicWrite(plain.myWString);
#pragma warning restore CS0612
#pragma warning disable CS0612
            myWString1000.LethargicWrite(plain.myWString1000);
#pragma warning restore CS0612
            var _myString10Array_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            myString10Array.Select(p => p.LethargicWrite(plain.myString10Array[_myString10Array_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities plain)
        {
#pragma warning disable CS0612
            myString10.LethargicWrite(plain.myString10);
#pragma warning restore CS0612
#pragma warning disable CS0612
            myWString10.LethargicWrite(plain.myWString10);
#pragma warning restore CS0612
#pragma warning disable CS0612
            myString.LethargicWrite(plain.myString);
#pragma warning restore CS0612
#pragma warning disable CS0612
            myWString.LethargicWrite(plain.myWString);
#pragma warning restore CS0612
#pragma warning disable CS0612
            myWString1000.LethargicWrite(plain.myWString1000);
#pragma warning restore CS0612
            var _myString10Array_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            myString10Array.Select(p => p.LethargicWrite(plain.myString10Array[_myString10Array_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities> ShadowToPlainAsync()
        {
            global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities plain = new global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities();
            plain.myString10 = myString10.Shadow;
            plain.myWString10 = myWString10.Shadow;
            plain.myString = myString.Shadow;
            plain.myWString = myWString.Shadow;
            plain.myWString1000 = myWString1000.Shadow;
            plain.myString10Array = myString10Array.Select(p => p.Shadow).ToArray();
            return plain;
        }

        protected async Task<global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities> ShadowToPlainAsync(global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities plain)
        {
            plain.myString10 = myString10.Shadow;
            plain.myWString10 = myWString10.Shadow;
            plain.myString = myString.Shadow;
            plain.myWString = myWString.Shadow;
            plain.myWString1000 = myWString1000.Shadow;
            plain.myString10Array = myString10Array.Select(p => p.Shadow).ToArray();
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities plain)
        {
            myString10.Shadow = plain.myString10;
            myWString10.Shadow = plain.myWString10;
            myString.Shadow = plain.myString;
            myWString.Shadow = plain.myWString;
            myWString1000.Shadow = plain.myWString1000;
            var _myString10Array_i_FE8484DAB3 = 0;
            myString10Array.Select(p => p.Shadow = plain.myString10Array[_myString10Array_i_FE8484DAB3++]).ToArray();
            return this.RetrievePrimitives();
        }

        ///<inheritdoc/>
        public async virtual Task<bool> AnyChangeAsync<T>(T plain)
        {
            return await this.DetectsAnyChangeAsync((dynamic)plain);
        }

        ///<summary>
        ///Compares if the current plain object has changed from the previous object.This method is used by the framework to determine if the object has changed and needs to be updated.
        ///[!NOTE] Any member in the hierarchy that is ignored by the compilers (e.g. when CompilerOmitAttribute is used) will not be compared, and therefore will not be detected as changed.
        ///</summary>
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities plain, global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.myString10 != myString10.LastValue)
                    somethingChanged = true;
                if (plain.myWString10 != myWString10.LastValue)
                    somethingChanged = true;
                if (plain.myString != myString.LastValue)
                    somethingChanged = true;
                if (plain.myWString != myWString.LastValue)
                    somethingChanged = true;
                if (plain.myWString1000 != myWString1000.LastValue)
                    somethingChanged = true;
                for (int i760901_3001_mimi = 0; i760901_3001_mimi < latest.myString10Array.Length; i760901_3001_mimi++)
                {
                    if (latest.myString10Array.ElementAt(i760901_3001_mimi) != plain.myString10Array[i760901_3001_mimi])
                        somethingChanged = true;
                }

                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities CreateEmptyPoco()
        {
            return new global::Pocos.StringCapacityNamespace.ClassWithDeclaredStringCapacities();
        }

        private IList<AXSharp.Connector.ITwinObject> Children { get; } = new List<AXSharp.Connector.ITwinObject>();

        public IEnumerable<AXSharp.Connector.ITwinObject> GetChildren()
        {
            return Children;
        }

        private IList<AXSharp.Connector.ITwinElement> Kids { get; } = new List<AXSharp.Connector.ITwinElement>();

        public IEnumerable<AXSharp.Connector.ITwinElement> GetKids()
        {
            return Kids;
        }

        private IList<AXSharp.Connector.ITwinPrimitive> ValueTags { get; } = new List<AXSharp.Connector.ITwinPrimitive>();

        public IEnumerable<AXSharp.Connector.ITwinPrimitive> GetValueTags()
        {
            return ValueTags;
        }

        public void AddValueTag(AXSharp.Connector.ITwinPrimitive valueTag)
        {
            ValueTags.Add(valueTag);
        }

        public void AddKid(AXSharp.Connector.ITwinElement kid)
        {
            Kids.Add(kid);
        }

        public void AddChild(AXSharp.Connector.ITwinObject twinObject)
        {
            Children.Add(twinObject);
        }

        protected AXSharp.Connector.Connector @Connector { get; }

        public AXSharp.Connector.Connector GetConnector()
        {
            return this.@Connector;
        }

        public string GetSymbolTail()
        {
            return this.SymbolTail;
        }

        public AXSharp.Connector.ITwinObject GetParent()
        {
            return this.@Parent;
        }

        public string Symbol { get; protected set; }

        private string _attributeName;
        public System.String AttributeName { get => string.IsNullOrEmpty(_attributeName) ? SymbolTail : _attributeName.Interpolate(this).CleanUpLocalizationTokens(); set => _attributeName = value; }

        public System.String GetAttributeName(System.Globalization.CultureInfo culture)
        {
            if (string.IsNullOrEmpty(_attributeName))
            {
                return SymbolTail;
            }

            return this.Translate(_attributeName, culture).Interpolate(this);
        }

        private string _humanReadable;
        public string HumanReadable { get => string.IsNullOrEmpty(_humanReadable) ? SymbolTail : _humanReadable.Interpolate(this).CleanUpLocalizationTokens(); set => _humanReadable = value; }

        public System.String GetHumanReadable(System.Globalization.CultureInfo culture)
        {
            return this.Translate(_humanReadable, culture);
        }

        protected System.String @SymbolTail { get; set; }
        protected AXSharp.Connector.ITwinObject @Parent { get; set; }
        public AXSharp.Connector.Localizations.Translator Interpreter => global::units.PlcTranslator.Instance;
    }
}