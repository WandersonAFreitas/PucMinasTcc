import { NgModule, Optional, SkipSelf, ModuleWithProviders } from '@angular/core';

import { LocalStorageService } from './local-storage.service';
import { CredentialsService } from './credentials.service';
import { HttpBaseService } from './http-base.service';
import { ModalLogoffService } from './modal-logoff.service';
import { ShellService } from './shell.service';
import { ModalConfirmacaoService } from './modal-confirmacao.service';
import { RestfulService } from './restful.service';
import { AssuntoService } from './assunto.service';
import { ArquivoService } from './arquivo.service';
import { ProcessoAutorService } from './processo-autor.service';
import { SetorService } from 'app/main/cadastro/empresa/setor.service';
import { AutorService } from './autor.service';
import { TramiteService } from './tramite.service';
import { TramiteArquivoService } from './tramite-arquivo.service';
import { AssuntoArquivoService } from './assunto-arquivo.service';
import { UserService } from './user.service';
import { ProcessoService } from 'app/main/cadastro/processo/processo.service';

@NgModule({
  providers: [
    CredentialsService,
    HttpBaseService,
    LocalStorageService,
    ModalLogoffService,
    ShellService,
    ModalConfirmacaoService,
    RestfulService,
    AssuntoService,
    ArquivoService,
    ProcessoAutorService,
    SetorService,
    AutorService,
    TramiteService,
    TramiteArquivoService,
    AssuntoArquivoService,
    UserService,
    ProcessoService,
    { provide: 'API_URL_OVERRIDE', useValue: '' },
  ]
})
export class CoreModule {

  static forRoot(): ModuleWithProviders {
    return {
      ngModule: CoreModule
    };
  }

  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error('CoreModule já foi carregado. Importe-o somente do AppModule.');
    }
  }
}
