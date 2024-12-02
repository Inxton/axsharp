using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;
using FileWithUsingsSimpleFirstLevelNamespace;
using FileWithUsingsSimpleQualifiedNamespace.Qualified;
using FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo;

namespace FileWithUsingsSimpleFirstLevelNamespace
{
    public partial class Hello : AXSharp.Connector.ITwinObject
    {
        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Hello(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello> OnlineToPlainAsync()
        {
            Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain = new Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello> _OnlineToPlainNoacAsync()
        {
            Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain = new Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello> _OnlineToPlainNoacAsync(Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain)
        {
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain)
        {
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello> ShadowToPlainAsync()
        {
            Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain = new Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello();
            return plain;
        }

        protected async Task<Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello> ShadowToPlainAsync(Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain)
        {
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
        public async Task<bool> DetectsAnyChangeAsync(Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain, Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello CreateEmptyPoco()
        {
            return new Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello();
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

namespace FileWithUsingsSimpleQualifiedNamespace.Qualified
{
    public partial class Hello : AXSharp.Connector.ITwinObject
    {
        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Hello(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello> OnlineToPlainAsync()
        {
            Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain = new Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello> _OnlineToPlainNoacAsync()
        {
            Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain = new Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello> _OnlineToPlainNoacAsync(Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain)
        {
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain)
        {
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello> ShadowToPlainAsync()
        {
            Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain = new Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello();
            return plain;
        }

        protected async Task<Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello> ShadowToPlainAsync(Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain)
        {
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
        public async Task<bool> DetectsAnyChangeAsync(Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain, Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello CreateEmptyPoco()
        {
            return new Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello();
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

namespace FileWithUsingsHelloLevelOne
{
    namespace FileWithUsingsHelloLevelTwo
    {
        public partial class Hello : AXSharp.Connector.ITwinObject
        {
            partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
            partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
            public Hello(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
            {
                Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
                this.@SymbolTail = symbolTail;
                this.@Connector = parent.GetConnector();
                this.@Parent = parent;
                HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
                PreConstruct(parent, readableTail, symbolTail);
                parent.AddChild(this);
                parent.AddKid(this);
                PostConstruct(parent, readableTail, symbolTail);
            }

            public async virtual Task<T> OnlineToPlain<T>()
            {
                return await (dynamic)this.OnlineToPlainAsync();
            }

            public async Task<Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello> OnlineToPlainAsync()
            {
                Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain = new Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello();
                await this.ReadAsync<IgnoreOnPocoOperation>();
                return plain;
            }

            [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
            [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
            public async Task<Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello> _OnlineToPlainNoacAsync()
            {
                Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain = new Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello();
                return plain;
            }

            [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
            [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
            protected async Task<Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello> _OnlineToPlainNoacAsync(Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain)
            {
                return plain;
            }

            public async virtual Task PlainToOnline<T>(T plain)
            {
                await this.PlainToOnlineAsync((dynamic)plain);
            }

            public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain)
            {
                return await this.WriteAsync<IgnoreOnPocoOperation>();
            }

            [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
            [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
            public async Task _PlainToOnlineNoacAsync(Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain)
            {
            }

            public async virtual Task<T> ShadowToPlain<T>()
            {
                return await (dynamic)this.ShadowToPlainAsync();
            }

            public async Task<Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello> ShadowToPlainAsync()
            {
                Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain = new Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello();
                return plain;
            }

            protected async Task<Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello> ShadowToPlainAsync(Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain)
            {
                return plain;
            }

            public async virtual Task PlainToShadow<T>(T plain)
            {
                await this.PlainToShadowAsync((dynamic)plain);
            }

            public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain)
            {
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
            public async Task<bool> DetectsAnyChangeAsync(Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain, Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello latest = null)
            {
                if (latest == null)
                    latest = await this._OnlineToPlainNoacAsync();
                var somethingChanged = false;
                return await Task.Run(async () =>
                {
                    plain = latest;
                    return somethingChanged;
                });
            }

            public void Poll()
            {
                this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
            }

            public Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello CreateEmptyPoco()
            {
                return new Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello();
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
}

namespace ExampleNamespace
{
    public partial class Hello : AXSharp.Connector.ITwinObject
    {
        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Hello(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.ExampleNamespace.Hello> OnlineToPlainAsync()
        {
            Pocos.ExampleNamespace.Hello plain = new Pocos.ExampleNamespace.Hello();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.ExampleNamespace.Hello> _OnlineToPlainNoacAsync()
        {
            Pocos.ExampleNamespace.Hello plain = new Pocos.ExampleNamespace.Hello();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.ExampleNamespace.Hello> _OnlineToPlainNoacAsync(Pocos.ExampleNamespace.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ExampleNamespace.Hello plain)
        {
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.ExampleNamespace.Hello plain)
        {
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.ExampleNamespace.Hello> ShadowToPlainAsync()
        {
            Pocos.ExampleNamespace.Hello plain = new Pocos.ExampleNamespace.Hello();
            return plain;
        }

        protected async Task<Pocos.ExampleNamespace.Hello> ShadowToPlainAsync(Pocos.ExampleNamespace.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ExampleNamespace.Hello plain)
        {
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
        public async Task<bool> DetectsAnyChangeAsync(Pocos.ExampleNamespace.Hello plain, Pocos.ExampleNamespace.Hello latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.ExampleNamespace.Hello CreateEmptyPoco()
        {
            return new Pocos.ExampleNamespace.Hello();
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