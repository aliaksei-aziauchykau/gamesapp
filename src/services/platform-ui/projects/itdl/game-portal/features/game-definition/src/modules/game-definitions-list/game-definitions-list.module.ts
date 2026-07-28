import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { GpllGameDefinitionModule } from '../../game-definition.module';
import { GpllGameDefinitionItemModule } from '../game-definition-item/game-definition-item.module';
import { GpllGameDefinitionsListComponent } from './game-definitions-list.component';

@NgModule({
    imports: [CommonModule, GpllGameDefinitionItemModule, GpllGameDefinitionModule],
    exports: [GpllGameDefinitionsListComponent],
    declarations: [GpllGameDefinitionsListComponent],
    providers: [],
})
export class GpllGameDefinitionsListModule {}
