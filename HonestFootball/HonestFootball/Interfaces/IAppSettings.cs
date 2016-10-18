using HonestFootball.Models;

namespace HonestFootball.Interfaces
{
    public interface IAppSettings
    {
        bool IsSoundOn { get; set; }
        User User { get; set; }
        void Save();
    }
}
