
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
                new Comment { Id=1, EventComment="Jim Baxter", Score="Everton 4, Fulchester 0", Minute="57" },
                new Comment { Id=2, EventComment="Jeff Baxter", Score="Everton 5, Fulchester 0", Minute="59" },
                new Comment { Id=3, EventComment="Joe Baxter", Score="Everton 5, Fulchester 0", Minute="61" },
            };
        }
    }
}
