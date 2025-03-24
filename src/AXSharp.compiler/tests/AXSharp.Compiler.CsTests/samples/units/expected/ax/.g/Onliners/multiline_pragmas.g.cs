using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

namespace MultilinePragmas
{
    public partial class Extendee2 : AXSharp.Connector.ITwinObject
    {
        public OnlinerInt _messge { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public Extendee2(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            _messge = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "_messge", "_messge");
            _messge.PlcTextList = @"[1]:'<#Messenger 1: message text for message code 1#>':'<#Messenger 1: help text for message code 1#>';
                                    [2]:'<#Messenger 1: message text for message code 2#>':'<#Messenger 1: help text for message code 2#>';
                                    [3]:'<#Messenger 1: message text for message code 3#>':'<#Messenger 1: help text for message code 3#>';
                                    [4]:'<#Messenger 1: message text for message code 4#>':'<#Messenger 1: help text for message code 4#>';
                                    [5]:'<#Messenger 1: message text for message code 5#>':'<#Messenger 1: help text for message code 5#>';
                                    [6]:'<#Messenger 1: message text for message code 6#>':'<#Messenger 1: help text for message code 6#>';
                                    [7]:'<#Messenger 1: message text for message code 7#>':'<#Messenger 1: help text for message code 7#>';
                                    [8]:'<#Messenger 1: message text for message code 8#>':'<#Messenger 1: help text for message code 8#>';
                                    [9]:'<#Messenger 1: message text for message code 9#>':'<#Messenger 1: help text for message code 9#>';
                                    [10]:'<#Messenger 1: message text for message code 10#>':'<#Messenger 1: help text for message code 10#>';
                                    [11]:'<#Messenger 1: message text for message code 11#>':'<#Messenger 1: help text for message code 11#>'";
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<global::Pocos.MultilinePragmas.Extendee2> OnlineToPlainAsync()
        {
            global::Pocos.MultilinePragmas.Extendee2 plain = new global::Pocos.MultilinePragmas.Extendee2();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain._messge = _messge.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.MultilinePragmas.Extendee2> _OnlineToPlainNoacAsync()
        {
            global::Pocos.MultilinePragmas.Extendee2 plain = new global::Pocos.MultilinePragmas.Extendee2();
            plain._messge = _messge.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.MultilinePragmas.Extendee2> _OnlineToPlainNoacAsync(global::Pocos.MultilinePragmas.Extendee2 plain)
        {
            plain._messge = _messge.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.MultilinePragmas.Extendee2 plain)
        {
#pragma warning disable CS0612
            _messge.LethargicWrite(plain._messge);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.MultilinePragmas.Extendee2 plain)
        {
#pragma warning disable CS0612
            _messge.LethargicWrite(plain._messge);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.MultilinePragmas.Extendee2> ShadowToPlainAsync()
        {
            global::Pocos.MultilinePragmas.Extendee2 plain = new global::Pocos.MultilinePragmas.Extendee2();
            plain._messge = _messge.Shadow;
            return plain;
        }

        protected async Task<global::Pocos.MultilinePragmas.Extendee2> ShadowToPlainAsync(global::Pocos.MultilinePragmas.Extendee2 plain)
        {
            plain._messge = _messge.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.MultilinePragmas.Extendee2 plain)
        {
            _messge.Shadow = plain._messge;
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.MultilinePragmas.Extendee2 plain, global::Pocos.MultilinePragmas.Extendee2 latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain._messge != _messge.LastValue)
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.MultilinePragmas.Extendee2 CreateEmptyPoco()
        {
            return new global::Pocos.MultilinePragmas.Extendee2();
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