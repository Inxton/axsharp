using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

namespace makereadonly
{
    public partial class MembersWithMakeReadOnly : AXSharp.Connector.ITwinObject
    {
        [ReadOnly()]
        public OnlinerString makeReadOnceMember { get; }
        public OnlinerString someOtherMember { get; }

        [ReadOnly()]
        public makereadonly.ComplexMember makeReadComplexMember { get; }
        public makereadonly.ComplexMember someotherComplexMember { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public MembersWithMakeReadOnly(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            makeReadOnceMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "makeReadOnceMember", "makeReadOnceMember");
            makeReadOnceMember.Capacity = 254;
            makeReadOnceMember.MakeReadOnly();
            someOtherMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "someOtherMember", "someOtherMember");
            someOtherMember.Capacity = 254;
            makeReadComplexMember = new makereadonly.ComplexMember(this, "makeReadComplexMember", "makeReadComplexMember");
            makeReadComplexMember.MakeReadOnly();
            someotherComplexMember = new makereadonly.ComplexMember(this, "someotherComplexMember", "someotherComplexMember");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<global::Pocos.makereadonly.MembersWithMakeReadOnly> OnlineToPlainAsync()
        {
            global::Pocos.makereadonly.MembersWithMakeReadOnly plain = new global::Pocos.makereadonly.MembersWithMakeReadOnly();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.makeReadOnceMember = makeReadOnceMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
#pragma warning disable CS0612
            plain.makeReadComplexMember = await makeReadComplexMember._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain.someotherComplexMember = await someotherComplexMember._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.makereadonly.MembersWithMakeReadOnly> _OnlineToPlainNoacAsync()
        {
            global::Pocos.makereadonly.MembersWithMakeReadOnly plain = new global::Pocos.makereadonly.MembersWithMakeReadOnly();
            plain.makeReadOnceMember = makeReadOnceMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
#pragma warning disable CS0612
            plain.makeReadComplexMember = await makeReadComplexMember._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain.someotherComplexMember = await someotherComplexMember._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.makereadonly.MembersWithMakeReadOnly> _OnlineToPlainNoacAsync(global::Pocos.makereadonly.MembersWithMakeReadOnly plain)
        {
            plain.makeReadOnceMember = makeReadOnceMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
#pragma warning disable CS0612
            plain.makeReadComplexMember = await makeReadComplexMember._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain.someotherComplexMember = await someotherComplexMember._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.makereadonly.MembersWithMakeReadOnly plain)
        {
#pragma warning disable CS0612
            makeReadOnceMember.LethargicWrite(plain.makeReadOnceMember);
#pragma warning restore CS0612
#pragma warning disable CS0612
            someOtherMember.LethargicWrite(plain.someOtherMember);
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this.makeReadComplexMember._PlainToOnlineNoacAsync(plain.makeReadComplexMember);
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this.someotherComplexMember._PlainToOnlineNoacAsync(plain.someotherComplexMember);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.makereadonly.MembersWithMakeReadOnly plain)
        {
#pragma warning disable CS0612
            makeReadOnceMember.LethargicWrite(plain.makeReadOnceMember);
#pragma warning restore CS0612
#pragma warning disable CS0612
            someOtherMember.LethargicWrite(plain.someOtherMember);
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this.makeReadComplexMember._PlainToOnlineNoacAsync(plain.makeReadComplexMember);
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this.someotherComplexMember._PlainToOnlineNoacAsync(plain.someotherComplexMember);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.makereadonly.MembersWithMakeReadOnly> ShadowToPlainAsync()
        {
            global::Pocos.makereadonly.MembersWithMakeReadOnly plain = new global::Pocos.makereadonly.MembersWithMakeReadOnly();
            plain.makeReadOnceMember = makeReadOnceMember.Shadow;
            plain.someOtherMember = someOtherMember.Shadow;
            plain.makeReadComplexMember = await makeReadComplexMember.ShadowToPlainAsync();
            plain.someotherComplexMember = await someotherComplexMember.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<global::Pocos.makereadonly.MembersWithMakeReadOnly> ShadowToPlainAsync(global::Pocos.makereadonly.MembersWithMakeReadOnly plain)
        {
            plain.makeReadOnceMember = makeReadOnceMember.Shadow;
            plain.someOtherMember = someOtherMember.Shadow;
            plain.makeReadComplexMember = await makeReadComplexMember.ShadowToPlainAsync();
            plain.someotherComplexMember = await someotherComplexMember.ShadowToPlainAsync();
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.makereadonly.MembersWithMakeReadOnly plain)
        {
            makeReadOnceMember.Shadow = plain.makeReadOnceMember;
            someOtherMember.Shadow = plain.someOtherMember;
            await this.makeReadComplexMember.PlainToShadowAsync(plain.makeReadComplexMember);
            await this.someotherComplexMember.PlainToShadowAsync(plain.someotherComplexMember);
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.makereadonly.MembersWithMakeReadOnly plain, global::Pocos.makereadonly.MembersWithMakeReadOnly latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.makeReadOnceMember != makeReadOnceMember.LastValue)
                    somethingChanged = true;
                if (plain.someOtherMember != someOtherMember.LastValue)
                    somethingChanged = true;
                if (await makeReadComplexMember.DetectsAnyChangeAsync(plain.makeReadComplexMember, latest.makeReadComplexMember))
                    somethingChanged = true;
                if (await someotherComplexMember.DetectsAnyChangeAsync(plain.someotherComplexMember, latest.someotherComplexMember))
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.makereadonly.MembersWithMakeReadOnly CreateEmptyPoco()
        {
            return new global::Pocos.makereadonly.MembersWithMakeReadOnly();
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

    public partial class ComplexMember : AXSharp.Connector.ITwinObject
    {
        public OnlinerString someMember { get; }
        public OnlinerString someOtherMember { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public ComplexMember(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            someMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "someMember", "someMember");
            someMember.Capacity = 254;
            someOtherMember = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "someOtherMember", "someOtherMember");
            someOtherMember.Capacity = 254;
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<global::Pocos.makereadonly.ComplexMember> OnlineToPlainAsync()
        {
            global::Pocos.makereadonly.ComplexMember plain = new global::Pocos.makereadonly.ComplexMember();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.someMember = someMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.makereadonly.ComplexMember> _OnlineToPlainNoacAsync()
        {
            global::Pocos.makereadonly.ComplexMember plain = new global::Pocos.makereadonly.ComplexMember();
            plain.someMember = someMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.makereadonly.ComplexMember> _OnlineToPlainNoacAsync(global::Pocos.makereadonly.ComplexMember plain)
        {
            plain.someMember = someMember.LastValue;
            plain.someOtherMember = someOtherMember.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.makereadonly.ComplexMember plain)
        {
#pragma warning disable CS0612
            someMember.LethargicWrite(plain.someMember);
#pragma warning restore CS0612
#pragma warning disable CS0612
            someOtherMember.LethargicWrite(plain.someOtherMember);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.makereadonly.ComplexMember plain)
        {
#pragma warning disable CS0612
            someMember.LethargicWrite(plain.someMember);
#pragma warning restore CS0612
#pragma warning disable CS0612
            someOtherMember.LethargicWrite(plain.someOtherMember);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.makereadonly.ComplexMember> ShadowToPlainAsync()
        {
            global::Pocos.makereadonly.ComplexMember plain = new global::Pocos.makereadonly.ComplexMember();
            plain.someMember = someMember.Shadow;
            plain.someOtherMember = someOtherMember.Shadow;
            return plain;
        }

        protected async Task<global::Pocos.makereadonly.ComplexMember> ShadowToPlainAsync(global::Pocos.makereadonly.ComplexMember plain)
        {
            plain.someMember = someMember.Shadow;
            plain.someOtherMember = someOtherMember.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.makereadonly.ComplexMember plain)
        {
            someMember.Shadow = plain.someMember;
            someOtherMember.Shadow = plain.someOtherMember;
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.makereadonly.ComplexMember plain, global::Pocos.makereadonly.ComplexMember latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.someMember != someMember.LastValue)
                    somethingChanged = true;
                if (plain.someOtherMember != someOtherMember.LastValue)
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.makereadonly.ComplexMember CreateEmptyPoco()
        {
            return new global::Pocos.makereadonly.ComplexMember();
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