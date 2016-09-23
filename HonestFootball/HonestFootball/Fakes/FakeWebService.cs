
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
                new Comment { Id=1, Text="Jim Baxter" },
                new Comment { Id=2, Text="Jeff Baxter" },
                new Comment { Id=3, Text="Joe Baxter" },
            };
        }
    }
}
