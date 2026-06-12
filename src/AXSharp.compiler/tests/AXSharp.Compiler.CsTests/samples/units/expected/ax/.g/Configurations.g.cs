using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

public partial class unitsTwinController : ITwinController
{
    public AXSharp.Connector.Connector Connector { get; }
    public ComplexForConfig Complex { get; }
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
    public OnlinerDate myLDATE { get; }
    public OnlinerTimeOfDay myTIME_OF_DAY { get; }
    public OnlinerLTimeOfDay myLTIME_OF_DAY { get; }
    public OnlinerDateTime myDATE_AND_TIME { get; }
    public OnlinerLDateTime myLDATE_AND_TIME { get; }
    public OnlinerChar myCHAR { get; }
    public OnlinerWChar myWCHAR { get; }
    public OnlinerString mySTRING { get; }
    public OnlinerWString myWSTRING { get; }
    public OnlinerString mySTRING_10 { get; }
    public OnlinerWString myWSTRING_10 { get; }

    [ReadOnce()]
    public OnlinerWString myWSTRING_readOnce { get; }

    [ReadOnly()]
    public OnlinerWString myWSTRING_readOnly { get; }

    [ReadOnce()]
    public ComplexForConfig cReadOnce { get; }

    [ReadOnly()]
    public ComplexForConfig cReadOnly { get; }

