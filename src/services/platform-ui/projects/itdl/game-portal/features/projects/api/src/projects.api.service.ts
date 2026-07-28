
import { Injectable } from '@angular/core';
import { Project } from '@itdl/game-portal/features/projects/entities';
import { EntityApiService } from '@itdl/game-portal/http';

@Injectable()
export class GpllProjectsApiService extends EntityApiService<string, Project> {
    protected getApiUrl(): string {
        return '/api/v1/Projects';
    }
}
