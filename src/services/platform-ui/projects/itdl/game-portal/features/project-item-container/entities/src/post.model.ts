import { ProjectItemContainer } from './project-item-container.model';

export class Post {
    postId!: string;
    title!: string;
    content!: string;
    containerId!: string;
    container!: ProjectItemContainer;
    viewComponent?: string;
}
