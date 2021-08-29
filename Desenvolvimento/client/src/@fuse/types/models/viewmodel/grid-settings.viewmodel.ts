export class GridSettings {
    constructor(
      public _search: boolean,
      public page: number,
      public rows: number,
      public sidx: string,
      public sord: string,
      public filters?: Filter,
    ) { }
  }

  export class Filter {
    constructor(
        public groupOp: number,
        public rules: Rule[],
        public groups?: Filter[]
    ) { }
  }

  export class Rule {
    constructor(
      public field: string,
      public op: string,
      public data: string,
    ) { }
  }

