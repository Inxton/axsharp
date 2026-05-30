using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

namespace TypeWithNameAttributes
{
    [AXSharp.Connector.SourceFileAttribute(@"types_with_name_attributes.st")]
    [Container(Layout.Wrap)]
    public partial class TypesNameAttrMotor : AXSharp.Connector.ITwinObject
    {
        public OnlinerBool isRunning { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public TypesNameAttrMotor(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

        public async Task<global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor plain = new global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor> _OnlineToPlainNoacAsync()
        {
            global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor plain = new global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor();
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        protected async Task<global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor> OnlineToPlainAsync(global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor plain)
        {
            plain.isRunning = isRunning.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor plain, eAccessPriority priority = eAccessPriority.Normal)
        {
#pragma warning disable CS0612
            isRunning.LethargicWrite(plain.isRunning);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor plain)
        {
#pragma warning disable CS0612
            isRunning.LethargicWrite(plain.isRunning);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor> ShadowToPlainAsync()
        {
            global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor plain = new global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor();
            plain.isRunning = isRunning.Shadow;
            return plain;
        }

        protected async Task<global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor> ShadowToPlainAsync(global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor plain)
        {
            plain.isRunning = isRunning.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor plain)
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor plain, global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor latest = null)
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

        public global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor CreateEmptyPoco()
        {
            return new global::Pocos.TypeWithNameAttributes.TypesNameAttrMotor();
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

    [AXSharp.Connector.SourceFileAttribute(@"types_with_name_attributes.st")]
    public partial class TypesNameAttrVehicle : AXSharp.Connector.ITwinObject
    {
        public TypeWithNameAttributes.TypesNameAttrMotor m { get; }
        public OnlinerInt displacement { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public TypesNameAttrVehicle(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            PreConstruct(parent, readableTail, symbolTail);
            m = new TypeWithNameAttributes.TypesNameAttrMotor(this, "m", "m");
            displacement = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "displacement", "displacement");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle plain = new global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
#pragma warning disable CS0612
            plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            plain.displacement = displacement.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle> _OnlineToPlainNoacAsync()
        {
            global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle plain = new global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle();
#pragma warning disable CS0612
            plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            plain.displacement = displacement.LastValue;
            return plain;
        }

        protected async Task<global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle> OnlineToPlainAsync(global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle plain)
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

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle plain, eAccessPriority priority = eAccessPriority.Normal)
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
        public async Task _PlainToOnlineNoacAsync(global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle plain)
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

        public async Task<global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle> ShadowToPlainAsync()
        {
            global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle plain = new global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle();
            plain.m = await m.ShadowToPlainAsync();
            plain.displacement = displacement.Shadow;
            return plain;
        }

        protected async Task<global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle> ShadowToPlainAsync(global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle plain)
        {
            plain.m = await m.ShadowToPlainAsync();
            plain.displacement = displacement.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle plain)
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle plain, global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle latest = null)
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

        public global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle CreateEmptyPoco()
        {
            return new global::Pocos.TypeWithNameAttributes.TypesNameAttrVehicle();
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

    [AXSharp.Connector.SourceFileAttribute(@"types_with_name_attributes.st")]
    public partial class NoAccessModifierClass : AXSharp.Connector.ITwinObject
    {
        private string _AttributeName;
        public string AttributeName { get => string.IsNullOrEmpty(_AttributeName) ? SymbolTail : _AttributeName.Interpolate(this).CleanUpLocalizationTokens(); set => _AttributeName = value; }

        public string GetAttributeName(System.Globalization.CultureInfo culture)
        {
            return this.Translate(_AttributeName, culture).Interpolate(this);
        }

        public string AttributeName_raw
        {
            get
            {
                return _AttributeName;
            }
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

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.TypeWithNameAttributes.NoAccessModifierClass> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.TypeWithNameAttributes.NoAccessModifierClass plain = new global::Pocos.TypeWithNameAttributes.NoAccessModifierClass();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.TypeWithNameAttributes.NoAccessModifierClass> _OnlineToPlainNoacAsync()
        {
            global::Pocos.TypeWithNameAttributes.NoAccessModifierClass plain = new global::Pocos.TypeWithNameAttributes.NoAccessModifierClass();
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.TypeWithNameAttributes.NoAccessModifierClass> _OnlineToPlainNoacAsync(global::Pocos.TypeWithNameAttributes.NoAccessModifierClass plain)
        {
            plain.SomeClassVariable = SomeClassVariable.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.TypeWithNameAttributes.NoAccessModifierClass plain, eAccessPriority priority = eAccessPriority.Normal)
        {
#pragma warning disable CS0612
            SomeClassVariable.LethargicWrite(plain.SomeClassVariable);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.TypeWithNameAttributes.NoAccessModifierClass plain)
        {
#pragma warning disable CS0612
            SomeClassVariable.LethargicWrite(plain.SomeClassVariable);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.TypeWithNameAttributes.NoAccessModifierClass> ShadowToPlainAsync()
        {
            global::Pocos.TypeWithNameAttributes.NoAccessModifierClass plain = new global::Pocos.TypeWithNameAttributes.NoAccessModifierClass();
            plain.SomeClassVariable = SomeClassVariable.Shadow;
            return plain;
        }

        protected async Task<global::Pocos.TypeWithNameAttributes.NoAccessModifierClass> ShadowToPlainAsync(global::Pocos.TypeWithNameAttributes.NoAccessModifierClass plain)
        {
            plain.SomeClassVariable = SomeClassVariable.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.TypeWithNameAttributes.NoAccessModifierClass plain)
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.TypeWithNameAttributes.NoAccessModifierClass plain, global::Pocos.TypeWithNameAttributes.NoAccessModifierClass latest = null)
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

        public global::Pocos.TypeWithNameAttributes.NoAccessModifierClass CreateEmptyPoco()
        {
            return new global::Pocos.TypeWithNameAttributes.NoAccessModifierClass();
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