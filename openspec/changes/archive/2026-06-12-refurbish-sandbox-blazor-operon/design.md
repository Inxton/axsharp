# Design — Refurbish integration sandbox Blazor app to Operon + Tailwind

## Context

`src/sanbox/integration/ix-integration-blazor` is a Blazor Server app that demonstrates `RenderableContentControl` against the `ix-integration-plc` twin. It is mid-migration:

- `Pages/_Layout.cshtml:11` — Bootstrap CSS link is commented out; `css/tailwind/tailwind.css` (compiled Tailwind 4 output) is linked instead.
- `Pages/_Layout.cshtml:31-32` — `ix-bootstrap.bundle.min.js` and jQuery are still loaded from `AXSharp.Presentation.Blazor.Controls` static assets.
- `Shared/NavMenu.razor`, `Shared/TopRow.razor`, `Shared/MainLayout.razor` — still pure Bootstrap/Blazor-template markup (`navbar`, `collapse`, `nav-item`, `form-control`, `oi oi-*` open-iconic icons, `page`/`sidebar` CSS).
- Pages use scattered Bootstrap classes: `btn btn-outline-primary` (StackedLayout, TabLayout, WrappedLayout - Copy), `container`/`row` (AutoRendering, RenderingExamples), `card` (Components/IxComponentView).
- Tailwind build pipeline exists: `package.json` (@tailwindcss/cli), `tailwind.ps1` (`App.css` → `wwwroot/css/tailwind/tailwind.css`), `wwwroot/css/operon-variables.css` already copied in but not imported.
- `Inxton.Operon` NuGet package already referenced in the csproj; the renderable-content templates in `AXSharp.Presentation.Blazor.Controls` already render through `Operon.Components.BaseInput`, so the auto-generated UI is Operon-styled — only the host shell and page chrome are not.

The reference implementation is `src/AXSharp.blazor/tests/sandbox/IxBlazor.App`, which completed the same migration: `_Host.cshtml` loads Operon CSS from `/_content/Inxton.Operon/css/momentum.css`, `MainLayout`/`NavMenu` are Tailwind, `Theme.razor` + `wwwroot/js/theme.js` implement light/dark/system toggle via `data-theme` attribute, and `Operon.Icons.HeroIcon` is used for icons.

## Goals / Non-Goals

**Audience:** customer demo aimed at PLC programmers — people who will read the ST declarations and judge whether RenderableContentControl saves them UI work. Pages must pair the live control with the declaration that produced it.

**Goals:**
- Zero Bootstrap: no Bootstrap classes, no `ix-bootstrap.bundle.min.js`, no jQuery, no open-iconic.
- App shell and every demo page styled with Tailwind utilities, preferring Operon components (`BaseInput`, `Tab`/`TabPage`, `HeroIcon`) and Operon CSS variables over hand-rolled styles.
- Each page reads as a deliberate showcase of one `RenderableContentControl` capability: title, short explanation, live control, and the ST/code snippet in a styled panel.
- Full capability coverage: add demo pages for template overrides (`PresentationTemplate`), the presentation-type pipeline (Base/Manual/Service), and control parameters (`HideLabel`, `Class`/`LayoutClass`/`LayoutChildrenClass`) — implemented host-side only.
- Fully offline: no CDN or external network dependency; demo must run on the hardware rig without internet.
- Light/dark theming via Operon variables with a visible toggle.
- Compelling first impression: a redesigned Index/landing page that explains what RenderableContentControl does and links to the scenarios.

**Non-Goals:**
- No changes to `AXSharp.Presentation.Blazor.Controls`, the connector, code generators, or PLC twin projects (`ix-integration-plc`, `ix-integration-library`) — new scenarios are chosen so existing twin types suffice.
- No work on `IxBlazor.App` or the `probes/ax-blazor-1` sandboxes; no sandbox convergence effort.
- No automated visual-regression tooling.

## Decisions

1. **Load Operon CSS from the NuGet static assets, mirror IxBlazor.App's `_Host.cshtml`.**
   `_Layout.cshtml` will link `/_content/Inxton.Operon/css/momentum.css` plus the locally compiled `css/tailwind/tailwind.css` for page-level utilities. Alternative considered: compiling everything into one local CSS — rejected because IxBlazor.App's split is the proven pattern and keeps Operon styling updatable via package bump.

2. **Keep the existing local Tailwind pipeline; fix its input.**
   `App.css` becomes the Tailwind source (`@import "tailwindcss"; @source "./"; @import "./operon-variables.css"; @custom-variant dark ...`), matching IxBlazor.App's `tailwind.css` source file. `tailwind.ps1` already targets `App.css` → `wwwroot/css/tailwind/tailwind.css`, so only the file content changes. The compiled output is committed, as elsewhere in the repo. Alternative: switch to MSBuild-integrated Tailwind — rejected as out of scope; repo convention is the ps1 + committed output.

3. **Port `Theme.razor` + `theme.js` from IxBlazor.App rather than reinventing.**
   Same `data-theme` attribute mechanism, same light/dark/system tri-state with `HeroIcon` sun/moon/computer-desktop icons. The Operon dark variant (`@custom-variant dark (&:where([data-theme=dark], [data-theme=dark] *))`) keys off this attribute.

4. **App shell layout: top navbar with five story links.**
   Replace the `page`/`sidebar` template structure with IxBlazor.App's pattern — a top header bar (brand, nav links, culture selector, theme toggle) and a centered `max-w-screen-xl` content container. Navigation is exactly five destinations in narrative order: Home, Rendering, Layouts, Customize, Integrate (see decision 10). No grouping taxonomy, no collapse toggle, no scrollable link row. Alternative: keep the sidebar and restyle it — rejected; the top-bar pattern matches the sibling sandbox and removes the collapse-toggle JS dependency.

