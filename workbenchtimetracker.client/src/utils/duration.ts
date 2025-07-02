/**
 * Converts a time string ('hh:mm:ss', 'mm:ss', or 'ss') to total seconds.
 */
export function timeStringToSeconds(timeStr: string): number {
  const parts = timeStr.split(':').map(Number).reverse();
  const [s = 0, m = 0, h = 0] = parts;
  return h * 3600 + m * 60 + s;
}

export type Duration = {
  hours: number;
  minutes: number;
  seconds: number;
};

/**
 * Converts total seconds to a duration object
 */
export function secondsToDurationObj(totalSeconds: number): Duration {
  const hours = Math.floor(totalSeconds / 3600);
  const minutes = Math.floor((totalSeconds % 3600) / 60);
  const seconds = totalSeconds % 60;
  return { hours, minutes, seconds };
}
