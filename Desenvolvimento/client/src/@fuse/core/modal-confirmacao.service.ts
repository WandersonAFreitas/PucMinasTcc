import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { ShellService } from './shell.service';

export interface ModalConfirmacaoModel {
  titulo: string;
  mensagem: string;
  callback: Function;
}

@Injectable()
export class ModalConfirmacaoService {

  constructor(public shellService: ShellService) { }

  show(modalConfirmacao: ModalConfirmacaoModel) {

    const config = {
      title: modalConfirmacao.titulo,
      content: modalConfirmacao.mensagem,
      actions: [
        { name: 'Cancelar', color: 'accent' },
        { name: 'Ok', closeResult: true, color: 'primary' }
      ],
      isAlert: false
    };

    // this.shellService.confirm().open(config).afterClosed().subscribe(
    //   (closeResult) => {
    //     if (closeResult) {
    //       modalConfirmacao.callback();
    //     }
    //   }
    // );
  }
}
