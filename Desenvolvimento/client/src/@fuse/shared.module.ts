import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { MAT_MOMENT_DATE_FORMATS, MomentDateAdapter } from '@angular/material-moment-adapter';
import { SimplePageScrollService } from 'ng2-simple-page-scroll';

import { NgxEditorModule } from 'ngx-editor';
import { FuseDirectivesModule } from '@fuse/directives/directives';
import { FusePipesModule } from '@fuse/pipes/pipes.module';
import { AppConfirmService } from './services/app-confirm/app-confirm.service';
import { AppLoaderService } from './services/app-loader/app-loader.service';
import { AppAlertService } from './services/app-alert/app-alert.service';
import { AppComfirmComponent } from './services/app-confirm/app-confirm.component';
import { AppLoaderComponent } from './services/app-loader/app-loader.component';
import { AppAlertComponent } from './services/app-alert/app-alert.component';
import { MaterialModule } from './material.module';
import { AddNewRowComponent } from './components/custom-mat-table/add-new-row/add-new-row.component';
import { EmpresaTabAssuntoDetailComponent } from './components/empresa/assunto/detail/assunto-detail.component';
import { EnderecoListComponent } from 'app/main/cadastro/endereco/endereco-list/endereco-list.component';
import { EnderecoDetailComponent } from 'app/main/cadastro/endereco/endereco-detail/endereco-detail.component';
import { InputSelectComponent } from './components/inputs/select/select.component';
import { InputFilterAutocompleteComponent } from './components/inputs/filter-autocomplete/filter-autocomplete.component';
import { InputAutocompleteComponent } from './components/inputs/autocomplete/autocomplete.component';
import { ReadMoreComponent } from './components/inputs/app-read-more/app-read-more.component';
import { EnderecoService } from 'app/main/cadastro/endereco/endereco.service';
import { ProcessoTabTramiteDetailComponent } from 'app/main/cadastro/processo/processo-tabs/tramite/tramite-detail/tramite-detail.component';
import { ProcessoTabAutorDetailComponent } from 'app/main/cadastro/processo/processo-tabs/autor/autor-detail/autor-detail.component';
import { ProcessoAtribuirComponent } from 'app/main/cadastro/processo/processo-atribuir/processo-atribuir.component';

const classesToInclude = [
    AppComfirmComponent,
    AppLoaderComponent,
    AppAlertComponent,
    AddNewRowComponent,
    EmpresaTabAssuntoDetailComponent,
    EnderecoListComponent,
    ReadMoreComponent,
    InputSelectComponent,
    InputFilterAutocompleteComponent,
    InputAutocompleteComponent,
    EnderecoDetailComponent,
    ProcessoTabTramiteDetailComponent,
    ProcessoTabAutorDetailComponent,
    ProcessoAtribuirComponent
];

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        MaterialModule,
        FlexLayoutModule,
        NgxEditorModule,
        FuseDirectivesModule,
        FusePipesModule,
        AngularFontAwesomeModule 
    ],
    entryComponents: [
        AppComfirmComponent,
        AppLoaderComponent,
        AppAlertComponent,
        AddNewRowComponent,
        EmpresaTabAssuntoDetailComponent,
        EnderecoDetailComponent,
        ProcessoTabTramiteDetailComponent,
        ProcessoTabAutorDetailComponent,
        ProcessoAtribuirComponent
    ],
    exports: [
        ...classesToInclude,
        MaterialModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        FlexLayoutModule,
        NgxEditorModule,
        FuseDirectivesModule,
        FusePipesModule,
        AngularFontAwesomeModule 
    ],
    declarations: classesToInclude,
    providers: [
        { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' },
        { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },
        { provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS },
        AppConfirmService,
        AppLoaderService,
        AppAlertService,
        SimplePageScrollService,
        EnderecoService
    ],
})
export class FuseSharedModule {
}
