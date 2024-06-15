using Calculator.ViewModels;
using Calculator.Views;
using Calculator.Services;
using Calculator.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Calculator.ViewModels.Interfaces;
using Calculator.Views.Services;

namespace Calculator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddTransient<IAlertService, AlertService>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.ConfigureValidators();

            return builder.Build();
        }

        private static IServiceCollection ConfigureValidators(this IServiceCollection services)
        {
            services.ConfigureSpecificValidators(typeof(IExpressionValidator))
                .ConfigureSpecificValidators(typeof(IFunctionValidator))
                .ConfigureSpecificValidators(typeof(IVariableValidator));


            return services.AddTransient<IValidatorManager, ValidatorManager>();
        }

        private static IServiceCollection ConfigureSpecificValidators(this IServiceCollection services, Type interfaceType)
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes();

            var iVariableValidatorImplementations = types
                .Where(type => interfaceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);

            foreach (var implementation in iVariableValidatorImplementations)
            {
                services.AddTransient(interfaceType, implementation);
            }

            return services;
        }
    }
}
