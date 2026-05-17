import { patchState, signalStore, withMethods, withState } from '@ngrx/signals';

export type SessionStatus = 'unknown' | 'anonymous' | 'authenticated';

type SessionState = {
  status: SessionStatus;
  displayName: string | null;
};

const initial: SessionState = {
  status: 'unknown',
  displayName: null,
};

export const SessionStore = signalStore(
  { providedIn: 'root' },
  withState(initial),
  withMethods((store) => ({
    setSession(status: SessionStatus, displayName: string | null) {
      patchState(store, { status, displayName });
    },
  })),
);
