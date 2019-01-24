using System;

namespace Probel.Gehova.Business.Services
{
    public interface IUpdateService
    {
        bool CheckVersion(Version version);
        void UpdateToVersion(Version version);
    }
}