5. **Shared `ShowcaseSection` scaffolding component with static code snippets — Showdown and its CDN script removed.**
   A small shared component (in `Shared/`) providing the repeated section chrome: section title, description slot, live-demo panel, and a collapsible "Code" panel. Chapter pages (decision 10) compose many of these. The code panel takes the ST snippet as a plain string (or `RenderFragment`) and renders it directly into a Tailwind-styled `<pre><code>` block — no runtime markdown conversion, no JS interop, no `showdown.min.js` from cdnjs. Rationale: the demo must run offline on the hardware rig, and a CDN script is a hard online dependency; the snippets are static text known at build time, so runtime conversion adds nothing. Alternative considered: self-hosting Showdown — still dead weight for static content.

6. **Icons: `Operon.Icons.HeroIcon` everywhere open-iconic was used.**
   Nav links get meaningful icons (home, squares for layouts, clock for polling, language for localizations, etc.) instead of the uniform `oi-plus`.

7. **Culture selector (`TopRow.razor`) becomes a Tailwind-styled `<select>` in the header.**
   Logic unchanged; only `form-control`/`ms-auto` classes replaced. Operon has no select component in use in the repo, so a plain styled select is acceptable.

8. **`WrappedLayout - Copy.razor` is deleted with the other standalone pages.**
   Its scenario (wrap layout + ST snippet) is salvaged as a section of `/layouts` per decision 10; no rename needed.

9. **New capability demos are host-side only — no ST/twin changes.**
   Coverage audit against the sibling sandbox found three gaps, all demonstrable with existing twin types:
   - **Template override**: a custom Razor template component in the app selected via `RenderableContentControl`'s `PresentationTemplate` parameter, shown side-by-side with the default rendering (see decision 11 for the gauge). This demos the override capability without the `RenderTemplateOverride` ST attribute, which would require PLC twin regeneration (explicitly out of scope).
   - **Presentation pipeline**: one twin (`Entry.Plc.test_example.ixcomponent_instance`, already used by RenderingExamples) rendered with Base/Manual/Service/Control presentations, with the fallback rules explained.
   - **Control parameters**: `HideLabel`, `Class`, `LayoutClass`, `LayoutChildrenClass` demonstrated on one structure with visible before/after (absorbs ClassInjecting).
   The PLC side already covers everything else (`weatherBase.st` has the full RenderIgnore matrix, `example.st` has all layout/group-layout combinations); demos expose, not extend, the twin.

10. **Information architecture: four chapter pages telling the story declare → render → arrange → customize → integrate.**
    Instead of restyling ~13 standalone pages, scenarios are consolidated into sections on four chapter pages, in narrative order:
    - `/rendering` — Control vs Display, ReadOnce/ReadOnly, RenderIgnore matrix, presentation pipeline (absorbs AutoRendering + new presentations demo).
    - `/layouts` — Stack, Wrap, Tabs, UniformGrid, GroupBox/Border, each with its ST declaration (absorbs Stacked/Wrapped/TabLayout + RenderingExamples layout sections).
    - `/customize` — gauge template override, control parameters, localizations (absorbs ClassInjecting + Localizations + new override demo).
    - `/integrate` — shadow properties, polling intervals, arrays, custom views from external libraries, measurement composed views (absorbs ShadowProperties + Polling + Arrays + RenderingExamples external views + Measurement).
    Old routes and page files are deleted, including the `Test.razor` scratch page; nothing external links to a demo app, so no redirects. Rationale: a customer demo is walked through linearly — chapter pages mirror the pitch script, and five nav links beat a 13-item menu. Alternative: keep one-page-per-scenario with grouped nav — rejected per IA decision; sections give each capability context within the story.

11. **Landing hero: "ST in → UI out"; gauge override as the customization money-shot.**
    - `/` hero shows a static ST declaration of the `weather` class next to the live `RenderableContentControl` rendering of that same struct with live PLC data, headline to the effect of "this UI was declared, not written", followed by chapter cards linking the story.
    - The custom override template is an inline-SVG gauge bound to `MeasurementExample.Measurement` (`measurement.st`): needle = `Acquired`, scale = `Min`..`Max`, color from the `Result` enum (None/Passed/Failed), styled with Tailwind/Operon variables. Chosen over the originally floated weather-temperature gauge because `weather` has no temperature member (adding one would violate the no-PLC-changes constraint) and `Measurement.Simulate()` mutates values cyclically, so the gauge moves live during the demo. No charting library — keeps the zero-external-dependency/offline guarantee and shows that a template is just a Razor component. Same twin rendered default (left) vs gauge (right) makes the override capability legible at a glance for a programmer audience.

## Risks / Trade-offs

- [Operon package version pin (`Inxton.Operon` alpha) may lack a component the design assumes] → All assumed components (`BaseInput`, `Tab`, `HeroIcon`) are already consumed by IxBlazor.App against the same centrally-managed version; no new component dependencies are introduced.
- [Removing jQuery/ix-bootstrap bundle could break a template that silently depends on it] → The renderable templates were already migrated to Operon (no Bootstrap JS usage); verify by exercising every page after removal. IxBlazor.App runs without these scripts.
- [Visual verification requires a running PLC/WebAPI connector; hardware rig has warm-up flakes] → Pages render their chrome without a live PLC (controls show empty/default values); shell and styling can be verified without hardware. Full data verification done against the rig when available.
- [Committed compiled CSS can drift from source] → tasks include regenerating `tailwind.css` as the final step after all class changes.

## Migration Plan

Single PR on `sandbox-app-refurbishment`. No deployment concerns (demo app). Rollback = revert the PR.

## Open Questions

- None. Nav grouping resolved by decision 10 (five story links).
