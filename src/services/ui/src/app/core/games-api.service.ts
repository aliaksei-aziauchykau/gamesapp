import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { API_BASE_URL } from './api.config';
import {
  GameDefinition,
  GameLobby,
  GameSession,
  ODataCollection,
} from '../shared/models/game-definition.model';

@Injectable({ providedIn: 'root' })
export class GamesApiService {
  private readonly http = inject(HttpClient);
  private readonly base = inject(API_BASE_URL);

  getGameDefinitions(): Observable<GameDefinition[]> {
    return this.http.get<GameDefinition[]>(`${this.base}/api/games/game-definitions`);
  }

  getGameDefinition(code: string): Observable<GameDefinition> {
    return this.http.get<GameDefinition>(`${this.base}/api/games/game-definitions/${code}`);
  }

  /** Legacy OData-shaped response from game-portal (`/api/v1/GameDefinitions`). */
  getGameDefinitionsOData(): Observable<GameDefinition[]> {
    return this.http
      .get<ODataCollection<GameDefinition>>(`${this.base}/api/v1/GameDefinitions`)
      .pipe(map((x) => x.value ?? []));
  }

  getGameSessions(): Observable<GameSession[]> {
    return this.http.get<GameSession[]>(`${this.base}/api/games/sessions`);
  }

  getLobbies(): Observable<GameLobby[]> {
    return this.http.get<GameLobby[]>(`${this.base}/api/games/lobbies`);
  }

  getStatus(): Observable<{ application: string; utcNow: string; environmentName: string }> {
    return this.http.get<{ application: string; utcNow: string; environmentName: string }>(
      `${this.base}/api/games/status`,
    );
  }
}
