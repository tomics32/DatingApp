export interface PaginatedResponse<T> {
    items: T[];
    currentPage: number;
    totalPages: number;
    pageSize: number;
    totalCount: number;
}