using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using RealMonsterData;

public partial class integratedTwinController : ITwinController
{
    public AXSharp.Connector.Connector Connector { get; }

    public MonsterData.Monster Monster { get; }

    public MonsterData.Monster OnlineToPlain_should_copy_entire_structure { get; }

    public MonsterData.Monster PlainToOnline_should_copy_entire_structure { get; }

    public MonsterData.Monster OnlineToShadowAsync_should_copy_entire_structure { get; }

    public MonsterData.Monster ShadowToOnlineAsync_should_copy_entire_structure { get; }

    public MonsterData.Monster ITwinObjectOnlineToPlain_should_copy_entire_structure { get; }

    public MonsterData.Monster ITwinObjectPlainToOnline_should_copy_entire_structure { get; }

    public MonsterData.Monster ITwinObjectOnlineToShadowAsync_should_copy_entire_structure { get; }

    public MonsterData.Monster ITwinObjectShadowToOnlineAsync_should_copy_entire_structure { get; }

    public MonsterData.Monster ShadowToPlainAsync_should_copy_entire_structure { get; }

    public MonsterData.Monster PlainToShadowAsync_should_copy_entire_structure { get; }

    public MonsterData.Monster ITwinObjectShadowToPlainAsync_should_copy_entire_structure { get; }

    public MonsterData.Monster ITwinObjectPlainToShadowAsync_should_copy_entire_structure { get; }

    public Pokus Pokus { get; }

    public RealMonsterData.RealMonster RealMonster { get; }

    public RealMonsterData.RealMonster OnlineToShadow_should_copy { get; }

    public RealMonsterData.RealMonster ShadowToOnline_should_copy { get; }

    public RealMonsterData.RealMonster OnlineToPlain_should_copy { get; }

    public RealMonsterData.RealMonster PlainToOnline_should_copy { get; }

    public RealMonsterData.RealMonster ITwinObjectOnlineToShadow_should_copy { get; }

    public RealMonsterData.RealMonster ITwinObjectShadowToOnline_should_copy { get; }

    public RealMonsterData.RealMonster ITwinObjectOnlineToPlain_should_copy { get; }

    public RealMonsterData.RealMonster ITwinObjectPlainToOnline_should_copy { get; }

    public all_primitives p_online_shadow { get; }

    public all_primitives p_shadow_online { get; }

    public all_primitives p_online_plain { get; }

    public all_primitives p_plain_online { get; }

    public all_primitives p_shadow_plain { get; }

    public all_primitives p_plain_shadow { get; }

    public RealMonsterData.RealMonster StartPolling_should_update_cyclic_property { get; }

    public RealMonsterData.RealMonster StartPolling_ConcurentOverload { get; }

    public RealMonsterData.RealMonster ChangeDetections { get; }

    public GH_ISSUE_183.GH_ISSUE_183_1 GH_ISSUE_183 { get; }

