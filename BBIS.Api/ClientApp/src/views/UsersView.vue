<template>
  <v-container fluid>
    <v-card>
      <create-user-account v-if="showCreateUserDialog" :toggle="showCreateUserDialog" @onClose="onCreateDialogClose"></create-user-account>
      <update-user-account v-if="showUpdateUserDialog" :toggle="showUpdateUserDialog" :id="selectedUserAccountId" @onClose="onUpdateDialogClose"></update-user-account>

      <v-card-actions>
        <h2 class="ml-2 mt-1 head-title ms-4 grey--text">Users</h2> 
        <v-spacer></v-spacer>
        <v-btn color="default" class="mt-2 mr-2" depressed @click="addClick"><v-icon size="25" color="primary" left>mdi-account-plus</v-icon> Add New User</v-btn>
      </v-card-actions>
       <v-divider></v-divider>

      <v-row no-gutters class="mx-3 pt-3">
        <v-col md="3" sm="9">
          <v-text-field type="text" label="Search" v-model.trim="pagedSearchDto.SearchText" dense outlined />
        </v-col>
        <v-col md="2" sm="2" class="pl-2" >
          <v-btn color="default" @click="loadrecords" depressed><v-icon size="25" color="primary" left>mdi-magnify</v-icon> Search</v-btn>
        </v-col>
      </v-row>
               
       <v-data-table
          :headers="columnHeaders"
          :items="records"
          :options.sync="options"
          :server-items-length="dataLoaded ? pagedResult.TotalCount : 0"
          :loading="loading"            
          class="elevation-2 pl-2 pr-2">

          <template v-slot:[`progress`]>
            <v-progress-linear color="#B92F2F" indeterminate></v-progress-linear>
          </template>

          <template v-slot:[`item.IsActive`]="{ item }">
            <v-chip small outlined :color="item.IsActive ? 'primary' : 'default' " label >{{ item.IsActive ? 'Active' : 'Inactive' }}</v-chip>     
          </template>

          <template v-slot:[`item.actions`]="{ item }">
            <v-btn icon @click="editClick(item.UserAccountId)">
              <v-icon medium>mdi-pencil-outline</v-icon>
            </v-btn> 
            <v-btn icon @click="onDelete(item)">
              <v-icon medium>mdi-delete-outline</v-icon>
            </v-btn> 
          </template>
        </v-data-table>
      
    </v-card>
 
  </v-container>
</template>

<script lang="ts">
import VueBase from '@/components/VueBase';
import { PagedSearchDto, PagedSearchResultDto } from '@/models/PagedSearchDto';
import { IUserAccountViewDto } from '@/models/UserAccounts/IUserAccountViewDto';
import UserAccountsService from '@/services/UserAccountsService';
import { Component, Watch } from 'vue-property-decorator';
import CreateUserAccount from '@/components/UserAccounts/CreateUserAccount.vue';
import UpdateUserAccount from '@/components/UserAccounts/UpdateUserAccount.vue';

@Component({ components: { CreateUserAccount, UpdateUserAccount } })
export default class UsersView extends VueBase {
  protected useraccountsService: UserAccountsService = new UserAccountsService();

  protected dataLoaded: boolean = false;
  protected loading: boolean = false;
  protected showError: boolean = false;
  protected errorMessage: string = 'Error while loading records';
  protected columnHeaders: any = [
    { text: 'User name',  value: 'Username',      sortable: true,   width: "10%"  },
    { text: 'First name', value: 'Firstname',     sortable: true,   width: "20%"  },
    { text: 'Last name',  value: 'Lastname',      sortable: true,   width: "20%"  },
    { text: 'Email',      value: 'EmailAddress',  sortable: true,   width: "30%"  },
    { text: 'Status',     value: 'IsActive',      sortable: false,  width: "10%"  },
    { text: '',           value: 'actions',       sortable: false,  width: "10%"  }
  ];
  protected records: Array<IUserAccountViewDto> = [];
  protected pagedSearchDto: PagedSearchDto = new PagedSearchDto();
  protected pagedResult!: PagedSearchResultDto<IUserAccountViewDto>;
  protected options: any = {}; 
  protected showCreateUserDialog: boolean = false;
  protected showUpdateUserDialog: boolean = false;
  protected selectedUserAccountId: Guid | null = null;
 
  @Watch('options')
  protected async optionChange(): Promise<void> {
    await this.loadrecords(); 
  }

  protected addClick(): void {
    this.showCreateUserDialog = true;
  }

  protected editClick(id: Guid): void {
    this.showUpdateUserDialog = true;
    this.selectedUserAccountId = id;
  }

  protected async onCreateDialogClose(refreshRecord: boolean): Promise<void> {
    this.showCreateUserDialog = false;

    if (refreshRecord) {
      await this.loadrecords();
    }
  }

  protected async onUpdateDialogClose(refreshRecord: boolean): Promise<void> {
    this.showUpdateUserDialog = false;

    if (refreshRecord) {
      await this.loadrecords();
    }
  }

  protected onDelete(item: IUserAccountViewDto): void {
    this.selectedUserAccountId = item.UserAccountId;
    this.confirm(`Delete this user '${item.Username}'?`, 'Delete', 'Delete', 'Cancel', this.deleteConfirm);
  }

  protected async deleteConfirm(confirm: boolean): Promise<void> {
    if (confirm && this.selectedUserAccountId) {
      let loader = this.showLoader();
      try {
        await this.useraccountsService.deleteUser(this.selectedUserAccountId as Guid);
        await this.loadrecords();
        this.notify_info('User successfully deleted.')
      } 
      catch (error) {
        console.log(error);
        this.notify_error('Something went wrong deleting user.');
      }
      finally {
        loader.hide();
      }
    }
  }


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
      this.pagedResult = await this.useraccountsService.getUserList(this.pagedSearchDto);
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

  protected async mounted() : Promise<void> {
    //await this.loadrecords();
  }

}

</script>