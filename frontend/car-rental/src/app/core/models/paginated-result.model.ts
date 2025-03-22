export interface PaginatedResult<T> {
  totalPages: number;
  pageSize: number;
  totalCount: number;
  currentPage: number;
  items: T[];
}
