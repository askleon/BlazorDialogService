using Microsoft.Extensions.DependencyInjection;

namespace BlazorDialogService.DialogComponent
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddBlazorDialogService(this IServiceCollection service)
        {
            return service.AddScoped<DialogService>();
        }
    }
}