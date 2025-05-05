import { Module, VuexModule, Mutation, Action  } from 'vuex-module-decorators';
import LookupService from '@/services/LookupService';
import { ILookup } from '@/models/Lookups/LookupDto';
import { ILookupOptions } from '@/models/Lookups/LookupOptions';

@Module({ namespaced: true, name: 'LookupModule' })
export default class LookupModule extends VuexModule {
  protected lookupService: LookupService = new LookupService();

  protected lookups: Array<ILookup> = [];

  @Mutation
  public LOAD_LOOKUPS(lookups: Array<ILookup>): void {
    this.lookups = lookups;
  }

  @Mutation
  public RESET_LOOKUPS(): void { 
    this.lookups = [];
  }


  @Action({ commit: 'LOAD_LOOKUPS' })
  public async loadLookups(keys: Array<string> = []): Promise<Array<ILookup>> { 
    if (this.lookups.length === 0) {
      return await this.lookupService.getLookupByKeys(keys);
    }

    return this.lookups;
  }

  public get getOptionsByKey(): (key: string) => Array<ILookupOptions> {
    return (key) => {
      let options = this.lookups.find(x => x.LookupKey === key);
      return options != null ? options.LookupOptions : [];
    }
  }
}