    public integratedTwinController(AXSharp.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        Monster = new MonsterData.Monster(this.Connector, "", "Monster");
        OnlineToPlain_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "OnlineToPlain_should_copy_entire_structure");
        PlainToOnline_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "PlainToOnline_should_copy_entire_structure");
        OnlineToShadowAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "OnlineToShadowAsync_should_copy_entire_structure");
        ShadowToOnlineAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ShadowToOnlineAsync_should_copy_entire_structure");
        ITwinObjectOnlineToPlain_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ITwinObjectOnlineToPlain_should_copy_entire_structure");
        ITwinObjectPlainToOnline_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ITwinObjectPlainToOnline_should_copy_entire_structure");
        ITwinObjectOnlineToShadowAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ITwinObjectOnlineToShadowAsync_should_copy_entire_structure");
        ITwinObjectShadowToOnlineAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ITwinObjectShadowToOnlineAsync_should_copy_entire_structure");
        ShadowToPlainAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ShadowToPlainAsync_should_copy_entire_structure");
        PlainToShadowAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "PlainToShadowAsync_should_copy_entire_structure");
        ITwinObjectShadowToPlainAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ITwinObjectShadowToPlainAsync_should_copy_entire_structure");
        ITwinObjectPlainToShadowAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ITwinObjectPlainToShadowAsync_should_copy_entire_structure");
        Pokus = new Pokus(this.Connector, "", "Pokus");
        RealMonster = new RealMonsterData.RealMonster(this.Connector, "", "RealMonster");
        OnlineToShadow_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "OnlineToShadow_should_copy");
        ShadowToOnline_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "ShadowToOnline_should_copy");
        OnlineToPlain_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "OnlineToPlain_should_copy");
        PlainToOnline_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "PlainToOnline_should_copy");
        ITwinObjectOnlineToShadow_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "ITwinObjectOnlineToShadow_should_copy");
        ITwinObjectShadowToOnline_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "ITwinObjectShadowToOnline_should_copy");
        ITwinObjectOnlineToPlain_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "ITwinObjectOnlineToPlain_should_copy");
        ITwinObjectPlainToOnline_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "ITwinObjectPlainToOnline_should_copy");
        p_online_shadow = new all_primitives(this.Connector, "", "p_online_shadow");
        p_shadow_online = new all_primitives(this.Connector, "", "p_shadow_online");
        p_online_plain = new all_primitives(this.Connector, "", "p_online_plain");
        p_plain_online = new all_primitives(this.Connector, "", "p_plain_online");
        p_shadow_plain = new all_primitives(this.Connector, "", "p_shadow_plain");
        p_plain_shadow = new all_primitives(this.Connector, "", "p_plain_shadow");
        StartPolling_should_update_cyclic_property = new RealMonsterData.RealMonster(this.Connector, "", "StartPolling_should_update_cyclic_property");
        StartPolling_ConcurentOverload = new RealMonsterData.RealMonster(this.Connector, "", "StartPolling_ConcurentOverload");
        ChangeDetections = new RealMonsterData.RealMonster(this.Connector, "", "ChangeDetections");
        GH_ISSUE_183 = new GH_ISSUE_183.GH_ISSUE_183_1(this.Connector, "", "GH_ISSUE_183");
    }

    public integratedTwinController(AXSharp.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        Monster = new MonsterData.Monster(this.Connector, "", "Monster");
        OnlineToPlain_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "OnlineToPlain_should_copy_entire_structure");
        PlainToOnline_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "PlainToOnline_should_copy_entire_structure");
        OnlineToShadowAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "OnlineToShadowAsync_should_copy_entire_structure");
        ShadowToOnlineAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ShadowToOnlineAsync_should_copy_entire_structure");
        ITwinObjectOnlineToPlain_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ITwinObjectOnlineToPlain_should_copy_entire_structure");
        ITwinObjectPlainToOnline_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ITwinObjectPlainToOnline_should_copy_entire_structure");
        ITwinObjectOnlineToShadowAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ITwinObjectOnlineToShadowAsync_should_copy_entire_structure");
        ITwinObjectShadowToOnlineAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ITwinObjectShadowToOnlineAsync_should_copy_entire_structure");
        ShadowToPlainAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ShadowToPlainAsync_should_copy_entire_structure");
        PlainToShadowAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "PlainToShadowAsync_should_copy_entire_structure");
        ITwinObjectShadowToPlainAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ITwinObjectShadowToPlainAsync_should_copy_entire_structure");
        ITwinObjectPlainToShadowAsync_should_copy_entire_structure = new MonsterData.Monster(this.Connector, "", "ITwinObjectPlainToShadowAsync_should_copy_entire_structure");
        Pokus = new Pokus(this.Connector, "", "Pokus");
        RealMonster = new RealMonsterData.RealMonster(this.Connector, "", "RealMonster");
        OnlineToShadow_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "OnlineToShadow_should_copy");
        ShadowToOnline_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "ShadowToOnline_should_copy");
        OnlineToPlain_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "OnlineToPlain_should_copy");
        PlainToOnline_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "PlainToOnline_should_copy");
        ITwinObjectOnlineToShadow_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "ITwinObjectOnlineToShadow_should_copy");
        ITwinObjectShadowToOnline_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "ITwinObjectShadowToOnline_should_copy");
        ITwinObjectOnlineToPlain_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "ITwinObjectOnlineToPlain_should_copy");
        ITwinObjectPlainToOnline_should_copy = new RealMonsterData.RealMonster(this.Connector, "", "ITwinObjectPlainToOnline_should_copy");
        p_online_shadow = new all_primitives(this.Connector, "", "p_online_shadow");
        p_shadow_online = new all_primitives(this.Connector, "", "p_shadow_online");
        p_online_plain = new all_primitives(this.Connector, "", "p_online_plain");
        p_plain_online = new all_primitives(this.Connector, "", "p_plain_online");
        p_shadow_plain = new all_primitives(this.Connector, "", "p_shadow_plain");
        p_plain_shadow = new all_primitives(this.Connector, "", "p_plain_shadow");
        StartPolling_should_update_cyclic_property = new RealMonsterData.RealMonster(this.Connector, "", "StartPolling_should_update_cyclic_property");
        StartPolling_ConcurentOverload = new RealMonsterData.RealMonster(this.Connector, "", "StartPolling_ConcurentOverload");
        ChangeDetections = new RealMonsterData.RealMonster(this.Connector, "", "ChangeDetections");
        GH_ISSUE_183 = new GH_ISSUE_183.GH_ISSUE_183_1(this.Connector, "", "GH_ISSUE_183");
    }
}

