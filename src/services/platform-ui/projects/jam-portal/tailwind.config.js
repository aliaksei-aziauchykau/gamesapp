/** @type {import('tailwindcss').Config} */
module.exports = {
    content: {
        relative: true,
        files: ['./src/**/*.{ts,html}', '../itdl/game-portal/**/*.{ts,html}', '../itdl/shared/**/*.{ts,html}'],
    },
    theme: {
        extend: {},
    },
    plugins: [],
};
