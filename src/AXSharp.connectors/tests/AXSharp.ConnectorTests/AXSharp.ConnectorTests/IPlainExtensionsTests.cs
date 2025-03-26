using AXSharp.Connector.Localizations;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.ConnectorTests
{
    using AXSharp.Connector;
    using T = MyPlain;
    using System;
    using Xunit;
    using NSubstitute;
    using System.Threading.Tasks;
    using System.Globalization;

    public class MyPlain : IPlain
    {
        public string SomeString { get; set; }
    }

    namespace Pocos
    {
        public partial class all_primitives : AXSharp.Connector.IPlain
        {
            public Boolean myBOOL { get; set; }

            public Byte myBYTE { get; set; }

            public UInt16 myWORD { get; set; }

            public UInt32 myDWORD { get; set; }

            public UInt64 myLWORD { get; set; }

            public SByte mySINT { get; set; }

            public Int16 myINT { get; set; }

            public Int32 myDINT { get; set; }

            public Int64 myLINT { get; set; }

            public Byte myUSINT { get; set; }

            public UInt16 myUINT { get; set; }

            public UInt32 myUDINT { get; set; }

            public UInt64 myULINT { get; set; }

            public Single myREAL { get; set; }

            public Double myLREAL { get; set; }

            public TimeSpan myTIME { get; set; } = default(TimeSpan);
            public TimeSpan myLTIME { get; set; } = default(TimeSpan);
            public DateOnly myDATE { get; set; } = default(DateOnly);
            public TimeSpan myTIME_OF_DAY { get; set; } = default(TimeSpan);
            public DateTime myDATE_AND_TIME { get; set; } = default(DateTime);
            public string mySTRING { get; set; } = string.Empty;
            public string myWSTRING { get; set; } = string.Empty;
        }
    }

    public partial class all_primitives : AXSharp.Connector.ITwinObject
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
        public all_primitives(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
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
            myWSTRING = @Connector.ConnectorAdapter.AdapterFactory.CreateWSTRING(this, "myWSTRING", "myWSTRING");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public string GetAttributeName(CultureInfo culture)
        {
            return this.Translate(this.AttributeName, culture);
        }

        public string GetHumanReadable(CultureInfo culture)
        {
            return this.Translate(this.HumanReadable, culture);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.all_primitives> OnlineToPlainAsync()
        {
            Pocos.all_primitives plain = new Pocos.all_primitives();
            await this.ReadAsync();
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

        protected async Task<Pocos.all_primitives> OnlineToPlainAsync(Pocos.all_primitives plain)
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

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.all_primitives plain)
        {
            myBOOL.Cyclic = plain.myBOOL;
            myBYTE.Cyclic = plain.myBYTE;
            myWORD.Cyclic = plain.myWORD;
            myDWORD.Cyclic = plain.myDWORD;
            myLWORD.Cyclic = plain.myLWORD;
            mySINT.Cyclic = plain.mySINT;
            myINT.Cyclic = plain.myINT;
            myDINT.Cyclic = plain.myDINT;
            myLINT.Cyclic = plain.myLINT;
            myUSINT.Cyclic = plain.myUSINT;
            myUINT.Cyclic = plain.myUINT;
            myUDINT.Cyclic = plain.myUDINT;
            myULINT.Cyclic = plain.myULINT;
            myREAL.Cyclic = plain.myREAL;
            myLREAL.Cyclic = plain.myLREAL;
            myTIME.Cyclic = plain.myTIME;
            myLTIME.Cyclic = plain.myLTIME;
            myDATE.Cyclic = plain.myDATE;
            myTIME_OF_DAY.Cyclic = plain.myTIME_OF_DAY;
            myDATE_AND_TIME.Cyclic = plain.myDATE_AND_TIME;
            mySTRING.Cyclic = plain.mySTRING;
            myWSTRING.Cyclic = plain.myWSTRING;
            return await this.WriteAsync();
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.all_primitives> ShadowToPlainAsync()
        {
            Pocos.all_primitives plain = new Pocos.all_primitives();
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

        protected async Task<Pocos.all_primitives> ShadowToPlainAsync(Pocos.all_primitives plain)
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

        public Task<bool> AnyChangeAsync<T1>(T1 plain)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.all_primitives plain)
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

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.all_primitives CreateEmptyPoco()
        {
            return new Pocos.all_primitives();
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
        public System.String AttributeName { get => string.IsNullOrEmpty(_attributeName) ? SymbolTail : _attributeName.Interpolate(this); set => _attributeName = value; }

        public string HumanReadable { get; set; }

        protected System.String @SymbolTail { get; set; }

        protected AXSharp.Connector.ITwinObject @Parent { get; set; }

        public AXSharp.Connector.Localizations.Translator Interpreter => new Translator();
    }

    public partial class ix_integration_plcTwinController : ITwinController
    {
        public AXSharp.Connector.Connector Connector { get; }

        public all_primitives all_primitives { get; }

      

        public ix_integration_plcTwinController(AXSharp.Connector.ConnectorAdapter adapter, object[] parameters)
        {
            this.Connector = adapter.GetConnector(parameters);
            all_primitives = new all_primitives(this.Connector, "", "all_primitives");
        }

        public ix_integration_plcTwinController(AXSharp.Connector.ConnectorAdapter adapter)
        {
            this.Connector = adapter.GetConnector(adapter.Parameters);
            all_primitives = new all_primitives(this.Connector, "", "all_primitives");
        }
    }

    public static class IPlainExtensionsTests
    {
        [Fact]
        public static void CanCallToShadow()
        {
            // Arrange
            var connector = new ix_integration_plcTwinController(ConnectorAdapterBuilder.Build().CreateDummy());
            var plain = new Pocos.all_primitives();
            plain.mySTRING = "hello";
            var twin = connector.all_primitives;
            

            // Act
            plain.ToShadow<Pocos.all_primitives>(twin);

            //// Assert
            Assert.Equal(plain.mySTRING, twin.mySTRING.Shadow);
        }

      
        [Fact]
        public static async Task CanCallFromShadow()
        {
            // Arrange
            var connector = new ix_integration_plcTwinController(ConnectorAdapterBuilder.Build().CreateDummy());
            var plain = new Pocos.all_primitives();
            var twin = connector.all_primitives;
            twin.mySTRING.Shadow = "hey";

            // Act
            var result = await plain.FromShadow<Pocos.all_primitives>(twin);

            //// Assert
            Assert.Equal(result.mySTRING, twin.mySTRING.Shadow);
        }


        [Fact]
        public static async Task CanCallToOnline()
        {
            // Arrange
            var connector = new ix_integration_plcTwinController(ConnectorAdapterBuilder.Build().CreateDummy());
            var plain = new Pocos.all_primitives();
            var twin = connector.all_primitives;
            plain.mySTRING = "hey";

            // Act
            await plain.ToOnline(twin);

            // Assert
            Assert.Equal(plain.mySTRING, twin.mySTRING.Cyclic);
        }

        
        [Fact]
        public static async Task CanCallFromOnline()
        {
            // Arrange
            var connector = new ix_integration_plcTwinController(ConnectorAdapterBuilder.Build().CreateDummy());
            var plain = new Pocos.all_primitives();
            var twin = connector.all_primitives;
            plain.mySTRING = "hey";

            // Act
            var result = await plain.FromOnline<Pocos.all_primitives>(twin);

            // Assert
            Assert.Equal(result.mySTRING, twin.mySTRING.Cyclic);
        }
    }
}