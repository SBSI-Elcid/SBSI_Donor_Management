import { ILookupOptions } from "./LookupOptions";

export interface ILookup {
  LookupId: number,
  LookupKey: string,
  Description: string,
  LookupOptions: Array<ILookupOptions>
}

export class Lookup implements ILookup{
  LookupId: number = 0;
  LookupKey: string = "";
  Description: string = "";
  LookupOptions: Array<ILookupOptions> = [];
}