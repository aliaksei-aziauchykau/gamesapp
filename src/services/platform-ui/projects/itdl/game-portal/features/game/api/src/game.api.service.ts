import { Injectable } from '@angular/core';
import { Game, GameCreateDto } from '@itdl/game-portal/features/game/entities';
import { EntityApiService } from '@itdl/game-portal/http';

@Injectable()
export class GpllGameApiService extends EntityApiService<string, Game, GameCreateDto> {
    protected getApiUrl(): string {
        return '/api/v1/Games';
    }
}
