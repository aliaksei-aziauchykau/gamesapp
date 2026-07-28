import { Entity } from '@itdl/game-portal/http';
import { Event } from './event.model';
import { Post } from './post.model';

export class ProjectItemContainer extends Entity {
    public containerId!: string;
    public events: Event[] = [];
    public posts: Post[] = [];
    public childContainers: ProjectItemContainer[] = [];
}
