using AXSharp.Connector;
namespace integrated.tests
{
    public class GH_ISSUE_183
    {

        public GH_ISSUE_183()
        {
#if NET6_0
            Task.Delay(250).Wait();
#endif

#if NET7_0
            Task.Delay(500).Wait();
#endif
        }

        [Fact]
        public async  Task ShouldReadNonZeroBasedArray()
        {
            var monster = Entry.Plc.GH_ISSUE_183;
            var read = await monster.ReadAsync();
        }
    }
}