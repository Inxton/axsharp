/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './**/*.{razor,html,cshtml}',
  ],
  theme: {
    extend: {},
  },
  plugins: [],
  // Disable Tailwind's preflight to avoid conflicts with existing styles during migration
  corePlugins: {
    preflight: false,
  }
}