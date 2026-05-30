using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

namespace CompilerOmmits
{
    [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
    public partial class ClassWithArrays : AXSharp.Connector.ITwinObject
    {
        [CompilerOmitsAttribute("POCO")]
        public CompilerOmmits.Complex _must_be_omitted_in_poco { get; }
        public OnlinerByte[] _primitive { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ClassWithArrays(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            _must_be_omitted_in_poco = new CompilerOmmits.Complex(this, "_must_be_omitted_in_poco", "_must_be_omitted_in_poco");
            _primitive = new OnlinerByte[11];
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(_primitive, this, "_primitive", "_primitive", (p, rt, st) => @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(p, rt, st), new[] { (0, 10) });
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.CompilerOmmits.ClassWithArrays> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.CompilerOmmits.ClassWithArrays plain = new global::Pocos.CompilerOmmits.ClassWithArrays();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            plain._primitive = _primitive.Select(p => p.LastValue).ToArray();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.CompilerOmmits.ClassWithArrays> _OnlineToPlainNoacAsync()
        {
            global::Pocos.CompilerOmmits.ClassWithArrays plain = new global::Pocos.CompilerOmmits.ClassWithArrays();
            plain._primitive = _primitive.Select(p => p.LastValue).ToArray();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.CompilerOmmits.ClassWithArrays> _OnlineToPlainNoacAsync(global::Pocos.CompilerOmmits.ClassWithArrays plain)
        {
            plain._primitive = _primitive.Select(p => p.LastValue).ToArray();
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.CompilerOmmits.ClassWithArrays plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            var __primitive_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            _primitive.Select(p => p.LethargicWrite(plain._primitive[__primitive_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.CompilerOmmits.ClassWithArrays plain)
        {
            var __primitive_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            _primitive.Select(p => p.LethargicWrite(plain._primitive[__primitive_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.CompilerOmmits.ClassWithArrays> ShadowToPlainAsync()
        {
            global::Pocos.CompilerOmmits.ClassWithArrays plain = new global::Pocos.CompilerOmmits.ClassWithArrays();
            plain._primitive = _primitive.Select(p => p.Shadow).ToArray();
            return plain;
        }

        protected async Task<global::Pocos.CompilerOmmits.ClassWithArrays> ShadowToPlainAsync(global::Pocos.CompilerOmmits.ClassWithArrays plain)
        {
            plain._primitive = _primitive.Select(p => p.Shadow).ToArray();
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.CompilerOmmits.ClassWithArrays plain)
        {
            var __primitive_i_FE8484DAB3 = 0;
            _primitive.Select(p => p.Shadow = plain._primitive[__primitive_i_FE8484DAB3++]).ToArray();
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.CompilerOmmits.ClassWithArrays plain, global::Pocos.CompilerOmmits.ClassWithArrays latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                for (int i760901_3001_mimi = 0; i760901_3001_mimi < latest._primitive.Length; i760901_3001_mimi++)
                {
                    if (latest._primitive.ElementAt(i760901_3001_mimi) != plain._primitive[i760901_3001_mimi])
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

        public global::Pocos.CompilerOmmits.ClassWithArrays CreateEmptyPoco()
        {
            return new global::Pocos.CompilerOmmits.ClassWithArrays();
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

    [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
    public partial class Complex : AXSharp.Connector.ITwinObject
    {
        public OnlinerString HelloString { get; }
        public OnlinerULInt Id { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Complex(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            HelloString = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "HelloString", "HelloString");
            Id = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Id", "Id");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.CompilerOmmits.Complex> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.CompilerOmmits.Complex plain = new global::Pocos.CompilerOmmits.Complex();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            plain.HelloString = HelloString.LastValue;
            plain.Id = Id.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.CompilerOmmits.Complex> _OnlineToPlainNoacAsync()
        {
            global::Pocos.CompilerOmmits.Complex plain = new global::Pocos.CompilerOmmits.Complex();
            plain.HelloString = HelloString.LastValue;
            plain.Id = Id.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.CompilerOmmits.Complex> _OnlineToPlainNoacAsync(global::Pocos.CompilerOmmits.Complex plain)
        {
            plain.HelloString = HelloString.LastValue;
            plain.Id = Id.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.CompilerOmmits.Complex plain, eAccessPriority priority = eAccessPriority.Normal)
        {
#pragma warning disable CS0612
            HelloString.LethargicWrite(plain.HelloString);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Id.LethargicWrite(plain.Id);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.CompilerOmmits.Complex plain)
        {
#pragma warning disable CS0612
            HelloString.LethargicWrite(plain.HelloString);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Id.LethargicWrite(plain.Id);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.CompilerOmmits.Complex> ShadowToPlainAsync()
        {
            global::Pocos.CompilerOmmits.Complex plain = new global::Pocos.CompilerOmmits.Complex();
            plain.HelloString = HelloString.Shadow;
            plain.Id = Id.Shadow;
            return plain;
        }

        protected async Task<global::Pocos.CompilerOmmits.Complex> ShadowToPlainAsync(global::Pocos.CompilerOmmits.Complex plain)
        {
            plain.HelloString = HelloString.Shadow;
            plain.Id = Id.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.CompilerOmmits.Complex plain)
        {
            HelloString.Shadow = plain.HelloString;
            Id.Shadow = plain.Id;
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.CompilerOmmits.Complex plain, global::Pocos.CompilerOmmits.Complex latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.HelloString != HelloString.LastValue)
                    somethingChanged = true;
                if (plain.Id != Id.LastValue)
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.CompilerOmmits.Complex CreateEmptyPoco()
        {
            return new global::Pocos.CompilerOmmits.Complex();
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

namespace CompilerOmmitsEnums
{
    [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
    public partial class ClassWithEnums : AXSharp.Connector.ITwinObject
    {
        [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(CompilerOmmitsEnums.Colors))]
        public OnlinerInt colors { get; }
        public CompilerOmmitsEnums.Colors colorsEnum { get => (CompilerOmmitsEnums.Colors)colors.LastValue; }

        [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(CompilerOmmitsEnums.NamedValuesColors))]
        public OnlinerString NamedValuesColors { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ClassWithEnums(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            colors = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "colors", "colors");
            NamedValuesColors = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "NamedValuesColors", "NamedValuesColors");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.CompilerOmmitsEnums.ClassWithEnums> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.CompilerOmmitsEnums.ClassWithEnums plain = new global::Pocos.CompilerOmmitsEnums.ClassWithEnums();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            plain.colors = (CompilerOmmitsEnums.Colors)colors.LastValue;
            plain.NamedValuesColors = NamedValuesColors.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.CompilerOmmitsEnums.ClassWithEnums> _OnlineToPlainNoacAsync()
        {
            global::Pocos.CompilerOmmitsEnums.ClassWithEnums plain = new global::Pocos.CompilerOmmitsEnums.ClassWithEnums();
            plain.colors = (CompilerOmmitsEnums.Colors)colors.LastValue;
            plain.NamedValuesColors = NamedValuesColors.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.CompilerOmmitsEnums.ClassWithEnums> _OnlineToPlainNoacAsync(global::Pocos.CompilerOmmitsEnums.ClassWithEnums plain)
        {
            plain.colors = (CompilerOmmitsEnums.Colors)colors.LastValue;
            plain.NamedValuesColors = NamedValuesColors.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.CompilerOmmitsEnums.ClassWithEnums plain, eAccessPriority priority = eAccessPriority.Normal)
        {
#pragma warning disable CS0612
            colors.LethargicWrite((short)plain.colors);
#pragma warning restore CS0612
#pragma warning disable CS0612
            NamedValuesColors.LethargicWrite(plain.NamedValuesColors);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.CompilerOmmitsEnums.ClassWithEnums plain)
        {
#pragma warning disable CS0612
            colors.LethargicWrite((short)plain.colors);
#pragma warning restore CS0612
#pragma warning disable CS0612
            NamedValuesColors.LethargicWrite(plain.NamedValuesColors);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.CompilerOmmitsEnums.ClassWithEnums> ShadowToPlainAsync()
        {
            global::Pocos.CompilerOmmitsEnums.ClassWithEnums plain = new global::Pocos.CompilerOmmitsEnums.ClassWithEnums();
            plain.colors = (CompilerOmmitsEnums.Colors)colors.Shadow;
            plain.NamedValuesColors = NamedValuesColors.Shadow;
            return plain;
        }

        protected async Task<global::Pocos.CompilerOmmitsEnums.ClassWithEnums> ShadowToPlainAsync(global::Pocos.CompilerOmmitsEnums.ClassWithEnums plain)
        {
            plain.colors = (CompilerOmmitsEnums.Colors)colors.Shadow;
            plain.NamedValuesColors = NamedValuesColors.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.CompilerOmmitsEnums.ClassWithEnums plain)
        {
            colors.Shadow = (short)plain.colors;
            NamedValuesColors.Shadow = plain.NamedValuesColors;
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.CompilerOmmitsEnums.ClassWithEnums plain, global::Pocos.CompilerOmmitsEnums.ClassWithEnums latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.colors != (CompilerOmmitsEnums.Colors)latest.colors)
                    somethingChanged = true;
                if (plain.NamedValuesColors != NamedValuesColors.LastValue)
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.CompilerOmmitsEnums.ClassWithEnums CreateEmptyPoco()
        {
            return new global::Pocos.CompilerOmmitsEnums.ClassWithEnums();
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

    [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
    public enum Colors
    {
        Red,
        Green,
        Blue
    }

    [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
    public enum NamedValuesColors : String
    {
        Red = 49,
        Green = 50,
        Blue = 51
    }
}

namespace CompilerOmmitsMisc
{
    [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
    public partial class VariousMembers : AXSharp.Connector.ITwinObject
    {
        public CompilerOmmitsMisc.SomeClass _SomeClass { get; }
        public CompilerOmmitsMisc.ComersMotor _Motor { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public VariousMembers(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            _SomeClass = new CompilerOmmitsMisc.SomeClass(this, "_SomeClass", "_SomeClass");
            _Motor = new CompilerOmmitsMisc.ComersMotor(this, "_Motor", "_Motor");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.CompilerOmmitsMisc.VariousMembers> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.CompilerOmmitsMisc.VariousMembers plain = new global::Pocos.CompilerOmmitsMisc.VariousMembers();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
#pragma warning disable CS0612
            plain._SomeClass = await _SomeClass._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain._Motor = await _Motor._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.CompilerOmmitsMisc.VariousMembers> _OnlineToPlainNoacAsync()
        {
            global::Pocos.CompilerOmmitsMisc.VariousMembers plain = new global::Pocos.CompilerOmmitsMisc.VariousMembers();
#pragma warning disable CS0612
            plain._SomeClass = await _SomeClass._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain._Motor = await _Motor._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.CompilerOmmitsMisc.VariousMembers> _OnlineToPlainNoacAsync(global::Pocos.CompilerOmmitsMisc.VariousMembers plain)
        {
#pragma warning disable CS0612
            plain._SomeClass = await _SomeClass._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain._Motor = await _Motor._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.CompilerOmmitsMisc.VariousMembers plain, eAccessPriority priority = eAccessPriority.Normal)
        {
#pragma warning disable CS0612
            await this._SomeClass._PlainToOnlineNoacAsync(plain._SomeClass);
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this._Motor._PlainToOnlineNoacAsync(plain._Motor);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.CompilerOmmitsMisc.VariousMembers plain)
        {
#pragma warning disable CS0612
            await this._SomeClass._PlainToOnlineNoacAsync(plain._SomeClass);
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this._Motor._PlainToOnlineNoacAsync(plain._Motor);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.CompilerOmmitsMisc.VariousMembers> ShadowToPlainAsync()
        {
            global::Pocos.CompilerOmmitsMisc.VariousMembers plain = new global::Pocos.CompilerOmmitsMisc.VariousMembers();
            plain._SomeClass = await _SomeClass.ShadowToPlainAsync();
            plain._Motor = await _Motor.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<global::Pocos.CompilerOmmitsMisc.VariousMembers> ShadowToPlainAsync(global::Pocos.CompilerOmmitsMisc.VariousMembers plain)
        {
            plain._SomeClass = await _SomeClass.ShadowToPlainAsync();
            plain._Motor = await _Motor.ShadowToPlainAsync();
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.CompilerOmmitsMisc.VariousMembers plain)
        {
            await this._SomeClass.PlainToShadowAsync(plain._SomeClass);
            await this._Motor.PlainToShadowAsync(plain._Motor);
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.CompilerOmmitsMisc.VariousMembers plain, global::Pocos.CompilerOmmitsMisc.VariousMembers latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (await _SomeClass.DetectsAnyChangeAsync(plain._SomeClass, latest._SomeClass))
                    somethingChanged = true;
                if (await _Motor.DetectsAnyChangeAsync(plain._Motor, latest._Motor))
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.CompilerOmmitsMisc.VariousMembers CreateEmptyPoco()
        {
            return new global::Pocos.CompilerOmmitsMisc.VariousMembers();
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

    [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
    public partial class SomeClass : AXSharp.Connector.ITwinObject
    {
        public OnlinerString SomeClassVariable { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public SomeClass(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            SomeClassVariable = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "SomeClassVariable", "SomeClassVariable");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.CompilerOmmitsMisc.SomeClass> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.CompilerOmmitsMisc.SomeClass plain = new global::Pocos.CompilerOmmitsMisc.SomeClass();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.CompilerOmmitsMisc.SomeClass> _OnlineToPlainNoacAsync()
        {
            global::Pocos.CompilerOmmitsMisc.SomeClass plain = new global::Pocos.CompilerOmmitsMisc.SomeClass();
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.CompilerOmmitsMisc.SomeClass> _OnlineToPlainNoacAsync(global::Pocos.CompilerOmmitsMisc.SomeClass plain)
        {
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.CompilerOmmitsMisc.SomeClass plain, eAccessPriority priority = eAccessPriority.Normal)
        {
#pragma warning disable CS0612
            SomeClassVariable.LethargicWrite(plain.SomeClassVariable);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.CompilerOmmitsMisc.SomeClass plain)
        {
#pragma warning disable CS0612
            SomeClassVariable.LethargicWrite(plain.SomeClassVariable);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.CompilerOmmitsMisc.SomeClass> ShadowToPlainAsync()
        {
            global::Pocos.CompilerOmmitsMisc.SomeClass plain = new global::Pocos.CompilerOmmitsMisc.SomeClass();
            plain.SomeClassVariable = SomeClassVariable.Shadow;
            return plain;
        }

        protected async Task<global::Pocos.CompilerOmmitsMisc.SomeClass> ShadowToPlainAsync(global::Pocos.CompilerOmmitsMisc.SomeClass plain)
        {
            plain.SomeClassVariable = SomeClassVariable.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.CompilerOmmitsMisc.SomeClass plain)
        {
            SomeClassVariable.Shadow = plain.SomeClassVariable;
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.CompilerOmmitsMisc.SomeClass plain, global::Pocos.CompilerOmmitsMisc.SomeClass latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.SomeClassVariable != SomeClassVariable.LastValue)
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.CompilerOmmitsMisc.SomeClass CreateEmptyPoco()
        {
            return new global::Pocos.CompilerOmmitsMisc.SomeClass();
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

    [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
    public partial class ComersMotor : AXSharp.Connector.ITwinObject
    {
        public OnlinerBool isRunning { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ComersMotor(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            PreConstruct(parent, readableTail, symbolTail);
            isRunning = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "isRunning", "isRunning");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.CompilerOmmitsMisc.ComersMotor> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.CompilerOmmitsMisc.ComersMotor plain = new global::Pocos.CompilerOmmitsMisc.ComersMotor();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.CompilerOmmitsMisc.ComersMotor> _OnlineToPlainNoacAsync()
        {
            global::Pocos.CompilerOmmitsMisc.ComersMotor plain = new global::Pocos.CompilerOmmitsMisc.ComersMotor();
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        protected async Task<global::Pocos.CompilerOmmitsMisc.ComersMotor> OnlineToPlainAsync(global::Pocos.CompilerOmmitsMisc.ComersMotor plain)
        {
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.CompilerOmmitsMisc.ComersMotor plain, eAccessPriority priority = eAccessPriority.Normal)
        {
#pragma warning disable CS0612
            isRunning.LethargicWrite(plain.isRunning);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.CompilerOmmitsMisc.ComersMotor plain)
        {
#pragma warning disable CS0612
            isRunning.LethargicWrite(plain.isRunning);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.CompilerOmmitsMisc.ComersMotor> ShadowToPlainAsync()
        {
            global::Pocos.CompilerOmmitsMisc.ComersMotor plain = new global::Pocos.CompilerOmmitsMisc.ComersMotor();
            plain.isRunning = isRunning.Shadow;
            return plain;
        }

        protected async Task<global::Pocos.CompilerOmmitsMisc.ComersMotor> ShadowToPlainAsync(global::Pocos.CompilerOmmitsMisc.ComersMotor plain)
        {
            plain.isRunning = isRunning.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.CompilerOmmitsMisc.ComersMotor plain)
        {
            isRunning.Shadow = plain.isRunning;
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.CompilerOmmitsMisc.ComersMotor plain, global::Pocos.CompilerOmmitsMisc.ComersMotor latest = null)
        {
            var somethingChanged = false;
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            return await Task.Run(async () =>
            {
                if (plain.isRunning != isRunning.LastValue)
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.CompilerOmmitsMisc.ComersMotor CreateEmptyPoco()
        {
            return new global::Pocos.CompilerOmmitsMisc.ComersMotor();
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

    [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
    public partial class ComersVehicle : AXSharp.Connector.ITwinObject
    {
        public CompilerOmmitsMisc.ComersMotor m { get; }
        public OnlinerInt displacement { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ComersVehicle(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            PreConstruct(parent, readableTail, symbolTail);
            m = new CompilerOmmitsMisc.ComersMotor(this, "m", "m");
            displacement = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "displacement", "displacement");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.CompilerOmmitsMisc.ComersVehicle> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.CompilerOmmitsMisc.ComersVehicle plain = new global::Pocos.CompilerOmmitsMisc.ComersVehicle();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
#pragma warning disable CS0612
            plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            plain.displacement = displacement.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.CompilerOmmitsMisc.ComersVehicle> _OnlineToPlainNoacAsync()
        {
            global::Pocos.CompilerOmmitsMisc.ComersVehicle plain = new global::Pocos.CompilerOmmitsMisc.ComersVehicle();
#pragma warning disable CS0612
            plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            plain.displacement = displacement.LastValue;
            return plain;
        }

        protected async Task<global::Pocos.CompilerOmmitsMisc.ComersVehicle> OnlineToPlainAsync(global::Pocos.CompilerOmmitsMisc.ComersVehicle plain)
        {
#pragma warning disable CS0612
            plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            plain.displacement = displacement.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.CompilerOmmitsMisc.ComersVehicle plain, eAccessPriority priority = eAccessPriority.Normal)
        {
#pragma warning disable CS0612
            await this.m._PlainToOnlineNoacAsync(plain.m);
#pragma warning restore CS0612
#pragma warning disable CS0612
            displacement.LethargicWrite(plain.displacement);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.CompilerOmmitsMisc.ComersVehicle plain)
        {
#pragma warning disable CS0612
            await this.m._PlainToOnlineNoacAsync(plain.m);
#pragma warning restore CS0612
#pragma warning disable CS0612
            displacement.LethargicWrite(plain.displacement);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.CompilerOmmitsMisc.ComersVehicle> ShadowToPlainAsync()
        {
            global::Pocos.CompilerOmmitsMisc.ComersVehicle plain = new global::Pocos.CompilerOmmitsMisc.ComersVehicle();
            plain.m = await m.ShadowToPlainAsync();
            plain.displacement = displacement.Shadow;
            return plain;
        }

        protected async Task<global::Pocos.CompilerOmmitsMisc.ComersVehicle> ShadowToPlainAsync(global::Pocos.CompilerOmmitsMisc.ComersVehicle plain)
        {
            plain.m = await m.ShadowToPlainAsync();
            plain.displacement = displacement.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.CompilerOmmitsMisc.ComersVehicle plain)
        {
            await this.m.PlainToShadowAsync(plain.m);
            displacement.Shadow = plain.displacement;
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.CompilerOmmitsMisc.ComersVehicle plain, global::Pocos.CompilerOmmitsMisc.ComersVehicle latest = null)
        {
            var somethingChanged = false;
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            return await Task.Run(async () =>
            {
                if (await m.DetectsAnyChangeAsync(plain.m, latest.m))
                    somethingChanged = true;
                if (plain.displacement != displacement.LastValue)
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.CompilerOmmitsMisc.ComersVehicle CreateEmptyPoco()
        {
            return new global::Pocos.CompilerOmmitsMisc.ComersVehicle();
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

namespace CompilerOmmitsUnknownArrays
{
    [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
    public partial class ClassWithArrays : AXSharp.Connector.ITwinObject
    {
        public CompilerOmmitsUnknownArrays.Complex[] _complexKnown { get; }
        public OnlinerByte[] _primitive { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ClassWithArrays(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            _complexKnown = new CompilerOmmitsUnknownArrays.Complex[11];
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(_complexKnown, this, "_complexKnown", "_complexKnown", (p, rt, st) => new CompilerOmmitsUnknownArrays.Complex(p, rt, st), new[] { (0, 10) });
            _primitive = new OnlinerByte[11];
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(_primitive, this, "_primitive", "_primitive", (p, rt, st) => @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(p, rt, st), new[] { (0, 10) });
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays plain = new global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
#pragma warning disable CS0612
            plain._complexKnown = _complexKnown.Select(async p => await p._OnlineToPlainNoacAsync()).Select(p => p.Result).ToArray();
#pragma warning restore CS0612
            plain._primitive = _primitive.Select(p => p.LastValue).ToArray();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays> _OnlineToPlainNoacAsync()
        {
            global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays plain = new global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays();
#pragma warning disable CS0612
            plain._complexKnown = _complexKnown.Select(async p => await p._OnlineToPlainNoacAsync()).Select(p => p.Result).ToArray();
#pragma warning restore CS0612
            plain._primitive = _primitive.Select(p => p.LastValue).ToArray();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays> _OnlineToPlainNoacAsync(global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays plain)
        {
#pragma warning disable CS0612
            plain._complexKnown = _complexKnown.Select(async p => await p._OnlineToPlainNoacAsync()).Select(p => p.Result).ToArray();
#pragma warning restore CS0612
            plain._primitive = _primitive.Select(p => p.LastValue).ToArray();
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            var __complexKnown_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            _complexKnown.Select(p => p._PlainToOnlineNoacAsync(plain._complexKnown[__complexKnown_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
            var __primitive_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            _primitive.Select(p => p.LethargicWrite(plain._primitive[__primitive_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays plain)
        {
            var __complexKnown_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            _complexKnown.Select(p => p._PlainToOnlineNoacAsync(plain._complexKnown[__complexKnown_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
            var __primitive_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            _primitive.Select(p => p.LethargicWrite(plain._primitive[__primitive_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays> ShadowToPlainAsync()
        {
            global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays plain = new global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays();
            plain._complexKnown = _complexKnown.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            plain._primitive = _primitive.Select(p => p.Shadow).ToArray();
            return plain;
        }

        protected async Task<global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays> ShadowToPlainAsync(global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays plain)
        {
            plain._complexKnown = _complexKnown.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            plain._primitive = _primitive.Select(p => p.Shadow).ToArray();
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays plain)
        {
            var __complexKnown_i_FE8484DAB3 = 0;
            _complexKnown.Select(p => p.PlainToShadowAsync(plain._complexKnown[__complexKnown_i_FE8484DAB3++])).ToArray();
            var __primitive_i_FE8484DAB3 = 0;
            _primitive.Select(p => p.Shadow = plain._primitive[__primitive_i_FE8484DAB3++]).ToArray();
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays plain, global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                for (int i760901_3001_mimi = 0; i760901_3001_mimi < latest._complexKnown.Length; i760901_3001_mimi++)
                {
                    if (await _complexKnown.ElementAt(i760901_3001_mimi).DetectsAnyChangeAsync(plain._complexKnown[i760901_3001_mimi], latest._complexKnown[i760901_3001_mimi]))
                        somethingChanged = true;
                }

                for (int i760901_3001_mimi = 0; i760901_3001_mimi < latest._primitive.Length; i760901_3001_mimi++)
                {
                    if (latest._primitive.ElementAt(i760901_3001_mimi) != plain._primitive[i760901_3001_mimi])
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

        public global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays CreateEmptyPoco()
        {
            return new global::Pocos.CompilerOmmitsUnknownArrays.ClassWithArrays();
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

    [AXSharp.Connector.SourceFileAttribute(@"compileromitsattribute.st")]
    public partial class Complex : AXSharp.Connector.ITwinObject
    {
        public OnlinerString HelloString { get; }
        public OnlinerULInt Id { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Complex(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            HelloString = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "HelloString", "HelloString");
            Id = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Id", "Id");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.CompilerOmmitsUnknownArrays.Complex> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.CompilerOmmitsUnknownArrays.Complex plain = new global::Pocos.CompilerOmmitsUnknownArrays.Complex();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            plain.HelloString = HelloString.LastValue;
            plain.Id = Id.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.CompilerOmmitsUnknownArrays.Complex> _OnlineToPlainNoacAsync()
        {
            global::Pocos.CompilerOmmitsUnknownArrays.Complex plain = new global::Pocos.CompilerOmmitsUnknownArrays.Complex();
            plain.HelloString = HelloString.LastValue;
            plain.Id = Id.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.CompilerOmmitsUnknownArrays.Complex> _OnlineToPlainNoacAsync(global::Pocos.CompilerOmmitsUnknownArrays.Complex plain)
        {
            plain.HelloString = HelloString.LastValue;
            plain.Id = Id.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.CompilerOmmitsUnknownArrays.Complex plain, eAccessPriority priority = eAccessPriority.Normal)
        {
#pragma warning disable CS0612
            HelloString.LethargicWrite(plain.HelloString);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Id.LethargicWrite(plain.Id);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.CompilerOmmitsUnknownArrays.Complex plain)
        {
#pragma warning disable CS0612
            HelloString.LethargicWrite(plain.HelloString);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Id.LethargicWrite(plain.Id);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.CompilerOmmitsUnknownArrays.Complex> ShadowToPlainAsync()
        {
            global::Pocos.CompilerOmmitsUnknownArrays.Complex plain = new global::Pocos.CompilerOmmitsUnknownArrays.Complex();
            plain.HelloString = HelloString.Shadow;
            plain.Id = Id.Shadow;
            return plain;
        }

        protected async Task<global::Pocos.CompilerOmmitsUnknownArrays.Complex> ShadowToPlainAsync(global::Pocos.CompilerOmmitsUnknownArrays.Complex plain)
        {
            plain.HelloString = HelloString.Shadow;
            plain.Id = Id.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.CompilerOmmitsUnknownArrays.Complex plain)
        {
            HelloString.Shadow = plain.HelloString;
            Id.Shadow = plain.Id;
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.CompilerOmmitsUnknownArrays.Complex plain, global::Pocos.CompilerOmmitsUnknownArrays.Complex latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.HelloString != HelloString.LastValue)
                    somethingChanged = true;
                if (plain.Id != Id.LastValue)
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.CompilerOmmitsUnknownArrays.Complex CreateEmptyPoco()
        {
            return new global::Pocos.CompilerOmmitsUnknownArrays.Complex();
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