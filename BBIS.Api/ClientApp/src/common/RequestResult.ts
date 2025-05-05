export interface RequestResult<T = any> {
  Success: boolean;
  Message?: string;
  Data?: T;
  StatusCode: number;
}