<template>
  <v-row no-gutters>
		<v-col cols="12">
			<label>User module access</label>
      
			<v-card>
				<template v-for="(item, index) in modules">
					<v-checkbox class="pl-4" style="margin-bottom: -32px;"
						:key="index"
						:label="item.Menu"
						:value="item"
						v-model="selectedModules"
						@change="() => { onSelectionChange(item) }"
					></v-checkbox>

						<template v-for="(subItem, subIndex) in item.SubMenuItems">
							<v-checkbox class="pl-8" style="margin-bottom: -32px;" color="indigo"
								:key="`sub_${subIndex}`"
								:label="subItem.Menu"
								:value="subItem"
								v-model="selectedModules"
								@change="() => { onSelectionChange(subItem) }"
							></v-checkbox>
						</template>
				</template>
			</v-card>

      <v-checkbox class="pt-5"
        label="Select All"
        v-model="selectAll"
        @change="onSelectAll"
      ></v-checkbox>
		</v-col>
	</v-row>      
</template>

<script lang="ts">
import { Component, Emit, Prop, Watch } from 'vue-property-decorator';
import VueBase from '../../components/VueBase';
import { ModuleDto } from '@/models/UserAccounts/ModuleDto';

@Component({ components: { } })
export default class SelectUserModule extends VueBase {
  @Prop({ required: true }) public modulesProp!: Array<ModuleDto>;
  @Prop({ required: true }) public selectedModulesProp!: Array<ModuleDto>;

	protected modules: Array<ModuleDto> = []
  protected selectedModules: Array<ModuleDto> =[]
  protected selectAll: boolean = false;
  
  @Watch('modulesProp', { deep: true })
  protected onModulesChange() : void {
    this.modules = [...this.modulesProp];
  }

  @Watch('selectedModulesProp', { deep: true })
  protected onSelectedModulesPropChange() : void {
    this.selectedModules = [...this.selectedModulesProp];
  }
  
  protected onSelectionChange(item: ModuleDto): void {
    const selected: boolean = this.selectedModules.some(x => x.ModuleId == item.ModuleId);
    if (item.IsParentMenu) {
      item.SubMenuItems.forEach(element => {
        const notExist = !this.selectedModules.some(x => x.ModuleId == element.ModuleId);
        if (selected && notExist) {
          this.selectedModules.push(element);
        }
        else {
          this.selectedModules = this.selectedModules.filter(x => x.ModuleId != element.ModuleId);
        }
      });
    }

    if (!item.IsParentMenu && item.ParentModuleId != null) {
      if (selected) {
        // check if the parentid is not selected
        const notExist = !this.selectedModules.some(x => x.ModuleId == item.ParentModuleId);
        if (notExist) {
          const parentItem = this.modulesProp.find(x => x.ModuleId == item.ParentModuleId);
          if (parentItem) {
            this.selectedModules.push(parentItem);
          }
        }
      }
       // if all sub items was unselected, unselect the parent as well
      else {
        //check if there are any related subitems selected
        const hasOtherSubItems = this.selectedModules.some(x => x.ParentModuleId == item.ParentModuleId);
        if (!hasOtherSubItems) {
          this.selectedModules = this.selectedModules.filter(x => x.ModuleId != item.ParentModuleId);
        }
      }
    }
    
		this.$emit('onChange', this.selectedModules);
  }

  protected onSelectAll(): void {
    if (this.selectAll) {
      this.modules.forEach(m => {
        this.selectedModules.push(m);
        if (m.SubMenuItems.length > 0) {
          this.selectedModules.push(...m.SubMenuItems);
        }
      });
    }
    else {
      this.selectedModules = [];
    }
  }

	protected mounted(): void {
		this.selectedModules = [...this.selectedModulesProp];
	}

}
</script>