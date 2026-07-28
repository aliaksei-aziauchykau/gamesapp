export const getWindowOutsideAngular = () => {
    if (typeof window === 'undefined') return {} as unknown as Window & typeof globalThis;

    return window;
};
