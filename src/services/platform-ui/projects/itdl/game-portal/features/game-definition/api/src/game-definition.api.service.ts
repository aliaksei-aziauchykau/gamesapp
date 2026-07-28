import { Injectable } from '@angular/core';
import { ClientDataToken, ServerDataToken } from '@itdl/game-portal/common';
import { GameDefinition } from '@itdl/game-portal/features/game-definition/entities';
import { EntityApiService } from '@itdl/game-portal/http';
import { ClientSideDataByToken, ServerSideDataByToken } from '@itdl/shared/common';
import { Observable, of } from 'rxjs';

@Injectable()
export class GpllGameDefinitionApiService extends EntityApiService<number, GameDefinition> {
    protected getApiUrl(): string {
        return '/api/v1/GameDefinitions';
    }

    @ServerSideDataByToken(ServerDataToken, (dataProvider) => dataProvider.gamedefs, (data) => of(data))
    @ClientSideDataByToken(ClientDataToken, (dataProvider) => dataProvider.gamedefs, (data) => of(data))
    public override getAll(): Observable<GameDefinition[]> {
        return super.getAll();
    }
}
