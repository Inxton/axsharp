using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

namespace TypeWithNameAttributes
{
    [Container(Layout.Wrap)]
    public partial class Motor : AXSharp.Connector.ITwinObject
    {
        public OnlinerBool isRunning { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Motor(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<TypeWithNameAttributes.Pocos.Motor> OnlineToPlainAsync()
        {
            TypeWithNameAttributes.Pocos.Motor plain = new TypeWithNameAttributes.Pocos.Motor();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<TypeWithNameAttributes.Pocos.Motor> _OnlineToPlainNoacAsync()
        {
            TypeWithNameAttributes.Pocos.Motor plain = new TypeWithNameAttributes.Pocos.Motor();
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        protected async Task<TypeWithNameAttributes.Pocos.Motor> OnlineToPlainAsync(TypeWithNameAttributes.Pocos.Motor plain)
        {
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(TypeWithNameAttributes.Pocos.Motor plain)
        {
#pragma warning disable CS0612
            isRunning.LethargicWrite(plain.isRunning);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(TypeWithNameAttributes.Pocos.Motor plain)
        {
#pragma warning disable CS0612
            isRunning.LethargicWrite(plain.isRunning);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<TypeWithNameAttributes.Pocos.Motor> ShadowToPlainAsync()
        {
            TypeWithNameAttributes.Pocos.Motor plain = new TypeWithNameAttributes.Pocos.Motor();
            plain.isRunning = isRunning.Shadow;
            return plain;
        }

        protected async Task<TypeWithNameAttributes.Pocos.Motor> ShadowToPlainAsync(TypeWithNameAttributes.Pocos.Motor plain)
        {
            plain.isRunning = isRunning.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(TypeWithNameAttributes.Pocos.Motor plain)
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
        public async Task<bool> DetectsAnyChangeAsync(TypeWithNameAttributes.Pocos.Motor plain, TypeWithNameAttributes.Pocos.Motor latest = null)
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

        public TypeWithNameAttributes.Pocos.Motor CreateEmptyPoco()
        {
            return new TypeWithNameAttributes.Pocos.Motor();
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

    public partial class Vehicle : AXSharp.Connector.ITwinObject
    {
        public TypeWithNameAttributes.Motor m { get; }

        public OnlinerInt displacement { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Vehicle(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            PreConstruct(parent, readableTail, symbolTail);
            m = new TypeWithNameAttributes.Motor(this, "m", "m");
            displacement = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "displacement", "displacement");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<TypeWithNameAttributes.Pocos.Vehicle> OnlineToPlainAsync()
        {
            TypeWithNameAttributes.Pocos.Vehicle plain = new TypeWithNameAttributes.Pocos.Vehicle();
            await this.ReadAsync<IgnoreOnPocoOperation>();
#pragma warning disable CS0612
            plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            plain.displacement = displacement.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<TypeWithNameAttributes.Pocos.Vehicle> _OnlineToPlainNoacAsync()
        {
            TypeWithNameAttributes.Pocos.Vehicle plain = new TypeWithNameAttributes.Pocos.Vehicle();
#pragma warning disable CS0612
            plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            plain.displacement = displacement.LastValue;
            return plain;
        }

        protected async Task<TypeWithNameAttributes.Pocos.Vehicle> OnlineToPlainAsync(TypeWithNameAttributes.Pocos.Vehicle plain)
        {
#pragma warning disable CS0612
            plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            plain.displacement = displacement.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(TypeWithNameAttributes.Pocos.Vehicle plain)
        {
#pragma warning disable CS0612
            await this.m._PlainToOnlineNoacAsync(plain.m);
#pragma warning restore CS0612
#pragma warning disable CS0612
            displacement.LethargicWrite(plain.displacement);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(TypeWithNameAttributes.Pocos.Vehicle plain)
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

        public async Task<TypeWithNameAttributes.Pocos.Vehicle> ShadowToPlainAsync()
        {
            TypeWithNameAttributes.Pocos.Vehicle plain = new TypeWithNameAttributes.Pocos.Vehicle();
            plain.m = await m.ShadowToPlainAsync();
            plain.displacement = displacement.Shadow;
            return plain;
        }

        protected async Task<TypeWithNameAttributes.Pocos.Vehicle> ShadowToPlainAsync(TypeWithNameAttributes.Pocos.Vehicle plain)
        {
            plain.m = await m.ShadowToPlainAsync();
            plain.displacement = displacement.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(TypeWithNameAttributes.Pocos.Vehicle plain)
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
        public async Task<bool> DetectsAnyChangeAsync(TypeWithNameAttributes.Pocos.Vehicle plain, TypeWithNameAttributes.Pocos.Vehicle latest = null)
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

        public TypeWithNameAttributes.Pocos.Vehicle CreateEmptyPoco()
        {
            return new TypeWithNameAttributes.Pocos.Vehicle();
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

    public partial class NoAccessModifierClass : AXSharp.Connector.ITwinObject
    {
        private string _AttributeName;
        public string AttributeName { get => string.IsNullOrEmpty(_AttributeName) ? SymbolTail : _AttributeName.Interpolate(this).CleanUpLocalizationTokens(); set => _AttributeName = value; }

        public string GetAttributeName(System.Globalization.CultureInfo culture)
        {
            return this.Translate(_AttributeName, culture).Interpolate(this);
        }

        public OnlinerString SomeClassVariable { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public NoAccessModifierClass(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<TypeWithNameAttributes.Pocos.NoAccessModifierClass> OnlineToPlainAsync()
        {
            TypeWithNameAttributes.Pocos.NoAccessModifierClass plain = new TypeWithNameAttributes.Pocos.NoAccessModifierClass();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<TypeWithNameAttributes.Pocos.NoAccessModifierClass> _OnlineToPlainNoacAsync()
        {
            TypeWithNameAttributes.Pocos.NoAccessModifierClass plain = new TypeWithNameAttributes.Pocos.NoAccessModifierClass();
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<TypeWithNameAttributes.Pocos.NoAccessModifierClass> _OnlineToPlainNoacAsync(TypeWithNameAttributes.Pocos.NoAccessModifierClass plain)
        {
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(TypeWithNameAttributes.Pocos.NoAccessModifierClass plain)
        {
#pragma warning disable CS0612
            SomeClassVariable.LethargicWrite(plain.SomeClassVariable);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(TypeWithNameAttributes.Pocos.NoAccessModifierClass plain)
        {
#pragma warning disable CS0612
            SomeClassVariable.LethargicWrite(plain.SomeClassVariable);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<TypeWithNameAttributes.Pocos.NoAccessModifierClass> ShadowToPlainAsync()
        {
            TypeWithNameAttributes.Pocos.NoAccessModifierClass plain = new TypeWithNameAttributes.Pocos.NoAccessModifierClass();
            plain.SomeClassVariable = SomeClassVariable.Shadow;
            return plain;
        }

        protected async Task<TypeWithNameAttributes.Pocos.NoAccessModifierClass> ShadowToPlainAsync(TypeWithNameAttributes.Pocos.NoAccessModifierClass plain)
        {
            plain.SomeClassVariable = SomeClassVariable.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(TypeWithNameAttributes.Pocos.NoAccessModifierClass plain)
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
        public async Task<bool> DetectsAnyChangeAsync(TypeWithNameAttributes.Pocos.NoAccessModifierClass plain, TypeWithNameAttributes.Pocos.NoAccessModifierClass latest = null)
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

        public TypeWithNameAttributes.Pocos.NoAccessModifierClass CreateEmptyPoco()
        {
            return new TypeWithNameAttributes.Pocos.NoAccessModifierClass();
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