import { AppSidebar } from "@/components/app-sidebar";
import { SiteHeader } from "@/components/site-header";
import { SidebarInset, SidebarProvider } from "@/components/ui/sidebar";
import { Toaster } from "@/components/ui/sonner";

import { Routes } from "./Routes";
import { useContext } from "react";
import { AuthContext } from "./utils/authContext";
import Login from "./features/login/pages/login";

export default function Page() {
  const { user } = useContext(AuthContext);

  return (
    <>
      {user ? (
        <SidebarProvider>
          <AppSidebar variant="inset" />
          <SidebarInset>
            <SiteHeader />
            <div className="flex flex-1 flex-col">
              <div className="@container/main flex flex-1 flex-col gap-2">
                <div className="flex flex-col gap-4 py-4 md:gap-6 md:py-6">
                  <Routes />
                </div>
              </div>
            </div>
          </SidebarInset>
          <Toaster />
        </SidebarProvider>
      ) : (
        <Login />
      )}
    </>
  );
}
