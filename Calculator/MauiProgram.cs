using Calculator.ViewModels;
using Calculator.Views;
using Calculator.Models;
using Calculator.Services;
using Calculator.Services.Data.Interfaces;
using Calculator.Services.Data.Repositories;
using Calculator.Services.Data.Services;
using Calculator.Services.Interfaces;
using Calculator.Services.Parsing;
using Calculator.Services.Parsing.Utility;
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

            builder.Services.AddSingleton<ITokenizer, Tokenizer>();
            builder.Services.AddSingleton<IParser<Variable>, VariableParser>();
            builder.Services.AddSingleton<IParser<Function>, FunctionParser>();
            builder.Services.AddSingleton<IParser<IEnumerable<Token>>, TokensParser>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services
                .ConfigureValidators()
                .ConfigurePresentation()
                .ConfigureDataServices();

            builder.Services.AddScoped<ICalculator, CalculatorService>();

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

        private static IServiceCollection ConfigureDataServices(this IServiceCollection services)
        {
            services.AddScoped<IFunctionRepository, FunctionRepository>();
            services.AddScoped<IVariableRepository, VariableRepository>();
            services.AddScoped<IDataService<Function>, FunctionService>();
            services.AddScoped<IDataService<Variable>, VariableService>();

            return services;
        }

        private static IServiceCollection ConfigurePresentation(this IServiceCollection services)
        {
            services.AddTransient<IAlertService, AlertService>();
            services.AddTransient<MainPage>();
            services.AddTransient<MainViewModel>();

            return services;
        }
    }
}
