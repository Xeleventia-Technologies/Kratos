import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import { toast } from "sonner";
import type { AddMemberRequest } from "@/features/members/types/member";
import { authFetch } from "@/utils/authFetch";

type AddMemberParams = {
  onSuccess: () => void;
  onError: () => void;
};

function buildFormData(memberInfo: AddMemberRequest) {
  const formData = new FormData();

  formData.append("fullName", memberInfo.fullName);
  formData.append("bio", memberInfo.bio);
  formData.append("position", memberInfo.position);
  formData.append("displayPicture", memberInfo.displayPicture);

  return formData;
}

async function addMember(request: AddMemberRequest) {
  const response = await authFetch(ApiUrls.addMemberUrl(), {
    method: "POST",
    body: buildFormData(request),
  });

  if (!response.ok) {
    let json = await response.json();
    throw new Error(json.detail);
  }
}

export function useAddMemberMutation(params: AddMemberParams) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: addMember,

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [QueryKeys.Members] });
      toast.success("Member added successfully");
      params.onSuccess();
    },

    onError: (error) => {
      toast.error(error?.message || "Failed to add member");
      params.onError();
    },
  });
}
