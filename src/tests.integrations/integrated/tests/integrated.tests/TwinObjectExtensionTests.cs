using AXSharp.Connector;
using AXSharp.Connector.ValueTypes;
using Microsoft.VisualBasic;

namespace integrated.tests
{
    public class TwinObjectExtensionTests
    {
        [Fact]
        public async Task OnlineToShadowAsync_should_copy_entire_structure()
        {

            var monster = Entry.Plc.OnlineToShadowAsync_should_copy_entire_structure;
            monster.Description.Cyclic = "from online to shadow";
            monster.Id.Cyclic = 111222;
            monster.ArrayOfBytes[0].Cyclic = 11;
            monster.ArrayOfBytes[1].Cyclic = 22;
            monster.ArrayOfBytes[2].Cyclic = 33;

            monster.ArrayOfDrives[0].Velo.Cyclic = 110;
            monster.ArrayOfDrives[0].Acc.Cyclic = 120;
            monster.ArrayOfDrives[0].Dcc.Cyclic = 130;
            monster.ArrayOfDrives[0].Position.Cyclic = 140;

            monster.ArrayOfDrives[1].Velo.Cyclic = 210;
            monster.ArrayOfDrives[1].Acc.Cyclic = 220;
            monster.ArrayOfDrives[1].Dcc.Cyclic = 230;
            monster.ArrayOfDrives[1].Position.Cyclic = 240;

            monster.ArrayOfDrives[2].Velo.Cyclic = 310;
            monster.ArrayOfDrives[2].Acc.Cyclic = 320;
            monster.ArrayOfDrives[2].Dcc.Cyclic = 330;
            monster.ArrayOfDrives[2].Position.Cyclic = 340;

            await monster.OnlineToShadowAsync();

            Assert.Equal(monster.Description.Cyclic, monster.Description.Shadow);
            Assert.Equal(monster.Id.Cyclic, monster.Id.Shadow);
            Assert.Equal(monster.ArrayOfBytes[0].Cyclic, monster.ArrayOfBytes[0].Shadow);
            Assert.Equal(monster.ArrayOfBytes[1].Cyclic, monster.ArrayOfBytes[1].Shadow);
            Assert.Equal(monster.ArrayOfBytes[2].Cyclic, monster.ArrayOfBytes[2].Shadow);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Cyclic, monster.ArrayOfDrives[0].Velo.Shadow);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Cyclic, monster.ArrayOfDrives[0].Acc.Shadow);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Cyclic, monster.ArrayOfDrives[0].Dcc.Shadow);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Cyclic, monster.ArrayOfDrives[0].Position.Shadow);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Cyclic, monster.ArrayOfDrives[1].Velo.Shadow);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Cyclic, monster.ArrayOfDrives[1].Acc.Shadow);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Cyclic, monster.ArrayOfDrives[1].Dcc.Shadow);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Cyclic, monster.ArrayOfDrives[1].Position.Shadow);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Cyclic, monster.ArrayOfDrives[2].Velo.Shadow);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Cyclic, monster.ArrayOfDrives[2].Acc.Shadow);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Cyclic, monster.ArrayOfDrives[2].Dcc.Shadow);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Cyclic, monster.ArrayOfDrives[2].Position.Shadow);
        }

        [Fact]
        public async Task ShadowToOnlineAsync_should_copy_entire_structure()
        {
            var monster = Entry.Plc.ShadowToOnlineAsync_should_copy_entire_structure;
            monster.Description.Cyclic = "from shadow to online";
            monster.Id.Cyclic = 333444;

            monster.ArrayOfBytes[0].Shadow = 41;
            monster.ArrayOfBytes[1].Shadow = 42;
            monster.ArrayOfBytes[2].Shadow = 43;

            monster.ArrayOfDrives[0].Velo.Shadow = 510;
            monster.ArrayOfDrives[0].Acc.Shadow = 520;
            monster.ArrayOfDrives[0].Dcc.Shadow = 530;
            monster.ArrayOfDrives[0].Position.Shadow = 540;

            monster.ArrayOfDrives[1].Velo.Shadow = 610;
            monster.ArrayOfDrives[1].Acc.Shadow = 620;
            monster.ArrayOfDrives[1].Dcc.Shadow = 630;
            monster.ArrayOfDrives[1].Position.Shadow = 640;

            monster.ArrayOfDrives[2].Velo.Shadow = 710;
            monster.ArrayOfDrives[2].Acc.Shadow = 720;
            monster.ArrayOfDrives[2].Dcc.Shadow = 730;
            monster.ArrayOfDrives[2].Position.Shadow = 740;

            await monster.ShadowToOnlineAsync();
            await monster.ReadAsync();

            Assert.Equal(monster.Description.Shadow, monster.Description.Cyclic);
            Assert.Equal(monster.Id.Shadow, monster.Id.Cyclic);
            Assert.Equal(monster.ArrayOfBytes[0].Shadow, monster.ArrayOfBytes[0].Cyclic);
            Assert.Equal(monster.ArrayOfBytes[1].Shadow, monster.ArrayOfBytes[1].Cyclic);
            Assert.Equal(monster.ArrayOfBytes[2].Shadow, monster.ArrayOfBytes[2].Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Shadow, monster.ArrayOfDrives[0].Velo.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Shadow, monster.ArrayOfDrives[0].Acc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Shadow, monster.ArrayOfDrives[0].Dcc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Shadow, monster.ArrayOfDrives[0].Position.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Shadow, monster.ArrayOfDrives[1].Velo.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Shadow, monster.ArrayOfDrives[1].Acc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Shadow, monster.ArrayOfDrives[1].Dcc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Shadow, monster.ArrayOfDrives[1].Position.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Shadow, monster.ArrayOfDrives[2].Velo.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Shadow, monster.ArrayOfDrives[2].Acc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Shadow, monster.ArrayOfDrives[2].Dcc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Shadow, monster.ArrayOfDrives[2].Position.Cyclic);

            Assert.Equal(monster.ArrayOfBytes[0].Shadow, monster.ArrayOfBytes[0].Cyclic);
            Assert.Equal(monster.ArrayOfBytes[1].Shadow, monster.ArrayOfBytes[1].Cyclic);
            Assert.Equal(monster.ArrayOfBytes[2].Shadow, monster.ArrayOfBytes[2].Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Shadow, monster.ArrayOfDrives[0].Velo.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Shadow, monster.ArrayOfDrives[0].Acc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Shadow, monster.ArrayOfDrives[0].Dcc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Shadow, monster.ArrayOfDrives[0].Position.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Shadow, monster.ArrayOfDrives[1].Velo.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Shadow, monster.ArrayOfDrives[1].Acc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Shadow, monster.ArrayOfDrives[1].Dcc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Shadow, monster.ArrayOfDrives[1].Position.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Shadow, monster.ArrayOfDrives[2].Velo.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Shadow, monster.ArrayOfDrives[2].Acc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Shadow, monster.ArrayOfDrives[2].Dcc.Cyclic);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Shadow, monster.ArrayOfDrives[2].Position.Cyclic);
        }

        [Fact]
        public async Task OnlineToShadow_RealMonster_should_copy()
        {
            var monster = Entry.Plc.OnlineToShadow_should_copy;
            var today = new DateTime(2022, 12, 15);
            var date = new DateOnly(1999, 2, 13);
            var timespan = new TimeSpan(13, 13, 13);

            monster.TestDateTime.Cyclic = today;
            monster.TestDate.Cyclic = date;
            monster.TestTimeSpan.Cyclic = timespan;
            monster.Description.Cyclic = "from plain to online";

            monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Cyclic = 123;
            await monster.WriteAsync();

            await monster.OnlineToShadowAsync();



            Assert.Equal(monster.Description.Cyclic, monster.Description.Shadow);
            Assert.Equal(monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Cyclic, monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Shadow);
            Assert.Equal(monster.TestDate.Cyclic, monster.TestDate.Shadow);
            Assert.Equal(monster.TestDateTime.Cyclic, monster.TestDateTime.Shadow);
            Assert.Equal(monster.TestTimeSpan.Cyclic, monster.TestTimeSpan.Shadow);
        }

        [Fact]
        public async Task ShadowToOnline_RealMonster_should_copy()
        {
            var monster = Entry.Plc.ShadowToOnline_should_copy;
            var today = new DateTime(2022, 12, 15);
            var date = new DateOnly(1999, 2, 13);
            var timespan = new TimeSpan(13, 13, 13);

            monster.TestDateTime.Shadow = today;
            monster.TestDate.Shadow = date;
            monster.TestTimeSpan.Shadow = timespan;
            monster.Description.Shadow = "from plain to online";

            monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Shadow = 123;

            await monster.ShadowToOnlineAsync();



            Assert.Equal(monster.Description.Shadow, monster.Description.Cyclic);
            Assert.Equal(monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Shadow, monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Cyclic);
            Assert.Equal(monster.TestDate.Shadow, monster.TestDate.Cyclic);
            Assert.Equal(monster.TestDateTime.Shadow, monster.TestDateTime.Cyclic);
            Assert.Equal(monster.TestTimeSpan.Shadow, monster.TestTimeSpan.Cyclic);
        }



        //tests primitives all

        //SHADOW_TO_ONLINE/ONLINE_TO_SHADOW
        [Fact]
        public async Task ShadowToOnline_primitives_should_copy_entire_structure()
        {
            //arrange
            var primitives = Entry.Plc.p_shadow_online;
            primitives = SetShadow(primitives);


            //act
            await primitives.ShadowToOnlineAsync();


            //assert
            Assert.Equal(primitives.myBOOL.Shadow, primitives.myBOOL.Cyclic);
            Assert.Equal(primitives.myBYTE.Shadow, primitives.myBYTE.Cyclic);
            Assert.Equal(primitives.myWORD.Shadow, primitives.myWORD.Cyclic);
            Assert.Equal(primitives.myDWORD.Shadow, primitives.myDWORD.Cyclic);
            Assert.Equal(primitives.myLWORD.Shadow, primitives.myLWORD.Cyclic);
            Assert.Equal(primitives.mySINT.Shadow, primitives.mySINT.Cyclic);
            Assert.Equal(primitives.myINT.Shadow, primitives.myINT.Cyclic);
            Assert.Equal(primitives.myDINT.Shadow, primitives.myDINT.Cyclic);
            Assert.Equal(primitives.myLINT.Shadow, primitives.myLINT.Cyclic);
            Assert.Equal(primitives.myUSINT.Shadow, primitives.myUSINT.Cyclic);
            Assert.Equal(primitives.myUINT.Shadow, primitives.myUINT.Cyclic);
            Assert.Equal(primitives.myUDINT.Shadow, primitives.myUDINT.Cyclic);
            Assert.Equal(primitives.myULINT.Shadow, primitives.myULINT.Cyclic);
            Assert.Equal(primitives.myREAL.Shadow, primitives.myREAL.Cyclic);
            Assert.Equal(primitives.myLREAL.Shadow, primitives.myLREAL.Cyclic);
            Assert.Equal(primitives.myTIME.Shadow, primitives.myTIME.Cyclic);
            Assert.Equal(primitives.myLTIME.Shadow, primitives.myLTIME.Cyclic);
            Assert.Equal(primitives.myDATE.Shadow, primitives.myDATE.Cyclic);
            Assert.Equal(primitives.myTIME_OF_DAY.Shadow, primitives.myTIME_OF_DAY.Cyclic);
            Assert.Equal(primitives.myDATE_AND_TIME.Shadow, primitives.myDATE_AND_TIME.Cyclic);
            Assert.Equal(primitives.mySTRING.Shadow, primitives.mySTRING.Cyclic);
            Assert.Equal(primitives.myWSTRING.Shadow, primitives.myWSTRING.Cyclic);
            Assert.Equal(primitives.myEnum.Shadow, primitives.myEnum.Cyclic);

        }

        [Fact]
        public async Task OnlineToShadow_primitives_should_copy_entire_structure()
        {
            //arrange
            var primitives = Entry.Plc.p_online_shadow;
            primitives = SetOnline(primitives);


            //act
            await primitives.OnlineToShadowAsync();


            //assert
            Assert.Equal(primitives.myBOOL.Shadow, primitives.myBOOL.Cyclic);
            Assert.Equal(primitives.myBYTE.Shadow, primitives.myBYTE.Cyclic);
            Assert.Equal(primitives.myWORD.Shadow, primitives.myWORD.Cyclic);
            Assert.Equal(primitives.myDWORD.Shadow, primitives.myDWORD.Cyclic);
            Assert.Equal(primitives.myLWORD.Shadow, primitives.myLWORD.Cyclic);
            Assert.Equal(primitives.mySINT.Shadow, primitives.mySINT.Cyclic);
            Assert.Equal(primitives.myINT.Shadow, primitives.myINT.Cyclic);
            Assert.Equal(primitives.myDINT.Shadow, primitives.myDINT.Cyclic);
            Assert.Equal(primitives.myLINT.Shadow, primitives.myLINT.Cyclic);
            Assert.Equal(primitives.myUSINT.Shadow, primitives.myUSINT.Cyclic);
            Assert.Equal(primitives.myUINT.Shadow, primitives.myUINT.Cyclic);
            Assert.Equal(primitives.myUDINT.Shadow, primitives.myUDINT.Cyclic);
            Assert.Equal(primitives.myULINT.Shadow, primitives.myULINT.Cyclic);
            Assert.Equal(primitives.myREAL.Shadow, primitives.myREAL.Cyclic);
            Assert.Equal(primitives.myLREAL.Shadow, primitives.myLREAL.Cyclic);
            Assert.Equal(primitives.myTIME.Shadow, primitives.myTIME.Cyclic);
            Assert.Equal(primitives.myLTIME.Shadow, primitives.myLTIME.Cyclic);
            Assert.Equal(primitives.myDATE.Shadow, primitives.myDATE.Cyclic);
            Assert.Equal(primitives.myTIME_OF_DAY.Shadow, primitives.myTIME_OF_DAY.Cyclic);
            Assert.Equal(primitives.myDATE_AND_TIME.Shadow, primitives.myDATE_AND_TIME.Cyclic);
            Assert.Equal(primitives.mySTRING.Shadow, primitives.mySTRING.Cyclic);
            Assert.Equal(primitives.myWSTRING.Shadow, primitives.myWSTRING.Cyclic);
            Assert.Equal(primitives.myEnum.Shadow, primitives.myEnum.Cyclic);

        }



        private all_primitives SetShadow(all_primitives p)
        {
            var today = new DateTime(2023, 12, 15);
            var date = new DateOnly(1999, 2, 13);
            var timespan = new TimeSpan(13, 13, 13);

            p.myBOOL.Shadow = true;
            p.myBYTE.Shadow = 1;
            p.myWORD.Shadow = 2;
            p.myDWORD.Shadow = 3;
            p.myLWORD.Shadow = 4;
            p.mySINT.Shadow = 5;
            p.myINT.Shadow = 6;
            p.myDINT.Shadow = 7;
            p.myLINT.Shadow = 8;
            p.myUSINT.Shadow = 9;
            p.myUINT.Shadow = 10;
            p.myUDINT.Shadow = 11;
            p.myULINT.Shadow = 12;
            p.myREAL.Shadow = 13;
            p.myLREAL.Shadow = 14;
            p.myTIME.Shadow = timespan;
            p.myLTIME.Shadow = timespan;
            p.myDATE.Shadow = date;
            p.myTIME_OF_DAY.Shadow = timespan;
            p.myDATE_AND_TIME.Shadow = today;
            p.mySTRING.Shadow = "anakin skywalker";
            p.myWSTRING.Shadow = "anakin skywalker";
            p.myEnum.Shadow = 2;

            return p;
        }

        private all_primitives SetOnline(all_primitives p)
        {
            var today = new DateTime(2023, 12, 15);
            var date = new DateOnly(1999, 2, 13);
            var timespan = new TimeSpan(13, 13, 13);

            p.myBOOL.Cyclic = true;
            p.myBYTE.Cyclic = 1;
            p.myWORD.Cyclic = 2;
            p.myDWORD.Cyclic = 3;
            p.myLWORD.Cyclic = 4;
            p.mySINT.Cyclic = 5;
            p.myINT.Cyclic = 6;
            p.myDINT.Cyclic = 7;
            p.myLINT.Cyclic = 8;
            p.myUSINT.Cyclic = 9;
            p.myUINT.Cyclic = 10;
            p.myUDINT.Cyclic = 11;
            p.myULINT.Cyclic = 12;
            p.myREAL.Cyclic = 13;
            p.myLREAL.Cyclic = 14;
            p.myTIME.Cyclic = timespan;
            p.myLTIME.Cyclic = timespan;
            p.myDATE.Cyclic = date;
            p.myTIME_OF_DAY.Cyclic = timespan;
            p.myDATE_AND_TIME.Cyclic = today;
            p.mySTRING.Cyclic = "anakin skywalker";
            p.myWSTRING.Cyclic = "anakin skywalker";
            p.myEnum.Cyclic = 2;

            return p;
        }


        [Fact]
        public async Task StartPolling_should_update_cyclic_property()
        {
            var holderObject = new object();
            //arrange
            var polling = Entry.Plc.StartPolling_should_update_cyclic_property;
            polling.GetParent().GetConnector().SubscriptionMode = ReadSubscriptionMode.Polling;
            Entry.Plc.Connector.BuildAndStart();

            var id = await polling.Id.GetAsync();
            var pos = await polling.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position.GetAsync();

            polling.StartPolling(50, holderObject);

            Task.Delay(250).Wait();

            Assert.True(polling.Id.Cyclic != id);
            Assert.True(polling.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position.Cyclic != pos);

            polling.StopPolling(holderObject);

            Task.Delay(250).Wait();

            id = polling.Id.Cyclic;
            pos = polling.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position.Cyclic;

            Task.Delay(250).Wait();

            Assert.Equal(id, polling.Id.Cyclic);
            Assert.Equal(pos, polling.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position.Cyclic);
        }

        [Fact]
        public async Task StartPolling_polling_should_continue_until_last_subscriber()
        {
            var holderObject = new object();
            var holderObject1 = new object();

            //arrange
            var polling = Entry.Plc.StartPolling_should_update_cyclic_property;
            polling.GetParent().GetConnector().SubscriptionMode = ReadSubscriptionMode.Polling;
            Entry.Plc.Connector.BuildAndStart();

            var id = await polling.Id.GetAsync();
            var pos = await polling.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position.GetAsync();

            polling.StartPolling(50, holderObject);
            polling.StartPolling(150, holderObject1);

            Task.Delay(250).Wait();

            Assert.NotEqual(id, polling.Id.Cyclic);
            Assert.NotEqual(pos, polling.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position.Cyclic);

            polling.StopPolling(holderObject);

            Task.Delay(250).Wait();

            id = polling.Id.Cyclic;
            pos = polling.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position.Cyclic;

            Task.Delay(250).Wait();

            Assert.NotEqual(id, polling.Id.Cyclic);
            Assert.NotEqual(pos, polling.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position.Cyclic);


            polling.StopPolling(holderObject1);

            Task.Delay(250).Wait();

            id = polling.Id.Cyclic;
            pos = polling.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position.Cyclic;

            Task.Delay(250).Wait();


            Assert.Equal(id, polling.Id.Cyclic);
            Assert.Equal(pos, polling.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Position.Cyclic);
        }

        [Fact]
        public async Task StartPolling_ConcurentOverload()
        {
            var polling = Entry.Plc.StartPolling_ConcurentOverload;

            var pollingConnector = polling.GetParent().GetConnector();
            pollingConnector.SubscriptionMode = ReadSubscriptionMode.Polling;
            pollingConnector.BuildAndStart().ReadWriteCycleDelay = 10;
            pollingConnector.ConcurrentRequestMaxCount = 6;
            pollingConnector.ConcurrentRequestDelay = 20;

            var testedMembes = new List<PollingTestMember>();
            foreach (var item in polling.RetrievePrimitives().ToList())
            {
                testedMembes.Add(new PollingTestMember(item));
            }

            try
            {
                // 20 polling groups
                int intervalMin = 10;
                int intervalMax = 70;
                int intervalStep = 3;

                // 20 diff. test times
                int duratonTimeMin = 500;
                int durationTimeMax = 1000;
                int durationTimeStep = 3;


                int PollingRepetionsPerMember =2;
                int TestRepetitions =2;


                int itemPollingTime = durationTimeMax;

                for (int i = 0; i < TestRepetitions; i++)
                {
                    int itemInterval = intervalMin;

                    foreach (var item in testedMembes)
                    {
                        item.InitializePollingOnTask(itemInterval, itemPollingTime, PollingRepetionsPerMember);

                        itemInterval = itemInterval + intervalStep;
                        if (itemInterval >= intervalMax) { itemInterval = intervalMin; }
                    }

                    // wait for all tests
                    foreach (var item in testedMembes)
                    {
                        await item.OnTask;
                    }

                    itemPollingTime = itemPollingTime - durationTimeStep;
                    if (itemPollingTime < duratonTimeMin) { itemPollingTime = durationTimeMax; }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Polling exception!", ex);
            }
        }

        public class PollingTestMember
        {
            public PollingTestMember(ITwinElement element)
            {
                Element = element;
            }

            public async void InitializePollingOnTask(int pollingInterval = 10, int pollingDurationTime = 100, int pollingRepetitions = 2)
            {
                PollingInterval = pollingInterval;
                Repetitions = pollingRepetitions;
                DurationTime = pollingDurationTime;

                this.OnTask = System.Threading.Tasks.Task.Run(async () =>
                {
                    for (int i = 0; i < Repetitions; i++)
                    {
                        this.Element.StartPolling(this.PollingInterval, this.ObjectHolder);

                        await Task.Delay(DurationTime);

                        this.Element.StopPolling(this.ObjectHolder);
                    }
                });
            }

            public ITwinElement Element { get; private set; }
            public Task OnTask { get; private set; }

            public int PollingInterval { get; private set; }
            public int Repetitions { get; private set; }
            public int DurationTime { get; private set; }

            public object ObjectHolder { get; private set; } = new();
        }
    }
}