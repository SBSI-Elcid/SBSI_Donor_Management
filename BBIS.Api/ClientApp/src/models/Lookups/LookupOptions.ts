export interface ILookupOptions {
  LookupOptionId: number,
  Text: string,
  Value: string
}

export class LookupOptions implements ILookupOptions{
  LookupOptionId: number = 0;
  Text: string = "";
  Value: string = "";
}