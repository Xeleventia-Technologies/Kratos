export enum ArticleApprovalStatus {
  Pending = "pending",
  Approved = "approved",
  Rejected = "rejected",
}

export type ArticleFilterParams = {
  count: number;
  page: number;
  search?: string;
  to?: string;
  from?: string;
  approval?: ArticleApprovalStatus;
};
