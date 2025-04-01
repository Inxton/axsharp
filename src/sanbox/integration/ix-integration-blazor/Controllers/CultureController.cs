using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using AXSharp.Connector;

namespace ix_integration_blazor;

[Route("/[controller]")]
[ApiController]
public class CultureController : ControllerBase
{
    public async Task<ActionResult> ChangeCulture([FromQuery] string culture)
    {
        if (culture != null)
        {
            HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
            new RequestCulture(culture, culture)));

            CultureExtensions.Culture = new CultureInfo(culture);
            Connector.SetCulture(CultureExtensions.Culture);

        }

        return Redirect("/");
    }
}

public static class CultureExtensions
{
    public static CultureInfo Culture { get; set; } = CultureInfo.CurrentCulture ?? CultureInfo.DefaultThreadCurrentCulture ?? new CultureInfo("en-US");
}
