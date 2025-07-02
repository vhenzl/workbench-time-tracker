import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'formatDuration',
})
export class FormatDurationPipe implements PipeTransform {
  private df = new Intl.DurationFormat('en', { style: 'narrow', format: ['hours', 'minutes'] });

  transform(duration: Intl.Duration | null | undefined): string {
    if (!duration || (typeof duration !== 'object')) return '';
    return this.df.format(duration);
  }
}
