import {
    Directive,
    ElementRef,
    Input,
    HostListener,
    OnChanges,
    SimpleChanges,
} from '@angular/core';
import { GROUP_OPERATION_FILTER } from '@fuse/types/models/enums/group-operation-enum';
import { GridSettings, Rule, Filter } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
@Directive({
    // tslint:disable-next-line:directive-selector
    selector: '[gridSettings]'
})
export class GridSettingsDirective implements OnChanges {
    private group = GROUP_OPERATION_FILTER;
    public el: HTMLElement;

    @Input() public model: GridSettings;
    @Input() public field: string;
    @Input() public op: string;
    constructor(public element: ElementRef) {
        this.el = this.element.nativeElement;
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes.input) {

        }
    }

    @HostListener('ngModelChange', ['$event'])
    changeEvent(event: any) {
        // this.el.tagName === 'MAT-SELECT'
        const data = !isNaN(event) || typeof event === 'string' || event instanceof String ? event : (<HTMLInputElement>this.el).value;
        const rule = new Rule(this.field, this.op, data);
        this.model.page = 1;
        if (!this.model.filters) {
            this.model.filters = new Filter(this.group.and, [rule]);
        } else {
            this.checkRules(this.model.filters.rules, rule);
            // TODO: verificar necessidade disso
            // if (this.model.filters.groups && this.model.filters.groups.length) {
            //     this.model.filters.groups.map(f => this.checkRules(f.rules, rule));
            // } else {
            //     this.checkRules(this.model.filters.rules, rule);
            // }
        }
    }

    private checkRules(rules: Rule[], rule: Rule) {
        const criteria = (x: Rule) => x.field === rule.field && x.op === rule.op;
        if (!rules.some(criteria)) {
            rules.push(rule);
        } else {
            rules.filter(criteria).map(x => x.data = rule.data);
        }
    }

    // @HostListener('window:keyup', ['$event'])
    // keyEvent(event: KeyboardEvent) {
    // }
}

