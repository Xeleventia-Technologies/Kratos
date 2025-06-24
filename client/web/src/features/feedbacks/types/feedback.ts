import type { Column } from "@/types/column";

export type Feedback = {
  id: number;
  rating: number;
};

export function createFeedbackMappings(): Column[] {
  return [
    {
      header: "Full Name",
      accessorKey: "fullName",
    },
    {
      header: "Rating out of 5",
      accessorKey: "outOfFiveRating",
    },
    {
      header: "Comment",
      accessorKey: "comment",
    },
  ];
}
