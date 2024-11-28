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

        public async Task<FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello> OnlineToPlainAsync()
        {
            FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello plain = new FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello> _OnlineToPlainNoacAsync()
        {
            FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello plain = new FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello> _OnlineToPlainNoacAsync(FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello plain)
        {
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello plain)
        {
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello> ShadowToPlainAsync()
        {
            FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello plain = new FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello();
            return plain;
        }

        protected async Task<FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello> ShadowToPlainAsync(FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello plain)
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
        public async Task<bool> DetectsAnyChangeAsync(FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello plain, FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello latest = null)
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

        public FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello CreateEmptyPoco()
        {
            return new FileWithUsingsSimpleFirstLevelNamespace.Pocos.Hello();
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

        public async Task<FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello> OnlineToPlainAsync()
        {
            FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello plain = new FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello> _OnlineToPlainNoacAsync()
        {
            FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello plain = new FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello> _OnlineToPlainNoacAsync(FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello plain)
        {
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello plain)
        {
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello> ShadowToPlainAsync()
        {
            FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello plain = new FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello();
            return plain;
        }

        protected async Task<FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello> ShadowToPlainAsync(FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello plain)
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
        public async Task<bool> DetectsAnyChangeAsync(FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello plain, FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello latest = null)
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

        public FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello CreateEmptyPoco()
        {
            return new FileWithUsingsSimpleQualifiedNamespace.Qualified.Pocos.Hello();
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

            public async Task<FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello> OnlineToPlainAsync()
            {
                FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello plain = new FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello();
                await this.ReadAsync<IgnoreOnPocoOperation>();
                return plain;
            }

            [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
            [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
            public async Task<FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello> _OnlineToPlainNoacAsync()
            {
                FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello plain = new FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello();
                return plain;
            }

            [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
            [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
            protected async Task<FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello> _OnlineToPlainNoacAsync(FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello plain)
            {
                return plain;
            }

            public async virtual Task PlainToOnline<T>(T plain)
            {
                await this.PlainToOnlineAsync((dynamic)plain);
            }

            public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello plain)
            {
                return await this.WriteAsync<IgnoreOnPocoOperation>();
            }

            [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
            [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
            public async Task _PlainToOnlineNoacAsync(FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello plain)
            {
            }

            public async virtual Task<T> ShadowToPlain<T>()
            {
                return await (dynamic)this.ShadowToPlainAsync();
            }

            public async Task<FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello> ShadowToPlainAsync()
            {
                FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello plain = new FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello();
                return plain;
            }

            protected async Task<FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello> ShadowToPlainAsync(FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello plain)
            {
                return plain;
            }

            public async virtual Task PlainToShadow<T>(T plain)
            {
                await this.PlainToShadowAsync((dynamic)plain);
            }

            public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello plain)
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
            public async Task<bool> DetectsAnyChangeAsync(FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello plain, FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello latest = null)
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

            public FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello CreateEmptyPoco()
            {
                return new FileWithUsingsHelloLevelOne.FileWithUsingsHelloLevelTwo.Pocos.Hello();
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

        public async Task<ExampleNamespace.Pocos.Hello> OnlineToPlainAsync()
        {
            ExampleNamespace.Pocos.Hello plain = new ExampleNamespace.Pocos.Hello();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<ExampleNamespace.Pocos.Hello> _OnlineToPlainNoacAsync()
        {
            ExampleNamespace.Pocos.Hello plain = new ExampleNamespace.Pocos.Hello();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<ExampleNamespace.Pocos.Hello> _OnlineToPlainNoacAsync(ExampleNamespace.Pocos.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(ExampleNamespace.Pocos.Hello plain)
        {
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(ExampleNamespace.Pocos.Hello plain)
        {
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<ExampleNamespace.Pocos.Hello> ShadowToPlainAsync()
        {
            ExampleNamespace.Pocos.Hello plain = new ExampleNamespace.Pocos.Hello();
            return plain;
        }

        protected async Task<ExampleNamespace.Pocos.Hello> ShadowToPlainAsync(ExampleNamespace.Pocos.Hello plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(ExampleNamespace.Pocos.Hello plain)
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
        public async Task<bool> DetectsAnyChangeAsync(ExampleNamespace.Pocos.Hello plain, ExampleNamespace.Pocos.Hello latest = null)
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

        public ExampleNamespace.Pocos.Hello CreateEmptyPoco()
        {
            return new ExampleNamespace.Pocos.Hello();
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