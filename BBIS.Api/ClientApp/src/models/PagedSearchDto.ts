export class PagedSearchDto {
  SearchText: string = '';
  PageNumber: number = 1;
  PageSize: number = 10;
  SortBy: string = '';
  SortDesc: boolean = false;
}

export class PagedSearchResultDto<T> {
  TotalCount: number = 0;
  CurrentPage: number = 0;
  PageSize: number = 0
  Results: Array<T> = [];
}