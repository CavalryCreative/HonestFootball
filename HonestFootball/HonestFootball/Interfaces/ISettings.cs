using HonestFootball.Models;

namespace HonestFootball.Interfaces
{
    public interface ISettings
    {
        bool IsSoundOn { get; set; }
        User User { get; set; }
        void Save();
    }
}
