import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'mask' })
export class MaskPipe implements PipeTransform {

  transform(value: any, pattern: string): string {
    if (value !== undefined) {
      let i = 0;
      const v = value.toString();
      return pattern.replace(/#/g, _ => v[i++]);
    } else {
      return '';
    }
  }
}
