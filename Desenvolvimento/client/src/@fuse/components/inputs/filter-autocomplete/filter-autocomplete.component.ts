import { Component, OnInit, OnDestroy, Input, forwardRef, SimpleChanges, OnChanges, Output, EventEmitter, ViewChild, ElementRef, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { coerceBooleanProperty } from '@angular/cdk/coercion';
import { MatAutocompleteSelectedEvent, MatSelectChange } from '@angular/material';
import { Observable, Subject } from 'rxjs';
import { FormGroup, FormControl, ControlValueAccessor } from '@angular/forms';
import { finalize, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { GridSettings, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { WHERE_OPERATION_FILTER } from '@fuse/types/models/enums/where-operation-enum';
import { Paginacao } from '@fuse/types/models/viewmodel/paginacao.viewmodel';

@Component({
  selector: 'app-input-filter-autocomplete',
  templateUrl: './filter-autocomplete.component.html',
  styleUrls: ['./filter-autocomplete.component.scss'],
})
export class InputFilterAutocompleteComponent implements OnInit, OnDestroy, OnChanges {
  // The internal data model for form control value access
  private innerValue: any = '';

  // Service
  @Input() service: any;
  @Input() action: string;
  @Input() param: any;
  @Input() gridSettingsModelFilter: GridSettings;
  @Input() model: GridSettings;

  // ID attribute for the field and for attribute for the label
  @Input() idd = '';

  // placeholder input
  @Input() pH: string;

  @ViewChild('input') inputRef: ElementRef;

  @Input() entityFilter: any;
  @Input() ngModelProperty: string;
  @Input() field: string;
  @Input() op = WHERE_OPERATION_FILTER.eq;

  @Input() selectKey: string;
  @Input() selectValue: string;

  // elements array
  public elements = new Array<any>();
  private elementObservable: Observable<Paginacao<any>>;
  private subjectElement: Subject<string> = new Subject<string>();

  constructor(
    private cdRef: ChangeDetectorRef
  ) {

  }

  ngOnInit() {
    this.initAutocompleteSubject();
  }

  ngOnDestroy() {
  }

  ngOnChanges() {
  }

  public searchAutocomplete(autocompleteValue: string): void {
    this.subjectElement.next(autocompleteValue);
  }

  private initAutocompleteSubject(): void {
    this.elementObservable = this.subjectElement
      .pipe(
        debounceTime(500),
        distinctUntilChanged(),
        switchMap((termo: string) => {
          if (this.action) {
            if (this.param) {
              return this.service[this.action](this.param);
            }
            return this.service[this.action]();
          }
          const gridSettingsModel = this.gridSettingsModelFilter;
          gridSettingsModel.filters.rules = gridSettingsModel.filters.rules.reduce((a: Rule[], c: Rule) => {
            c.data = termo;
            a.push(c);
            return a;
          }, new Array<Rule>());
          return this.service.getByFilter(gridSettingsModel, false);
        })
      );

    this.elementObservable.subscribe(paginacao => {
      this.elements = paginacao.content;
    });
  }

  public optionSelected(event: MatAutocompleteSelectedEvent) {
    const viewValue = event.option.viewValue;
    this.inputRef.nativeElement['value'] = viewValue;
  }

  public clearSetor() {
    this.entityFilter[this.ngModelProperty] = undefined;
    this.inputRef.nativeElement['value'] = '';
  }
}
