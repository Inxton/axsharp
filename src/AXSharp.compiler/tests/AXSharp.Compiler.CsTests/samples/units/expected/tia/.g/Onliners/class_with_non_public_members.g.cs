using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

namespace ClassWithNonTraspilableMemberssNamespace
{
    public partial class ClassWithNonTraspilableMembers : AXSharp.Connector.ITwinObject
    {
        public ClassWithNonTraspilableMemberssNamespace.ComplexType1 myComplexType { get; }
        protected ClassWithNonTraspilableMemberssNamespace.ComplexType1 myComplexType1 { get; }
        protected OnlinerBool myBooool { get; }
        protected ClassWithNonTraspilableMemberssNamespace.ComplexType1 myComplexType2 { get; }
        protected OnlinerBool myBooool1 { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ClassWithNonTraspilableMembers(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            myComplexType = new ClassWithNonTraspilableMemberssNamespace.ComplexType1(this, "myComplexType", "myComplexType");
            myComplexType1 = new ClassWithNonTraspilableMemberssNamespace.ComplexType1(this, "myComplexType1", "myComplexType1");
            myBooool = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myBooool", "myBooool");
            myComplexType2 = new ClassWithNonTraspilableMemberssNamespace.ComplexType1(this, "myComplexType2", "myComplexType2");
            myBooool1 = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myBooool1", "myBooool1");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain = new global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
#pragma warning disable CS0612
            plain.myComplexType = await myComplexType._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers> _OnlineToPlainNoacAsync()
        {
            global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain = new global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers();
#pragma warning disable CS0612
            plain.myComplexType = await myComplexType._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers> _OnlineToPlainNoacAsync(global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain)
        {
#pragma warning disable CS0612
            plain.myComplexType = await myComplexType._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain, eAccessPriority priority = eAccessPriority.Normal)
        {
#pragma warning disable CS0612
            await this.myComplexType._PlainToOnlineNoacAsync(plain.myComplexType);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain)
        {
#pragma warning disable CS0612
            await this.myComplexType._PlainToOnlineNoacAsync(plain.myComplexType);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers> ShadowToPlainAsync()
        {
            global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain = new global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers();
            plain.myComplexType = await myComplexType.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers> ShadowToPlainAsync(global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain)
        {
            plain.myComplexType = await myComplexType.ShadowToPlainAsync();
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain)
        {
            await this.myComplexType.PlainToShadowAsync(plain.myComplexType);
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers plain, global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (await myComplexType.DetectsAnyChangeAsync(plain.myComplexType, latest.myComplexType))
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers CreateEmptyPoco()
        {
            return new global::Pocos.ClassWithNonTraspilableMemberssNamespace.ClassWithNonTraspilableMembers();
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

    public partial class ComplexType1 : AXSharp.Connector.ITwinObject
    {
        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ComplexType1(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

        public async Task<global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain = new global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1> _OnlineToPlainNoacAsync()
        {
            global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain = new global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1();
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1> _OnlineToPlainNoacAsync(global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain)
        {
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain)
        {
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1> ShadowToPlainAsync()
        {
            global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain = new global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1();
            return plain;
        }

        protected async Task<global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1> ShadowToPlainAsync(global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain)
        {
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain)
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 plain, global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 latest = null)
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

        public global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1 CreateEmptyPoco()
        {
            return new global::Pocos.ClassWithNonTraspilableMemberssNamespace.ComplexType1();
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