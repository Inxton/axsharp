# Refurbish integration sandbox Blazor app to Operon + Tailwind

## Why

The integration sandbox app (`src/sanbox/integration/ix-integration-blazor`) is a **customer-facing demo for PLC programmers** — the primary demonstration of `RenderableContentControl` capabilities against a real PLC — but it is stuck mid-migration: Bootstrap CSS is commented out while the layout, navigation, and pages still use Bootstrap markup (`navbar`, `btn`, `container/row`, `form-control`, open-iconic icons), and `ix-bootstrap.bundle.min.js` + jQuery are still loaded. The result is a broken, unstyled demo that misrepresents what AXSharp + Operon can do. The renderable-content templates in `AXSharp.Presentation.Blazor.Controls` already render through Operon components, so the host app is the only piece left behind. The app also under-sells the control: template overrides, the presentation-type pipeline (Base/Manual/Service), and host-side control parameters (`HideLabel`, `Class`/`LayoutClass`) have no dedicated demo pages.

## What Changes

- Remove all Bootstrap remnants from `ix-integration-blazor`: Bootstrap utility classes in `.razor` files, commented-out `bootstrap.min.css` link, `ix-bootstrap.bundle.min.js`, jQuery, and open-iconic icon classes (`oi oi-*`).
- Rebuild the app shell (`MainLayout`, `NavMenu`, `TopRow`, `_Layout.cshtml`) with Tailwind 4 utilities and Operon components (e.g. `HeroIcon` for nav icons, Operon variables for theming), following the patterns already proven in `src/AXSharp.blazor/tests/sandbox/IxBlazor.App`.
- Restructure the demo as a story told in four chapter pages — `/rendering` (declare & render), `/layouts` (arrange), `/customize` (customize), `/integrate` (integrate) — each composed of showcase sections (title, description, live control, ST snippet panel). All existing scenarios (AutoRendering, Measurement, Stacked/Wrapped/Tab layouts, ShadowProperties, RenderingExamples, Arrays, Polling, ClassInjecting, Localizations) are absorbed as sections; their standalone pages and routes are removed, along with the `Test.razor` scratch page.
- Redesign the landing page as an "ST in → UI out" hero: a static ST declaration side by side with the live `RenderableContentControl` rendering of that same struct, plus links to the four chapters.
- Add light/dark theme support using the Operon CSS variable system (`operon-variables.css` is already in `wwwroot`) with a theme toggle, mirroring `IxBlazor.App`'s `Theme.razor` + `theme.js` approach.
- Add demo sections for capabilities the app does not yet show, implemented host-side only (no PLC/ST changes): template override via the `PresentationTemplate` parameter with a custom SVG gauge template for the `Measurement` twin (live needle from `Acquired`, scale from `Min`/`Max`, color from `Result`), the presentation-type pipeline (Base/Manual/Service with fallback explained, using the existing `ixcomponent` twin), and control parameters (`HideLabel`, `Class`, `LayoutClass`, `LayoutChildrenClass` — consolidating/extending ClassInjecting).
- Replace runtime Showdown markdown conversion (CDN script + JS interop) with static pre-rendered code snippets, so the demo works fully offline on the hardware rig.
- Keep the existing Tailwind build pipeline (`tailwind.ps1`, `package.json`, `App.css` → `wwwroot/css/tailwind/tailwind.css`) and extend `App.css` to import Operon variables; regenerate the compiled CSS.
- Delete dead artifacts: `WrappedLayout - Copy.razor`, commented-out error UI in `_Layout.cshtml`.
- No changes to `AXSharp.Presentation.Blazor.Controls` templates, the connector, or the PLC twin projects (`ix-integration-plc`, `ix-integration-library`) — this is a host-app refurbishment only.

## Capabilities

### New Capabilities

- `integration-sandbox-showcase`: The integration sandbox Blazor app presents RenderableContentControl scenarios (auto-rendering, layouts, shadow presentation, polling, localization, template overrides, presentation pipeline, control parameters) in a Bootstrap-free, Tailwind + Operon UI with light/dark theming, runnable fully offline.

### Modified Capabilities

<!-- none — existing specs (cyclic-write-delivery, priority-request-dispatching) are unaffected -->

## Impact

- **Affected code**: `src/sanbox/integration/ix-integration-blazor` only — `Pages/*.razor` (including new capability demo pages), `Pages/_Layout.cshtml`, `Shared/*.razor`, `Components/*.razor` (plus new custom-template components), `wwwroot/css/App.css`, compiled `wwwroot/css/tailwind/tailwind.css`, new `wwwroot/js/theme.js`.
- **Dependencies**: no new NuGet packages — `Inxton.Operon` is already referenced. Tailwind 4 toolchain already present (`@tailwindcss/cli` in `package.json`).
- **Removed**: jQuery, `ix-bootstrap.bundle.min.js`, and Showdown CDN script tags; Bootstrap/open-iconic class usage; runtime markdown conversion (replaced by static pre-rendered snippets — no external network dependency, demo runs offline on the rig).
- **Risk**: low — demo app, not shipped as a package. Main verification is visual (run app, check each page in light/dark) plus `dotnet build` of the sandbox solution. Note from prior work: this app is unrunnable against hardware pre-Operon; the refurbishment is what makes it runnable/demonstrable again.
