using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

[AXSharp.Connector.SourceFileAttribute(@"class_all_primitives.st")]
public partial class class_all_primitives : AXSharp.Connector.ITwinObject
{
    public OnlinerBool myBOOL { get; }
    public OnlinerByte myBYTE { get; }
    public OnlinerWord myWORD { get; }
    public OnlinerDWord myDWORD { get; }
    public OnlinerLWord myLWORD { get; }
    public OnlinerSInt mySINT { get; }
    public OnlinerInt myINT { get; }
    public OnlinerDInt myDINT { get; }
    public OnlinerLInt myLINT { get; }
    public OnlinerUSInt myUSINT { get; }
    public OnlinerUInt myUINT { get; }
    public OnlinerUDInt myUDINT { get; }
    public OnlinerULInt myULINT { get; }
    public OnlinerReal myREAL { get; }
    public OnlinerLReal myLREAL { get; }
    public OnlinerTime myTIME { get; }
    public OnlinerLTime myLTIME { get; }
    public OnlinerDate myDATE { get; }
    public OnlinerTimeOfDay myTIME_OF_DAY { get; }
    public OnlinerDateTime myDATE_AND_TIME { get; }
    public OnlinerString mySTRING { get; }
    public OnlinerWString myWSTRING { get; }

    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public class_all_primitives(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
    {
        Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
        this.@SymbolTail = symbolTail;
        this.@Connector = parent.GetConnector();
        this.@Parent = parent;
        HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
        PreConstruct(parent, readableTail, symbolTail);
        myBOOL = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this, "myBOOL", "myBOOL");
        myBYTE = @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(this, "myBYTE", "myBYTE");
        myWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateWORD(this, "myWORD", "myWORD");
        myDWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateDWORD(this, "myDWORD", "myDWORD");
        myLWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateLWORD(this, "myLWORD", "myLWORD");
        mySINT = @Connector.ConnectorAdapter.AdapterFactory.CreateSINT(this, "mySINT", "mySINT");
        myINT = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this, "myINT", "myINT");
        myDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateDINT(this, "myDINT", "myDINT");
        myLINT = @Connector.ConnectorAdapter.AdapterFactory.CreateLINT(this, "myLINT", "myLINT");
        myUSINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUSINT(this, "myUSINT", "myUSINT");
        myUINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUINT(this, "myUINT", "myUINT");
        myUDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUDINT(this, "myUDINT", "myUDINT");
        myULINT = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "myULINT", "myULINT");
        myREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this, "myREAL", "myREAL");
        myLREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "myLREAL", "myLREAL");
        myTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME(this, "myTIME", "myTIME");
        myLTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME(this, "myLTIME", "myLTIME");
        myDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this, "myDATE", "myDATE");
        myTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this, "myTIME_OF_DAY", "myTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this, "myDATE_AND_TIME", "myDATE_AND_TIME");
        mySTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "mySTRING", "mySTRING");
        mySTRING.Capacity = 254;
        myWSTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this, "myWSTRING", "myWSTRING");
        myWSTRING.Capacity = 254;
        parent.AddChild(this);
        parent.AddKid(this);
        PostConstruct(parent, readableTail, symbolTail);
    }

    public async virtual Task<T> OnlineToPlain<T>(eAccessPriority priority = eAccessPriority.Normal)
    {
        return await (dynamic)this.OnlineToPlainAsync(priority);
    }

    public async Task<global::Pocos.class_all_primitives> OnlineToPlainAsync(eAccessPriority priority = eAccessPriority.Normal)
    {
        global::Pocos.class_all_primitives plain = new global::Pocos.class_all_primitives();
        await this.ReadAsync<IgnoreOnPocoOperation>(priority);
        plain.myBOOL = myBOOL.LastValue;
        plain.myBYTE = myBYTE.LastValue;
        plain.myWORD = myWORD.LastValue;
        plain.myDWORD = myDWORD.LastValue;
        plain.myLWORD = myLWORD.LastValue;
        plain.mySINT = mySINT.LastValue;
        plain.myINT = myINT.LastValue;
        plain.myDINT = myDINT.LastValue;
        plain.myLINT = myLINT.LastValue;
        plain.myUSINT = myUSINT.LastValue;
        plain.myUINT = myUINT.LastValue;
        plain.myUDINT = myUDINT.LastValue;
        plain.myULINT = myULINT.LastValue;
        plain.myREAL = myREAL.LastValue;
        plain.myLREAL = myLREAL.LastValue;
        plain.myTIME = myTIME.LastValue;
        plain.myLTIME = myLTIME.LastValue;
        plain.myDATE = myDATE.LastValue;
        plain.myTIME_OF_DAY = myTIME_OF_DAY.LastValue;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.LastValue;
        plain.mySTRING = mySTRING.LastValue;
        plain.myWSTRING = myWSTRING.LastValue;
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task<global::Pocos.class_all_primitives> _OnlineToPlainNoacAsync()
    {
        global::Pocos.class_all_primitives plain = new global::Pocos.class_all_primitives();
        plain.myBOOL = myBOOL.LastValue;
        plain.myBYTE = myBYTE.LastValue;
        plain.myWORD = myWORD.LastValue;
        plain.myDWORD = myDWORD.LastValue;
        plain.myLWORD = myLWORD.LastValue;
        plain.mySINT = mySINT.LastValue;
        plain.myINT = myINT.LastValue;
        plain.myDINT = myDINT.LastValue;
        plain.myLINT = myLINT.LastValue;
        plain.myUSINT = myUSINT.LastValue;
        plain.myUINT = myUINT.LastValue;
        plain.myUDINT = myUDINT.LastValue;
        plain.myULINT = myULINT.LastValue;
        plain.myREAL = myREAL.LastValue;
        plain.myLREAL = myLREAL.LastValue;
        plain.myTIME = myTIME.LastValue;
        plain.myLTIME = myLTIME.LastValue;
        plain.myDATE = myDATE.LastValue;
        plain.myTIME_OF_DAY = myTIME_OF_DAY.LastValue;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.LastValue;
        plain.mySTRING = mySTRING.LastValue;
        plain.myWSTRING = myWSTRING.LastValue;
        return plain;
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    protected async Task<global::Pocos.class_all_primitives> _OnlineToPlainNoacAsync(global::Pocos.class_all_primitives plain)
    {
        plain.myBOOL = myBOOL.LastValue;
        plain.myBYTE = myBYTE.LastValue;
        plain.myWORD = myWORD.LastValue;
        plain.myDWORD = myDWORD.LastValue;
        plain.myLWORD = myLWORD.LastValue;
        plain.mySINT = mySINT.LastValue;
        plain.myINT = myINT.LastValue;
        plain.myDINT = myDINT.LastValue;
        plain.myLINT = myLINT.LastValue;
        plain.myUSINT = myUSINT.LastValue;
        plain.myUINT = myUINT.LastValue;
        plain.myUDINT = myUDINT.LastValue;
        plain.myULINT = myULINT.LastValue;
        plain.myREAL = myREAL.LastValue;
        plain.myLREAL = myLREAL.LastValue;
        plain.myTIME = myTIME.LastValue;
        plain.myLTIME = myLTIME.LastValue;
        plain.myDATE = myDATE.LastValue;
        plain.myTIME_OF_DAY = myTIME_OF_DAY.LastValue;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.LastValue;
        plain.mySTRING = mySTRING.LastValue;
        plain.myWSTRING = myWSTRING.LastValue;
        return plain;
    }

    public async virtual Task PlainToOnline<T>(T plain, eAccessPriority priority = eAccessPriority.Normal)
    {
        await this.PlainToOnlineAsync((dynamic)plain, priority);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(global::Pocos.class_all_primitives plain, eAccessPriority priority = eAccessPriority.Normal)
    {
#pragma warning disable CS0612
        myBOOL.LethargicWrite(plain.myBOOL);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myBYTE.LethargicWrite(plain.myBYTE);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myWORD.LethargicWrite(plain.myWORD);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myDWORD.LethargicWrite(plain.myDWORD);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myLWORD.LethargicWrite(plain.myLWORD);
#pragma warning restore CS0612
#pragma warning disable CS0612
        mySINT.LethargicWrite(plain.mySINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myINT.LethargicWrite(plain.myINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myDINT.LethargicWrite(plain.myDINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myLINT.LethargicWrite(plain.myLINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myUSINT.LethargicWrite(plain.myUSINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myUINT.LethargicWrite(plain.myUINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myUDINT.LethargicWrite(plain.myUDINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myULINT.LethargicWrite(plain.myULINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myREAL.LethargicWrite(plain.myREAL);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myLREAL.LethargicWrite(plain.myLREAL);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myTIME.LethargicWrite(plain.myTIME);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myLTIME.LethargicWrite(plain.myLTIME);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myDATE.LethargicWrite(plain.myDATE);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myTIME_OF_DAY.LethargicWrite(plain.myTIME_OF_DAY);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myDATE_AND_TIME.LethargicWrite(plain.myDATE_AND_TIME);
#pragma warning restore CS0612
#pragma warning disable CS0612
        mySTRING.LethargicWrite(plain.mySTRING);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myWSTRING.LethargicWrite(plain.myWSTRING);
#pragma warning restore CS0612
        return await this.WriteAsync<IgnoreOnPocoOperation>(priority);
    }

    [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
    public async Task _PlainToOnlineNoacAsync(global::Pocos.class_all_primitives plain)
    {
#pragma warning disable CS0612
        myBOOL.LethargicWrite(plain.myBOOL);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myBYTE.LethargicWrite(plain.myBYTE);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myWORD.LethargicWrite(plain.myWORD);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myDWORD.LethargicWrite(plain.myDWORD);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myLWORD.LethargicWrite(plain.myLWORD);
#pragma warning restore CS0612
#pragma warning disable CS0612
        mySINT.LethargicWrite(plain.mySINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myINT.LethargicWrite(plain.myINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myDINT.LethargicWrite(plain.myDINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myLINT.LethargicWrite(plain.myLINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myUSINT.LethargicWrite(plain.myUSINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myUINT.LethargicWrite(plain.myUINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myUDINT.LethargicWrite(plain.myUDINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myULINT.LethargicWrite(plain.myULINT);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myREAL.LethargicWrite(plain.myREAL);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myLREAL.LethargicWrite(plain.myLREAL);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myTIME.LethargicWrite(plain.myTIME);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myLTIME.LethargicWrite(plain.myLTIME);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myDATE.LethargicWrite(plain.myDATE);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myTIME_OF_DAY.LethargicWrite(plain.myTIME_OF_DAY);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myDATE_AND_TIME.LethargicWrite(plain.myDATE_AND_TIME);
#pragma warning restore CS0612
#pragma warning disable CS0612
        mySTRING.LethargicWrite(plain.mySTRING);
#pragma warning restore CS0612
#pragma warning disable CS0612
        myWSTRING.LethargicWrite(plain.myWSTRING);
#pragma warning restore CS0612
    }

    public async virtual Task<T> ShadowToPlain<T>()
    {
        return await (dynamic)this.ShadowToPlainAsync();
    }

    public async Task<global::Pocos.class_all_primitives> ShadowToPlainAsync()
    {
        global::Pocos.class_all_primitives plain = new global::Pocos.class_all_primitives();
        plain.myBOOL = myBOOL.Shadow;
        plain.myBYTE = myBYTE.Shadow;
        plain.myWORD = myWORD.Shadow;
        plain.myDWORD = myDWORD.Shadow;
        plain.myLWORD = myLWORD.Shadow;
        plain.mySINT = mySINT.Shadow;
        plain.myINT = myINT.Shadow;
        plain.myDINT = myDINT.Shadow;
        plain.myLINT = myLINT.Shadow;
        plain.myUSINT = myUSINT.Shadow;
        plain.myUINT = myUINT.Shadow;
        plain.myUDINT = myUDINT.Shadow;
        plain.myULINT = myULINT.Shadow;
        plain.myREAL = myREAL.Shadow;
        plain.myLREAL = myLREAL.Shadow;
        plain.myTIME = myTIME.Shadow;
        plain.myLTIME = myLTIME.Shadow;
        plain.myDATE = myDATE.Shadow;
        plain.myTIME_OF_DAY = myTIME_OF_DAY.Shadow;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.Shadow;
        plain.mySTRING = mySTRING.Shadow;
        plain.myWSTRING = myWSTRING.Shadow;
        return plain;
    }

    protected async Task<global::Pocos.class_all_primitives> ShadowToPlainAsync(global::Pocos.class_all_primitives plain)
    {
        plain.myBOOL = myBOOL.Shadow;
        plain.myBYTE = myBYTE.Shadow;
        plain.myWORD = myWORD.Shadow;
        plain.myDWORD = myDWORD.Shadow;
        plain.myLWORD = myLWORD.Shadow;
        plain.mySINT = mySINT.Shadow;
        plain.myINT = myINT.Shadow;
        plain.myDINT = myDINT.Shadow;
        plain.myLINT = myLINT.Shadow;
        plain.myUSINT = myUSINT.Shadow;
        plain.myUINT = myUINT.Shadow;
        plain.myUDINT = myUDINT.Shadow;
        plain.myULINT = myULINT.Shadow;
        plain.myREAL = myREAL.Shadow;
        plain.myLREAL = myLREAL.Shadow;
        plain.myTIME = myTIME.Shadow;
        plain.myLTIME = myLTIME.Shadow;
        plain.myDATE = myDATE.Shadow;
        plain.myTIME_OF_DAY = myTIME_OF_DAY.Shadow;
        plain.myDATE_AND_TIME = myDATE_AND_TIME.Shadow;
        plain.mySTRING = mySTRING.Shadow;
        plain.myWSTRING = myWSTRING.Shadow;
        return plain;
    }

    public async virtual Task PlainToShadow<T>(T plain)
    {
        await this.PlainToShadowAsync((dynamic)plain);
    }

    public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(global::Pocos.class_all_primitives plain)
    {
        myBOOL.Shadow = plain.myBOOL;
        myBYTE.Shadow = plain.myBYTE;
        myWORD.Shadow = plain.myWORD;
        myDWORD.Shadow = plain.myDWORD;
        myLWORD.Shadow = plain.myLWORD;
        mySINT.Shadow = plain.mySINT;
        myINT.Shadow = plain.myINT;
        myDINT.Shadow = plain.myDINT;
        myLINT.Shadow = plain.myLINT;
        myUSINT.Shadow = plain.myUSINT;
        myUINT.Shadow = plain.myUINT;
        myUDINT.Shadow = plain.myUDINT;
        myULINT.Shadow = plain.myULINT;
        myREAL.Shadow = plain.myREAL;
        myLREAL.Shadow = plain.myLREAL;
        myTIME.Shadow = plain.myTIME;
        myLTIME.Shadow = plain.myLTIME;
        myDATE.Shadow = plain.myDATE;
        myTIME_OF_DAY.Shadow = plain.myTIME_OF_DAY;
        myDATE_AND_TIME.Shadow = plain.myDATE_AND_TIME;
        mySTRING.Shadow = plain.mySTRING;
        myWSTRING.Shadow = plain.myWSTRING;
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
    public async Task<bool> DetectsAnyChangeAsync(global::Pocos.class_all_primitives plain, global::Pocos.class_all_primitives latest = null)
    {
        if (latest == null)
            latest = await this._OnlineToPlainNoacAsync();
        var somethingChanged = false;
        return await Task.Run(async () =>
        {
            if (plain.myBOOL != myBOOL.LastValue)
                somethingChanged = true;
            if (plain.myBYTE != myBYTE.LastValue)
                somethingChanged = true;
            if (plain.myWORD != myWORD.LastValue)
                somethingChanged = true;
            if (plain.myDWORD != myDWORD.LastValue)
                somethingChanged = true;
            if (plain.myLWORD != myLWORD.LastValue)
                somethingChanged = true;
            if (plain.mySINT != mySINT.LastValue)
                somethingChanged = true;
            if (plain.myINT != myINT.LastValue)
                somethingChanged = true;
            if (plain.myDINT != myDINT.LastValue)
                somethingChanged = true;
            if (plain.myLINT != myLINT.LastValue)
                somethingChanged = true;
            if (plain.myUSINT != myUSINT.LastValue)
                somethingChanged = true;
            if (plain.myUINT != myUINT.LastValue)
                somethingChanged = true;
            if (plain.myUDINT != myUDINT.LastValue)
                somethingChanged = true;
            if (plain.myULINT != myULINT.LastValue)
                somethingChanged = true;
            if (plain.myREAL != myREAL.LastValue)
                somethingChanged = true;
            if (plain.myLREAL != myLREAL.LastValue)
                somethingChanged = true;
            if (plain.myTIME != myTIME.LastValue)
                somethingChanged = true;
            if (plain.myLTIME != myLTIME.LastValue)
                somethingChanged = true;
            if (plain.myDATE != myDATE.LastValue)
                somethingChanged = true;
            if (plain.myTIME_OF_DAY != myTIME_OF_DAY.LastValue)
                somethingChanged = true;
            if (plain.myDATE_AND_TIME != myDATE_AND_TIME.LastValue)
                somethingChanged = true;
            if (plain.mySTRING != mySTRING.LastValue)
                somethingChanged = true;
            if (plain.myWSTRING != myWSTRING.LastValue)
                somethingChanged = true;
            plain = latest;
            return somethingChanged;
        });
    }

    public void Poll()
    {
        this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
    }

    public global::Pocos.class_all_primitives CreateEmptyPoco()
    {
        return new global::Pocos.class_all_primitives();
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