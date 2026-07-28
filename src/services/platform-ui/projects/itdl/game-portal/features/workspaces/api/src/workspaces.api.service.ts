
import { Injectable } from '@angular/core';
import { Workspace } from '@itdl/game-portal/features/workspaces/entities';
import { EntityApiService } from '@itdl/game-portal/http';

@Injectable()
export class GpllWorkspacesApiService extends EntityApiService<string, Workspace> {
    protected getApiUrl(): string {
        return '/api/v1/Workspaces';
    }
}
