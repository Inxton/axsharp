# Tasks — Refurbish integration sandbox Blazor app to Operon + Tailwind

All paths relative to `src/sanbox/integration/ix-integration-blazor/` unless stated otherwise.

## 1. CSS pipeline and host page

- [x] 1.1 Rewrite `wwwroot/css/App.css` as the Tailwind 4 source: `@import "tailwindcss"`, `@source "./"`, `@import "./operon-variables.css"`, and the dark custom variant (`@custom-variant dark (&:where([data-theme=dark], [data-theme=dark] *))`) — mirror `src/AXSharp.blazor/tests/sandbox/IxBlazor.App/wwwroot/css/tailwind.css`; purge the old Blazor-template rules (`.page`/`.sidebar`/`.top-row`)
- [x] 1.2 Update `Pages/_Layout.cshtml`: add `/_content/Inxton.Operon/css/momentum.css` link, keep `css/tailwind/tailwind.css`, delete the commented Bootstrap link and commented error-UI block, remove `ix-bootstrap.bundle.min.js`, `jquery-3.6.0.min.js`, the Showdown CDN script, and the `converter` inline script (keep only blazor.server.js — offline requirement)
- [x] 1.3 Port theme manager: copy/adapt `IxBlazor.App/wwwroot/js/theme.js` into `wwwroot/js/theme.js`, reference it from `_Layout.cshtml`

## 2. App shell

- [x] 2.1 Rebuild `Shared/MainLayout.razor` with the IxBlazor.App pattern: top header bar + centered `max-w-screen-xl` Tailwind content container; drop the `page`/`sidebar` template structure
- [x] 2.2 Rebuild `Shared/NavMenu.razor` as a Tailwind top-nav with exactly five links in story order — Home, Rendering, Layouts, Customize, Integrate — each with a meaningful `Operon.Icons.HeroIcon`; no `collapse` toggle code
- [x] 2.3 Rebuild `Shared/TopRow.razor` (or fold into NavMenu): Tailwind-styled culture `<select>` (en-US, sk-SK, es-ES), logic unchanged
- [x] 2.4 Add `Shared/Theme.razor` theme toggle (light/dark/system, HeroIcon sun/moon/computer-desktop) wired to `theme.js`, placed in the header

## 3. Showcase scaffolding

- [x] 3.1 Create `Shared/ShowcaseSection.razor` component: section title, description slot, styled live-demo panel, optional toggleable code panel rendering a static ST snippet string into Tailwind-styled `pre/code` (no Showdown, no JS interop)
- [x] 3.2 Create the SVG gauge template component (e.g. `Components/Templates/MeasurementGaugeView.razor`): inline SVG arc gauge for the `Measurement` twin — needle from `Acquired`, scale from `Min`/`Max`, color from `Result` enum — Tailwind/Operon-variable styling, resolvable via `PresentationTemplate`

## 4. Landing page (`/`)

- [x] 4.1 Rebuild `Pages/Index.razor` as the "ST in → UI out" hero: static ST declaration of the `weather` class beside the live `RenderableContentControl` of `Entry.Plc.weather`, declared-not-written headline, chapter cards linking Rendering / Layouts / Customize / Integrate

## 5. Chapter pages (compose ShowcaseSection; salvage content + snippets from old pages)

- [x] 5.1 `Pages/Rendering.razor` (`/rendering`): sections for Control vs Display (`weather`), ReadOnce/ReadOnly (`weather_readOnce`/`weather_readOnly`), RenderIgnore matrix (`weather_wrapped`), and presentation pipeline — Base/Manual/Service/Control on `test_example.ixcomponent_instance` with fallback rules explained
- [x] 5.2 `Pages/Layouts.razor` (`/layouts`): sections for Stack, Wrap, Tabs, UniformGrid (`test_example.primitives_*`, `weather_*`) and GroupBox/Border (`test_example.test_groupbox`/`test_border`), each with its ST declaration snippet
- [x] 5.3 `Pages/Customize.razor` (`/customize`): sections for gauge template override on a `Measurement` twin (default vs `PresentationTemplate` gauge side by side — live-moving via `Simulate()` — with the custom-template snippet shown), control parameters (`HideLabel`, `Class`/`LayoutClass`/`LayoutChildrenClass` before/after), and localizations (salvage `Localizations.razor` content)
- [x] 5.4 `Pages/Integrate.razor` (`/integrate`): sections for shadow properties (salvage `ShadowProperties.razor` incl. sync buttons), polling intervals (salvage `Pollings/Polling.razor` interval comparison), arrays (salvage `Arrays.razor`), custom views from external libraries (salvage `RenderingExamples.razor` external-view part), and measurement composed views (salvage `Measurement.razor`)
- [x] 5.5 Restyle salvaged components: `Components/IxComponentView.razor` (replace Bootstrap `card`), `Components/MeasurementServiceView.razor`
- [x] 5.6 Delete superseded pages and routes: `AutoRendering.razor`, `StackedLayout.razor`, `WrappedLayout - Copy.razor`, `TabLayout.razor`, `RenderingExamples.razor`, `ShadowProperties.razor`, `Pollings/Polling.razor`, `Arrays.razor`, `ClassInjecting.razor`, `Localizations.razor`, `Measurement.razor`, `Test.razor`

## 6. Verification

- [x] 6.1 Regenerate compiled CSS: `npm install` (if needed) then run `tailwind.ps1` one-shot (without `--watch`) so `wwwroot/css/tailwind/tailwind.css` reflects all class changes; commit output
- [x] 6.2 Grep gate per spec: no `navbar|btn btn-|form-control|container-fluid|nav-item|oi oi-|bootstrap|showdown|cdnjs|jquery` matches in project `.razor`/`.cshtml` files
- [x] 6.3 `dotnet build` of `ix-integration-blazor.csproj` succeeds
- [x] 6.4 Run the app, walk the story (Home → Rendering → Layouts → Customize → Integrate) in light and dark mode; verify every pre-refurbishment scenario appears as a section, theme persists across reload, culture switch still works (PLC data optional — chrome verification sufficient without rig)
- [x] 6.5 Offline gate: with browser devtools network tab open, confirm no requests leave the app origin / `_content` package assets
