<template>
  <div>
    <v-tabs v-model="selectedTab">
      <v-tab v-for="tab in availableTabs" :key="tab.name" v-model="selectedTab" :to="tab.route">
        <v-icon left>{{ tab.icon }}</v-icon>
        <span>{{ tab.name }}</span>
      </v-tab>
      <v-tabs-items v-model="selectedTab">
        <router-view />
      </v-tabs-items>
    </v-tabs>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import { getModule } from 'vuex-module-decorators';
import DonorModule from '@/store/DonorModule';
import { TabNames } from '@/models/Enums/TabNames';
import AuthModule from '@/store/AuthModule';
import Common from '@/common/Common';
import { Roles } from '@/models/Enums/Roles';

@Component
export default class BloodDonorProcess extends Vue {
  protected authModule: AuthModule = getModule(AuthModule, this.$store);
  protected donorModule: DonorModule = getModule(DonorModule, this.$store);
  
  protected donorId: Guid = '';
  protected donorRegistrationId: Guid = '';
  protected selectedTab: any = null;
  protected isDisabled: boolean = true;
  protected get tabs(): Array<{name: string, icon: string, route: string, isShow: boolean, isDisabled: boolean}>  {
    return [
    {
      name: TabNames.DonorInformation,
      icon: 'mdi-account',
      route: this.tabRoutes(TabNames.DonorInformation),
      isShow: Common.hasValue(this.donorRegistrationId) && this.showTab([Roles.DonorAdmin, Roles.InitialScreener, Roles.PhysicalExamScreener, Roles.BloodCollector]),
      isDisabled: this.tabDisabled
    },
    {
        name: TabNames.DonorVitalSigns,
        icon: 'mdi-badge-account',
        route: this.tabRoutes(TabNames.DonorVitalSigns),
        isShow: Common.hasValue(this.donorRegistrationId) && this.showTab([Roles.DonorAdmin, Roles.InitialScreener, Roles.PhysicalExamScreener, Roles.BloodCollector]),
        isDisabled: this.tabDisabled
    },
    {
      name: TabNames.InitialScreening,
      icon: 'mdi-badge-account',
      route: this.tabRoutes(TabNames.InitialScreening),
      isShow: /*this.showScreeningTabs &&*/ this.showTab([Roles.DonorAdmin, Roles.InitialScreener]),
      isDisabled: this.tabDisabled
    },
    {
      name: TabNames.PhysicalExam,
      icon: 'mdi-medical-bag',
      route: this.tabRoutes(TabNames.PhysicalExam),
      isShow: /*this.showScreeningTabs &&*/ this.showTab([Roles.DonorAdmin, Roles.PhysicalExamScreener]),
      isDisabled: this.tabDisabled
    },
    {
      name: TabNames.Counseling,
      icon: '',
      route: this.tabRoutes(TabNames.Counseling),
      isShow: /*this.showScreeningTabs &&*/ this.showTab([Roles.DonorAdmin, Roles.PhysicalExamScreener]),
      isDisabled: this.tabDisabled
    },
    {
      name: TabNames.ConsentForm,
      icon: '',
      route: this.tabRoutes(TabNames.ConsentForm),
      isShow: /*this.showScreeningTabs &&*/ this.showTab([Roles.DonorAdmin, Roles.PhysicalExamScreener]),
      isDisabled: this.tabDisabled
    },
    {
      name: TabNames.MethodOfBloodCollection,
      icon: '',
      route: this.tabRoutes(TabNames.MethodOfBloodCollection),
      isShow: /*this.showScreeningTabs &&*/ this.showTab([Roles.DonorAdmin, Roles.PhysicalExamScreener]),
      isDisabled: this.tabDisabled
    },
    {
      name: TabNames.IssuanceOfBloodBag,
      icon: '',
      route: this.tabRoutes(TabNames.IssuanceOfBloodBag),
      isShow: /*this.showScreeningTabs &&*/ this.showTab([Roles.DonorAdmin, Roles.PhysicalExamScreener]),
      isDisabled: this.tabDisabled
    },
    {
      name: TabNames.BloodCollection,
      icon: 'mdi-blood-bag',
      route: this.tabRoutes(TabNames.BloodCollection),
      isShow: /*this.showScreeningTabs &&*/ this.showTab([Roles.DonorAdmin, Roles.BloodCollector]),
      isDisabled: this.tabDisabled
    },
    {
      name: TabNames.PostDonationCare,
      icon: '',
      route: this.tabRoutes(TabNames.PostDonationCare),
      isShow: /*this.showScreeningTabs &&*/ this.showTab([Roles.DonorAdmin, Roles.BloodCollector]),
      isDisabled: this.tabDisabled
    }
    ];
  }

