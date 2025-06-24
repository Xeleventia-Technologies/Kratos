import { useMutation, useQueryClient } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import { toast } from "sonner";
import type { LoginRequest, LoginResponse } from "@/features/login/types/auth";

type LoginParams = {
  onSuccess: (loginResponse: LoginResponse) => void;
  onError: () => void;
};

async function login(request: LoginRequest) {
  const response = await fetch(ApiUrls.loginUrl(), {
    method: "POST",
    credentials: "include",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(request),
  });

  if (!response.ok) {
    let json = await response.json();
    throw new Error(json.detail);
  }

  return response.json();
}

export function useLoginMutation(params: LoginParams) {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: login,

    onSuccess: (loginResponse: LoginResponse) => {
      queryClient.invalidateQueries({ queryKey: [QueryKeys.Login] });
      toast.success("Logged In successfully");
      params.onSuccess(loginResponse);
    },

    onError: () => {
      toast.error("Failed logged in !");
      params.onError();
    },
  });
}
