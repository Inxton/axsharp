using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

namespace Simatic.Ax.StateFramework
{
    [AXSharp.Connector.SourceFileAttribute(@"type_with_enum.st")]
    public partial interface IGuard
    {
    }
}

namespace Simatic.Ax.StateFramework
{
    [AXSharp.Connector.SourceFileAttribute(@"type_with_enum.st")]
    public enum Condition
    {
        GT,
        EQ,
        LT,
        NE,
        GE,
        LE
    }

    [AXSharp.Connector.SourceFileAttribute(@"type_with_enum.st")]
    public partial class CompareGuardLint : AXSharp.Connector.ITwinObject, IGuard
    {
        public OnlinerLInt CompareToValue { get; }

        [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(Simatic.Ax.StateFramework.Condition))]
        public OnlinerInt Condition { get; }
        public Simatic.Ax.StateFramework.Condition ConditionEnum { get => (Simatic.Ax.StateFramework.Condition)Condition.LastValue; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public CompareGuardLint(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            CompareToValue = @Connector.ConnectorAdapter.AdapterFactory.CreateLINT(this, "CompareToValue", "CompareToValue");
            Condition = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "Condition", "Condition");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
        {
            return await (dynamic)this.OnlineToPlainAsync(priority);
        }

        public async Task<global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
        {
            global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain = new global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint();
            await this.ReadAsync<IgnoreOnPocoOperation>(priority);
            plain.CompareToValue = CompareToValue.LastValue;
            plain.Condition = (Simatic.Ax.StateFramework.Condition)Condition.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint> _OnlineToPlainNoacAsync()
        {
            global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain = new global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint();
            plain.CompareToValue = CompareToValue.LastValue;
            plain.Condition = (Simatic.Ax.StateFramework.Condition)Condition.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint> _OnlineToPlainNoacAsync(global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain)
        {
            plain.CompareToValue = CompareToValue.LastValue;
            plain.Condition = (Simatic.Ax.StateFramework.Condition)Condition.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
        {
            await this.PlainToOnlineAsync((dynamic)plain, priority);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain, eAccessPriority priority = eAccessPriority.Normal)
        {
#pragma warning disable CS0612
            CompareToValue.LethargicWrite(plain.CompareToValue);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Condition.LethargicWrite((short)plain.Condition);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain)
        {
#pragma warning disable CS0612
            CompareToValue.LethargicWrite(plain.CompareToValue);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Condition.LethargicWrite((short)plain.Condition);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint> ShadowToPlainAsync()
        {
            global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain = new global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint();
            plain.CompareToValue = CompareToValue.Shadow;
            plain.Condition = (Simatic.Ax.StateFramework.Condition)Condition.Shadow;
            return plain;
        }

        protected async Task<global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint> ShadowToPlainAsync(global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain)
        {
            plain.CompareToValue = CompareToValue.Shadow;
            plain.Condition = (Simatic.Ax.StateFramework.Condition)Condition.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain)
        {
            CompareToValue.Shadow = plain.CompareToValue;
            Condition.Shadow = (short)plain.Condition;
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
        public async Task<bool> DetectsAnyChangeAsync(global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint plain, global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.CompareToValue != CompareToValue.LastValue)
                    somethingChanged = true;
                if (plain.Condition != (Simatic.Ax.StateFramework.Condition)latest.Condition)
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint CreateEmptyPoco()
        {
            return new global::Pocos.Simatic.Ax.StateFramework.CompareGuardLint();
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