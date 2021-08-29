import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id: 'menu',
        title: 'MENU',
        translate: 'NAV.APPLICATIONS',
        roles: ['admin'],
        type: 'group',
        icon: 'pages',
        children: [
            {
                id: 'home',
                title: 'Home',
                translate: 'NAV.SAMPLE.TITLE',
                type: 'item',
                icon: '',
                url: '/cadastro/home',
                roles: ['admin']
            },
            {
                id: 'cadastro',
                title: 'Cadastro',
                type: 'collapsable',
                roles: ['admin'],
                icon: 'folder_shared',
                children: [
                     {
                        id: 'empresa',
                        title: 'Empresas',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'work',
                        url: '/cadastro/empresa',
                        roles: ['admin']
                    },
                    {
                        id: 'fluxo',
                        title: 'Fluxos',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'assignment_ind',
                        url: '/cadastro/fluxo',
                        roles: ['admin']
                    },
                    {
                        id: 'parametro',
                        title: 'Parâmetros',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'all_inbox',
                        url: '/cadastro/parametro',
                        roles: ['admin']
                    },
                    {
                        id: 'situacao',
                        title: 'Situações',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/cadastro/situacao',
                        roles: ['admin']
                    },
                    {
                        id: 'tipoanexo',
                        title: 'Tipos de anexos',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/cadastro/tipoanexo',
                        roles: ['admin']
                    },
                    {
                        id: 'acao',
                        title: 'Ações',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/cadastro/acao',
                        roles: ['admin']
                    },
                    {
                        id: 'insumo',
                        title: 'Insumo',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/cadastro/insumo',
                        roles: ['admin']
                    },
                    {
                        id: 'tipoinsumo',
                        title: 'Tipos de insumos',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/cadastro/tipoinsumo',
                        roles: ['admin']
                    },
                    {
                        id: 'barragem',
                        title: 'Barragem',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/barragem',
                        roles: ['admin']
                    },
                    {
                        id: 'consultoria',
                        title: 'Consultoria',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/consultoria',
                        roles: ['admin']
                    },
                    {
                        id: 'fornecedor',
                        title: 'Fornecedor',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/fornecedor',
                        roles: ['admin']
                    },
                    {
                        id: 'marca',
                        title: 'Marca',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/marca',
                        roles: ['admin']
                    },
                    {
                        id: 'Pessoa',
                        title: 'Pessoa',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/Pessoa',
                        roles: ['admin']
                    },
                    {
                        id: 'sensor',
                        title: 'Sesores',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/sensor',
                        roles: ['admin']
                    },
                    {
                        id: 'tipomanutencao',
                        title: 'Tipo de manutenção',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/tipomanutencao',
                        roles: ['admin']
                    },
                    {
                        id: 'tipominerio',
                        title: 'Tipo de minerio',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/tipominerio',
                        roles: ['admin']
                    },
                    {
                        id: 'tipomonitoramento',
                        title: 'Tipo de monitoramento',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/tipomonitoramento',
                        roles: ['admin']
                    },
                    {
                        id: 'turno',
                        title: 'Turno',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/turno',
                        roles: ['admin']
                    },
                    {
                        id: 'unidademedida',
                        title: 'Unidade de medida',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'filter_none',
                        url: '/unidademedida',
                        roles: ['admin']
                    },
                    {
                        id: 'endereco',
                        title: 'Endereços',
                        type: 'collapsable',
                        roles: ['admin'],
                        icon: 'person_pin_circle',
                        children: [
                            {
                                id: 'pais',
                                title: 'Países',
                                translate: 'NAV.SAMPLE.TITLE',
                                type: 'item',
                                icon: 'my_location',
                                url: '/cadastro/pais',
                                roles: ['admin']
                            },
                            {
                                id: 'estado',
                                title: 'Estados',
                                translate: 'NAV.SAMPLE.TITLE',
                                type: 'item',
                                icon: 'my_location',
                                url: '/cadastro/estado',
                                roles: ['admin']
                            },
                            {
                                id: 'municipio',
                                title: 'Municípios',
                                translate: 'NAV.SAMPLE.TITLE',
                                type: 'item',
                                icon: 'my_location',
                                url: '/cadastro/municipio',
                                roles: ['admin']
                            },
                            {
                                id: 'logradouro',
                                title: 'Logradouros',
                                translate: 'NAV.SAMPLE.TITLE',
                                type: 'item',
                                icon: 'my_location',
                                url: '/cadastro/logradouro',
                                roles: ['admin']
                            }
                        ]
                    },
                ]
            },
            {
                id: 'processos',
                title: 'Processos',
                type: 'collapsable',
                roles: ['admin'],
                icon: 'settings',
                children: [
                    {
                        id: 'meus_processos',
                        title: 'Meus processos',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'record_voice_over',
                        url: '/cadastro/processo/meusprocessos',
                        roles: ['admin']
                    },
                    {
                        id: 'nao_atribuidos',
                        title: 'Não atribuídos',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'record_voice_over',
                        url: '/cadastro/processo/naoatribuido',
                        roles: ['admin']
                    },
                    {
                        id: 'todos_processos',
                        title: 'Todos',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'record_voice_over',
                        url: '/cadastro/processo/todos',
                        roles: ['admin']
                    },
                ]
            },
            {
                id: 'monitoramento',
                title: 'Monitoramento',
                type: 'collapsable',
                roles: ['admin'],
                icon: 'settings',
                children: [
                    {
                        id: 'barragem',
                        title: 'Barragem',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'record_voice_over',
                        url: '/cadastro/monitoramentobarragem',
                        roles: ['admin']
                    },
                    {
                        id: 'insumos',
                        title: 'Insumos',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'record_voice_over',
                        url: '/insumos',
                        roles: ['admin']
                    }
                ]
            },
            {
                id: 'segurancacomunicacao',
                title: 'Segurança e Comunicação',
                type: 'collapsable',
                roles: ['admin'],
                icon: 'settings',
                children: [
                    {
                        id: 'comunidade',
                        title: 'Comunidade',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'record_voice_over',
                        url: '/comunidade',
                        roles: ['admin']
                    },
                    {
                        id: 'defesacivil',
                        title: 'Defesa Civil',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'record_voice_over',
                        url: '/defesacivil',
                        roles: ['admin']
                    },
                    {
                        id: 'procedimentos',
                        title: 'Procedimentos',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'record_voice_over',
                        url: '/procedimentos',
                        roles: ['admin']
                    }
                ]
            },
            {
                id: 'relatorio',
                title: 'Relatório',
                type: 'collapsable',
                roles: ['admin'],
                icon: 'settings',
                children: [
                    {
                        id: 'cadrelatorio',
                        title: 'Cadastro de relatórios',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'record_voice_over',
                        url: '/cadrelatorio',
                        roles: ['admin']
                    }
                ]
            },
            {
                id: 'perfil',
                title: 'Perfil',
                translate: 'NAV.SAMPLE.TITLE',
                type: 'item',
                icon: 'settings',
                url: '/profile/settings',
                roles: ['admin']
            },
            {
                id: 'administracao',
                title: 'Administração',
                type: 'collapsable',
                roles: ['admin'],
                icon: 'build',
                children: [
                    {
                        id: 'usuario',
                        title: 'Usuários',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'assignment_ind',
                        url: '/manage/user',
                        roles: ['admin']
                    },
                    {
                        id: 'papeis',
                        title: 'Papeis',
                        translate: 'NAV.SAMPLE.TITLE',
                        type: 'item',
                        icon: 'assignment_ind',
                        url: '/manage/role',
                        roles: ['admin']
                    },
                ]
            },
        ]
    }
];
