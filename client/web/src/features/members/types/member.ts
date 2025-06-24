import type { ActionType } from "@/constants/actionType";
import type { Column } from "@/types/column";

export type Member = {
  id: number;
  fullName: string;
  bio: string;
  position: string;
  displayPictureFileName: string;
};

export type AddMemberRequest = {
  fullName: string;
  bio: string;
  position: string;
  displayPicture: File;
};

export type EditMemberRequest = {
  id: number;
  fullName: string;
  bio: string;
  position: string;
  displayPicture?: File;
};

export type MemberActions = {
  actionName: ActionType.Edit | ActionType.Delete;
  actionFn: () => EditMemberRequest;
};

export function createMemberMappings(): Column[] {
  return [
    {
      header: "Full Name",
      accessorKey: "fullName",
    },
    {
      header: "Bio",
      accessorKey: "bio",
    },
  ];
}
