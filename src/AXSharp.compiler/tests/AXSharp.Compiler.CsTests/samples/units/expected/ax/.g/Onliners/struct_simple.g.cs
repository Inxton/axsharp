using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

[AXSharp.Connector.SourceFileAttribute(@"struct_simple.st")]
public partial class StructSimpleMotor : AXSharp.Connector.ITwinObject
{
    public OnlinerBool isRunning { get; }

    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public StructSimpleMotor(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

    public async Task<global::Pocos.StructSimpleMotor> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
    {
        global::Pocos.StructSimpleMotor plain = new global::Pocos.StructSimpleMotor();
        await this.ReadAsync<IgnoreOnPocoOperation>(priority);
        plain.isRunning = isRunning.LastValue;
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task<global::Pocos.StructSimpleMotor> _OnlineToPlainNoacAsync()
    {
        global::Pocos.StructSimpleMotor plain = new global::Pocos.StructSimpleMotor();
        plain.isRunning = isRunning.LastValue;
        return plain;
    }

    protected async Task<global::Pocos.StructSimpleMotor> OnlineToPlainAsync(global::Pocos.StructSimpleMotor plain)
    {
        plain.isRunning = isRunning.LastValue;
        return plain;
    }

    public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
    {
        await this.PlainToOnlineAsync((dynamic)plain, priority);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.StructSimpleMotor plain, eAccessPriority priority = eAccessPriority.Normal)
    {
#pragma warning disable CS0612
        isRunning.LethargicWrite(plain.isRunning);
#pragma warning restore CS0612
        return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task _PlainToOnlineNoacAsync(global::Pocos.StructSimpleMotor plain)
    {
#pragma warning disable CS0612
        isRunning.LethargicWrite(plain.isRunning);
#pragma warning restore CS0612
    }

    public async virtual Task<T> ShadowToPlain<T>()
    {
        return await (dynamic)this.ShadowToPlainAsync();
    }

    public async Task<global::Pocos.StructSimpleMotor> ShadowToPlainAsync()
    {
        global::Pocos.StructSimpleMotor plain = new global::Pocos.StructSimpleMotor();
        plain.isRunning = isRunning.Shadow;
        return plain;
    }

    protected async Task<global::Pocos.StructSimpleMotor> ShadowToPlainAsync(global::Pocos.StructSimpleMotor plain)
    {
        plain.isRunning = isRunning.Shadow;
        return plain;
    }

    public async virtual Task PlainToShadow<T>(T plain)
    {
        await this.PlainToShadowAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.StructSimpleMotor plain)
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
    public async Task<bool> DetectsAnyChangeAsync(global::Pocos.StructSimpleMotor plain, global::Pocos.StructSimpleMotor latest = null)
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

    public global::Pocos.StructSimpleMotor CreateEmptyPoco()
    {
        return new global::Pocos.StructSimpleMotor();
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

[AXSharp.Connector.SourceFileAttribute(@"struct_simple.st")]
public partial class StructSimpleVehicle : AXSharp.Connector.ITwinObject
{
    public StructSimpleMotor m { get; }
    public OnlinerInt displacement { get; }

    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public StructSimpleVehicle(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        PreConstruct(parent, readableTail, symbolTail);
        m = new StructSimpleMotor(this, "m", "m");
        displacement = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "displacement", "displacement");
        parent.AddChild(this);
        parent.AddKid(this);
        PostConstruct(parent, readableTail, symbolTail);
    }

    public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
    {
        return await (dynamic)this.OnlineToPlainAsync(priority);
    }

    public async Task<global::Pocos.StructSimpleVehicle> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
    {
        global::Pocos.StructSimpleVehicle plain = new global::Pocos.StructSimpleVehicle();
        await this.ReadAsync<IgnoreOnPocoOperation>(priority);
#pragma warning disable CS0612
        plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
        plain.displacement = displacement.LastValue;
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task<global::Pocos.StructSimpleVehicle> _OnlineToPlainNoacAsync()
    {
        global::Pocos.StructSimpleVehicle plain = new global::Pocos.StructSimpleVehicle();
#pragma warning disable CS0612
        plain.m = await m._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
        plain.displacement = displacement.LastValue;
        return plain;
    }

    protected async Task<global::Pocos.StructSimpleVehicle> OnlineToPlainAsync(global::Pocos.StructSimpleVehicle plain)
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

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.StructSimpleVehicle plain, eAccessPriority priority = eAccessPriority.Normal)
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
    public async Task _PlainToOnlineNoacAsync(global::Pocos.StructSimpleVehicle plain)
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

    public async Task<global::Pocos.StructSimpleVehicle> ShadowToPlainAsync()
    {
        global::Pocos.StructSimpleVehicle plain = new global::Pocos.StructSimpleVehicle();
        plain.m = await m.ShadowToPlainAsync();
        plain.displacement = displacement.Shadow;
        return plain;
    }

    protected async Task<global::Pocos.StructSimpleVehicle> ShadowToPlainAsync(global::Pocos.StructSimpleVehicle plain)
    {
        plain.m = await m.ShadowToPlainAsync();
        plain.displacement = displacement.Shadow;
        return plain;
    }

    public async virtual Task PlainToShadow<T>(T plain)
    {
        await this.PlainToShadowAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.StructSimpleVehicle plain)
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
    public async Task<bool> DetectsAnyChangeAsync(global::Pocos.StructSimpleVehicle plain, global::Pocos.StructSimpleVehicle latest = null)
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

    public global::Pocos.StructSimpleVehicle CreateEmptyPoco()
    {
        return new global::Pocos.StructSimpleVehicle();
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