import type { Column } from "@/types/column";

export type Service = {
  id: number;
  name: string;
  description: string;
  summary: string;
  imageFileName: string;
  parentServiceName?: string;
  parentServiceId?: number;
};

export type AddServiceRequest = {
  name: string;
  description: string;
  summary: string;
  image: File;
  parentServiceId?: number;
};

export type EditServiceRequest = {
  id: number;
  name: string;
  description: string;
  summary: string;
  image?: File;
  parentServiceId?: number;
};

export function createServiceMappings(): Column[] {
  return [
    {
      header: "Name",
      accessorKey: "name",
    },
    {
      header: "Summary",
      accessorKey: "summary",
    },
    {
      header: "Parent Service Name",
      accessorKey: "parentServiceName",
    },
  ];
}
