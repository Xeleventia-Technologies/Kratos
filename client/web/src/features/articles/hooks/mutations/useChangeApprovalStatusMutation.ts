import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import { toast } from "sonner";
import type { ChangeApprovalStatus } from "@/features/articles/types/article";
import { authFetch } from "@/utils/authFetch";

type EditApprovalStatusParams = {
  onSuccess: () => void;
  onError: () => void;
};

async function changeArticleApprovalStatus(request: ChangeApprovalStatus) {
  const response = await authFetch(
    ApiUrls.changeApprovalStatusUrl(request.id),
    {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        approvalStatus: request.approvalStatus,
      }),
    }
  );

  if (!response.ok) {
    let json = await response.json();
    throw new Error(json.detail);
  }
}

export function useChangeApprovalStatusMutation(
  params: EditApprovalStatusParams
) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: changeArticleApprovalStatus,

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [QueryKeys.Articles] });
      toast.success("Article updated successfully");
      params.onSuccess();
    },

    onError: (error) => {
      toast.error(error?.message || "Failed to update article");
      params.onError();
    },
  });
}
