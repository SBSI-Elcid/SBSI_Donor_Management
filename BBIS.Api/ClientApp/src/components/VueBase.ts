import { Component, Vue } from 'vue-property-decorator';
import moment from 'moment';
import Common from '@/common/Common';
import AuthModule from '@/store/AuthModule';
import { getModule } from 'vuex-module-decorators';
import { Roles } from '@/models/Enums/Roles';
import { DonorStatus } from '@/models/Enums/DonorStatus';
import BrowserStorageService from '@/services/BrowserStorageService';
import { StorageKeysEnum } from '@/common/StorageKeysEnum';
import ApplicationSettingService from '@/services/ApplicationSettingService';
import CommonLoader from '@/components/Common/CommonLoader.vue';

@Component({ components: { CommonLoader } })
export default class VueBase extends Vue {
  protected storageService: Storage = new BrowserStorageService();

  protected notify_success(message: string): void {
    this.$swal.fire({
      position: 'top',
      icon: 'success',
      title: message,
      showConfirmButton: false,
      toast: true,
      timer: 5000
    })
  }

  protected notify_info(message: string): void {
    this.$swal.fire({
      position: 'top',
      icon: 'info',
      title: message,
      showConfirmButton: false,
      toast: true,
      timer: 5000
    })
  }

  protected notify_error(message: string): void {
    this.$swal.fire({
      position: 'top',
      icon: 'error',
      title: message,
      showConfirmButton: false,
      toast: true,
      timer: 5000
    })
  }  

  protected notify_warning(message: string): void {
    this.$swal.fire({
      position: 'top',
      icon: 'warning',
      title: message,
      showConfirmButton: false,
      toast: true,
      timer: 5000
    });
  }  

  protected notify_something_went_wrong(): void {
    this.$swal.fire({
      position: 'top',
      icon: 'error',
      title: 'Opps..something went wrong.',
      showConfirmButton: false,
      toast: true,
      timer: 5000
    });
  }

  protected confirm(confirmMessage: string, title: string, confirmBtnText: string, cancelBtntext: string, callbackFunc: Function): void {
    this.$swal.fire({
      title: title,
      text: confirmMessage,
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: confirmBtnText,
      cancelButtonText: cancelBtntext,
      toast: false
    }).then((result) => {
      if (result.isConfirmed) {
       callbackFunc(true);
      }
      else {
        callbackFunc(false);
      }
    });
  }

  protected confirm_input(confirmMessage: string, title: string, inputType: any, placeholder: string, confirmBtnText: string, cancelBtntext: string, callbackFunc: Function): void {
    this.$swal.fire({
      title: title,
      input: inputType,
      inputPlaceholder: placeholder,
      text: confirmMessage,
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: confirmBtnText,
      cancelButtonText: cancelBtntext,
      toast: false
    }).then((result) => {
      if (result.isConfirmed) {
       callbackFunc(true, result.value);
      }
      else {
        callbackFunc(false);
      }
    });
  }

  protected mark_deferred(confirmMessage: string, title: string, confirmBtnText: string, cancelBtntext: string, callbackFunc: Function): void {
    this.$swal.fire({
      title: title,
      html: `<div class="swal_input_wrapper">
              <div class="swal2-radio" style="display: flex;">
                <label><input type="radio" name="swal2-radio" value="Temporary"><span class="swal2-label">Temporary</span></label>
                <label><input type="radio" name="swal2-radio" value="Permanent"><span class="swal2-label">Permanent</span></label>
              </div>
              <textarea id="swal-input3" style="width:75%" placeholder="Remarks" class="swal2-textarea"></textarea>
            </div>`,
      icon: 'question',
      text: confirmMessage,
      showCancelButton: true,
      confirmButtonText: confirmBtnText,
      cancelButtonText: cancelBtntext,
      toast: false,
      preConfirm: () => {
        let deferralStatus = (<HTMLInputElement>document.querySelector('input[type=\'radio\'][name=\'swal2-radio\']:checked'))?.value;
        let remarks = (<HTMLInputElement>document.getElementById('swal-input3'))?.value;

        if (Common.hasValue(deferralStatus)) {
            return [{'DeferralStatus': deferralStatus, 
                     'Remarks': remarks }]
        } else {
          this.$swal.showValidationMessage('Please select between Temporary or Permanent.');
        }
      }
    }).then(function(result) {
      if (result.isConfirmed) {
        callbackFunc(true, result.value)
      }
      else {
        callbackFunc(false);
      }
    });
  }

  protected formatDate(date: Date | string | null | undefined, format?: string): string | null {
    return (typeof (date) === 'undefined' || date === null || date === '') ? null : moment.utc(date).local().format("ll");
  }

  protected addSpaceBetweenUpperCaseLetters(pascalCaseValue: string): string {
    return pascalCaseValue.replace(/([A-Z])/g, ' $1').trim();
  }

  protected hasAccess(roles: Array<string>): boolean {
    let authModule: AuthModule = getModule(AuthModule, this.$store);
    if (authModule.userHasAnyRole(roles)) {
      return true;
    }

    return false;
  }

  protected showLoader(properties: any = {}, slots: any = { default: this.$createElement('common-loader')}): any {
     return this.$loading.show(properties, slots);
  }

  protected hasManyRoles(): boolean {
    let authModule: AuthModule = getModule(AuthModule, this.$store);
    return authModule.userRoles.length > 1;
  }

  protected getDonorStatusByRoles(): Array<string> {
    let authModule: AuthModule = getModule(AuthModule, this.$store);
    var userRoles = authModule.userRoles;

    let statuses: Array<string> = [];

    if (userRoles.includes(Roles.Admin) || userRoles.includes(Roles.DonorAdmin)) {
      return [DonorStatus.ForInitialScreening, DonorStatus.ForPhysicalExamination, DonorStatus.ForBloodCollection, DonorStatus.ForLaboratoryTest, DonorStatus.Success, DonorStatus.Deferred, DonorStatus.Inventory];
    }

    if (userRoles.includes(Roles.InitialScreener)) {
      statuses.push(DonorStatus.ForInitialScreening);
    }

    if (userRoles.includes(Roles.PhysicalExamScreener)) {
      statuses.push(DonorStatus.ForPhysicalExamination);
    }

    if (userRoles.includes(Roles.BloodCollector)) {
      statuses.push(DonorStatus.ForBloodCollection);
    }

    return statuses;
  }

  protected get isOffline(): boolean {
    let value: string | null = this.storageService.getItem(StorageKeysEnum.IsOffline);
    
    let offlineStatus: boolean = false;

    if (value !== null) {
      offlineStatus = value.toLowerCase() === 'true';
    }

    return offlineStatus;
  }
 
  protected async getOfflineStatus(): Promise<void> {
    try {
      let appSettingService: ApplicationSettingService = new ApplicationSettingService();
      let status: boolean = await appSettingService.getAppStatus();
      this.storageService.setItem(StorageKeysEnum.IsOffline, `${status}`);
    } 
    catch (error) {
      console.log(error);
    }
  }
}
