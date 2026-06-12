# integration-sandbox-showcase

## Purpose

Present `RenderableContentControl` capabilities (auto-rendering, layouts, shadow presentation, polling, localization, template overrides, presentation pipeline, control parameters) to PLC programmers through the integration sandbox Blazor app (`ix-integration-blazor`) in a Bootstrap-free, Tailwind + Operon UI with light/dark theming, runnable fully offline against a real PLC.

## Requirements

### Requirement: Bootstrap-free integration sandbox
The integration sandbox app (`ix-integration-blazor`) SHALL contain no Bootstrap artifacts: no Bootstrap CSS references (active or commented), no Bootstrap utility classes in markup, no `ix-bootstrap.bundle.min.js`, no jQuery, and no open-iconic (`oi oi-*`) icon classes.

#### Scenario: No Bootstrap classes in markup
- **WHEN** the project's `.razor` and `.cshtml` files are searched for Bootstrap class patterns (`navbar`, `btn btn-`, `form-control`, `container-fluid`, `nav-item`, `oi oi-`, `collapse`)
- **THEN** no matches are found

#### Scenario: No Bootstrap or jQuery scripts loaded
- **WHEN** the rendered page's script tags are inspected
- **THEN** neither `ix-bootstrap.bundle.min.js` nor `jquery-3.6.0.min.js` is loaded

### Requirement: Offline operation
The app SHALL run without internet access: all stylesheets, scripts, and fonts SHALL be served from the app itself or referenced package static assets, with no CDN or external URLs.

#### Scenario: App runs on an offline rig
- **WHEN** the app is served on a machine with no internet connectivity and a user navigates all pages
- **THEN** every page renders fully styled and functional, and the host page contains no external (CDN) resource references

#### Scenario: Code snippets render without runtime conversion
- **WHEN** a user opens a code panel on any demo page
- **THEN** the ST snippet is rendered from static pre-rendered content without invoking any JavaScript markdown converter

### Requirement: Tailwind + Operon app shell
The app shell (layout, navigation, header) SHALL be styled with Tailwind utilities and Operon components, loading Operon CSS from the `Inxton.Operon` package static assets and a locally compiled Tailwind stylesheet whose source imports the Operon CSS variables.

#### Scenario: Stylesheets loaded
- **WHEN** the app's host page is rendered
- **THEN** it links `/_content/Inxton.Operon/css/momentum.css` and the locally compiled `css/tailwind/tailwind.css`

#### Scenario: Navigation uses Operon icons
- **WHEN** the navigation is rendered
- **THEN** each destination is reachable via a `NavLink` decorated with an `Operon.Icons.HeroIcon` (no open-iconic spans)

### Requirement: Light and dark theming
The app SHALL support light, dark, and system theme modes driven by the Operon CSS variable system via a `data-theme` attribute, with a user-visible toggle in the header, and the selection SHALL persist across page reloads.

#### Scenario: User switches to dark mode
- **WHEN** the user selects dark mode from the theme toggle
- **THEN** the root element carries `data-theme="dark"` and pages, including rendered `RenderableContentControl` content, restyle to dark Operon colors

#### Scenario: Theme persists
- **WHEN** the user reloads the app after selecting a theme
- **THEN** the previously selected theme mode is applied

### Requirement: Story-structured navigation
The app SHALL organize all demos into four chapter pages in narrative order — Rendering (declare & render), Layouts (arrange), Customize, Integrate — reachable from a five-link navigation (Home + four chapters). Each demo SHALL be a section within its chapter, presented with a consistent showcase structure: a section title, a short description of the demonstrated capability, the live rendered control in a styled panel, and (where an ST snippet applies) a toggleable, styled code panel.

#### Scenario: Navigation shows the story
- **WHEN** the user views the navigation
- **THEN** it contains exactly Home, Rendering, Layouts, Customize, and Integrate in that order

#### Scenario: Layout section rendered
- **WHEN** a user opens the Layouts chapter
- **THEN** each layout (Stack, Wrap, Tabs, UniformGrid, GroupBox/Border) appears as a section with a description, the live `RenderableContentControl` output, and a control revealing the ST declaration snippet in a styled code panel

#### Scenario: All existing scenarios preserved as sections
- **WHEN** the user walks the four chapters
- **THEN** every pre-refurbishment demo remains reachable as a section: AutoRendering content (Rendering), Stacked/Wrapped/Tab layouts and rendering examples (Layouts), class injecting and localizations (Customize), Measurement, Shadow presentation, Arrays, and Polling (Integrate)

### Requirement: Capability coverage sections
The app SHALL include dedicated demo sections for RenderableContentControl capabilities currently undemonstrated, implemented without modifying the PLC twin projects: template override via the `PresentationTemplate` parameter (Customize chapter), the presentation-type pipeline (Base, Manual, Service, Control — Rendering chapter), and host-side control parameters (`HideLabel`, `Class`, `LayoutClass`, `LayoutChildrenClass` — Customize chapter).

#### Scenario: Gauge template override demo
- **WHEN** the user opens the template-override section of the Customize chapter
- **THEN** the same `Measurement` twin structure is shown rendered with its default template and with a custom app-defined SVG gauge template (needle from `Acquired`, scale from `Min`/`Max`, color from `Result`) selected via `PresentationTemplate`, side by side

#### Scenario: Presentation pipeline demo
- **WHEN** the user opens the presentation-pipeline section of the Rendering chapter
- **THEN** one twin object is rendered under Base, Manual, Service, and Control presentation types with an explanation of the fallback rules

#### Scenario: Control parameters demo
- **WHEN** the user opens the control-parameters section of the Customize chapter
- **THEN** a structure is rendered with and without `HideLabel` and with injected `Class`/`LayoutClass`/`LayoutChildrenClass` values, with visible difference

### Requirement: Landing page hero shows ST in, UI out
The landing page SHALL present a hero that pairs a static ST declaration with the live `RenderableContentControl` rendering of that same structure, communicating that the UI is declared rather than hand-written, and SHALL link to the four chapter pages.

#### Scenario: User opens the app root
- **WHEN** the user navigates to `/`
- **THEN** the hero shows the ST declaration of a twin structure side by side with its live rendered control, and chapter links to Rendering, Layouts, Customize, and Integrate

### Requirement: Culture selection preserved
The header SHALL retain the culture selector (en-US, sk-SK, es-ES) with unchanged switching behavior, styled with Tailwind instead of Bootstrap form classes.

#### Scenario: User changes culture
- **WHEN** the user selects a different culture in the header selector
- **THEN** the app navigates to the culture controller and reloads with the selected culture applied
