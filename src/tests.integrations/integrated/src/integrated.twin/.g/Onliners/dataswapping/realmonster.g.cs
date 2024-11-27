using System;
using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using System.Collections.Generic;
using AXSharp.Connector.Localizations;
using AXSharp.Abstractions.Presentation;

namespace RealMonsterData
{
    public partial class RealMonsterBase : AXSharp.Connector.ITwinObject
    {
        public OnlinerString Description { get; }

        public OnlinerULInt Id { get; }

        public OnlinerDate TestDate { get; }

        public OnlinerDateTime TestDateTime { get; }

        public OnlinerTimeOfDay TestTimeSpan { get; }

        public OnlinerByte[] ArrayOfBytes { get; }

        public RealMonsterData.DriveBaseNested[] ArrayOfDrives { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public RealMonsterBase(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            Description = @Connector.ConnectorAdapter.AdapterFactory.CreateSTRING(this, "Description", "Description");
            Id = @Connector.ConnectorAdapter.AdapterFactory.CreateULINT(this, "Id", "Id");
            TestDate = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE(this, "TestDate", "TestDate");
            TestDateTime = @Connector.ConnectorAdapter.AdapterFactory.CreateDATE_AND_TIME(this, "TestDateTime", "TestDateTime");
            TestTimeSpan = @Connector.ConnectorAdapter.AdapterFactory.CreateTIME_OF_DAY(this, "TestTimeSpan", "TestTimeSpan");
            ArrayOfBytes = new OnlinerByte[4];
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfBytes, this, "ArrayOfBytes", "ArrayOfBytes", (p, rt, st) => @Connector.ConnectorAdapter.AdapterFactory.CreateBYTE(p, rt, st), new[] { (0, 3) });
            ArrayOfDrives = new RealMonsterData.DriveBaseNested[4];
            AXSharp.Connector.BuilderHelpers.Arrays.InstantiateArray(ArrayOfDrives, this, "ArrayOfDrives", "ArrayOfDrives", (p, rt, st) => new RealMonsterData.DriveBaseNested(p, rt, st), new[] { (0, 3) });
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.RealMonsterData.RealMonsterBase> OnlineToPlainAsync()
        {
            Pocos.RealMonsterData.RealMonsterBase plain = new Pocos.RealMonsterData.RealMonsterBase();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.Description = Description.LastValue;
            plain.Id = Id.LastValue;
            plain.TestDate = TestDate.LastValue;
            plain.TestDateTime = TestDateTime.LastValue;
            plain.TestTimeSpan = TestTimeSpan.LastValue;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.LastValue).ToArray();
#pragma warning disable CS0612
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p._OnlineToPlainNoacAsync()).Select(p => p.Result).ToArray();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.RealMonsterData.RealMonsterBase> _OnlineToPlainNoacAsync()
        {
            Pocos.RealMonsterData.RealMonsterBase plain = new Pocos.RealMonsterData.RealMonsterBase();
            plain.Description = Description.LastValue;
            plain.Id = Id.LastValue;
            plain.TestDate = TestDate.LastValue;
            plain.TestDateTime = TestDateTime.LastValue;
            plain.TestTimeSpan = TestTimeSpan.LastValue;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.LastValue).ToArray();
#pragma warning disable CS0612
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p._OnlineToPlainNoacAsync()).Select(p => p.Result).ToArray();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.RealMonsterData.RealMonsterBase> _OnlineToPlainNoacAsync(Pocos.RealMonsterData.RealMonsterBase plain)
        {
            plain.Description = Description.LastValue;
            plain.Id = Id.LastValue;
            plain.TestDate = TestDate.LastValue;
            plain.TestDateTime = TestDateTime.LastValue;
            plain.TestTimeSpan = TestTimeSpan.LastValue;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.LastValue).ToArray();
#pragma warning disable CS0612
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p._OnlineToPlainNoacAsync()).Select(p => p.Result).ToArray();
#pragma warning restore CS0612
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RealMonsterData.RealMonsterBase plain)
        {
#pragma warning disable CS0612
            Description.LethargicWrite(plain.Description);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Id.LethargicWrite(plain.Id);
#pragma warning restore CS0612
#pragma warning disable CS0612
            TestDate.LethargicWrite(plain.TestDate);
#pragma warning restore CS0612
#pragma warning disable CS0612
            TestDateTime.LethargicWrite(plain.TestDateTime);
#pragma warning restore CS0612
#pragma warning disable CS0612
            TestTimeSpan.LethargicWrite(plain.TestTimeSpan);
#pragma warning restore CS0612
            var _ArrayOfBytes_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            ArrayOfBytes.Select(p => p.LethargicWrite(plain.ArrayOfBytes[_ArrayOfBytes_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
            var _ArrayOfDrives_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            ArrayOfDrives.Select(p => p._PlainToOnlineNoacAsync(plain.ArrayOfDrives[_ArrayOfDrives_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.RealMonsterData.RealMonsterBase plain)
        {
#pragma warning disable CS0612
            Description.LethargicWrite(plain.Description);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Id.LethargicWrite(plain.Id);
#pragma warning restore CS0612
#pragma warning disable CS0612
            TestDate.LethargicWrite(plain.TestDate);
#pragma warning restore CS0612
#pragma warning disable CS0612
            TestDateTime.LethargicWrite(plain.TestDateTime);
#pragma warning restore CS0612
#pragma warning disable CS0612
            TestTimeSpan.LethargicWrite(plain.TestTimeSpan);
#pragma warning restore CS0612
            var _ArrayOfBytes_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            ArrayOfBytes.Select(p => p.LethargicWrite(plain.ArrayOfBytes[_ArrayOfBytes_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
            var _ArrayOfDrives_i_FE8484DAB3 = 0;
#pragma warning disable CS0612
            ArrayOfDrives.Select(p => p._PlainToOnlineNoacAsync(plain.ArrayOfDrives[_ArrayOfDrives_i_FE8484DAB3++])).ToArray();
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.RealMonsterData.RealMonsterBase> ShadowToPlainAsync()
        {
            Pocos.RealMonsterData.RealMonsterBase plain = new Pocos.RealMonsterData.RealMonsterBase();
            plain.Description = Description.Shadow;
            plain.Id = Id.Shadow;
            plain.TestDate = TestDate.Shadow;
            plain.TestDateTime = TestDateTime.Shadow;
            plain.TestTimeSpan = TestTimeSpan.Shadow;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.Shadow).ToArray();
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        protected async Task<Pocos.RealMonsterData.RealMonsterBase> ShadowToPlainAsync(Pocos.RealMonsterData.RealMonsterBase plain)
        {
            plain.Description = Description.Shadow;
            plain.Id = Id.Shadow;
            plain.TestDate = TestDate.Shadow;
            plain.TestDateTime = TestDateTime.Shadow;
            plain.TestTimeSpan = TestTimeSpan.Shadow;
            plain.ArrayOfBytes = ArrayOfBytes.Select(p => p.Shadow).ToArray();
            plain.ArrayOfDrives = ArrayOfDrives.Select(async p => await p.ShadowToPlainAsync()).Select(p => p.Result).ToArray();
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.RealMonsterData.RealMonsterBase plain)
        {
            Description.Shadow = plain.Description;
            Id.Shadow = plain.Id;
            TestDate.Shadow = plain.TestDate;
            TestDateTime.Shadow = plain.TestDateTime;
            TestTimeSpan.Shadow = plain.TestTimeSpan;
            var _ArrayOfBytes_i_FE8484DAB3 = 0;
            ArrayOfBytes.Select(p => p.Shadow = plain.ArrayOfBytes[_ArrayOfBytes_i_FE8484DAB3++]).ToArray();
            var _ArrayOfDrives_i_FE8484DAB3 = 0;
            ArrayOfDrives.Select(p => p.PlainToShadowAsync(plain.ArrayOfDrives[_ArrayOfDrives_i_FE8484DAB3++])).ToArray();
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
        public async Task<bool> DetectsAnyChangeAsync(Pocos.RealMonsterData.RealMonsterBase plain, Pocos.RealMonsterData.RealMonsterBase latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.Description != Description.LastValue)
                    somethingChanged = true;
                if (plain.Id != Id.LastValue)
                    somethingChanged = true;
                if (plain.TestDate != TestDate.LastValue)
                    somethingChanged = true;
                if (plain.TestDateTime != TestDateTime.LastValue)
                    somethingChanged = true;
                if (plain.TestTimeSpan != TestTimeSpan.LastValue)
                    somethingChanged = true;
                for (int i760901_3001_mimi = 0; i760901_3001_mimi < latest.ArrayOfBytes.Length; i760901_3001_mimi++)
                {
                    if (latest.ArrayOfBytes.ElementAt(i760901_3001_mimi) != plain.ArrayOfBytes[i760901_3001_mimi])
                        somethingChanged = true;
                }

                for (int i760901_3001_mimi = 0; i760901_3001_mimi < latest.ArrayOfDrives.Length; i760901_3001_mimi++)
                {
                    if (await ArrayOfDrives.ElementAt(i760901_3001_mimi).DetectsAnyChangeAsync(plain.ArrayOfDrives[i760901_3001_mimi], latest.ArrayOfDrives[i760901_3001_mimi]))
                        somethingChanged = true;
                }

                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.RealMonsterData.RealMonsterBase CreateEmptyPoco()
        {
            return new Pocos.RealMonsterData.RealMonsterBase();
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

    public partial class RealMonster : RealMonsterData.RealMonsterBase
    {
        public RealMonsterData.DriveBaseNested DriveA { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public RealMonster(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail) : base(parent, readableTail, symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            PreConstruct(parent, readableTail, symbolTail);
            DriveA = new RealMonsterData.DriveBaseNested(this, "DriveA", "DriveA");
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async override Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public new async Task<Pocos.RealMonsterData.RealMonster> OnlineToPlainAsync()
        {
            Pocos.RealMonsterData.RealMonster plain = new Pocos.RealMonsterData.RealMonster();
            await this.ReadAsync<IgnoreOnPocoOperation>();
#pragma warning disable CS0612
            await base._OnlineToPlainNoacAsync(plain);
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain.DriveA = await DriveA._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public new async Task<Pocos.RealMonsterData.RealMonster> _OnlineToPlainNoacAsync()
        {
            Pocos.RealMonsterData.RealMonster plain = new Pocos.RealMonsterData.RealMonster();
#pragma warning disable CS0612
            await base._OnlineToPlainNoacAsync(plain);
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain.DriveA = await DriveA._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.RealMonsterData.RealMonster> _OnlineToPlainNoacAsync(Pocos.RealMonsterData.RealMonster plain)
        {
#pragma warning disable CS0612
            await base._OnlineToPlainNoacAsync(plain);
#pragma warning restore CS0612
#pragma warning disable CS0612
            plain.DriveA = await DriveA._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        public async override Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RealMonsterData.RealMonster plain)
        {
            await base._PlainToOnlineNoacAsync(plain);
#pragma warning disable CS0612
            await this.DriveA._PlainToOnlineNoacAsync(plain.DriveA);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.RealMonsterData.RealMonster plain)
        {
            await base._PlainToOnlineNoacAsync(plain);
#pragma warning disable CS0612
            await this.DriveA._PlainToOnlineNoacAsync(plain.DriveA);
#pragma warning restore CS0612
        }

        public async override Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public new async Task<Pocos.RealMonsterData.RealMonster> ShadowToPlainAsync()
        {
            Pocos.RealMonsterData.RealMonster plain = new Pocos.RealMonsterData.RealMonster();
            await base.ShadowToPlainAsync(plain);
            plain.DriveA = await DriveA.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.RealMonsterData.RealMonster> ShadowToPlainAsync(Pocos.RealMonsterData.RealMonster plain)
        {
            await base.ShadowToPlainAsync(plain);
            plain.DriveA = await DriveA.ShadowToPlainAsync();
            return plain;
        }

        public async override Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.RealMonsterData.RealMonster plain)
        {
            await base.PlainToShadowAsync(plain);
            await this.DriveA.PlainToShadowAsync(plain.DriveA);
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
        public new async Task<bool> DetectsAnyChangeAsync(Pocos.RealMonsterData.RealMonster plain, Pocos.RealMonsterData.RealMonster latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (await base.DetectsAnyChangeAsync(plain))
                    return true;
                if (await DriveA.DetectsAnyChangeAsync(plain.DriveA, latest.DriveA))
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public new void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public new Pocos.RealMonsterData.RealMonster CreateEmptyPoco()
        {
            return new Pocos.RealMonsterData.RealMonster();
        }
    }

    public partial class DriveBaseNested : AXSharp.Connector.ITwinObject
    {
        public OnlinerLReal Position { get; }

        public OnlinerLReal Velo { get; }

        public OnlinerLReal Acc { get; }

        public OnlinerLReal Dcc { get; }

        public RealMonsterData.NestedLevelOne NestedLevelOne { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public DriveBaseNested(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            Position = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Position", "Position");
            Velo = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Velo", "Velo");
            Acc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Acc", "Acc");
            Dcc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Dcc", "Dcc");
            NestedLevelOne = new RealMonsterData.NestedLevelOne(this, "NestedLevelOne", "NestedLevelOne");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.RealMonsterData.DriveBaseNested> OnlineToPlainAsync()
        {
            Pocos.RealMonsterData.DriveBaseNested plain = new Pocos.RealMonsterData.DriveBaseNested();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
#pragma warning disable CS0612
            plain.NestedLevelOne = await NestedLevelOne._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.RealMonsterData.DriveBaseNested> _OnlineToPlainNoacAsync()
        {
            Pocos.RealMonsterData.DriveBaseNested plain = new Pocos.RealMonsterData.DriveBaseNested();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
#pragma warning disable CS0612
            plain.NestedLevelOne = await NestedLevelOne._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.RealMonsterData.DriveBaseNested> _OnlineToPlainNoacAsync(Pocos.RealMonsterData.DriveBaseNested plain)
        {
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
#pragma warning disable CS0612
            plain.NestedLevelOne = await NestedLevelOne._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RealMonsterData.DriveBaseNested plain)
        {
#pragma warning disable CS0612
            Position.LethargicWrite(plain.Position);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Velo.LethargicWrite(plain.Velo);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Acc.LethargicWrite(plain.Acc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Dcc.LethargicWrite(plain.Dcc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this.NestedLevelOne._PlainToOnlineNoacAsync(plain.NestedLevelOne);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.RealMonsterData.DriveBaseNested plain)
        {
#pragma warning disable CS0612
            Position.LethargicWrite(plain.Position);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Velo.LethargicWrite(plain.Velo);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Acc.LethargicWrite(plain.Acc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Dcc.LethargicWrite(plain.Dcc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this.NestedLevelOne._PlainToOnlineNoacAsync(plain.NestedLevelOne);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.RealMonsterData.DriveBaseNested> ShadowToPlainAsync()
        {
            Pocos.RealMonsterData.DriveBaseNested plain = new Pocos.RealMonsterData.DriveBaseNested();
            plain.Position = Position.Shadow;
            plain.Velo = Velo.Shadow;
            plain.Acc = Acc.Shadow;
            plain.Dcc = Dcc.Shadow;
            plain.NestedLevelOne = await NestedLevelOne.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.RealMonsterData.DriveBaseNested> ShadowToPlainAsync(Pocos.RealMonsterData.DriveBaseNested plain)
        {
            plain.Position = Position.Shadow;
            plain.Velo = Velo.Shadow;
            plain.Acc = Acc.Shadow;
            plain.Dcc = Dcc.Shadow;
            plain.NestedLevelOne = await NestedLevelOne.ShadowToPlainAsync();
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.RealMonsterData.DriveBaseNested plain)
        {
            Position.Shadow = plain.Position;
            Velo.Shadow = plain.Velo;
            Acc.Shadow = plain.Acc;
            Dcc.Shadow = plain.Dcc;
            await this.NestedLevelOne.PlainToShadowAsync(plain.NestedLevelOne);
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
        public async Task<bool> DetectsAnyChangeAsync(Pocos.RealMonsterData.DriveBaseNested plain, Pocos.RealMonsterData.DriveBaseNested latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.Position != Position.LastValue)
                    somethingChanged = true;
                if (plain.Velo != Velo.LastValue)
                    somethingChanged = true;
                if (plain.Acc != Acc.LastValue)
                    somethingChanged = true;
                if (plain.Dcc != Dcc.LastValue)
                    somethingChanged = true;
                if (await NestedLevelOne.DetectsAnyChangeAsync(plain.NestedLevelOne, latest.NestedLevelOne))
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.RealMonsterData.DriveBaseNested CreateEmptyPoco()
        {
            return new Pocos.RealMonsterData.DriveBaseNested();
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

    public partial class NestedLevelOne : AXSharp.Connector.ITwinObject
    {
        public OnlinerLReal Position { get; }

        public OnlinerLReal Velo { get; }

        public OnlinerLReal Acc { get; }

        public OnlinerLReal Dcc { get; }

        public RealMonsterData.NestedLevelTwo NestedLevelTwo { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public NestedLevelOne(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            Position = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Position", "Position");
            Velo = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Velo", "Velo");
            Acc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Acc", "Acc");
            Dcc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Dcc", "Dcc");
            NestedLevelTwo = new RealMonsterData.NestedLevelTwo(this, "NestedLevelTwo", "NestedLevelTwo");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.RealMonsterData.NestedLevelOne> OnlineToPlainAsync()
        {
            Pocos.RealMonsterData.NestedLevelOne plain = new Pocos.RealMonsterData.NestedLevelOne();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
#pragma warning disable CS0612
            plain.NestedLevelTwo = await NestedLevelTwo._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.RealMonsterData.NestedLevelOne> _OnlineToPlainNoacAsync()
        {
            Pocos.RealMonsterData.NestedLevelOne plain = new Pocos.RealMonsterData.NestedLevelOne();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
#pragma warning disable CS0612
            plain.NestedLevelTwo = await NestedLevelTwo._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.RealMonsterData.NestedLevelOne> _OnlineToPlainNoacAsync(Pocos.RealMonsterData.NestedLevelOne plain)
        {
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
#pragma warning disable CS0612
            plain.NestedLevelTwo = await NestedLevelTwo._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RealMonsterData.NestedLevelOne plain)
        {
#pragma warning disable CS0612
            Position.LethargicWrite(plain.Position);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Velo.LethargicWrite(plain.Velo);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Acc.LethargicWrite(plain.Acc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Dcc.LethargicWrite(plain.Dcc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this.NestedLevelTwo._PlainToOnlineNoacAsync(plain.NestedLevelTwo);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.RealMonsterData.NestedLevelOne plain)
        {
#pragma warning disable CS0612
            Position.LethargicWrite(plain.Position);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Velo.LethargicWrite(plain.Velo);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Acc.LethargicWrite(plain.Acc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Dcc.LethargicWrite(plain.Dcc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this.NestedLevelTwo._PlainToOnlineNoacAsync(plain.NestedLevelTwo);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.RealMonsterData.NestedLevelOne> ShadowToPlainAsync()
        {
            Pocos.RealMonsterData.NestedLevelOne plain = new Pocos.RealMonsterData.NestedLevelOne();
            plain.Position = Position.Shadow;
            plain.Velo = Velo.Shadow;
            plain.Acc = Acc.Shadow;
            plain.Dcc = Dcc.Shadow;
            plain.NestedLevelTwo = await NestedLevelTwo.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.RealMonsterData.NestedLevelOne> ShadowToPlainAsync(Pocos.RealMonsterData.NestedLevelOne plain)
        {
            plain.Position = Position.Shadow;
            plain.Velo = Velo.Shadow;
            plain.Acc = Acc.Shadow;
            plain.Dcc = Dcc.Shadow;
            plain.NestedLevelTwo = await NestedLevelTwo.ShadowToPlainAsync();
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.RealMonsterData.NestedLevelOne plain)
        {
            Position.Shadow = plain.Position;
            Velo.Shadow = plain.Velo;
            Acc.Shadow = plain.Acc;
            Dcc.Shadow = plain.Dcc;
            await this.NestedLevelTwo.PlainToShadowAsync(plain.NestedLevelTwo);
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
        public async Task<bool> DetectsAnyChangeAsync(Pocos.RealMonsterData.NestedLevelOne plain, Pocos.RealMonsterData.NestedLevelOne latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.Position != Position.LastValue)
                    somethingChanged = true;
                if (plain.Velo != Velo.LastValue)
                    somethingChanged = true;
                if (plain.Acc != Acc.LastValue)
                    somethingChanged = true;
                if (plain.Dcc != Dcc.LastValue)
                    somethingChanged = true;
                if (await NestedLevelTwo.DetectsAnyChangeAsync(plain.NestedLevelTwo, latest.NestedLevelTwo))
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.RealMonsterData.NestedLevelOne CreateEmptyPoco()
        {
            return new Pocos.RealMonsterData.NestedLevelOne();
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

    public partial class NestedLevelTwo : AXSharp.Connector.ITwinObject
    {
        public OnlinerLReal Position { get; }

        public OnlinerLReal Velo { get; }

        public OnlinerLReal Acc { get; }

        public OnlinerLReal Dcc { get; }

        public RealMonsterData.NestedLevelThree NestedLevelThree { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public NestedLevelTwo(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            Position = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Position", "Position");
            Velo = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Velo", "Velo");
            Acc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Acc", "Acc");
            Dcc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Dcc", "Dcc");
            NestedLevelThree = new RealMonsterData.NestedLevelThree(this, "NestedLevelThree", "NestedLevelThree");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.RealMonsterData.NestedLevelTwo> OnlineToPlainAsync()
        {
            Pocos.RealMonsterData.NestedLevelTwo plain = new Pocos.RealMonsterData.NestedLevelTwo();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
#pragma warning disable CS0612
            plain.NestedLevelThree = await NestedLevelThree._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.RealMonsterData.NestedLevelTwo> _OnlineToPlainNoacAsync()
        {
            Pocos.RealMonsterData.NestedLevelTwo plain = new Pocos.RealMonsterData.NestedLevelTwo();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
#pragma warning disable CS0612
            plain.NestedLevelThree = await NestedLevelThree._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.RealMonsterData.NestedLevelTwo> _OnlineToPlainNoacAsync(Pocos.RealMonsterData.NestedLevelTwo plain)
        {
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
#pragma warning disable CS0612
            plain.NestedLevelThree = await NestedLevelThree._OnlineToPlainNoacAsync();
#pragma warning restore CS0612
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RealMonsterData.NestedLevelTwo plain)
        {
#pragma warning disable CS0612
            Position.LethargicWrite(plain.Position);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Velo.LethargicWrite(plain.Velo);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Acc.LethargicWrite(plain.Acc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Dcc.LethargicWrite(plain.Dcc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this.NestedLevelThree._PlainToOnlineNoacAsync(plain.NestedLevelThree);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.RealMonsterData.NestedLevelTwo plain)
        {
#pragma warning disable CS0612
            Position.LethargicWrite(plain.Position);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Velo.LethargicWrite(plain.Velo);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Acc.LethargicWrite(plain.Acc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Dcc.LethargicWrite(plain.Dcc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            await this.NestedLevelThree._PlainToOnlineNoacAsync(plain.NestedLevelThree);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.RealMonsterData.NestedLevelTwo> ShadowToPlainAsync()
        {
            Pocos.RealMonsterData.NestedLevelTwo plain = new Pocos.RealMonsterData.NestedLevelTwo();
            plain.Position = Position.Shadow;
            plain.Velo = Velo.Shadow;
            plain.Acc = Acc.Shadow;
            plain.Dcc = Dcc.Shadow;
            plain.NestedLevelThree = await NestedLevelThree.ShadowToPlainAsync();
            return plain;
        }

        protected async Task<Pocos.RealMonsterData.NestedLevelTwo> ShadowToPlainAsync(Pocos.RealMonsterData.NestedLevelTwo plain)
        {
            plain.Position = Position.Shadow;
            plain.Velo = Velo.Shadow;
            plain.Acc = Acc.Shadow;
            plain.Dcc = Dcc.Shadow;
            plain.NestedLevelThree = await NestedLevelThree.ShadowToPlainAsync();
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.RealMonsterData.NestedLevelTwo plain)
        {
            Position.Shadow = plain.Position;
            Velo.Shadow = plain.Velo;
            Acc.Shadow = plain.Acc;
            Dcc.Shadow = plain.Dcc;
            await this.NestedLevelThree.PlainToShadowAsync(plain.NestedLevelThree);
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
        public async Task<bool> DetectsAnyChangeAsync(Pocos.RealMonsterData.NestedLevelTwo plain, Pocos.RealMonsterData.NestedLevelTwo latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.Position != Position.LastValue)
                    somethingChanged = true;
                if (plain.Velo != Velo.LastValue)
                    somethingChanged = true;
                if (plain.Acc != Acc.LastValue)
                    somethingChanged = true;
                if (plain.Dcc != Dcc.LastValue)
                    somethingChanged = true;
                if (await NestedLevelThree.DetectsAnyChangeAsync(plain.NestedLevelThree, latest.NestedLevelThree))
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.RealMonsterData.NestedLevelTwo CreateEmptyPoco()
        {
            return new Pocos.RealMonsterData.NestedLevelTwo();
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

    public partial class NestedLevelThree : AXSharp.Connector.ITwinObject
    {
        public OnlinerLReal Position { get; }

        public OnlinerLReal Velo { get; }

        public OnlinerLReal Acc { get; }

        public OnlinerLReal Dcc { get; }

        partial void PreConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        partial void PostConstruct(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail);
        public NestedLevelThree(AXSharp.Connector.ITwinObject parent, string readableTail, string symbolTail)
        {
            Symbol = AXSharp.Connector.Connector.CreateSymbol(parent.Symbol, symbolTail);
            this.@SymbolTail = symbolTail;
            this.@Connector = parent.GetConnector();
            this.@Parent = parent;
            HumanReadable = AXSharp.Connector.Connector.CreateHumanReadable(parent.HumanReadable, readableTail);
            PreConstruct(parent, readableTail, symbolTail);
            Position = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Position", "Position");
            Velo = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Velo", "Velo");
            Acc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Acc", "Acc");
            Dcc = @Connector.ConnectorAdapter.AdapterFactory.CreateLREAL(this, "Dcc", "Dcc");
            parent.AddChild(this);
            parent.AddKid(this);
            PostConstruct(parent, readableTail, symbolTail);
        }

        public async virtual Task<T> OnlineToPlain<T>()
        {
            return await (dynamic)this.OnlineToPlainAsync();
        }

        public async Task<Pocos.RealMonsterData.NestedLevelThree> OnlineToPlainAsync()
        {
            Pocos.RealMonsterData.NestedLevelThree plain = new Pocos.RealMonsterData.NestedLevelThree();
            await this.ReadAsync<IgnoreOnPocoOperation>();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task<Pocos.RealMonsterData.NestedLevelThree> _OnlineToPlainNoacAsync()
        {
            Pocos.RealMonsterData.NestedLevelThree plain = new Pocos.RealMonsterData.NestedLevelThree();
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            return plain;
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `OnlineToPlain` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        protected async Task<Pocos.RealMonsterData.NestedLevelThree> _OnlineToPlainNoacAsync(Pocos.RealMonsterData.NestedLevelThree plain)
        {
            plain.Position = Position.LastValue;
            plain.Velo = Velo.LastValue;
            plain.Acc = Acc.LastValue;
            plain.Dcc = Dcc.LastValue;
            return plain;
        }

        public async virtual Task PlainToOnline<T>(T plain)
        {
            await this.PlainToOnlineAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToOnlineAsync(Pocos.RealMonsterData.NestedLevelThree plain)
        {
#pragma warning disable CS0612
            Position.LethargicWrite(plain.Position);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Velo.LethargicWrite(plain.Velo);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Acc.LethargicWrite(plain.Acc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Dcc.LethargicWrite(plain.Dcc);
#pragma warning restore CS0612
            return await this.WriteAsync<IgnoreOnPocoOperation>();
        }

        [Obsolete("This method should not be used if you indent to access the controllers data. Use `PlainToOnline` instead.")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        public async Task _PlainToOnlineNoacAsync(Pocos.RealMonsterData.NestedLevelThree plain)
        {
#pragma warning disable CS0612
            Position.LethargicWrite(plain.Position);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Velo.LethargicWrite(plain.Velo);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Acc.LethargicWrite(plain.Acc);
#pragma warning restore CS0612
#pragma warning disable CS0612
            Dcc.LethargicWrite(plain.Dcc);
#pragma warning restore CS0612
        }

        public async virtual Task<T> ShadowToPlain<T>()
        {
            return await (dynamic)this.ShadowToPlainAsync();
        }

        public async Task<Pocos.RealMonsterData.NestedLevelThree> ShadowToPlainAsync()
        {
            Pocos.RealMonsterData.NestedLevelThree plain = new Pocos.RealMonsterData.NestedLevelThree();
            plain.Position = Position.Shadow;
            plain.Velo = Velo.Shadow;
            plain.Acc = Acc.Shadow;
            plain.Dcc = Dcc.Shadow;
            return plain;
        }

        protected async Task<Pocos.RealMonsterData.NestedLevelThree> ShadowToPlainAsync(Pocos.RealMonsterData.NestedLevelThree plain)
        {
            plain.Position = Position.Shadow;
            plain.Velo = Velo.Shadow;
            plain.Acc = Acc.Shadow;
            plain.Dcc = Dcc.Shadow;
            return plain;
        }

        public async virtual Task PlainToShadow<T>(T plain)
        {
            await this.PlainToShadowAsync((dynamic)plain);
        }

        public async Task<IEnumerable<ITwinPrimitive>> PlainToShadowAsync(Pocos.RealMonsterData.NestedLevelThree plain)
        {
            Position.Shadow = plain.Position;
            Velo.Shadow = plain.Velo;
            Acc.Shadow = plain.Acc;
            Dcc.Shadow = plain.Dcc;
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
        public async Task<bool> DetectsAnyChangeAsync(Pocos.RealMonsterData.NestedLevelThree plain, Pocos.RealMonsterData.NestedLevelThree latest = null)
        {
            if (latest == null)
                latest = await this._OnlineToPlainNoacAsync();
            var somethingChanged = false;
            return await Task.Run(async () =>
            {
                if (plain.Position != Position.LastValue)
                    somethingChanged = true;
                if (plain.Velo != Velo.LastValue)
                    somethingChanged = true;
                if (plain.Acc != Acc.LastValue)
                    somethingChanged = true;
                if (plain.Dcc != Dcc.LastValue)
                    somethingChanged = true;
                plain = latest;
                return somethingChanged;
            });
        }

        public void Poll()
        {
            this.RetrievePrimitives().ToList().ForEach(x => x.Poll());
        }

        public Pocos.RealMonsterData.NestedLevelThree CreateEmptyPoco()
        {
            return new Pocos.RealMonsterData.NestedLevelThree();
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
}