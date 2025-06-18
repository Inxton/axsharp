// ix-integration-blazor
// Copyright (c) 2023 MTS spol. s r.o.,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/inxton/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/inxton/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/inxton/axsharp/blob/master/notices.md

using System.Globalization;
using AXSharp.Connector;
using AXSharp.Presentation.Blazor.Services;
using ix_integration_blazor.Data;
using ix_integration_plc;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Text.RegularExpressions;

namespace ix_integration_blazor
{
    public class Program
    {
        private static WebApplication App { get; set; }

        public static void Main(string[] args)
        {
            //ix_integration_plc.PlcTranslator.Instance.SetLocalizationResource(typeof(ix_integration_plc.ResourcesOverrride.OverridePlcStringResources));

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddIxBlazorServices();
            builder.Services.AddLocalization();

            var app = builder.Build();

            App = app;

            Entry.Plc.Connector.BuildAndStart().ReadWriteCycleDelay = 10;
            Entry.Plc.Connector.BuildAndStart().SubscriptionMode = ReadSubscriptionMode.Polling;
            Entry.Plc.Connector.ExceptionBehaviour = CommExceptionBehaviour.Ignore;


            Entry.Plc.Connector.SetLoggerConfiguration(new LoggerConfiguration()
                .WriteTo
                .Console()
                //.WriteTo
                //.File($"connector.log",
                //    outputTemplate: "{Timestamp:yyyy-MMM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}",
                //    fileSizeLimitBytes: 100000)
                .MinimumLevel.Debug()
                .CreateLogger());

            //Entry.Plc.Connector.Translator.SetLocalizationResource(Entry.Plc.GetType(), "Properties.PlcStringResources");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            var supportedCultures = new[] { "en-US", "sk-SK", "es-ES" };
            var localizationOptions = new RequestLocalizationOptions()
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}