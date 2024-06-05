using Microsoft.Extensions.DependencyInjection;
using Sofa.Core.Interfaces.Modules;
using Sofa.Database.Impl.Base;
using Sofa.Database.Interfaces;

namespace Sofa.Database.Modules;

public class DatabaseModule : IContainerModule
{
    public void Load(IServiceCollection services)
    {
        services.AddSingleton(typeof(IDataAccess<>), typeof(AbstractDataAccess<>));
    }
}
