using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;

namespace Simatic.Ax.StateFramework
{
    public partial class State1Transition : Simatic.Ax.StateFramework.AbstractState
    {
        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public State1Transition(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            PreConstruct(parent, readableTail, symbolTail);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async override Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public new async Task<Pocos.Simatic.Ax.StateFramework.State1Transition> OnlineToPlainAsync()
        {
            Pocos.Simatic.Ax.StateFramework.State1Transition plain = new Pocos.Simatic.Ax.StateFramework.State1Transition();
            await this.ReadAsync<IgnoreOnPocoOperation>();
#pragma warning disable CS0612
            await base._OnlineToPlainNoacAsync(plain);
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public new async Task<Pocos.Simatic.Ax.StateFramework.State1Transition> _OnlineToPlainNoacAsync()
        {
            Pocos.Simatic.Ax.StateFramework.State1Transition plain = new Pocos.Simatic.Ax.StateFramework.State1Transition();
#pragma warning disable CS0612
            await base._OnlineToPlainNoacAsync(plain);
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.Simatic.Ax.StateFramework.State1Transition> _OnlineToPlainNoacAsync(Pocos.Simatic.Ax.StateFramework.State1Transition plain)
        {
#pragma warning disable CS0612
            await base._OnlineToPlainNoacAsync(plain);
#pragma warning restore CS0612
            return plain;
        }

        public async override Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Simatic.Ax.StateFramework.State1Transition plain)
        {
            await base._PlainToOnlineNoacAsync(plain);
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.Simatic.Ax.StateFramework.State1Transition plain)
        {
            await base._PlainToOnlineNoacAsync(plain);
        }

        public async override Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public new async Task<Pocos.Simatic.Ax.StateFramework.State1Transition> ShadowToPlainAsync()
        {
            Pocos.Simatic.Ax.StateFramework.State1Transition plain = new Pocos.Simatic.Ax.StateFramework.State1Transition();
            await base.ShadowToPlainAsync(plain);
            return plain;
        }

        protected async Task<Pocos.Simatic.Ax.StateFramework.State1Transition> ShadowToPlainAsync(Pocos.Simatic.Ax.StateFramework.State1Transition plain)
        {
            await base.ShadowToPlainAsync(plain);
            return plain;
        }

        public async override Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Simatic.Ax.StateFramework.State1Transition plain)
        {
            await base.PlainToShadowAsync(plain);
            return this.RetrievePrimitives();
        }

        ///<inheritdoc/>
        public async override Task<bool> AnyChangeAsync<T>(T plain)
        {
            return await this.DetectsAnyChangeAsync((dynamic)plain);
        }

        ///<summary>
        ///Compares if the current plain object has changed from the previous object.This method is used by the framework to determine if the object has changed and needs to be updated.
        ///[!NOTE] Any member in the hierarchy that is ignored by the compilers (e.g. when CompilerOmitAttribute is used) will not be compared, and therefore will not be detected as changed.
        ///</summary>
        public new async Task<bool> DetectsAnyChangeAsync(Pocos.Simatic.Ax.StateFramework.State1Transition plain, Pocos.Simatic.Ax.StateFramework.State1Transition latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (await base.DetectsAnyChangeAsync(plain))
                    return true;
                plain = latest;
                return somethingChanged;
            });
        }

        public new void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public new Pocos.Simatic.Ax.StateFramework.State1Transition CreateEmptyPoco()
        {
            return new Pocos.Simatic.Ax.StateFramework.State1Transition();
        }
    }
}

namespace Simatic.Ax.StateFramework
{
    public partial class AbstractState : AXSharp.Connector.ITwinObject, IState, IStateMuteable
    {
        public OnlinerInt StateID { get; }

        public OnlinerString StateName { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public AbstractState(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            StateID = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "StateID", "StateID");
            StateName = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "StateName", "StateName");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.Simatic.Ax.StateFramework.AbstractState> OnlineToPlainAsync()
        {
            Pocos.Simatic.Ax.StateFramework.AbstractState plain = new Pocos.Simatic.Ax.StateFramework.AbstractState();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.StateID = StateID.LastValue;
            plain.StateName = StateName.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.Simatic.Ax.StateFramework.AbstractState> _OnlineToPlainNoacAsync()
        {
            Pocos.Simatic.Ax.StateFramework.AbstractState plain = new Pocos.Simatic.Ax.StateFramework.AbstractState();
            plain.StateID = StateID.LastValue;
            plain.StateName = StateName.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.Simatic.Ax.StateFramework.AbstractState> _OnlineToPlainNoacAsync(Pocos.Simatic.Ax.StateFramework.AbstractState plain)
        {
            plain.StateID = StateID.LastValue;
            plain.StateName = StateName.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Simatic.Ax.StateFramework.AbstractState plain)
        {
#pragma warning disable CS0612
            StateID.LethargicWrite(plain.StateID);
#pragma warning restore CS0612
#pragma warning disable CS0612
            StateName.LethargicWrite(plain.StateName);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.Simatic.Ax.StateFramework.AbstractState plain)
        {
#pragma warning disable CS0612
            StateID.LethargicWrite(plain.StateID);
#pragma warning restore CS0612
#pragma warning disable CS0612
            StateName.LethargicWrite(plain.StateName);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.Simatic.Ax.StateFramework.AbstractState> ShadowToPlainAsync()
        {
            Pocos.Simatic.Ax.StateFramework.AbstractState plain = new Pocos.Simatic.Ax.StateFramework.AbstractState();
            plain.StateID = StateID.Shadow;
            plain.StateName = StateName.Shadow;
            return plain;
        }

        protected async Task<Pocos.Simatic.Ax.StateFramework.AbstractState> ShadowToPlainAsync(Pocos.Simatic.Ax.StateFramework.AbstractState plain)
        {
            plain.StateID = StateID.Shadow;
            plain.StateName = StateName.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Simatic.Ax.StateFramework.AbstractState plain)
        {
            StateID.Shadow = plain.StateID;
            StateName.Shadow = plain.StateName;
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
        public async Task<bool> DetectsAnyChangeAsync(Pocos.Simatic.Ax.StateFramework.AbstractState plain, Pocos.Simatic.Ax.StateFramework.AbstractState latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.StateID != StateID.LastValue)
                    somethingChanged = true;
                if (plain.StateName != StateName.LastValue)
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.Simatic.Ax.StateFramework.AbstractState CreateEmptyPoco()
        {
            return new Pocos.Simatic.Ax.StateFramework.AbstractState();
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