public partial class Pokus : AXSharp.Connector.ITwinObject
{
    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public Pokus(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

    public async Task<Pocos.Pokus> OnlineToPlainAsync()
    {
        Pocos.Pokus plain = new Pocos.Pokus();
        await this.ReadAsync<IgnoreOnPocoOperation>();
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task<Pocos.Pokus> _OnlineToPlainNoacAsync()
    {
        Pocos.Pokus plain = new Pocos.Pokus();
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    protected async Task<Pocos.Pokus> _OnlineToPlainNoacAsync(Pocos.Pokus plain)
    {
        return plain;
    }

    public async virtual Task PlainToOnline<T>(T plain)
    {
        await this.PlainToOnlineAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Pokus plain)
    {
        return await this.WriteAsync<IgnoreOnPocoOperation>();
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task _PlainToOnlineNoacAsync(Pocos.Pokus plain)
    {
    }

    public async virtual Task<T> ShadowToPlain<T>()
    {
        return await (dynamic)this.ShadowToPlainAsync();
    }

    public async Task<Pocos.Pokus> ShadowToPlainAsync()
    {
        Pocos.Pokus plain = new Pocos.Pokus();
        return plain;
    }

    protected async Task<Pocos.Pokus> ShadowToPlainAsync(Pocos.Pokus plain)
    {
        return plain;
    }

    public async virtual Task PlainToShadow<T>(T plain)
    {
        await this.PlainToShadowAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Pokus plain)
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
    public async Task<bool> DetectsAnyChangeAsync(Pocos.Pokus plain, Pocos.Pokus latest = null)
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

    public Pocos.Pokus CreateEmptyPoco()
    {
        return new Pocos.Pokus();
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

    public AXSharp.Connector.Localizations.Translator Interpreter => global::integrated.PlcTranslator.Instance;
}

public partial class Nested : AXSharp.Connector.ITwinObject
{
    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public Nested(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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

    public async Task<Pocos.Nested> OnlineToPlainAsync()
    {
        Pocos.Nested plain = new Pocos.Nested();
        await this.ReadAsync<IgnoreOnPocoOperation>();
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task<Pocos.Nested> _OnlineToPlainNoacAsync()
    {
        Pocos.Nested plain = new Pocos.Nested();
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    protected async Task<Pocos.Nested> _OnlineToPlainNoacAsync(Pocos.Nested plain)
    {
        return plain;
    }

    public async virtual Task PlainToOnline<T>(T plain)
    {
        await this.PlainToOnlineAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.Nested plain)
    {
        return await this.WriteAsync<IgnoreOnPocoOperation>();
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task _PlainToOnlineNoacAsync(Pocos.Nested plain)
    {
    }

    public async virtual Task<T> ShadowToPlain<T>()
    {
        return await (dynamic)this.ShadowToPlainAsync();
    }

    public async Task<Pocos.Nested> ShadowToPlainAsync()
    {
        Pocos.Nested plain = new Pocos.Nested();
        return plain;
    }

    protected async Task<Pocos.Nested> ShadowToPlainAsync(Pocos.Nested plain)
    {
        return plain;
    }

    public async virtual Task PlainToShadow<T>(T plain)
    {
        await this.PlainToShadowAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.Nested plain)
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
    public async Task<bool> DetectsAnyChangeAsync(Pocos.Nested plain, Pocos.Nested latest = null)
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

    public Pocos.Nested CreateEmptyPoco()
    {
        return new Pocos.Nested();
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

    public AXSharp.Connector.Localizations.Translator Interpreter => global::integrated.PlcTranslator.Instance;
}