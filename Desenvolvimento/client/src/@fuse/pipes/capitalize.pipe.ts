import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'capitalize'
})
export class CapitalizePipe implements PipeTransform {

  transform(value: any) {
    if (value) {
      if (value === value.toString().toUpperCase()) {
          value = value.toString().toLowerCase();
      }
      return value.charAt(0).toUpperCase() + value.slice(1);
    }
    return value;
  }

}
