import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import { toast } from "sonner";
import { authFetch } from "@/utils/authFetch";

type LogoutParams = {
  onSuccess: () => void;
  onError: () => void;
};

async function logout() {
  const response = await authFetch(ApiUrls.logoutUrl(), {
    method: "DELETE",
    credentials: "include",
  });

  if (!response.ok) {
    throw new Error("Failed logged out !");
  }
}

export function useLogoutMutation(params: LogoutParams) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: logout,

    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: [QueryKeys.Login] });
      toast.success("Logged Out successfully");
      localStorage.removeItem("user");
      params.onSuccess();
    },

    onError: () => {
      toast.error("Failed logged out !");
      params.onError();
    },
  });
}
