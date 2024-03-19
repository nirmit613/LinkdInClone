import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeAgo',
  standalone: true,
})
export class TimeAgoPipe implements PipeTransform {
  transform(value: string): string {
    if (!value) return '';

    const seconds = Math.floor((+new Date() - +new Date(value)) / 1000);

    let interval = Math.floor(seconds / 31536000);
    if (interval > 1) {
      return interval + ' yr';
    }

    interval = Math.floor(seconds / 2592000);
    if (interval > 1) {
      return interval + ' mos';
    }

    interval = Math.floor(seconds / 86400);
    if (interval > 1) {
      return interval + ' days ago';
    }

    interval = Math.floor(seconds / 3600);
    if (interval > 1) {
      return interval + ' hr ago';
    }

    interval = Math.floor(seconds / 60);
    if (interval > 1) {
      return interval + ' min ago';
    }

    return Math.floor(seconds) + ' sec ago';
  }
}
