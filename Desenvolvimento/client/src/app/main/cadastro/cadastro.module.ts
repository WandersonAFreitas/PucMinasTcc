import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { MatTabsModule } from "@angular/material";
import { FuseSharedModule } from "@fuse/shared.module";
import { AcaoModule } from "./acao/acao.module";
import { EmpresaModule } from "./empresa/empresa.module";
import { PaisModule } from "./pais/pais.module";
import { EstadoModule } from "./estado/estado.module";
import { MunicipioModule } from "./municipio/municipio.module";
import { LogradouroModule } from "./logradouro/logradouro.module";
import { FluxoModule } from "./fluxo/fluxo.module";
import { TipoAnexoModule } from "./tipoanexo/tipoanexo.module";
import { SituacaoModule } from "./situacao/situacao.module";
import { AssuntoModule } from "./assunto/assunto.module";
import { ParametroModule } from "./parametro/parametro.module";
import { ProcessoModule } from "./processo/processo.module";
import { TipoInsumoModule } from "./tipoinsumo/tipoinsumo.module";
import { InsumoModule } from "./insumo/insumo.module";
import { UnidadeMedidaModule } from "./unidademedida/unidademedida.module";
import { MarcaModule } from "./marca/marca.module";
import { FornecedorModule } from "./fornecedor/fornecedor.module";
import { HomeModule } from "./home/home.module";
import { MonitoramentoBarragemModule } from "./monitoramentobarragem/monitoramentobarragem.module";
import { BarragemModule } from "./barragem/barragem.module";
import { NivelMonitorarmentoModule } from "./nivelmonitoramento/nivelmonitoramento.module";
import { SensorModule } from "./sensor/sensor.module";
import { ConsultoriaModule } from "./consultoria/consultoria.module";
import { TipoMonitoramentomentoModule } from "./tipomonitoramento/tipomonitoramento.module";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MatTabsModule,
    FuseSharedModule,
    AcaoModule,
    AssuntoModule,
    FluxoModule,
    EmpresaModule,
    SituacaoModule,
    ParametroModule,
    ProcessoModule,
    PaisModule,
    EstadoModule,
    MunicipioModule,
    TipoAnexoModule,
    LogradouroModule,
    TipoInsumoModule,
    InsumoModule,
    UnidadeMedidaModule,
    MarcaModule,
    FornecedorModule,
    HomeModule,
    MonitoramentoBarragemModule,
    BarragemModule,
    NivelMonitorarmentoModule,
    SensorModule,
    ConsultoriaModule,
    TipoMonitoramentomentoModule
  ],
  declarations: [
  ],
  providers: [
  ],
})
export class CadastroModule { }
