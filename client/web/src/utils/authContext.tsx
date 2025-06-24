import type { LoginResponse } from "@/features/login/types/auth";
import React, { createContext, useState, type ReactNode } from "react";

type AuthContext = {
  user: LoginResponse | null;
  setUser: React.Dispatch<React.SetStateAction<LoginResponse | null>>;
};

export const AuthContext = createContext<AuthContext>({} as AuthContext);

export function AuthContextProvider({ children }: { children: ReactNode }) {
  const userDetail = localStorage.getItem("user");
  const [user, setUser] = useState<LoginResponse | null>(
    userDetail ? JSON.parse(userDetail) : null
  );

  return (
    <AuthContext.Provider value={{ user, setUser }}>
      {children}
    </AuthContext.Provider>
  );
}
