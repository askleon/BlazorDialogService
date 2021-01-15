using Microsoft.Extensions.DependencyInjection;

namespace BlazorDialogService
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddBlazorDialogService(this IServiceCollection service)
        {
            return service.AddScoped<DialogService>();
        }
    }
}