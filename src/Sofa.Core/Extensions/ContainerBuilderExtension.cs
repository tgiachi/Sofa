using Microsoft.Extensions.DependencyInjection;
using Sofa.Core.Interfaces.Modules;

namespace Sofa.Core.Extensions;

public static class ContainerBuilderExtension
{
    public static IServiceCollection AddModule<TModule>(this IServiceCollection serviceCollection)
        where TModule : IContainerModule
    {
        var module = Activator.CreateInstance<TModule>();
        if (module == null)
        {
            throw new InvalidOperationException($"Could not create an instance of {typeof(TModule).FullName}");
        }

        module.Load(serviceCollection);
        return serviceCollection;
    }
}