    protected get tabRoutes(): (tabName: string) => string {
        return (tabName) => {
            switch (tabName) {
                case TabNames.DonorInformation:
                    return `/donor/info/${this.donorRegistrationId}`;
                case TabNames.DonorVitalSigns:
                    return `/donor/vitalsigns/${this.donorRegistrationId}`;
                case TabNames.InitialScreening:
                    return `/donor/initialscreening/${this.donorRegistrationId}`;
                case TabNames.PhysicalExam:
                    return `/donor/physicalexamination/${this.donorRegistrationId}`;
                case TabNames.Counseling:
                    return `/donor/counseling/${this.donorRegistrationId}`;
                case TabNames.ConsentForm:
                    return `/donor/consentform/${this.donorRegistrationId}`;
                case TabNames.MethodOfBloodCollection:
                    return `/donor/methodofbloodcollection/${this.donorRegistrationId}`;
                case TabNames.IssuanceOfBloodBag:
                    return `/donor/issuanceofbloodbag/${this.donorRegistrationId}`;
                case TabNames.BloodCollection:
                    return `/donor/bloodcollection/${this.donorRegistrationId}`;
                case TabNames.PostDonationCare:
                    return `/donor/postdonationcare/${this.donorRegistrationId}`;
                default:
                    return `/donor/info/${this.donorRegistrationId}`;
            }
        }
    }

    protected get showScreeningTabs(): boolean {
      return Common.hasValue(this.donorRegistrationId) && this.donorModule.hasDonorTransaction;
    
  }

  protected get tabDisabled(): boolean {
    return !this.donorModule.hasDonorTransaction;
  }

  protected get availableTabs():  Array<{name: string, icon: string, route: string, isShow: boolean}> {
    return this.tabs.filter(x => x.isShow === true);
  }

  protected get showTab(): (roles: Array<string>) => boolean {
    return (roles) => this.authModule.userHasAnyRole(roles);
  }

  protected async mounted(): Promise<void> {
    if (this.$route.params.reg_id && typeof (this.$route.params.reg_id) === 'string') {
      this.donorRegistrationId = this.$route.params.reg_id;
    }
      
  }
}
</script>

<style lang="scss" scoped>

  :deep(.form-container) {
    width: 100%;
    display: block;
    margin: 0px !important;
    padding: 20px 30px 30px 30px;
  }

  :deep(.section-outer-container) {
    width: 100%;
    display: block;
    padding: 0;
    margin: 0;
  }

  :deep(.section-container-w700) {
    min-width: 700px;
    display: inline-block;
    vertical-align: middle;
    text-align: left;
    padding: 0;
    margin: 0;
  }

  :deep(.section-container-w500) {
    width: 500px;
    display: inline-block;
    vertical-align: middle;
    text-align: left;
    padding: 0;
    margin: 0;
  }

  :deep(.section-container-w350) {
    width: 350px;
    display: inline-block;
    vertical-align: middle;
    text-align: left;
    padding: 0;
    margin: 0;
  }

  :deep(.section-container-w250) {
    width: 250px;
    display: inline-block;
    vertical-align: middle;
    text-align: left;
    padding: 0;
    margin: 0;
  }

  :deep(.section-container-w100) {
    width: 100px;
    display: inline-block;
    vertical-align: middle;
    text-align: left;
    padding: 0;
    margin: 0;
  }

  :deep(.label-container) {
    width: 100px;
    vertical-align: middle;
    text-align: left;
    color: grey;
    font-size: 14px;
    font-weight: 400;
    letter-spacing: 0.0071428571em;
    line-height: 1.375rem;
  }

  :deep(.status-container) {
    display: inline-block;
    margin-right: 20px;
  }

  :deep(.row-radio-group-container) {
    margin-top: 2px;
  }

  :deep(.padding-right-10) {
    padding-right: 10px !important;
  }

  :deep(.margin-top-25) {
    margin-top: 25px !important;
  }
</style>
