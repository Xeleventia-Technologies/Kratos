import { useState } from "react";
import { DataTable } from "@/components/data-table";
import { SkeletonLoading } from "@/components/skeleton-loading";
import { ErrorDisplay } from "@/components/error-display";
import { Drawer } from "@/components/drawer";

import { useAddMemberMutation } from "@/features/members/hooks/mutations/useAddMemberMutation";
import { useEditMemberMutation } from "@/features/members/hooks/mutations/useEditMemberMutation";
import {
  createMemberMappings,
  type AddMemberRequest,
  type EditMemberRequest,
  type Member,
} from "@/features/members/types/member";
import { useMembersQuery } from "@/features/members/hooks/queries/useGetMemberQuery";
import { ActionType } from "@/constants/actionType";
import { AddForm } from "@/features/members/components/add-form";
import { EditForm } from "@/features/members/components/edit-form";

export default function ListMembers() {
  const [selectedData, setSelectedData] = useState<Member | undefined>();
  const [drawer, setDrawer] = useState<ActionType>(ActionType.None);
  const memberColumns = createMemberMappings();
  const membersQuery = useMembersQuery();

  const handleOpen = (type: ActionType, id?: number) => {
    if (id) {
      setSelectedData(membersQuery.data?.find((member) => member.id === id));
    }
    setDrawer(type);
  };

  const handleClose = () => {
    setDrawer(ActionType.None);
  };

  const addMemberMutation = useAddMemberMutation({
    onSuccess: () => {
      handleClose();
    },
    onError: () => {
      handleClose();
    },
  });

  const editMemberMutation = useEditMemberMutation({
    onSuccess: () => {
      handleClose();
    },
    onError: () => {
      handleClose();
    },
  });

  function addMember(body: AddMemberRequest) {
    addMemberMutation.mutate(body);
  }

  function editMember(body: EditMemberRequest) {
    editMemberMutation.mutate(body);
  }

  if (membersQuery.isLoading) {
    return <SkeletonLoading />;
  } else if (membersQuery.isError) {
    return <ErrorDisplay errorMessage={membersQuery.error?.message} />;
  }

  return (
    <>
      <DataTable
        data={membersQuery.data || []}
        columns={memberColumns}
        handleOpen={handleOpen}
        defaultRowsCount={10}
      />

      <Drawer
        open={drawer === ActionType.Add}
        onClose={handleClose}
        titleName={"Add Member"}
        description={"Add a new member"}
      >
        <AddForm
          parentServices={membersQuery.data || []}
          handleSubmit={addMember}
          isSubmitting={addMemberMutation.isPending}
        />
      </Drawer>

      <Drawer
        open={drawer === ActionType.Edit}
        onClose={handleClose}
        titleName={"Edit Member"}
        description={"Edit a member"}
      >
        <EditForm
          initialData={selectedData}
          parentServices={membersQuery.data || []}
          handleSubmit={editMember}
          isSubmitting={editMemberMutation.isPending}
        />
      </Drawer>
    </>
  );
}
