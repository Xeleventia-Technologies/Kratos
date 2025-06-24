import {
  Drawer as _Drawer,
  DrawerClose,
  DrawerContent,
  DrawerDescription,
  DrawerHeader,
  DrawerTitle,
} from "@/components/ui/drawer";

import { Button } from "@/components/ui/button";

import { useIsMobile } from "@/hooks/use-mobile";
import React, { type PropsWithChildren } from "react";
import { X } from "lucide-react";

export type DrawerProps = {
  open: boolean;
  onClose: () => void;
  titleName: string;
  description: string;
  children: React.ReactNode;
};

export function Drawer(props: PropsWithChildren<DrawerProps>) {
  const isMobile = useIsMobile();

  return (
    <_Drawer
      direction={isMobile ? "bottom" : "right"}
      open={props.open}
      onClose={props.onClose}
    >
      <DrawerContent>
        <DrawerClose asChild>
          <Button
            variant="ghost"
            size="icon"
            className="absolute right-4 top-4 z-10"
          >
            <X className="h-5 w-5" />
          </Button>
        </DrawerClose>

        <DrawerHeader className="gap-1">
          <DrawerTitle>{props.titleName}</DrawerTitle>
          <DrawerDescription>{props.description}</DrawerDescription>
        </DrawerHeader>

        <div className="flex-1 overflow-y-auto">{props.children}</div>
      </DrawerContent>
    </_Drawer>
  );
}
