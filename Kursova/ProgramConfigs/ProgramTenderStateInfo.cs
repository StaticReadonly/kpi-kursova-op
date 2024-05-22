using Services.Abstractions;
using Services.TenderStateInfoService;

namespace Kursova.ProgramConfigs
{
    public static class ProgramTenderStateInfo
    {
        public static void AddTenderStateInfo(IServiceCollection services)
        {
            services.AddSingleton<ITenderStateInfoService, TenderStateInfoService>();
        }
    }
}
