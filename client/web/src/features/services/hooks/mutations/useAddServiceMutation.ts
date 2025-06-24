import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import { toast } from "sonner";
import type { AddServiceRequest } from "@/features/services/types/service";
import { authFetch } from "@/utils/authFetch";

type AddServiceParams = {
  onSuccess: () => void;
  onError: () => void;
};

function buildFormData(serviceInfo: AddServiceRequest) {
  const formData = new FormData();

  formData.append("name", serviceInfo.name);
  formData.append("description", serviceInfo.description);
  formData.append("summary", serviceInfo.summary);
  formData.append("image", serviceInfo.image);

  if (serviceInfo.parentServiceId) {
    formData.append("parentServiceId", serviceInfo.parentServiceId.toString());
  }

  return formData;
}

async function addService(request: AddServiceRequest) {
  const response = await authFetch(ApiUrls.addSerivceUrl(), {
    method: "POST",
    body: buildFormData(request),
  });

  if (!response.ok) {
    let json = await response.json();
    throw new Error(json.detail);
  }
}

export function useAddServiceMutation(params: AddServiceParams) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: addService,

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [QueryKeys.Services] });
      toast.success("Service added successfully");
      params.onSuccess();
    },

    onError: (error) => {
      toast.error(error?.message || "Failed to add service");
      params.onError();
    },
  });
}
