<template>
    <v-container fluid>
        <QuestionnareUpsertModal v-if="showUpsertQuestionnareManagement" :toggle="showUpsertQuestionnareManagement" :questionnareDto ="passedObjectDto" @onClose="onCreateDialogClose" />
        <v-card>
            <v-row class="mx-3 pt-3">
                <v-col cols="6">
                    <v-text-field class="pr-2" type="text" label="Search" v-model.trim="pagedSearchDto.SearchText" dense outlined />
                </v-col>
                <v-col cols="2">
                    <v-btn class="pr-2" color="default" @click="loadrecords" depressed><v-icon left>mdi-magnify</v-icon> Search</v-btn>
                </v-col>
                <v-col cols="3" class="mx-1 text-right ">
                    <v-btn class="pr-2" color="default" @click="onAdd" depressed><v-icon size="25" color="primary" left>mdi-playlist-plus</v-icon>Add New Questionnare</v-btn>
                </v-col>
            </v-row>

            <v-data-table :headers="columnHeaders"
                          :items="records"
                          :options.sync="options"
                          :server-items-length="dataLoaded ? pagedResult.TotalCount : 0"
                          :loading="loading"
                          class="elevation-2 pl-2 pr-2">

                <template v-slot:[`item.SettingKey`]="{ item }">
                    {{addSpaceBetweenUpperCaseLetters(item.SettingKey)}}
                </template>

                <template v-slot:[`item.Actions`]="{ item }">
                    <v-icon @click="onEdit(item)" class="mr-3">mdi-pencil</v-icon>
                </template>

                <!--<template v-slot:[`item.SettingValue`]="{ item }">
            <v-text-field
                          v-model="editedItem.SettingValue"

                          outlined dense />-->
                <!--<span v-else>{{item.SettingValue}}</span>-->
                <!--</template>-->


            </v-data-table>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import Common from '@/common/Common';
    import VueBase from '@/components/VueBase';
    import { ApplicationSettingDto, IApplicationSettingDto } from '@/models/ApplicationSetting/ApplicationSettingDto';
    import { PagedSearchDto, PagedSearchResultDto } from '@/models/PagedSearchDto';
    import ApplicationSettingService from '@/services/ApplicationSettingService';
    import { Component, Watch } from 'vue-property-decorator';
    import { IRoleDto, RoleDto } from '../../models/ApplicationSetting/RoleDto';
    import LibrariesService from '../../services/LibrariesService';

    import QuestionnareUpsertModal from './QuestionnareUpsertModal.vue';
import { IMedicalQuestionnaireDto, MedicalQuestionnaireDto } from '../../models/DonorRegistration/MedicalQuestionnaireDto';

    @Component({ components: { QuestionnareUpsertModal } })
    export default class RoleManagementSettings extends VueBase {
        protected librariesService: ApplicationSettingService = new ApplicationSettingService();
        protected dataLoaded: boolean = false;
        protected loading: boolean = false;
        protected showError: boolean = false;
        protected errorMessage: string = 'Error while loading records';
        protected columnHeaders: any = [
            { text: 'Header Text', value: 'HeaderText', sortable: true, width: '20%' },
            { text: 'Tagalog Text', value: 'QuestionTagalogText', sortable: true, width: '20%' },
            { text: 'English Text', value: 'QuestionEnglishText', sortable: true, width: '20%' },
            { text: 'Other Dialect Text', value: 'QuestionOtherDialectText', sortable: true, width: '15%' },
            { text: 'Gender Option', value: 'GenderOption', sortable: true, width: '10%' },
            { text: 'Order No', value: 'OrderNo', sortable: true, width: '10%' },
            { text: '', value: 'Actions', sortable: false, width: '5%' }
        ];
        protected records: Array<IApplicationSettingDto> = [];
        protected pagedSearchDto: PagedSearchDto = new PagedSearchDto();
        protected pagedResult!: PagedSearchResultDto<IMedicalQuestionnaireDto>;
        protected options: any = {};
        protected selectedId: Guid | null = null;
        protected selectedRoleName: string = "";
        protected showUpsertQuestionnareManagement: boolean = false;
        protected rules: any = { ...Common.ValidationRules }
        protected passedObjectDto: IMedicalQuestionnaireDto = new MedicalQuestionnaireDto();

        @Watch('options')
        protected async optionChange(): Promise<void> {
            await this.loadrecords();
        }

        protected onAdd(): void {
            this.showUpsertQuestionnareManagement = true;
            this.passedObjectDto = new MedicalQuestionnaireDto();
        }

        protected async onCreateDialogClose(refreshRecord: boolean): Promise<void> {
            this.showUpsertQuestionnareManagement = false;
            this.passedObjectDto = new MedicalQuestionnaireDto();


            if (refreshRecord) {
                await this.loadrecords();
            }
        }

        protected onEdit(item: IMedicalQuestionnaireDto): void {
            this.passedObjectDto = item;
             this.showUpsertQuestionnareManagement = true;
        }

        //protected close(): void {
        //  this.editedItem = Object.assign({}, new ApplicationSettingDto());
        //  this.editedIndex = -1;
        //}

        //protected async save(): Promise<void> {
        //  if (this.editedIndex > -1) {

        //    let loader = this.showLoader();
        //    try {
        //      await this.applicationSettingService.updateApplicationSetting(this.editedItem);
        //      Object.assign(this.records[this.editedIndex], this.editedItem)
        //      this.notify_success('Application Setting successfully updated.');
        //      this.close()
        //    }
        //    catch(error: any) {
        //      if (error.StatusCode != 500) {
        //        this.errorMessage = error.Message;
        //        this.notify_error(this.errorMessage);
        //      }
        //    }
        //    finally {
        //      loader.hide();
        //    }
        //  }
        //}

        protected async loadrecords(): Promise<void> {
            const { page, itemsPerPage, sortBy, sortDesc } = this.options;
            this.pagedSearchDto.PageNumber = page;
            this.pagedSearchDto.PageSize = itemsPerPage;
            this.pagedSearchDto.SortBy = sortBy[0] || '';
            this.pagedSearchDto.SortDesc = sortDesc[0];
            this.dataLoaded = false;
            this.loading = true;
            this.showError = false;

            try {
                this.pagedResult = await this.librariesService.getLibrariesQuestionnareSettings(this.pagedSearchDto);
                this.loading = false;
                this.records = this.pagedResult.Results;
                this.dataLoaded = true;
            }
            catch (error) {
                this.showError = true;
                this.loading = false;
                this.dataLoaded = false;
            }
        }

        protected async mounted(): Promise<void> {
            //await this.loadrecords();
        }

    }</script>