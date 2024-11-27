using AXSharp.Connector;
namespace integrated.tests
{
    public class ChangeDetectionTests
    {

        public ChangeDetectionTests()
        {
#if NET6_0
            Task.Delay(250).Wait();
#endif

#if NET7_0
            Task.Delay(500).Wait();
#endif

#if NET8_0
            Task.Delay(750).Wait();
#endif
        }

        [Fact]
        public async Task AnyChangeAsync_should_detect_change_on_a_complex_structure()
        {
            var monster = Entry.Plc.ChangeDetections;
            await monster.DriveA.Acc.SetAsync(0.0d);
            var plain = await monster.OnlineToPlainAsync();
            bool actual = await monster.AnyChangeAsync(plain);
            Assert.False(actual);
            await monster.DriveA.Acc.SetAsync(0.5d);
            await monster.ReadAsync();
            actual = await monster.AnyChangeAsync(plain);
            Assert.True(actual);
        }

        [Fact]
        public async Task AnyChangeAsync_should_not_detect_change_on_a_complex_structure()
        {
            var monster = Entry.Plc.ChangeDetections;
            await monster.DriveA.Acc.SetAsync(0.0d);
            var plain = await monster.OnlineToPlainAsync();
            bool actual = await monster.AnyChangeAsync(plain);
            Assert.False(actual);
            await monster.DriveA.Acc.SetAsync(0.0d);
            await monster.ReadAsync();
            actual = await monster.AnyChangeAsync(plain);
            Assert.False(actual);
        }

        [Fact]
        public async  Task DetectsAnyChangeAsync_should_detect_change_on_a_complex_structure()
        {
            var monster = Entry.Plc.ChangeDetections;
            await monster.DriveA.Acc.SetAsync(0.0d);
            var plain = await monster.OnlineToPlainAsync();
            bool actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.False(actual);
            await monster.DriveA.Acc.SetAsync(0.5d);
            await monster.ReadAsync();
            actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.True(actual);
        }

        [Fact]
        public async Task DetectsAnyChangeAsync_should_detect_change_on_a_complex_structure_on_array_primitive_type()
        {
            var monster = Entry.Plc.ChangeDetections;
            await monster.ArrayOfBytes[0].SetAsync(0x00);
            var plain = await monster.OnlineToPlainAsync();
            bool actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.False(actual);
            await monster.ArrayOfBytes[0].SetAsync(0x01);
            await monster.ReadAsync();
            actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.True(actual);
        }

        [Fact]
        public async Task DetectsAnyChangeAsync_should_detect_change_on_a_complex_structure_on_array_complex_type()
        {
            var monster = Entry.Plc.ChangeDetections;
            await monster.ArrayOfDrives[0].Acc.SetAsync(0x00);
            var plain = await monster.OnlineToPlainAsync();
            bool actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.False(actual);
            await monster.ArrayOfDrives[0].Acc.SetAsync(0x01);
            await monster.ReadAsync();
            actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.True(actual);
        }

        [Fact]
        public async Task DetectsAnyChangeAsync_should_detect_change_on_a_complex_structure_on_a_primitive_type()
        {
            var monster = Entry.Plc.ChangeDetections;
            await monster.Description.SetAsync("");
            var plain = await monster.OnlineToPlainAsync();
            bool actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.False(actual);
            await monster.Description.SetAsync("Hello");
            await monster.ReadAsync();
            actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.True(actual);
        }

        [Fact]
        public async Task DetectsAnyChangeAsync_should_not_detect_change_on_a_complex_structure()
        {
            var monster = Entry.Plc.ChangeDetections;
            await monster.DriveA.Acc.SetAsync(0.0d);
            var plain = await monster.OnlineToPlainAsync();
            bool actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.False(actual);
            await monster.DriveA.Acc.SetAsync(0.0d);
            await monster.ReadAsync();
            actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.False(actual);
        }

        [Fact]
        public async Task DetectsAnyChangeAsync_should_not_detect_change_on_a_complex_structure_on_array_primitive_type()
        {
            var monster = Entry.Plc.ChangeDetections;
            await monster.ArrayOfBytes[0].SetAsync(0x00);
            var plain = await monster.OnlineToPlainAsync();
            bool actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.False(actual);
            await monster.ArrayOfBytes[0].SetAsync(0x00);
            await monster.ReadAsync();
            actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.False(actual);
        }

        [Fact]
        public async Task DetectsAnyChangeAsync_should_not_detect_change_on_a_complex_structure_on_array_complex_type()
        {
            var monster = Entry.Plc.ChangeDetections;
            await monster.ArrayOfDrives[0].Acc.SetAsync(0x00);
            var plain = await monster.OnlineToPlainAsync();
            bool actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.False(actual);
            await monster.ArrayOfDrives[0].Acc.SetAsync(0x00);
            await monster.ReadAsync();
            actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.False(actual);
        }

        [Fact]
        public async Task DetectsAnyChangeAsync_should_not_detect_change_on_a_complex_structure_on_a_primitive_type()
        {
            var monster = Entry.Plc.ChangeDetections;
            await monster.Description.SetAsync("");
            var plain = await monster.OnlineToPlainAsync();
            bool actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.False(actual);
            await monster.Description.SetAsync("");
            await monster.ReadAsync();
            actual = await monster.DetectsAnyChangeAsync(plain);
            Assert.False(actual);
        }
    }
}