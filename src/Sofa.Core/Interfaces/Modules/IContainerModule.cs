using Microsoft.Extensions.DependencyInjection;

namespace Sofa.Core.Interfaces.Modules;

public interface IContainerModule
{
    void Load(IServiceCollection services);
}
