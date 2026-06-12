window.themeManager = {
    init: () => {
        if (localStorage.theme === 'dark' || (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
            document.documentElement.setAttribute('data-theme', 'dark');
        } else {
            document.documentElement.setAttribute('data-theme', 'light');
        }
    },
    setLight: () => {
        localStorage.theme = 'light';
        document.documentElement.setAttribute('data-theme', 'light');
    },
    setDark: () => {
        localStorage.theme = 'dark';
        document.documentElement.setAttribute('data-theme', 'dark');
    },
    setSystem: () => {
        localStorage.removeItem('theme');
        // re-run init to pick up OS setting
        window.themeManager.init();
    }
};
