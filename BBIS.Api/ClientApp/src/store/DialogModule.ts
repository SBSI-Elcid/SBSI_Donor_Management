import { Module, VuexModule, Mutation, Action  } from 'vuex-module-decorators';

@Module({ namespaced: true, name: 'DialogModule' })
export default class DialogModule extends VuexModule {

  protected option: any = null;
  public show: boolean = false;
  public message: string = '';
  public title: string = 'Confirm';
  public width: number = 500;
  public onConfirmFunc: any = null;
  public okButtonText: string = '';
  public cancelButtonText: string = '';

  @Mutation
  public SET_DIALOG_OPTIONS(option: any): void {
    this.show = option.show;
    this.message = option.message;
    this.onConfirmFunc = option.onConfirmFunc;
    this.title = option.title;
    this.width = option.width;  
    if(option.okButtonText) {
      this.okButtonText = option.okButtonText;
    }   
    if(option.cancelButtonText) {
      this.cancelButtonText = option.cancelButtonText;
    } 
  }

  @Mutation
  public RESET_DIALOG_OPTIONS(): void {
    this.show = false;
    this.message = '';
    this.title = '';
    this.onConfirmFunc = null;     
  }
  
  @Action({ commit: 'SET_DIALOG_OPTIONS' })
  public toggle(option: any): void { 
    return option;
  }

  @Action({ commit: 'RESET_DIALOG_OPTIONS' })
  public reset(): void { 
    
  }
}