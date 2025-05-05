
export default class BrowserStorageService implements Storage {
  [name: string]: any;
  get length(): number {
    return localStorage.length
  }

  public clear(): void {
    localStorage.clear();
  }

  public getItem(key: string): string {
    let result = localStorage.getItem(key)
    return result ? result : "";
  }

  public key(index: number): string {
    throw new Error("Method not implemented.");
  }
  
  public removeItem(key: string): void | boolean {
    localStorage.removeItem(key);
  }

  public setItem(key: string, value: string): void {    
    localStorage.setItem(key, value);
  }
}