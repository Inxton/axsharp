using AXSharp.Connector;
namespace integrated.tests
{
    public class PlainersSwappingTests
    {

        public PlainersSwappingTests()
        {
            Task.Delay(750).Wait();
        }

        [Fact]
        public async  Task OnlineToPlain_should_copy_entire_structure()
        {
            var monster = Entry.Plc.OnlineToPlain_should_copy_entire_structure;

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

            await monster.WriteAsync();

            var p = await monster.OnlineToPlainAsync();

            Assert.Equal(monster.Description.Cyclic, p.Description);
            Assert.Equal(monster.Id.Cyclic, p.Id);
            Assert.Equal(monster.ArrayOfBytes[0].Cyclic, p.ArrayOfBytes[0]);
            Assert.Equal(monster.ArrayOfBytes[1].Cyclic, p.ArrayOfBytes[1]);
            Assert.Equal(monster.ArrayOfBytes[2].Cyclic, p.ArrayOfBytes[2]);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Cyclic, p.ArrayOfDrives[0].Velo);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Cyclic, p.ArrayOfDrives[0].Acc);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Cyclic, p.ArrayOfDrives[0].Dcc);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Cyclic, p.ArrayOfDrives[0].Position);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Cyclic, p.ArrayOfDrives[1].Velo);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Cyclic, p.ArrayOfDrives[1].Acc);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Cyclic, p.ArrayOfDrives[1].Dcc);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Cyclic, p.ArrayOfDrives[1].Position);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Cyclic, p.ArrayOfDrives[2].Velo);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Cyclic, p.ArrayOfDrives[2].Acc);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Cyclic, p.ArrayOfDrives[2].Dcc);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Cyclic, p.ArrayOfDrives[2].Position);
        }

        [Fact]
        public async Task OnlineToPlain_should_copy_entire_structure_ignore_on_poco_operations()
        {
            var monster = Entry.Plc.OnlineToPlain_should_copy_entire_structure;

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

            monster.DriveBase_tobeignoredbypocooperations.Velo.Cyclic = 510;
            monster.DriveBase_tobeignoredbypocooperations.Acc.Cyclic = 520; 
            monster.DriveBase_tobeignoredbypocooperations.Dcc.Cyclic = 530;
            monster.DriveBase_tobeignoredbypocooperations.Position.Cyclic = 540;


            await monster.WriteAsync();

            monster.Id.Cyclic = 3344;
            monster.ArrayOfBytes[0].Cyclic = 111;
            monster.ArrayOfBytes[1].Cyclic = 122;
            monster.ArrayOfBytes[2].Cyclic = 133;

            monster.ArrayOfDrives[0].Velo.Cyclic = 310;
            monster.ArrayOfDrives[0].Acc.Cyclic  = 320;
            monster.ArrayOfDrives[0].Dcc.Cyclic  = 330;
            monster.ArrayOfDrives[0].Position.Cyclic = 140;

            
            //those members should be not written
            monster.DriveBase_tobeignoredbypocooperations.Velo.Cyclic = 610;
            monster.DriveBase_tobeignoredbypocooperations.Acc.Cyclic = 620;
            monster.DriveBase_tobeignoredbypocooperations.Dcc.Cyclic = 630;
            monster.DriveBase_tobeignoredbypocooperations.Position.Cyclic = 640;

            var p = await monster.OnlineToPlainAsync();

            Assert.Equal(0, p.DriveBase_tobeignoredbypocooperations.Velo);
            Assert.Equal(0, p.DriveBase_tobeignoredbypocooperations.Acc);
            Assert.Equal(0, p.DriveBase_tobeignoredbypocooperations.Dcc);
            Assert.Equal(0, p.DriveBase_tobeignoredbypocooperations.Position);

            Assert.Equal(monster.Description.Cyclic, p.Description);
            Assert.Equal(monster.Id.Cyclic, p.Id);
            Assert.Equal(monster.ArrayOfBytes[0].Cyclic, p.ArrayOfBytes[0]);
            Assert.Equal(monster.ArrayOfBytes[1].Cyclic, p.ArrayOfBytes[1]);
            Assert.Equal(monster.ArrayOfBytes[2].Cyclic, p.ArrayOfBytes[2]);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Cyclic, p.ArrayOfDrives[0].Velo);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Cyclic, p.ArrayOfDrives[0].Acc);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Cyclic, p.ArrayOfDrives[0].Dcc);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Cyclic, p.ArrayOfDrives[0].Position);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Cyclic, p.ArrayOfDrives[1].Velo);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Cyclic, p.ArrayOfDrives[1].Acc);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Cyclic, p.ArrayOfDrives[1].Dcc);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Cyclic, p.ArrayOfDrives[1].Position);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Cyclic, p.ArrayOfDrives[2].Velo);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Cyclic, p.ArrayOfDrives[2].Acc);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Cyclic, p.ArrayOfDrives[2].Dcc);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Cyclic, p.ArrayOfDrives[2].Position);
        }

        [Fact]
        public async Task ITwinObject_OnlineToPlain_should_copy_entire_structure()
        {
            var monster = Entry.Plc.ITwinObjectOnlineToPlain_should_copy_entire_structure;

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

            await monster.WriteAsync();

            var p = await ((ITwinObject)monster).OnlineToPlain<MonsterData.Pocos.Monster>();

            Assert.Equal(monster.Description.Cyclic, p.Description);
            Assert.Equal(monster.Id.Cyclic, p.Id);
            Assert.Equal(monster.ArrayOfBytes[0].Cyclic, p.ArrayOfBytes[0]);
            Assert.Equal(monster.ArrayOfBytes[1].Cyclic, p.ArrayOfBytes[1]);
            Assert.Equal(monster.ArrayOfBytes[2].Cyclic, p.ArrayOfBytes[2]);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Cyclic, p.ArrayOfDrives[0].Velo);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Cyclic, p.ArrayOfDrives[0].Acc);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Cyclic, p.ArrayOfDrives[0].Dcc);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Cyclic, p.ArrayOfDrives[0].Position);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Cyclic, p.ArrayOfDrives[1].Velo);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Cyclic, p.ArrayOfDrives[1].Acc);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Cyclic, p.ArrayOfDrives[1].Dcc);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Cyclic, p.ArrayOfDrives[1].Position);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Cyclic, p.ArrayOfDrives[2].Velo);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Cyclic, p.ArrayOfDrives[2].Acc);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Cyclic, p.ArrayOfDrives[2].Dcc);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Cyclic, p.ArrayOfDrives[2].Position);
        }

        [Fact]
        public async Task PlainToOnline_should_copy_entire_structure()
        {
            var monster = Entry.Plc.PlainToOnline_should_copy_entire_structure;

            var p = new MonsterData.Pocos.Monster();

            p.Description = "from plain to online";
            p.Id = 111222;
            p.ArrayOfBytes[0] = 11;
            p.ArrayOfBytes[1] = 22;
            p.ArrayOfBytes[2] = 33;

            //for (int i = 0; i < p.ArrayOfDrives.Length; i++)
            //{
            //    p.ArrayOfDrives[i] = new();
            //}


            p.ArrayOfDrives[0].Velo = 110;
            p.ArrayOfDrives[0].Acc = 120;
            p.ArrayOfDrives[0].Dcc = 130;
            p.ArrayOfDrives[0].Position = 140;
            
            p.ArrayOfDrives[1].Velo = 210;
            p.ArrayOfDrives[1].Acc = 220;
            p.ArrayOfDrives[1].Dcc = 230;
            p.ArrayOfDrives[1].Position = 240;
            
            p.ArrayOfDrives[2].Velo = 310;
            p.ArrayOfDrives[2].Acc = 320;
            p.ArrayOfDrives[2].Dcc = 330;
            p.ArrayOfDrives[2].Position = 340;

            await monster.PlainToOnlineAsync(p);

            await monster.WriteAsync();
            await monster.ReadAsync();


            Assert.Equal(monster.Description.Cyclic, p.Description);
            Assert.Equal(monster.Id.Cyclic, p.Id);
            Assert.Equal(monster.ArrayOfBytes[0].Cyclic, p.ArrayOfBytes[0]);
            Assert.Equal(monster.ArrayOfBytes[1].Cyclic, p.ArrayOfBytes[1]);
            Assert.Equal(monster.ArrayOfBytes[2].Cyclic, p.ArrayOfBytes[2]);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Cyclic, p.ArrayOfDrives[0].Velo);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Cyclic, p.ArrayOfDrives[0].Acc);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Cyclic, p.ArrayOfDrives[0].Dcc);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Cyclic, p.ArrayOfDrives[0].Position);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Cyclic, p.ArrayOfDrives[1].Velo);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Cyclic, p.ArrayOfDrives[1].Acc);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Cyclic, p.ArrayOfDrives[1].Dcc);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Cyclic, p.ArrayOfDrives[1].Position);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Cyclic, p.ArrayOfDrives[2].Velo);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Cyclic, p.ArrayOfDrives[2].Acc);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Cyclic, p.ArrayOfDrives[2].Dcc);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Cyclic, p.ArrayOfDrives[2].Position);
        }

        [Fact]
        public async Task PlainToOnline_should_copy_entire_structure_ignore_on_poco_operations()
        {
            var monster = Entry.Plc.PlainToOnline_should_copy_entire_structure;

            var p = new MonsterData.Pocos.Monster();

            p.Description = "from plain to online";
            p.Id = 111222;
            p.ArrayOfBytes[0] = 11;
            p.ArrayOfBytes[1] = 22;
            p.ArrayOfBytes[2] = 33;

            for (int i = 0; i < p.ArrayOfDrives.Length; i++)
            {
                p.ArrayOfDrives[i] = new();
            }


            p.ArrayOfDrives[0].Velo = 110;
            p.ArrayOfDrives[0].Acc = 120;
            p.ArrayOfDrives[0].Dcc = 130;
            p.ArrayOfDrives[0].Position = 140;

            p.ArrayOfDrives[1].Velo = 210;
            p.ArrayOfDrives[1].Acc = 220;
            p.ArrayOfDrives[1].Dcc = 230;
            p.ArrayOfDrives[1].Position = 240;

            p.ArrayOfDrives[2].Velo = 310;
            p.ArrayOfDrives[2].Acc = 320;
            p.ArrayOfDrives[2].Dcc = 330;
            p.ArrayOfDrives[2].Position = 340;

            monster.DriveBase_tobeignoredbypocooperations.Velo.Cyclic = 510;
            monster.DriveBase_tobeignoredbypocooperations.Acc.Cyclic = 520;
            monster.DriveBase_tobeignoredbypocooperations.Dcc.Cyclic = 530;
            monster.DriveBase_tobeignoredbypocooperations.Position.Cyclic = 540;

            await monster.DriveBase_tobeignoredbypocooperations.WriteAsync();

            // This should not be written to the controller with PlainToOnline
            p.DriveBase_tobeignoredbypocooperations.Velo = 610;
            p.DriveBase_tobeignoredbypocooperations.Acc = 620;
            p.DriveBase_tobeignoredbypocooperations.Dcc = 630;
            p.DriveBase_tobeignoredbypocooperations.Position = 640;

            await monster.PlainToOnlineAsync(p);
            await monster.ReadAsync(); // Read again from controller

            // These should not change after PlainToOnline operation
            Assert.Equal(510, monster.DriveBase_tobeignoredbypocooperations.Velo.Cyclic);
            Assert.Equal(520, monster.DriveBase_tobeignoredbypocooperations.Acc.Cyclic);
            Assert.Equal(530, monster.DriveBase_tobeignoredbypocooperations.Dcc.Cyclic);
            Assert.Equal(540, monster.DriveBase_tobeignoredbypocooperations.Position.Cyclic);


            Assert.Equal(monster.Description.Cyclic, p.Description);
            Assert.Equal(monster.Id.Cyclic, p.Id);
            Assert.Equal(monster.ArrayOfBytes[0].Cyclic, p.ArrayOfBytes[0]);
            Assert.Equal(monster.ArrayOfBytes[1].Cyclic, p.ArrayOfBytes[1]);
            Assert.Equal(monster.ArrayOfBytes[2].Cyclic, p.ArrayOfBytes[2]);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Cyclic, p.ArrayOfDrives[0].Velo);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Cyclic, p.ArrayOfDrives[0].Acc);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Cyclic, p.ArrayOfDrives[0].Dcc);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Cyclic, p.ArrayOfDrives[0].Position);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Cyclic, p.ArrayOfDrives[1].Velo);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Cyclic, p.ArrayOfDrives[1].Acc);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Cyclic, p.ArrayOfDrives[1].Dcc);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Cyclic, p.ArrayOfDrives[1].Position);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Cyclic, p.ArrayOfDrives[2].Velo);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Cyclic, p.ArrayOfDrives[2].Acc);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Cyclic, p.ArrayOfDrives[2].Dcc);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Cyclic, p.ArrayOfDrives[2].Position);
        }

        [Fact]
        public async Task ITwinObject_PlainToOnline_should_copy_entire_structure()
        {
            var monster = Entry.Plc.ITwinObjectPlainToOnline_should_copy_entire_structure;

            var p = new MonsterData.Pocos.Monster();

            p.Description = "from plain to online";
            p.Id = 111222;
            p.ArrayOfBytes[0] = 11;
            p.ArrayOfBytes[1] = 22;
            p.ArrayOfBytes[2] = 33;

            for (int i = 0; i < p.ArrayOfDrives.Length; i++)
            {
                p.ArrayOfDrives[i] = new();
            }


            p.ArrayOfDrives[0].Velo = 110;
            p.ArrayOfDrives[0].Acc = 120;
            p.ArrayOfDrives[0].Dcc = 130;
            p.ArrayOfDrives[0].Position = 140;

            p.ArrayOfDrives[1].Velo = 210;
            p.ArrayOfDrives[1].Acc = 220;
            p.ArrayOfDrives[1].Dcc = 230;
            p.ArrayOfDrives[1].Position = 240;

            p.ArrayOfDrives[2].Velo = 310;
            p.ArrayOfDrives[2].Acc = 320;
            p.ArrayOfDrives[2].Dcc = 330;
            p.ArrayOfDrives[2].Position = 340;

            await ((ITwinObject)monster).PlainToOnline(p);

            await monster.WriteAsync();
            await monster.ReadAsync();


            Assert.Equal(monster.Description.Cyclic, p.Description);
            Assert.Equal(monster.Id.Cyclic, p.Id);
            Assert.Equal(monster.ArrayOfBytes[0].Cyclic, p.ArrayOfBytes[0]);
            Assert.Equal(monster.ArrayOfBytes[1].Cyclic, p.ArrayOfBytes[1]);
            Assert.Equal(monster.ArrayOfBytes[2].Cyclic, p.ArrayOfBytes[2]);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Cyclic, p.ArrayOfDrives[0].Velo);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Cyclic, p.ArrayOfDrives[0].Acc);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Cyclic, p.ArrayOfDrives[0].Dcc);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Cyclic, p.ArrayOfDrives[0].Position);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Cyclic, p.ArrayOfDrives[1].Velo);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Cyclic, p.ArrayOfDrives[1].Acc);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Cyclic, p.ArrayOfDrives[1].Dcc);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Cyclic, p.ArrayOfDrives[1].Position);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Cyclic, p.ArrayOfDrives[2].Velo);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Cyclic, p.ArrayOfDrives[2].Acc);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Cyclic, p.ArrayOfDrives[2].Dcc);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Cyclic, p.ArrayOfDrives[2].Position);
        }


        [Fact]
        public async Task OnlineToPlain_RealMonster_should_copy()
        {
            var monster = Entry.Plc.OnlineToPlain_should_copy;
            var today = DateTime.UtcNow;
            var date = new DateOnly(1999, 2, 13);
            var timespan = new TimeSpan(13, 13, 13);

            monster.TestDateTime.Cyclic = today;
            monster.TestDate.Cyclic = date;
            monster.TestTimeSpan.Cyclic = timespan;
            monster.Description.Cyclic = "from plain to online";

            monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Cyclic = 123;
            await monster.WriteAsync();


            var p = await monster.OnlineToPlainAsync();


            Assert.Equal(monster.Description.Cyclic, p.Description);
            Assert.Equal(monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Cyclic, p.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc);
            Assert.Equal(monster.TestDate.Cyclic, p.TestDate);
            Assert.Equal(monster.TestDateTime.Cyclic, p.TestDateTime);
            Assert.Equal(monster.TestTimeSpan.Cyclic, p.TestTimeSpan);
        }


        [Fact]
        public async Task ITwinObject_OnlineToPlain_RealMonster_should_copy()
        {
            var monster = Entry.Plc.ITwinObjectOnlineToPlain_should_copy;
            var today = DateTime.UtcNow;
            var date = new DateOnly(1999, 2, 13);
            var timespan = new TimeSpan(13, 13, 13);

            monster.TestDateTime.Cyclic = today;
            monster.TestDate.Cyclic = date;
            monster.TestTimeSpan.Cyclic = timespan;
            monster.Description.Cyclic = "from plain to online";

            monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Cyclic = 123;
            await monster.WriteAsync();

            var p = await ((ITwinObject)monster).OnlineToPlain<RealMonsterData.Pocos.RealMonster>();

            Assert.Equal(monster.Description.Cyclic, p.Description);
            Assert.Equal(monster.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc.Cyclic, p.DriveA.NestedLevelOne.NestedLevelTwo.NestedLevelThree.Acc);
            Assert.Equal(monster.TestDate.Cyclic, p.TestDate);
            Assert.Equal(monster.TestDateTime.Cyclic, p.TestDateTime);
            Assert.Equal(monster.TestTimeSpan.Cyclic, p.TestTimeSpan);
        }


        //shadowtoplain
        [Fact]
        public async Task ShadowToPlain_should_copy_entire_structure()
        {
            var monster = Entry.Plc.ShadowToPlainAsync_should_copy_entire_structure;

            monster.Description.Shadow = "from online to shadow";
            monster.Id.Shadow = 111222;
            monster.ArrayOfBytes[0].Shadow = 11;
            monster.ArrayOfBytes[1].Shadow = 22;
            monster.ArrayOfBytes[2].Shadow = 33;

            monster.ArrayOfDrives[0].Velo.Shadow = 110;
            monster.ArrayOfDrives[0].Acc.Shadow = 120;
            monster.ArrayOfDrives[0].Dcc.Shadow = 130;
            monster.ArrayOfDrives[0].Position.Shadow = 140;

            monster.ArrayOfDrives[1].Velo.Shadow = 210;
            monster.ArrayOfDrives[1].Acc.Shadow = 220;
            monster.ArrayOfDrives[1].Dcc.Shadow = 230;
            monster.ArrayOfDrives[1].Position.Shadow = 240;

            monster.ArrayOfDrives[2].Velo.Shadow = 310;
            monster.ArrayOfDrives[2].Acc.Shadow = 320;
            monster.ArrayOfDrives[2].Dcc.Shadow = 330;
            monster.ArrayOfDrives[2].Position.Shadow = 340;

            //await monster.WriteAsync();

            var p = await monster.ShadowToPlainAsync();

            Assert.Equal(monster.Description.Shadow, p.Description);
            Assert.Equal(monster.Id.Shadow, p.Id);
            Assert.Equal(monster.ArrayOfBytes[0].Shadow, p.ArrayOfBytes[0]);
            Assert.Equal(monster.ArrayOfBytes[1].Shadow, p.ArrayOfBytes[1]);
            Assert.Equal(monster.ArrayOfBytes[2].Shadow, p.ArrayOfBytes[2]);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Shadow, p.ArrayOfDrives[0].Velo);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Shadow, p.ArrayOfDrives[0].Acc);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Shadow, p.ArrayOfDrives[0].Dcc);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Shadow, p.ArrayOfDrives[0].Position);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Shadow, p.ArrayOfDrives[1].Velo);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Shadow, p.ArrayOfDrives[1].Acc);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Shadow, p.ArrayOfDrives[1].Dcc);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Shadow, p.ArrayOfDrives[1].Position);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Shadow, p.ArrayOfDrives[2].Velo);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Shadow, p.ArrayOfDrives[2].Acc);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Shadow, p.ArrayOfDrives[2].Dcc);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Shadow, p.ArrayOfDrives[2].Position);
        }

        [Fact]
        public async Task ITwinObject_ShadowToPlain_should_copy_entire_structure()
        {
            var monster = Entry.Plc.ITwinObjectShadowToPlainAsync_should_copy_entire_structure;

            monster.Description.Shadow = "from online to shadow";
            monster.Id.Shadow = 111222;
            monster.ArrayOfBytes[0].Shadow = 11;
            monster.ArrayOfBytes[1].Shadow = 22;
            monster.ArrayOfBytes[2].Shadow = 33;

            monster.ArrayOfDrives[0].Velo.Shadow = 110;
            monster.ArrayOfDrives[0].Acc.Shadow = 120;
            monster.ArrayOfDrives[0].Dcc.Shadow = 130;
            monster.ArrayOfDrives[0].Position.Shadow = 140;

            monster.ArrayOfDrives[1].Velo.Shadow = 210;
            monster.ArrayOfDrives[1].Acc.Shadow = 220;
            monster.ArrayOfDrives[1].Dcc.Shadow = 230;
            monster.ArrayOfDrives[1].Position.Shadow = 240;

            monster.ArrayOfDrives[2].Velo.Shadow = 310;
            monster.ArrayOfDrives[2].Acc.Shadow = 320;
            monster.ArrayOfDrives[2].Dcc.Shadow = 330;
            monster.ArrayOfDrives[2].Position.Shadow = 340;

            //await monster.WriteAsync();

            var p = await ((ITwinObject)monster).ShadowToPlain<MonsterData.Pocos.Monster>();

            Assert.Equal(monster.Description.Shadow, p.Description);
            Assert.Equal(monster.Id.Shadow, p.Id);
            Assert.Equal(monster.ArrayOfBytes[0].Shadow, p.ArrayOfBytes[0]);
            Assert.Equal(monster.ArrayOfBytes[1].Shadow, p.ArrayOfBytes[1]);
            Assert.Equal(monster.ArrayOfBytes[2].Shadow, p.ArrayOfBytes[2]);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Shadow, p.ArrayOfDrives[0].Velo);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Shadow, p.ArrayOfDrives[0].Acc);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Shadow, p.ArrayOfDrives[0].Dcc);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Shadow, p.ArrayOfDrives[0].Position);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Shadow, p.ArrayOfDrives[1].Velo);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Shadow, p.ArrayOfDrives[1].Acc);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Shadow, p.ArrayOfDrives[1].Dcc);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Shadow, p.ArrayOfDrives[1].Position);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Shadow, p.ArrayOfDrives[2].Velo);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Shadow, p.ArrayOfDrives[2].Acc);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Shadow, p.ArrayOfDrives[2].Dcc);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Shadow, p.ArrayOfDrives[2].Position);
        }

        [Fact]
        public async Task PlainToShadow_should_copy_entire_structure()
        {
            var monster = Entry.Plc.PlainToShadowAsync_should_copy_entire_structure;

            var p = new MonsterData.Pocos.Monster();

            p.Description = "from plain to shadow";
            p.Id = 111222;
            p.ArrayOfBytes[0] = 11;
            p.ArrayOfBytes[1] = 22;
            p.ArrayOfBytes[2] = 33;

            for (int i = 0; i < p.ArrayOfDrives.Length; i++)
            {
                p.ArrayOfDrives[i] = new();
            }


            p.ArrayOfDrives[0].Velo = 110;
            p.ArrayOfDrives[0].Acc = 120;
            p.ArrayOfDrives[0].Dcc = 130;
            p.ArrayOfDrives[0].Position = 140;

            p.ArrayOfDrives[1].Velo = 210;
            p.ArrayOfDrives[1].Acc = 220;
            p.ArrayOfDrives[1].Dcc = 230;
            p.ArrayOfDrives[1].Position = 240;

            p.ArrayOfDrives[2].Velo = 310;
            p.ArrayOfDrives[2].Acc = 320;
            p.ArrayOfDrives[2].Dcc = 330;
            p.ArrayOfDrives[2].Position = 340;

            var x = await monster.PlainToShadowAsync(p);



            Assert.Equal(monster.Description.Shadow, p.Description);
            Assert.Equal(monster.Id.Shadow, p.Id);
            Assert.Equal(monster.ArrayOfBytes[0].Shadow, p.ArrayOfBytes[0]);
            Assert.Equal(monster.ArrayOfBytes[1].Shadow, p.ArrayOfBytes[1]);
            Assert.Equal(monster.ArrayOfBytes[2].Shadow, p.ArrayOfBytes[2]);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Shadow, p.ArrayOfDrives[0].Velo);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Shadow, p.ArrayOfDrives[0].Acc);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Shadow, p.ArrayOfDrives[0].Dcc);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Shadow, p.ArrayOfDrives[0].Position);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Shadow, p.ArrayOfDrives[1].Velo);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Shadow, p.ArrayOfDrives[1].Acc);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Shadow, p.ArrayOfDrives[1].Dcc);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Shadow, p.ArrayOfDrives[1].Position);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Shadow, p.ArrayOfDrives[2].Velo);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Shadow, p.ArrayOfDrives[2].Acc);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Shadow, p.ArrayOfDrives[2].Dcc);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Shadow, p.ArrayOfDrives[2].Position);
        }

        [Fact]
        public async Task ITwinObject_PlainToShadow_should_copy_entire_structure()
        {
            var monster = Entry.Plc.ITwinObjectPlainToShadowAsync_should_copy_entire_structure;

            var p = new MonsterData.Pocos.Monster();

            p.Description = "from plain to shadow";
            p.Id = 111222;
            p.ArrayOfBytes[0] = 11;
            p.ArrayOfBytes[1] = 22;
            p.ArrayOfBytes[2] = 33;

            for (int i = 0; i < p.ArrayOfDrives.Length; i++)
            {
                p.ArrayOfDrives[i] = new();
            }


            p.ArrayOfDrives[0].Velo = 110;
            p.ArrayOfDrives[0].Acc = 120;
            p.ArrayOfDrives[0].Dcc = 130;
            p.ArrayOfDrives[0].Position = 140;

            p.ArrayOfDrives[1].Velo = 210;
            p.ArrayOfDrives[1].Acc = 220;
            p.ArrayOfDrives[1].Dcc = 230;
            p.ArrayOfDrives[1].Position = 240;

            p.ArrayOfDrives[2].Velo = 310;
            p.ArrayOfDrives[2].Acc = 320;
            p.ArrayOfDrives[2].Dcc = 330;
            p.ArrayOfDrives[2].Position = 340;

            await ((ITwinObject)monster).PlainToShadow(p);


            Assert.Equal(monster.Description.Shadow, p.Description);
            Assert.Equal(monster.Id.Shadow, p.Id);
            Assert.Equal(monster.ArrayOfBytes[0].Shadow, p.ArrayOfBytes[0]);
            Assert.Equal(monster.ArrayOfBytes[1].Shadow, p.ArrayOfBytes[1]);
            Assert.Equal(monster.ArrayOfBytes[2].Shadow, p.ArrayOfBytes[2]);
            Assert.Equal(monster.ArrayOfDrives[0].Velo.Shadow, p.ArrayOfDrives[0].Velo);
            Assert.Equal(monster.ArrayOfDrives[0].Acc.Shadow, p.ArrayOfDrives[0].Acc);
            Assert.Equal(monster.ArrayOfDrives[0].Dcc.Shadow, p.ArrayOfDrives[0].Dcc);
            Assert.Equal(monster.ArrayOfDrives[0].Position.Shadow, p.ArrayOfDrives[0].Position);
            Assert.Equal(monster.ArrayOfDrives[1].Velo.Shadow, p.ArrayOfDrives[1].Velo);
            Assert.Equal(monster.ArrayOfDrives[1].Acc.Shadow, p.ArrayOfDrives[1].Acc);
            Assert.Equal(monster.ArrayOfDrives[1].Dcc.Shadow, p.ArrayOfDrives[1].Dcc);
            Assert.Equal(monster.ArrayOfDrives[1].Position.Shadow, p.ArrayOfDrives[1].Position);
            Assert.Equal(monster.ArrayOfDrives[2].Velo.Shadow, p.ArrayOfDrives[2].Velo);
            Assert.Equal(monster.ArrayOfDrives[2].Acc.Shadow, p.ArrayOfDrives[2].Acc);
            Assert.Equal(monster.ArrayOfDrives[2].Dcc.Shadow, p.ArrayOfDrives[2].Dcc);
            Assert.Equal(monster.ArrayOfDrives[2].Position.Shadow, p.ArrayOfDrives[2].Position);
        }
        //tests primitives all

        //PLAIN_TO_ONLINE/ONLINE_TO_PLAIN
        [Fact]
        public async Task OnlineToPlain_primitives_should_copy_entire_structure()
        {
            //arrange
            var primitives = SetOnline(Entry.Plc.p_online_plain);


            //act
            await primitives.WriteAsync();
            var p = await primitives.OnlineToPlainAsync();

            //assert
            Assert.Equal(primitives.myBOOL.Cyclic, p.myBOOL);
            Assert.Equal(primitives.myBYTE.Cyclic, p.myBYTE);
            Assert.Equal(primitives.myWORD.Cyclic, p.myWORD);
            Assert.Equal(primitives.myDWORD.Cyclic, p.myDWORD);
            Assert.Equal(primitives.myLWORD.Cyclic, p.myLWORD);
            Assert.Equal(primitives.mySINT.Cyclic, p.mySINT);
            Assert.Equal(primitives.myINT.Cyclic, p.myINT);
            Assert.Equal(primitives.myDINT.Cyclic, p.myDINT);
            Assert.Equal(primitives.myLINT.Cyclic, p.myLINT);
            Assert.Equal(primitives.myUSINT.Cyclic, p.myUSINT);
            Assert.Equal(primitives.myUINT.Cyclic, p.myUINT);
            Assert.Equal(primitives.myUDINT.Cyclic, p.myUDINT);
            Assert.Equal(primitives.myULINT.Cyclic, p.myULINT);
            Assert.Equal(primitives.myREAL.Cyclic, p.myREAL);
            Assert.Equal(primitives.myLREAL.Cyclic, p.myLREAL);
            Assert.Equal(primitives.myTIME.Cyclic, p.myTIME);
            Assert.Equal(primitives.myLTIME.Cyclic, p.myLTIME);
            Assert.Equal(primitives.myDATE.Cyclic, p.myDATE);
            Assert.Equal(primitives.myTIME_OF_DAY.Cyclic, p.myTIME_OF_DAY);
            Assert.Equal(primitives.myDATE_AND_TIME.Cyclic, p.myDATE_AND_TIME);
            Assert.Equal(primitives.mySTRING.Cyclic, p.mySTRING);
            Assert.Equal(primitives.myWSTRING.Cyclic, p.myWSTRING);
            Assert.Equal(primitives.myEnum.Cyclic, (int)p.myEnum);

        }

        [Fact]
        public async Task PlainToOnline_primitives_should_copy_entire_structure()
        {
            //arrange
            var primitives = Entry.Plc.p_plain_online;
            var p = new Pocos.all_primitives();
            p = SetPlain(p);
           

            //act
            await primitives.PlainToOnlineAsync(p);
            await primitives.WriteAsync();
            await primitives.ReadAsync();



            //assert
            Assert.Equal(p.myBOOL, primitives.myBOOL.Cyclic);
            Assert.Equal(p.myBYTE, primitives.myBYTE.Cyclic);
            Assert.Equal(p.myWORD, primitives.myWORD.Cyclic);
            Assert.Equal(p.myDWORD, primitives.myDWORD.Cyclic);
            Assert.Equal(p.myLWORD, primitives.myLWORD.Cyclic);
            Assert.Equal(p.mySINT, primitives.mySINT.Cyclic);
            Assert.Equal(p.myINT, primitives.myINT.Cyclic);
            Assert.Equal(p.myDINT, primitives.myDINT.Cyclic);
            Assert.Equal(p.myLINT, primitives.myLINT.Cyclic);
            Assert.Equal(p.myUSINT, primitives.myUSINT.Cyclic);
            Assert.Equal(p.myUINT, primitives.myUINT.Cyclic);
            Assert.Equal(p.myUDINT, primitives.myUDINT.Cyclic);
            Assert.Equal(p.myULINT, primitives.myULINT.Cyclic);
            Assert.Equal(p.myREAL, primitives.myREAL.Cyclic);
            Assert.Equal(p.myLREAL, primitives.myLREAL.Cyclic);
            Assert.Equal(p.myTIME, primitives.myTIME.Cyclic);
            Assert.Equal(p.myLTIME, primitives.myLTIME.Cyclic);
            Assert.Equal(p.myDATE, primitives.myDATE.Cyclic);
            Assert.Equal(p.myTIME_OF_DAY, primitives.myTIME_OF_DAY.Cyclic);
            Assert.Equal(p.myDATE_AND_TIME, primitives.myDATE_AND_TIME.Cyclic);
            Assert.Equal(p.mySTRING, primitives.mySTRING.Cyclic);
            Assert.Equal(p.myWSTRING, primitives.myWSTRING.Cyclic);
            Assert.Equal((int)p.myEnum, primitives.myEnum.Cyclic);

        }

        //PLAIN_TO_SHADOW/SHADOW_TO_PLAIN
        [Fact]
        public async Task ShadowToPlain_primitives_should_copy_entire_structure()
        {
            //arrange
            var primitives = SetShadow(Entry.Plc.p_shadow_plain);


            //act
            var p = await primitives.ShadowToPlainAsync();

            //assert
            Assert.Equal(primitives.myBOOL.Shadow, p.myBOOL);
            Assert.Equal(primitives.myBYTE.Shadow, p.myBYTE);
            Assert.Equal(primitives.myWORD.Shadow, p.myWORD);
            Assert.Equal(primitives.myDWORD.Shadow, p.myDWORD);
            Assert.Equal(primitives.myLWORD.Shadow, p.myLWORD);
            Assert.Equal(primitives.mySINT.Shadow, p.mySINT);
            Assert.Equal(primitives.myINT.Shadow, p.myINT);
            Assert.Equal(primitives.myDINT.Shadow, p.myDINT);
            Assert.Equal(primitives.myLINT.Shadow, p.myLINT);
            Assert.Equal(primitives.myUSINT.Shadow, p.myUSINT);
            Assert.Equal(primitives.myUINT.Shadow, p.myUINT);
            Assert.Equal(primitives.myUDINT.Shadow, p.myUDINT);
            Assert.Equal(primitives.myULINT.Shadow, p.myULINT);
            Assert.Equal(primitives.myREAL.Shadow, p.myREAL);
            Assert.Equal(primitives.myLREAL.Shadow, p.myLREAL);
            Assert.Equal(primitives.myTIME.Shadow, p.myTIME);
            Assert.Equal(primitives.myLTIME.Shadow, p.myLTIME);
            Assert.Equal(primitives.myDATE.Shadow, p.myDATE);
            Assert.Equal(primitives.myTIME_OF_DAY.Shadow, p.myTIME_OF_DAY);
            Assert.Equal(primitives.myDATE_AND_TIME.Shadow, p.myDATE_AND_TIME);
            Assert.Equal(primitives.mySTRING.Shadow, p.mySTRING);
            Assert.Equal(primitives.myWSTRING.Shadow, p.myWSTRING);
            Assert.Equal(primitives.myEnum.Shadow, (int)p.myEnum);

        }


        [Fact]
        public async Task PlainToShadow_primitives_should_copy_entire_structure()
        {
            //arrange
            var primitives = Entry.Plc.p_plain_shadow;
            var p = new Pocos.all_primitives();
            p = SetPlain(p);


            //act
            await primitives.PlainToShadowAsync(p);



            //assert
            Assert.Equal(p.myBOOL, primitives.myBOOL.Shadow);
            Assert.Equal(p.myBYTE, primitives.myBYTE.Shadow);
            Assert.Equal(p.myWORD, primitives.myWORD.Shadow);
            Assert.Equal(p.myDWORD, primitives.myDWORD.Shadow);
            Assert.Equal(p.myLWORD, primitives.myLWORD.Shadow);
            Assert.Equal(p.mySINT, primitives.mySINT.Shadow);
            Assert.Equal(p.myINT, primitives.myINT.Shadow);
            Assert.Equal(p.myDINT, primitives.myDINT.Shadow);
            Assert.Equal(p.myLINT, primitives.myLINT.Shadow);
            Assert.Equal(p.myUSINT, primitives.myUSINT.Shadow);
            Assert.Equal(p.myUINT, primitives.myUINT.Shadow);
            Assert.Equal(p.myUDINT, primitives.myUDINT.Shadow);
            Assert.Equal(p.myULINT, primitives.myULINT.Shadow);
            Assert.Equal(p.myREAL, primitives.myREAL.Shadow);
            Assert.Equal(p.myLREAL, primitives.myLREAL.Shadow);
            Assert.Equal(p.myTIME, primitives.myTIME.Shadow);
            Assert.Equal(p.myLTIME, primitives.myLTIME.Shadow);
            Assert.Equal(p.myDATE, primitives.myDATE.Shadow);
            Assert.Equal(p.myTIME_OF_DAY, primitives.myTIME_OF_DAY.Shadow);
            Assert.Equal(p.myDATE_AND_TIME, primitives.myDATE_AND_TIME.Shadow);
            Assert.Equal(p.mySTRING, primitives.mySTRING.Shadow);
            Assert.Equal(p.myWSTRING, primitives.myWSTRING.Shadow);
            Assert.Equal((int)p.myEnum, primitives.myEnum.Shadow);

        }


        private Pocos.all_primitives SetPlain(Pocos.all_primitives p)
        {
            var today = new DateTime(2023, 12, 15);
            var date = new DateOnly(1999, 2, 13);
            var timespan = new TimeSpan(13, 13, 13);

            p.myBOOL = true;
            p.myBYTE = 1;
            p.myWORD = 2;
            p.myDWORD = 3;
            p.myLWORD = 4;
            p.mySINT = 5;
            p.myINT = 6;
            p.myDINT = 7;
            p.myLINT = 8;
            p.myUSINT = 9;
            p.myUINT = 10;
            p.myUDINT = 11;
            p.myULINT = 12;
            p.myREAL = 13;
            p.myLREAL = 14;
            p.myTIME = timespan;
            p.myLTIME = timespan;
            p.myDATE = date;
            p.myTIME_OF_DAY = timespan;
            p.myDATE_AND_TIME = today;
            p.mySTRING = "anakin skywalker";
            p.myWSTRING = "anakin skywalker";
            p.myEnum = myEnum.UnAvailable;

            return p;
        }

        private all_primitives SetOnline(all_primitives p)
        {
            var today = new DateTime(2023,12,15);
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

    }
}