import { ApiUrls } from "@/constants/apiUrls";

export async function authFetch(
  input: RequestInfo,
  init: RequestInit = {}
): Promise<Response> {
  const user = localStorage.getItem("user");
  const token = user ? JSON.parse(user).accessToken : null;

  const headers: Record<string, string> = {
    ...(init.headers as Record<string, string>),
    ...(token ? { Authorization: `Bearer ${token}` } : {}),
  };

  const response = await fetch(input, {
    ...init,
    headers,
  });

  if (response.status === 401) {
    await refreshTokens();
    return authFetch(input, init);
  }

  return response;
}

async function refreshTokens() {
  let userJson = localStorage.getItem("user");

  const user = userJson ? JSON.parse(userJson) : null;
  const accessToken = user?.accessToken;
  const response = await fetch(ApiUrls.refreshTokensUrl(), {
    method: "POST",
    credentials: "include",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${accessToken}`,
    },
  });

  if (response.status === 401) {
    const response = await authFetch(ApiUrls.logoutUrl(), {
      method: "DELETE",
      credentials: "include",
    });

    if (response.ok) {
      localStorage.clear();
      window.location.href = "/";
    }
  }

  if (!response.ok) throw new Error("Failed to refresh tokens");

  const json = await response.json();
  user.accessToken = json.accessToken;
  localStorage.setItem("user", JSON.stringify(user));
}
