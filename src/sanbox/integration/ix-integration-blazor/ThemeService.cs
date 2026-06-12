using Microsoft.JSInterop;

namespace ix_integration_blazor
{
    public class ThemeService
    {
        private readonly IJSRuntime _js;

        public ThemeService(IJSRuntime js) => _js = js;

        public ValueTask InitAsync() =>
            _js.InvokeVoidAsync("themeManager.init");

        public ValueTask SetLightAsync() =>
            _js.InvokeVoidAsync("themeManager.setLight");

        public ValueTask SetDarkAsync() =>
            _js.InvokeVoidAsync("themeManager.setDark");

        public ValueTask SetSystemAsync() =>
            _js.InvokeVoidAsync("themeManager.setSystem");
    }
}
