import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import { toast } from "sonner";
import type { EditServiceRequest } from "@/features/services/types/service";
import { authFetch } from "@/utils/authFetch";

type EditServiceParams = {
  onSuccess: () => void;
  onError: () => void;
};

function buildFormData(serviceInfo: EditServiceRequest) {
  const formData = new FormData();

  formData.append("name", serviceInfo.name);
  formData.append("description", serviceInfo.description);
  formData.append("summary", serviceInfo.summary);

  if (serviceInfo.image) {
    formData.append("image", serviceInfo.image);
  }

  if (serviceInfo.parentServiceId) {
    formData.append("parentServiceId", serviceInfo.parentServiceId.toString());
  }

  return formData;
}

async function editService(request: EditServiceRequest) {
  const response = await authFetch(ApiUrls.updateServiceUrl(request.id), {
    method: "PUT",
    body: buildFormData(request),
  });

  if (!response.ok) {
    let json = await response.json();
    throw new Error(json.detail);
  }
}

export function useEditServiceMutation(params: EditServiceParams) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: editService,

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [QueryKeys.Services] });
      toast.success("Service updated successfully");
      params.onSuccess();
    },

    onError: (error) => {
      toast.error(error?.message || "Failed to update service");
      params.onError();
    },
  });
}
