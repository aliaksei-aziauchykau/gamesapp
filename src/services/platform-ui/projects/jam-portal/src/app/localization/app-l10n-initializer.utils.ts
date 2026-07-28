import { L10nLoader } from 'angular-l10n';

export function appL10nInitializer(l10nLoader: L10nLoader) {
    return () => l10nLoader.init();
}
