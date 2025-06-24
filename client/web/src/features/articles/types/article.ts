import type { Column } from "@/types/column";

export type Article = {
  id: number;
  title: string;
  summary: string;
  content: string;
  imageFileNameL: string;
  createdByUserFullName: string;
  createdDate: string;
  approvalStatus: string;
};

export type ArticleResponse = {
  articles: Array<Article>;
  totalCount: number;
};

export type ChangeApprovalStatus = {
  id: number;
  approvalStatus: string;
};

export function createArticleMappings(): Column[] {
  return [
    {
      header: "Title",
      accessorKey: "title",
    },
    {
      header: "Summary",
      accessorKey: "summary",
    },
    {
      header: "User Full Name",
      accessorKey: "createdByUserFullName",
    },
    {
      header: "Date",
      accessorKey: "createdAt",
    },
  ];
}
