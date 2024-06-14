using Calculator.Models;
using Calculator.Services.Interfaces;
using Calculator.Services.Parsing;
using Calculator.Services.Parsing.Utility;
using Microsoft.Extensions.Logging;

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

            return builder.Build();
        }
    }
}
