using System.Reflection;

namespace RoomHub.Api.Common.Cqrs;

public static class HandlerRegistrationExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services, Assembly assembly)
    {
        var handlerInterfaces = new[]
        {
            typeof(ICommandHandler<,>),
            typeof(IQueryHandler<,>)
        };

        foreach (var type in assembly.GetTypes())
        {
            if (type.IsAbstract || type.IsInterface)
                continue;

            foreach (var implementedInterface in type.GetInterfaces())
            {
                if (implementedInterface.IsGenericType &&
                    handlerInterfaces.Contains(implementedInterface.GetGenericTypeDefinition()))
                {
                    services.AddScoped(implementedInterface, type);
                }
            }
        }

        return services;
    }
}
