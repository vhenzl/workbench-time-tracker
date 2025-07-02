// TS 5.8 doesn't support Intl.DurationFormat yet.
// This is a minimal type definition for my use case.
// TODO: remove once this is resolved: https://github.com/microsoft/TypeScript/issues/60608
declare namespace Intl {
  interface DurationFormatOptions {
    style?: 'long' | 'short' | 'narrow';
    format?: Array<'years' | 'months' | 'weeks' | 'days' | 'hours' | 'minutes' | 'seconds'>;
  }

  interface DurationFormatPart {
    type: string;
    value: string;
  }

  interface Duration {
    years?: number;
    months?: number;
    weeks?: number;
    days?: number;
    hours?: number;
    minutes?: number;
    seconds?: number;
  }

  class DurationFormat {
    constructor(locales?: string | string[], options?: DurationFormatOptions);
    format(duration: Duration): string;
    formatToParts(duration: Duration): DurationFormatPart[];
    resolvedOptions(): DurationFormatOptions;
  }
}
