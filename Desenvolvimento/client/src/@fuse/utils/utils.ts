export function getIndexBy(array: Array<{}>, { name, value }): number {
  for (let i = 0; i < array.length; i++) {
    if (array[i][name] === value) {
      return i;
    }
  }
  return -1;
}

export function padLeft(value: any, l: number, c: string) {
  const valueString = value.toString();
  return Array(l - valueString.length + 1).join(c || ' ') + valueString;
}

export function padRight(value: any, l: number, c: string) {
  const valueString = value.toString();
  return valueString + Array(l - valueString.length + 1).join(c || ' ');
}
