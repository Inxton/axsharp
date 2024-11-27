using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

namespace ArrayDeclarationSimpleNamespace
{
    public partial class array_declaration_class : AXSharp.Connector.ITwinObject
    {
        public OnlinerInt[] primitive { get; }

        public ArrayDeclarationSimpleNamespace.some_complex_type[] complex { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public array_declaration_class(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            primitive = new OnlinerInt[100];
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(primitive, this, "primitive", "primitive", (p, rt, st) => @Connector.ConnectorAdapter.AdapterFactory.CreateINT(p, rt, st), new[] { (1, 100) });
            complex = new ArrayDeclarationSimpleNamespace.some_complex_type[100];
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(complex, this, "complex", "complex", (p, rt, st) => new ArrayDeclarationSimpleNamespace.some_complex_type(p, rt, st), new[] { (1, 100) });
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class> OnlineToPlainAsync()
        {
            Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain = new Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.primitive = primitive.Select(p => p.LastValue).ToArray();
#pragma warning disable CS0612
            plain.complex = complex.Select(async p => await p._OnlineToPlainNoacAsync()).Select(p => p.Result).ToArray();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class> _OnlineToPlainNoacAsync()
        {
            Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain = new Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class();
            plain.primitive = primitive.Select(p => p.LastValue).ToArray();
#pragma warning disable CS0612
            plain.complex = complex.Select(async p => await p._OnlineToPlainNoacAsync()).Select(p => p.Result).ToArray();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class> _OnlineToPlainNoacAsync(Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain)
        {
            plain.primitive = primitive.Select(p => p.LastValue).ToArray();
#pragma warning disable CS0612
            plain.complex = complex.Select(async p => await p._OnlineToPlainNoacAsync()).Select(p => p.Result).ToArray();
#pragma warning restore CS0612
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain)
        {
            var _primitive_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            primitive.Select(p => p.LethargicWrite(plain.primitive[_primitive_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
            var _complex_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            complex.Select(p => p._PlainToOnlineNoacAsync(plain.complex[_complex_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain)
        {
            var _primitive_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            primitive.Select(p => p.LethargicWrite(plain.primitive[_primitive_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
            var _complex_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            complex.Select(p => p._PlainToOnlineNoacAsync(plain.complex[_complex_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class> ShadowToPlainAsync()
        {
            Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain = new Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class();
            plain.primitive = primitive.Select(p => p.Shadow).ToArray();
            plain.complex = complex.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        protected async Task<Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class> ShadowToPlainAsync(Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain)
        {
            plain.primitive = primitive.Select(p => p.Shadow).ToArray();
            plain.complex = complex.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain)
        {
            var _primitive_i_FE8484DAB3 = 0;
            primitive.Select(p => p.Shadow = plain.primitive[_primitive_i_FE8484DAB3++]).ToArray();
            var _complex_i_FE8484DAB3 = 0;
            complex.Select(p => p.PlainToShadowAsync(plain.complex[_complex_i_FE8484DAB3++])).ToArray();
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
        public async Task<bool> DetectsAnyChangeAsync(Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class plain, Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                for (int i760901_3001_mimi = 0; i760901_3001_mimi < latest.primitive.Length; i760901_3001_mimi++)
                {
                    if (latest.primitive.ElementAt(i760901_3001_mimi) != plain.primitive[i760901_3001_mimi])
                        somethingChanged = true;
                }

                for (int i760901_3001_mimi = 0; i760901_3001_mimi < latest.complex.Length; i760901_3001_mimi++)
                {
                    if (await complex.ElementAt(i760901_3001_mimi).DetectsAnyChangeAsync(plain.complex[i760901_3001_mimi], latest.complex[i760901_3001_mimi]))
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

        public Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class CreateEmptyPoco()
        {
            return new Pocos.ArrayDeclarationSimpleNamespace.array_declaration_class();
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

    public partial class some_complex_type : AXSharp.Connector.ITwinObject
    {
        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public some_complex_type(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

        public async Task<Pocos.ArrayDeclarationSimpleNamespace.some_complex_type> OnlineToPlainAsync()
        {
            Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain = new Pocos.ArrayDeclarationSimpleNamespace.some_complex_type();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.ArrayDeclarationSimpleNamespace.some_complex_type> _OnlineToPlainNoacAsync()
        {
            Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain = new Pocos.ArrayDeclarationSimpleNamespace.some_complex_type();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.ArrayDeclarationSimpleNamespace.some_complex_type> _OnlineToPlainNoacAsync(Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain)
        {
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain)
        {
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.ArrayDeclarationSimpleNamespace.some_complex_type> ShadowToPlainAsync()
        {
            Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain = new Pocos.ArrayDeclarationSimpleNamespace.some_complex_type();
            return plain;
        }

        protected async Task<Pocos.ArrayDeclarationSimpleNamespace.some_complex_type> ShadowToPlainAsync(Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain)
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
        public async Task<bool> DetectsAnyChangeAsync(Pocos.ArrayDeclarationSimpleNamespace.some_complex_type plain, Pocos.ArrayDeclarationSimpleNamespace.some_complex_type latest = null)
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

        public Pocos.ArrayDeclarationSimpleNamespace.some_complex_type CreateEmptyPoco()
        {
            return new Pocos.ArrayDeclarationSimpleNamespace.some_complex_type();
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