    [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(Colorss))]
    public OnlinerInt Colorss { get; }
    public Colorss ColorssEnum { get => (Colorss)Colorss.LastValue; }

    [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(Colorsss))]
    public OnlinerULInt Colorsss { get; }
    public Colorsss ColorsssEnum { get => (Colorsss)Colorsss.LastValue; }

    [CompilerOmitsAttribute("POCO")]
    public OnlinerBool _must_be_omitted_in_poco { get; }

    [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(Colorss))]
    public OnlinerInt Colorss2 { get; }
    public Colorss Colorss2Enum { get => (Colorss)Colorss2.LastValue; }

    [AXSharp.Connector.EnumeratorDiscriminatorAttribute(typeof(Colorsss))]
    public OnlinerULInt Colorsss2 { get; }
    public Colorsss Colorsss2Enum { get => (Colorsss)Colorsss2.LastValue; }
    public OnlinerBool MotorOn { get; }
    public OnlinerInt MotorState { get; }
    public MixedAccessMotor Motor1 { get; }
    public MixedAccessMotor Motor2 { get; }
    public struct1 s1 { get; }
    public struct4 s4 { get; }
    public SpecificMotorA mot1 { get; }

    partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
    public unitsTwinController(AXSharp.Connector.ConnectorAdapter adapter, object[] parameters)
    {
        this.Connector = adapter.GetConnector(parameters);
        Complex = new ComplexForConfig(this.Connector, "", "Complex");
        myBOOL = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this.Connector, "", "myBOOL");
        myBYTE = @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(this.Connector, "", "myBYTE");
        myWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateWORD(this.Connector, "", "myWORD");
        myDWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateDWORD(this.Connector, "", "myDWORD");
        myLWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateLWORD(this.Connector, "", "myLWORD");
        mySINT = @Connector.ConnectorAdapter.AdapterFactory.CreateSINT(this.Connector, "", "mySINT");
        myINT = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "myINT");
        myDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateDINT(this.Connector, "", "myDINT");
        myLINT = @Connector.ConnectorAdapter.AdapterFactory.CreateLINT(this.Connector, "", "myLINT");
        myUSINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUSINT(this.Connector, "", "myUSINT");
        myUINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUINT(this.Connector, "", "myUINT");
        myUDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUDINT(this.Connector, "", "myUDINT");
        myULINT = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this.Connector, "", "myULINT");
        myREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this.Connector, "", "myREAL");
        myLREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this.Connector, "", "myLREAL");
        myTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME(this.Connector, "", "myTIME");
        myLTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME(this.Connector, "", "myLTIME");
        myDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this.Connector, "", "myDATE");
        myLDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE(this.Connector, "", "myLDATE");
        myTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this.Connector, "", "myTIME_OF_DAY");
        myLTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME_OF_DAY(this.Connector, "", "myLTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this.Connector, "", "myDATE_AND_TIME");
        myLDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE_AND_TIME(this.Connector, "", "myLDATE_AND_TIME");
        myCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateCHAR(this.Connector, "", "myCHAR");
        myWCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateWCHAR(this.Connector, "", "myWCHAR");
        mySTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this.Connector, "", "mySTRING");
        mySTRING.Capacity = 254;
        myWSTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING");
        myWSTRING.Capacity = 254;
        mySTRING_10 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this.Connector, "", "mySTRING_10");
        mySTRING_10.Capacity = 10;
        myWSTRING_10 = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING_10");
        myWSTRING_10.Capacity = 10;
        myWSTRING_readOnce = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING_readOnce");
        myWSTRING_readOnce.Capacity = 254;
        myWSTRING_readOnce.MakeReadOnce();
        myWSTRING_readOnly = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING_readOnly");
        myWSTRING_readOnly.Capacity = 254;
        myWSTRING_readOnly.MakeReadOnly();
        cReadOnce = new ComplexForConfig(this.Connector, "", "cReadOnce");
        cReadOnce.MakeReadOnce();
        cReadOnly = new ComplexForConfig(this.Connector, "", "cReadOnly");
        cReadOnly.MakeReadOnly();
        Colorss = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "Colorss");
        Colorsss = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Colorsss", "Colorsss");
        _must_be_omitted_in_poco = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this.Connector, "", "_must_be_omitted_in_poco");
        Colorss2 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "Colorss2");
        Colorsss2 = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Colorsss2", "Colorsss2");
        MotorOn = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this.Connector, "", "MotorOn");
        MotorState = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "MotorState");
        Motor1 = new MixedAccessMotor(this.Connector, "", "Motor1");
        Motor2 = new MixedAccessMotor(this.Connector, "", "Motor2");
        s1 = new struct1(this.Connector, "", "s1");
        s4 = new struct4(this.Connector, "", "s4");
        mot1 = new SpecificMotorA(this.Connector, "", "mot1");
    }

    public unitsTwinController(AXSharp.Connector.ConnectorAdapter adapter)
    {
        this.Connector = adapter.GetConnector(adapter.Parameters);
        Complex = new ComplexForConfig(this.Connector, "", "Complex");
        myBOOL = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this.Connector, "", "myBOOL");
        myBYTE = @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(this.Connector, "", "myBYTE");
        myWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateWORD(this.Connector, "", "myWORD");
        myDWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateDWORD(this.Connector, "", "myDWORD");
        myLWORD = @Connector.ConnectorAdapter.AdapterFactory.CreateLWORD(this.Connector, "", "myLWORD");
        mySINT = @Connector.ConnectorAdapter.AdapterFactory.CreateSINT(this.Connector, "", "mySINT");
        myINT = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "myINT");
        myDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateDINT(this.Connector, "", "myDINT");
        myLINT = @Connector.ConnectorAdapter.AdapterFactory.CreateLINT(this.Connector, "", "myLINT");
        myUSINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUSINT(this.Connector, "", "myUSINT");
        myUINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUINT(this.Connector, "", "myUINT");
        myUDINT = @Connector.ConnectorAdapter.AdapterFactory.CreateUDINT(this.Connector, "", "myUDINT");
        myULINT = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this.Connector, "", "myULINT");
        myREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateREAL(this.Connector, "", "myREAL");
        myLREAL = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this.Connector, "", "myLREAL");
        myTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME(this.Connector, "", "myTIME");
        myLTIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME(this.Connector, "", "myLTIME");
        myDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this.Connector, "", "myDATE");
        myLDATE = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE(this.Connector, "", "myLDATE");
        myTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this.Connector, "", "myTIME_OF_DAY");
        myLTIME_OF_DAY = @Connector.ConnectorAdapter.AdapterFactory.CreateLTIME_OF_DAY(this.Connector, "", "myLTIME_OF_DAY");
        myDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this.Connector, "", "myDATE_AND_TIME");
        myLDATE_AND_TIME = @Connector.ConnectorAdapter.AdapterFactory.CreateLDATE_AND_TIME(this.Connector, "", "myLDATE_AND_TIME");
        myCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateCHAR(this.Connector, "", "myCHAR");
        myWCHAR = @Connector.ConnectorAdapter.AdapterFactory.CreateWCHAR(this.Connector, "", "myWCHAR");
        mySTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this.Connector, "", "mySTRING");
        mySTRING.Capacity = 254;
        myWSTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING");
        myWSTRING.Capacity = 254;
        mySTRING_10 = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this.Connector, "", "mySTRING_10");
        mySTRING_10.Capacity = 10;
        myWSTRING_10 = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING_10");
        myWSTRING_10.Capacity = 10;
        myWSTRING_readOnce = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING_readOnce");
        myWSTRING_readOnce.Capacity = 254;
        myWSTRING_readOnce.MakeReadOnce();
        myWSTRING_readOnly = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this.Connector, "", "myWSTRING_readOnly");
        myWSTRING_readOnly.Capacity = 254;
        myWSTRING_readOnly.MakeReadOnly();
        cReadOnce = new ComplexForConfig(this.Connector, "", "cReadOnce");
        cReadOnce.MakeReadOnce();
        cReadOnly = new ComplexForConfig(this.Connector, "", "cReadOnly");
        cReadOnly.MakeReadOnly();
        Colorss = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "Colorss");
        Colorsss = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Colorsss", "Colorsss");
        _must_be_omitted_in_poco = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this.Connector, "", "_must_be_omitted_in_poco");
        Colorss2 = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "Colorss2");
        Colorsss2 = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Colorsss2", "Colorsss2");
        MotorOn = @Connector.ConnectorAdapter.AdapterFactory.CreateBOOL(this.Connector, "", "MotorOn");
        MotorState = @Connector.ConnectorAdapter.AdapterFactory.CreateINT(this.Connector, "", "MotorState");
        Motor1 = new MixedAccessMotor(this.Connector, "", "Motor1");
        Motor2 = new MixedAccessMotor(this.Connector, "", "Motor2");
        s1 = new struct1(this.Connector, "", "s1");
        s4 = new struct4(this.Connector, "", "s4");
        mot1 = new SpecificMotorA(this.Connector, "", "mot1");
    }
}