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

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain = new global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello> _OnlineToPlainNoacAsync()
        {
            global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain = new global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello> _OnlineToPlainNoacAsync(global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain)
        {
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello> ShadowToPlainAsync()
        {
            global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain = new global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello();
            return plain;
        }

        protected async Task<global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello> ShadowToPlainAsync(global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain)
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello plain, global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello latest = null)
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

        public global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello CreateEmptyPoco()
        {
            return new global::Pocos.FileWithUsingsSimpleFirstLevelNamespace.Hello();
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

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain = new global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello> _OnlineToPlainNoacAsync()
        {
            global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain = new global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello> _OnlineToPlainNoacAsync(global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain)
        {
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello> ShadowToPlainAsync()
        {
            global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain = new global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello();
            return plain;
        }

        protected async Task<global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello> ShadowToPlainAsync(global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain)
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello plain, global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello latest = null)
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

        public global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello CreateEmptyPoco()
        {
            return new global::Pocos.FileWithUsingsSimpleQualifiedNamespace.Qualified.Hello();
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

            public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
            {
                return await (dynamic)this.OnlineToPlainAsync(priority);
            }

            public async Task<global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
            {
                global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain = new global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello();
                await this.ReadAsync<IgnoreOnPocoOperation>(priority);
                return plain;
            }

            [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
            [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
            public async Task<global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello> _OnlineToPlainNoacAsync()
            {
                global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain = new global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello();
                return plain;
            }

            [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
            [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
            protected async Task<global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello> _OnlineToPlainNoacAsync(global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain)
            {
                return plain;
            }

            public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
            {
                await this.PlainToOnlineAsync((dynamic)plain, priority);
            }

            public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain, eAccessPriority priority = eAccessPriority.Normal)
            {
                return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
            }

            [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
            [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
            public async Task _PlainToOnlineNoacAsync(global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain)
            {
            }

            public async virtual Task<T> ShadowToPlain<T>()
            {
                return await (dynamic)this.ShadowToPlainAsync();
            }

            public async Task<global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello> ShadowToPlainAsync()
            {
                global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain = new global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello();
                return plain;
            }

            protected async Task<global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello> ShadowToPlainAsync(global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain)
            {
                return plain;
            }

            public async virtual Task PlainToShadow<T>(T plain)
            {
                await this.PlainToShadowAsync((dynamic)plain);
            }

            public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain)
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
            public async Task<bool> DetectsAnyChangeAsync(global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello plain, global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello latest = null)
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

            public global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello CreateEmptyPoco()
            {
                return new global::Pocos.FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Hello();
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

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.ExampleNamespace.Hello> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.ExampleNamespace.Hello plain = new global::Pocos.ExampleNamespace.Hello();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.ExampleNamespace.Hello> _OnlineToPlainNoacAsync()
        {
            global::Pocos.ExampleNamespace.Hello plain = new global::Pocos.ExampleNamespace.Hello();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.ExampleNamespace.Hello> _OnlineToPlainNoacAsync(global::Pocos.ExampleNamespace.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.ExampleNamespace.Hello plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.ExampleNamespace.Hello plain)
        {
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.ExampleNamespace.Hello> ShadowToPlainAsync()
        {
            global::Pocos.ExampleNamespace.Hello plain = new global::Pocos.ExampleNamespace.Hello();
            return plain;
        }

        protected async Task<global::Pocos.ExampleNamespace.Hello> ShadowToPlainAsync(global::Pocos.ExampleNamespace.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.ExampleNamespace.Hello plain)
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.ExampleNamespace.Hello plain, global::Pocos.ExampleNamespace.Hello latest = null)
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

        public global::Pocos.ExampleNamespace.Hello CreateEmptyPoco()
        {
            return new global::Pocos.ExampleNamespace.Hello();
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