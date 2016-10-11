
using System.Threading.Tasks;
using HonestFootball.Models;

namespace HonestFootball.Fakes
{
    public class FakeWebService
    {
        public int SleepDuration { get; set; }

        public FakeWebService()
        {
            SleepDuration = 1;
        }
        private Task Sleep()
        {
            return Task.Delay(SleepDuration);
        }

        public async Task<Comment[]> GetComments()
        {
            await Sleep();

            return new[]
            {
                new Comment { Id=1, Text="Jim Baxter", Score="Everton 4, Fulchester 0", Time="57" },
                new Comment { Id=2, Text="Jeff Baxter", Score="Everton 5, Fulchester 0", Time="59" },
                new Comment { Id=3, Text="Joe Baxter", Score="Everton 5, Fulchester 0", Time="61" },
            };
        }
    }
}
