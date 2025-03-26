using AXSharp.Connector;
namespace integrated.tests
{
    public class GH_ISSUE_183
    {

        public GH_ISSUE_183()
        {
            Task.Delay(250).Wait();
        }

        [Fact]
        public async  Task ShouldReadNonZeroBasedArray()
        {
            var monster = Entry.Plc.GH_ISSUE_183;
            var read = await monster.ReadAsync();
        }
    }
}