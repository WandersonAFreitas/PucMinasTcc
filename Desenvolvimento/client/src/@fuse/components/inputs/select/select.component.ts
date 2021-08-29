import { Component, OnInit, OnDestroy, Input, forwardRef, SimpleChanges, OnChanges, Output, EventEmitter, ViewChild, ElementRef, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { coerceBooleanProperty } from '@angular/cdk/coercion';
import { MatAutocompleteSelectedEvent, MatSelectChange, MatAutocompleteTrigger } from '@angular/material';
import { FormGroup, FormControl, ControlValueAccessor } from '@angular/forms';

@Component({
  selector: 'app-input-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.scss'],
})
export class InputSelectComponent implements OnInit, OnDestroy, ControlValueAccessor, AfterViewInit, OnChanges {
  public isOk: boolean;

  @Input() selectKey: string;
  @Input() selectValue: string;

  // elements array
  @Input() elements = new Array<any>();

  // The internal data model for form control value access
  private innerValue: any = '';

  @Input() formGroup: any;

  @Input() formControlName = '';

  // ID attribute for the field and for attribute for the label
  @Input() idd = '';

  // The field name text . used to set placeholder also if no pH (placeholder) input is given
  @Input() text = '';

  @Input() leftValue = '';

  @Input() error = 'Obrigatório';

  // placeholder input
  @Input() pH: string;

  // current form control input. helpful in validating and accessing form control
  @Input() c: FormControl = new FormControl();

  // set true if we need not show the asterisk in red color
  @Input()
  get required(): boolean { return this._required; }
  set required(required: boolean) {
    this._required = coerceBooleanProperty(required);
  }
  _required = false;

  // @Input() v:boolean = true; // validation input. if false we will not show error message.

  // errors for the form control will be stored in this array
  errors: Array<any> = ['This field is required'];

  // get reference to the input element
  @ViewChild('input') inputRef: ElementRef;


  constructor(private cdRef: ChangeDetectorRef) {

  }

  ngOnInit() {
  }

  ngOnDestroy() {
  }

  ngOnChanges() {
  }

  // Lifecycle hook. angular.io for more info
  ngAfterViewInit() {
    // set placeholder default value when no input given to pH property
    if (!this.pH) {
      this.pH = this.text;
    }

    if (!this.c.root['controls']) {
      this.c = this.formGroup.controls[this.formControlName];
      this.cdRef.detectChanges();
    }

    // RESET the custom input form control UI when the form control is RESET
    this.c.valueChanges.subscribe(
      () => {
        // check condition if the form control is RESET
        if (this.c.value === '' || this.c.value == null || this.c.value === undefined) {
          this.innerValue = '';
          this.inputRef.nativeElement && (this.inputRef.nativeElement.value = '');
        }
      this.cdRef.detectChanges();
      }
    );
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
}
