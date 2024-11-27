// AXSharp.Presentation.Blazor
// Copyright (c) 2023 Peter Kurhajec (PTKu), MTS,  and Contributors. All Rights Reserved.
// Contributors: https://github.com/ix-ax/axsharp/graphs/contributors
// See the LICENSE file in the repository root for more information.
// https://github.com/ix-ax/axsharp/blob/dev/LICENSE
// Third party licenses: https://github.com/ix-ax/axsharp/blob/master/notices.md

using Microsoft.Extensions.DependencyInjection;


namespace AXSharp.Presentation.Blazor.Services
{
    /// <summary>
    /// Class containing DI services.
    /// </summary>
    public static class ServicesConfiguration
    {
        public static void AddIxBlazorServices(this IServiceCollection services)
        {
            services.AddSingleton<ComponentService>();
            services.AddSingleton<AttributesHandler>();
            services.AddScoped<ViewModelCacheService>();
        }
    }
    
}
