import type { Column } from "@/types/column";

export type Testimonial = {
  id: number;
  text: string;
  fullName: string;
};

export function createTestimonialMappings(): Column[] {
  return [
    {
      header: "Text",
      accessorKey: "text",
    },
    {
      header: "Full Name",
      accessorKey: "fullName",
    },
  ];
}
