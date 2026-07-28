import { Injectable } from '@angular/core';
import { ProjectItemContainer } from '@itdl/game-portal/features/project-item-container/entities';
import { EntityApiService } from '@itdl/game-portal/http';

@Injectable()
export class GpllProjectItemContainerApiService extends EntityApiService<string, ProjectItemContainer> {
    protected getApiUrl(): string {
        return '/api/v1/Containers';
    }
}
