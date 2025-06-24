import { Separator } from "@/components/ui/separator";
import { SidebarTrigger } from "@/components/ui/sidebar";
import { ModeToggle } from "@/components/mode-toggle";
import { SearchCommandDialog } from "@/components/search-dialog";
import { useLocation } from "react-router-dom";
import { routes } from "@/constants/routes";

export function SiteHeader() {
  const location = useLocation();

  return (
    <header className="flex h-(--header-height) shrink-0 items-center gap-2 border-b transition-[width,height] ease-linear group-has-data-[collapsible=icon]/sidebar-wrapper:h-(--header-height)">
      <div className="flex w-full items-center gap-1 px-4 m-1 lg:gap-2 lg:px-6">
        <SidebarTrigger className="-ml-1" />
        <Separator
          orientation="vertical"
          className="mx-2 data-[orientation=vertical]:h-4"
        />
        <h1 className="text-base font-medium">
          {routes.map((route) => {
            const isActive = location.pathname === route.path;
            return (
              <span
                key={route.name}
                className={isActive ? "font-semibold" : ""}
              >
                {isActive ? route.name : ""}
              </span>
            );
          })}
        </h1>
        <div className="ml-auto flex items-center gap-2">
          <SearchCommandDialog buttonClassName="hidden sm:flex" />
          <ModeToggle />
        </div>
      </div>
    </header>
  );
}
