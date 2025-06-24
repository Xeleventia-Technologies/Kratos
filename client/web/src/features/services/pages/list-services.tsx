import { useState } from "react";
import { DataTable } from "@/components/data-table";
import { SkeletonLoading } from "@/components/skeleton-loading";
import { ErrorDisplay } from "@/components/error-display";
import { Drawer } from "@/components/drawer";

import { useAddServiceMutation } from "@/features/services/hooks/mutations/useAddServiceMutation";
import { useEditServiceMutation } from "@/features/services/hooks/mutations/useEditServiceMutation";
import {
  createServiceMappings,
  type AddServiceRequest,
  type EditServiceRequest,
  type Service,
} from "@/features/services/types/service";
import { useServicesQuery } from "@/features/services/hooks/queries/useGetServiceQuery";
import { ActionType } from "@/constants/actionType";
import { AddForm } from "@/features/services/components/add-form";
import { EditForm } from "@/features/services/components/edit-form";

export default function ListServices() {
  const [selectedData, setSelectedData] = useState<Service | undefined>();
  const [drawer, setDrawer] = useState<ActionType>(ActionType.None);
  const serviceColumns = createServiceMappings();
  const servicesQuery = useServicesQuery();

  const handleOpen = (type: ActionType, id?: number) => {
    if (id) {
      setSelectedData(servicesQuery.data?.find((service) => service.id === id));
    }
    setDrawer(type);
  };

  const handleClose = () => {
    setDrawer(ActionType.None);
  };

  const addServiceMutation = useAddServiceMutation({
    onSuccess: () => {
      handleClose();
    },
    onError: () => {
      handleClose();
    },
  });

  const editServiceMutation = useEditServiceMutation({
    onSuccess: () => {
      handleClose();
    },
    onError: () => {
      handleClose();
    },
  });

  function addService(body: AddServiceRequest) {
    addServiceMutation.mutate(body);
  }

  function editService(body: EditServiceRequest) {
    editServiceMutation.mutate(body);
  }

  if (servicesQuery.isLoading) {
    return <SkeletonLoading />;
  } else if (servicesQuery.isError) {
    return <ErrorDisplay errorMessage={servicesQuery.error?.message} />;
  }

  return (
    <>
      <DataTable
        data={servicesQuery.data || []}
        columns={serviceColumns}
        handleOpen={handleOpen}
        defaultRowsCount={10}
      />

      <Drawer
        open={drawer === ActionType.Add}
        onClose={handleClose}
        titleName={"Add Service"}
        description={"Add a new service"}
      >
        <AddForm
          parentServices={servicesQuery.data || []}
          handleSubmit={addService}
          isSubmitting={addServiceMutation.isPending}
        />
      </Drawer>

      <Drawer
        open={drawer === ActionType.Edit}
        onClose={handleClose}
        titleName={"Edit Service"}
        description={"Edit a service"}
      >
        <EditForm
          initialData={selectedData}
          parentServices={servicesQuery.data || []}
          handleSubmit={editService}
          isSubmitting={editServiceMutation.isPending}
        />
      </Drawer>
    </>
  );
}
