import { Module, VuexModule, Mutation, Action  } from 'vuex-module-decorators';
import { DonorDto, IDonorDto } from '@/models/DonorRegistration/DonorDto';
import { IMedicalQuestionnaireDto } from '@/models/DonorRegistration/MedicalQuestionnaireDto';
import DonorRegistrationService from '@/services/DonorRegistrationService';

@Module({ namespaced: true, name: 'DonorModule' })
export default class DonorModule extends VuexModule {
  protected donorRegistrationService: DonorRegistrationService = new DonorRegistrationService();

  protected medicalQuestionnaire: Array<IMedicalQuestionnaireDto> = [];
  protected donorInfo: IDonorDto = new DonorDto();
  protected donorTransactionId: Guid | null = null;
  protected donorStatus: string = '';

  @Mutation
  public SET_DONOR_STATUS(status: string): void {
    this.donorStatus = status;
  }

  @Mutation
  public SET_MEDICAL_QUESTIONNAIRE(questions: Array<IMedicalQuestionnaireDto>): void {
    this.medicalQuestionnaire = questions;
  }

  @Mutation
  public SET_DONOR_INFORMATION(donorInfo: IDonorDto): void {
    this.donorInfo = donorInfo;
  }

  @Mutation
  public RESET_DONOR_INFORMATION(): void { 
    this.donorInfo = new DonorDto();
    this.donorStatus = '';
  }

  @Mutation
  public SET_TRANSACTIONID(id: Guid): void { 
    this.donorTransactionId = id;
  }

  @Action({ commit: 'SET_TRANSACTIONID' })
  public setTransactionId(id: Guid | null): Guid | null { 
    return id;
  }


  @Action({ commit: 'SET_MEDICAL_QUESTIONNAIRE' })
  public async loadMedicalQuestionnaire(): Promise<Array<IMedicalQuestionnaireDto>> { 
    return await this.donorRegistrationService.getMedicalQuestions();
  }

  @Action({ commit: 'SET_DONOR_INFORMATION' })
  public setDonorInformation(donorInfo: IDonorDto): IDonorDto { 
    return donorInfo;
  }

  @Action({ commit: 'RESET_DONOR_INFORMATION' })
  public resetDonor(): void { 
  }

  @Action({ commit: 'SET_DONOR_STATUS' })
  public setDonorStatus(status: string): string { 
    return status;
  }


  public get getMedicalQuestionnaire(): Array<IMedicalQuestionnaireDto> {
    return this.medicalQuestionnaire;
  }

  public get getDonorInformation(): IDonorDto {
    return this.donorInfo;
  }

  public get hasDonorTransaction(): boolean {
    return this.donorTransactionId !== null;
  }

  public get getDonorStatus(): string {
    return this.donorStatus;
  }
}