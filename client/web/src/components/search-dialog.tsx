import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Search } from "lucide-react";
import { routes } from "@/constants/routes";
import {
  Command,
  CommandEmpty,
  CommandInput,
  CommandItem,
  CommandList,
} from "../components/ui/command";
import { Dialog, DialogContent, DialogTitle } from "../components/ui/dialog";

interface SearchCommandDialogProps {
  className?: string;
  buttonClassName?: string;
}

export function SearchCommandDialog({
  className,
  buttonClassName,
}: SearchCommandDialogProps) {
  const [open, setOpen] = useState<boolean>(false);
  const navigate = useNavigate();

  useEffect(() => {
    const down = (e: KeyboardEvent): void => {
      if (e.key === "k" && (e.metaKey || e.ctrlKey)) {
        e.preventDefault();
        setOpen((prev) => !prev);
      }
    };
    document.addEventListener("keydown", down);
    return () => document.removeEventListener("keydown", down);
  }, []);

  const handleSelect = (path: string): void => {
    navigate(path);
    setOpen(false);
  };

  return (
    <>
      <button
        onClick={() => setOpen(true)}
        className={`flex items-center gap-2 border rounded-md px-3 py-2 text-sm text-muted-foreground ${
          buttonClassName || ""
        }`}
        aria-label="Search"
      >
        <Search className="h-4 w-4" />
        <span>Search...</span>
        <kbd className="ml-4 inline-flex h-5 select-none items-center gap-1 rounded border bg-muted px-1.5 font-mono text-xs font-medium">
          <span className="text-xs">⌘</span>K
        </kbd>
      </button>

      <Dialog open={open} onOpenChange={setOpen}>
        <DialogContent className={`p-0 ${className || ""}`}>
          <DialogTitle className="sr-only">Search</DialogTitle>
          <Command className="rounded-lg border shadow-md">
            <CommandInput placeholder="Type a command or search..." />
            <CommandList>
              <CommandEmpty>No results found.</CommandEmpty>
              {routes.map((route) => (
                <CommandItem
                  key={route.path}
                  onSelect={() => handleSelect(route.path)}
                  className="items-center justify-between py-3"
                >
                  <div>
                    <div className="font-medium">{route.name}</div>
                    <div className="text-xs text-muted-foreground">
                      {route.description ?? route.path}
                    </div>
                  </div>
                </CommandItem>
              ))}
            </CommandList>
          </Command>
        </DialogContent>
      </Dialog>
    </>
  );
}
