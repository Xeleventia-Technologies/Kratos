import React, { useRef } from "react";
import { Separator } from "@/components/ui/separator";
import { Label } from "@/components/ui/label";
import { Input } from "@/components/ui/input";
import { Loader2 } from "lucide-react";
import type { AddMemberRequest, Member } from "@/features/members/types/member";
import { Button } from "@/components/ui/button";

export type AddFormProps = {
  parentServices: Member[];
  handleSubmit: (data: AddMemberRequest) => void;
  isSubmitting: boolean;
};

export function AddForm(props: AddFormProps) {
  const formRef = useRef<HTMLFormElement>(null);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const member = Object.fromEntries(new FormData(formRef.current!));

    props.handleSubmit(member as unknown as AddMemberRequest);
  };

  return (
    <div>
      <div className="flex flex-col gap-4 overflow-y-auto px-4 text-sm">
        <Separator />
        <form
          className="flex flex-col gap-4"
          ref={formRef}
          onSubmit={handleSubmit}
        >
          <div className="flex flex-col gap-3">
            <Label htmlFor="fullName">Full Name</Label>
            <Input id="fullName" type="text" name="fullName" required />
          </div>
          <div className="flex flex-col gap-3">
            <Label htmlFor="position">Position</Label>
            <Input id="position" type="text" name="position" required />
          </div>
          <div className="flex flex-col gap-3">
            <Label htmlFor="bio">Bio</Label>
            <Input id="bio" type="text" name="bio" required />
          </div>
          <div className="flex flex-col gap-3">
            <Label htmlFor="file">Display Profile Image</Label>
            <Input
              id="file"
              type="file"
              name="displayPicture"
              accept="image/*"
            />
          </div>
          <Button type="submit" disabled={props.isSubmitting}>
            {props.isSubmitting ? (
              <>
                <Loader2 className="mr-2 h-4 w-4 animate-spin" />
                Submitting...
              </>
            ) : (
              "Submit"
            )}
          </Button>
        </form>
      </div>
    </div>
  );
}
