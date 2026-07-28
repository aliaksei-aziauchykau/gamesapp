import { ProjectItemContainer } from './project-item-container.model';

export class Event {
    eventId!: string;
    name!: string;
    description!: string;
    location!: string;
    startDate!: Date;
    endDate!: Date;
    containerId!: string;
    container!: ProjectItemContainer;
    viewComponent?: string;
}
