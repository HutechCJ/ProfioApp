type ApiResponse<T = any> = {
  data: T;
  isError: boolean;
  errorMessage: any;
};

type Paging<T> = {
  pageIndex: number;
  pageSize: number;
  count: number;
  totalCount: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  items: T[];
};

type PagingOptions = {
  Filter: string;
  OrderBy: string;
  OrderByDescending: string;
  IncludeStrings: string[];
  PageNumber: number;
  PageSize: number;
};
