import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import { toast } from "sonner";
import type { EditMemberRequest } from "@/features/members/types/member";
import { authFetch } from "@/utils/authFetch";

type EditMemberParams = {
  onSuccess: () => void;
  onError: () => void;
};

function buildFormData(memberInfo: EditMemberRequest) {
  const formData = new FormData();

  formData.append("fullName", memberInfo.fullName);
  formData.append("bio", memberInfo.bio);
  formData.append("position", memberInfo.position);

  if (memberInfo.displayPicture) {
    formData.append("displayPicture", memberInfo.displayPicture);
  }

  return formData;
}

async function editMember(request: EditMemberRequest) {
  const response = await authFetch(ApiUrls.updateMemberUrl(request.id), {
    method: "PUT",
    body: buildFormData(request),
  });

  if (!response.ok) {
    let json = await response.json();
    throw new Error(json.detail);
  }
}

export function useEditMemberMutation(params: EditMemberParams) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: editMember,

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [QueryKeys.Members] });
      toast.success("Member updated successfully");
      params.onSuccess();
    },

    onError: (error) => {
      toast.error(error?.message || "Failed to update member");
      params.onError();
    },
  });
}
