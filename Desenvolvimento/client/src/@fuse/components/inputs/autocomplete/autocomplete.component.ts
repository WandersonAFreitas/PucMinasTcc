import { Component, OnInit, OnDestroy, Input, OnChanges,  ViewChild, ElementRef, AfterViewInit, ChangeDetectorRef, EventEmitter, Output } from '@angular/core';
import { coerceBooleanProperty } from '@angular/cdk/coercion';
import { MatAutocompleteSelectedEvent, MatSelectChange, MatAutocompleteTrigger } from '@angular/material';
import { Observable, Subject, Subscription } from 'rxjs';
import { FormControl, ControlValueAccessor } from '@angular/forms';
import { debounceTime, switchMap } from 'rxjs/operators';
import { GridSettings, Rule } from '@fuse/types/models/viewmodel/grid-settings.viewmodel';
import { Paginacao } from '@fuse/types/models/viewmodel/paginacao.viewmodel';

@Component({
  selector: 'app-input-autocomplete',
  templateUrl: './autocomplete.component.html',
  styleUrls: ['./autocomplete.component.scss'],
})
export class InputAutocompleteComponent implements OnInit, OnDestroy, ControlValueAccessor, AfterViewInit, OnChanges {

  @ViewChild(MatAutocompleteTrigger) trigger: MatAutocompleteTrigger;

  // The internal data model for form control value access
  private innerValue: any = '';

  // Forms
  @Input() formGroup: any;
  @Input() formControlName = '';
  @Input() formControKey = '';

  // Service
  @Input() service: any;
  @Input() action: string;
  @Input() param: any;
  @Input() gridSettingsModel: GridSettings;
  @Input() updateGridSettingsModel: any;

  // ID attribute for the field and for attribute for the label
  @Input() idd = '';

  @Input() error = 'Obrigatório';

  // placeholder input
  @Input() pH: string;

  // set true if we need not show the asterisk in red color
  @Input()
  get required(): boolean { return this._required; }
  set required(required: boolean) {
    this._required = coerceBooleanProperty(required);
  }
  _required = false;

  @Input()
  get disabled(): boolean { return this._disabled; }
  set disabled(disabled: boolean) {
    this._disabled = coerceBooleanProperty(disabled);
  }
  _disabled = false;

  @Input()
  get panelClosingActions(): boolean { return this._panelClosingActions; }
  set panelClosingActions(panelClosingActions: boolean) {
    this._panelClosingActions = coerceBooleanProperty(panelClosingActions);
  }
  _panelClosingActions = false;

  @Input()
  get preStartSearch(): boolean { return this._preStartSearch; }
  set preStartSearch(preStartSearch: boolean) {
    this._preStartSearch = coerceBooleanProperty(preStartSearch);
  }
  _preStartSearch = false;

  @ViewChild('input') inputRef: ElementRef;

  @Input() selectKey: string;
  @Input() selectValue: string;

  @Output() eventChange: EventEmitter<any> = new EventEmitter<any>();
  @Output() eventClear: EventEmitter<any> = new EventEmitter<any>();

  // elements array
  public elements = new Array<any>();
  // current form control input. helpful in validating and accessing form control
  public c: FormControl = new FormControl();

  private elementObservable: Observable<Paginacao<any>>;
  private subjectElement: Subject<string> = new Subject<string>();

  private subscription: Subscription;

  // errors for the form control will be stored in this array
  errors: Array<any> = ['This field is required'];


  constructor(
    private cdRef: ChangeDetectorRef
  ) {

  }

  ngOnInit() {
    this.initAutocompleteSubject();
  }

  ngOnDestroy() {
    if (this.subscription && !this.subscription.closed) {
      this.subscription.unsubscribe();
    }
  }

  ngOnChanges() {
  }

  private subscribeToClosingActions(): void {
    if (this.subscription && !this.subscription.closed) {
      this.subscription.unsubscribe();
    }

    this.subscription = this.trigger.panelClosingActions
      .subscribe(e => {
        if (!e || !e.source) {
          this._panelClosingActions && this.c.setValue(null);
        }
      },
        err => this.subscribeToClosingActions(),
        () => this.subscribeToClosingActions());
  }

  // Lifecycle hook. angular.io for more info
  ngAfterViewInit() {


    if (!this.c.root['controls']) {
      this.c = this.formGroup.controls[this.formControlName];
      this.cdRef.detectChanges();
    }

    if (this._preStartSearch) {
      this.subjectElement.next();
    }

    this.subscribeToClosingActions();

    // RESET the custom input form control UI when the form control is RESET
    this.c.valueChanges.subscribe(
      () => {
        // check condition if the form control is RESET
        if (this.c.value === '' || this.c.value == null || this.c.value === undefined) {
          this.innerValue = '';
          this.inputRef.nativeElement && (this.inputRef.nativeElement.value = '');
        }

        if (this._preStartSearch) {
          this.elements = new Array<any>();
          this.subjectElement.next();
        }

        if (this.updateGridSettingsModel) {
          this.elements = new Array<any>();
          this.updateGridSettingsModel();
        }
      }
    );
    // this.cdRef.detectChanges();
  }

  // event fired when input value is changed . later propagated up to the form control using the custom value accessor interface
  onChange(event: MatSelectChange, value: any) {
    // set changed value
    this.innerValue = value;
    // propagate value into form control using control value accessor interface
    this.propagateChange(this.innerValue);
    // reset errors
    this.errors = [];
    // setting, resetting error messages into an array (to loop) and adding the validation messages to show below the field area
    for (const key in this.c.errors) {
      if (this.c.errors.hasOwnProperty(key)) {
        if (key === 'required') {
          this.errors.push('This field is required');
        } else {
          this.errors.push(this.c.errors[key]);
        }
      }
    }
  }

  // get accessor
  get value(): any {
    return this.innerValue;
  }

  // set accessor including call the onchange callback
  set value(v: any) {
    if (v !== this.innerValue) {
      this.innerValue = v;
    }
  }

  // propagate changes into the custom form control
  propagateChange = (_: any) => { };

  // From ControlValueAccessor interface
  writeValue(value: any) {
    this.innerValue = value;
  }

  // From ControlValueAccessor interface
  registerOnChange(fn: any) {
    this.propagateChange = fn;
  }

  // From ControlValueAccessor interface
  registerOnTouched(fn: any) {

  }

  public searchAutocomplete(autocompleteValue: string): void {
    this.subjectElement.next(autocompleteValue);
  }

  private initAutocompleteSubject(): void {
    this.elementObservable = this.subjectElement
      .pipe(
        debounceTime(500),
        switchMap((termo: string) => {
          if (this.action) {
            if (this.param) {
              return this.service[this.action](this.param);
            }
            return this.service[this.action]();
          }

          const gridSettingsModel = this.gridSettingsModel;

          if (gridSettingsModel && gridSettingsModel.filters) {
            gridSettingsModel.filters.rules = gridSettingsModel.filters.rules.reduce((a: Rule[], c: Rule) => {
              c.data = termo;
              a.push(c);
              return a;
            }, new Array<Rule>());
          }

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
    this.formGroup.get(this.formControKey).setValue(event.option.value);
    this.formGroup.get(this.formControlName).setValue(viewValue);
    this.eventChange.emit();
  }

  public onClear() {
    this.inputRef.nativeElement['value'] = '';
    this.formGroup.get(this.formControKey).setValue(null);
    this.eventClear.emit();
  }
}
