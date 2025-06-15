import { Module, VuexModule, Mutation, Action } from 'vuex-module-decorators';
import { DonorDto, IDonorDto } from '@/models/DonorRegistration/DonorDto';
import { IMedicalQuestionnaireDto } from '@/models/DonorRegistration/MedicalQuestionnaireDto';
import DonorRegistrationService from '@/services/DonorRegistrationService';
import DonorScreeningService from '../services/DonorScreeningService';
import ScheduleService from '../services/ScheduleService';
import { ISchedule } from '../models/Schedules/ScheduleDto';

@Module({ namespaced: true, name: 'DonorModule' })
export default class DonorModule extends VuexModule {
    protected donorRegistrationService: DonorRegistrationService = new DonorRegistrationService();

    protected medicalQuestionnaire: Array<IMedicalQuestionnaireDto> = [];
    protected donorInfo: IDonorDto = new DonorDto();
    protected donorTransactionId: Guid | null = null;
    protected donorStatus: string = '';
    protected donorActivityType: string = '';
    protected donorScreeningService: DonorScreeningService = new DonorScreeningService();
    protected scheduleService: ScheduleService = new ScheduleService();

    @Mutation
    public SET_DONOR_STATUS(status: string): void {
        this.donorStatus = status;
    }
    @Mutation
    public SET_DONOR_ACTIVITY_TYPE(type: string): void {
        this.donorActivityType = type;
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
    //@Action({ commit: 'SET_DONOR_ACTIVITY_TYPE' })
    //public setDonorActivityType(type: string): string {
    //    return type;
    //}
    //@Action({ commit: 'SET_DONOR_ACTIVITY_TYPE' })
    //public async fetchDonorActivityType(id: Guid): Promise<string> {
    //    const scheduleId: string | null = await this.donorScreeningService.getDonorActivityScheduleId(id);

    //    // If scheduleId is null, treat as "Walk-In"
    //    if (!scheduleId) return "Walk-In";

    //    const activityType: ISchedule = await this.scheduleService.getSchedulesById(scheduleId);
    //    return activityType.ActivityType;
    //}

    @Action({ commit: 'SET_DONOR_ACTIVITY_TYPE' })
    public async fetchDonorActivityType(id: Guid): Promise<string> {
        try {
            const scheduleId = await this.donorScreeningService.getDonorActivityScheduleId(id);

            const activityType = scheduleId
                ? (await this.scheduleService.getSchedulesById(scheduleId)).ActivityType
                : "Walk-In";

            return activityType;
        } catch (error) {
            return "Walk-In"; 
        }
        
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

    public get getDonorActivityType(): string {
        return this.donorActivityType;
    }
}