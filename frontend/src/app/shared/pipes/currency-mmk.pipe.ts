import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'currencyMMK', standalone: true })
export class CurrencyMMKPipe implements PipeTransform {
  transform(value: number | string): string {
    const num = typeof value === 'string' ? parseFloat(value) : value;
    if (isNaN(num)) return 'K 0';
    return 'K ' + num.toLocaleString('en-US', { minimumFractionDigits: 0, maximumFractionDigits: 2 });
  }
}
