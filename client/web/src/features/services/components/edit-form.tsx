import React, { useRef, useState, useEffect } from "react";
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Separator } from "@/components/ui/separator";
import { Label } from "@/components/ui/label";
import { Input } from "@/components/ui/input";
import { Loader2 } from "lucide-react";
import { Urls } from "@/constants/apiUrls";
import type { EditServiceRequest, Service } from "@/features/services/types/service";
import { Button } from "@/components/ui/button";
import { Textarea } from "@/components/ui/textarea";

export type EditFormProps = {
  initialData?: Service;
  parentServices: Service[];
  handleSubmit: (data: EditServiceRequest) => void;
  isSubmitting: boolean;
};

export function EditForm(props: EditFormProps) {
  const formRef = useRef<HTMLFormElement>(null);
  const [selectedParentService, setSelectedParentService] = useState<number>();

  useEffect(() => {
    if (props.initialData) {
      setSelectedParentService(props.initialData.parentServiceId);
    }
  }, [props.initialData]);

  const handleSelectChange = (value: string) => {
    const serviceId = Number(value);
    setSelectedParentService(serviceId);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const service = Object.fromEntries(new FormData(formRef.current!));

    props.handleSubmit(service as unknown as EditServiceRequest);
    setSelectedParentService(0);
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
            <Label htmlFor="name">Select a Parent Service</Label>
            <Input
              type="hidden"
              name="parentServiceId"
              value={selectedParentService?.toString()}
            />
            <Select
              value={selectedParentService?.toString()}
              onValueChange={handleSelectChange}
            >
              <SelectTrigger className="w-full">
                <SelectValue placeholder="Select a Parent Service" />
              </SelectTrigger>
              <SelectContent>
                <SelectGroup>
                  <SelectLabel>All Services</SelectLabel>
                  {props.parentServices.map((service) => (
                    <SelectItem key={service.id} value={service.id.toString()}>
                      {service.name}
                    </SelectItem>
                  ))}
                </SelectGroup>
              </SelectContent>
            </Select>
          </div>
          <div className="flex flex-col gap-3">
            <Label htmlFor="name">Title *</Label>
            <Input
              id="name"
              type="text"
              name="name"
              defaultValue={props.initialData?.name}
              required
            />
            <Input
              id="id"
              type="hidden"
              name="id"
              defaultValue={props.initialData?.id}
            />
          </div>
          <div className="flex flex-col gap-3">
            <Label htmlFor="summary">Summary *</Label>
            <Textarea
              id="summary"
              name="summary"
              defaultValue={props.initialData?.summary}
              required
            />
          </div>
          <div className="flex flex-col gap-3">
            <Label htmlFor="description">Description *</Label>
            <Textarea
              id="description"
              name="description"
              defaultValue={props.initialData?.description}
              required
            />
          </div>
          <div className="flex flex-col gap-3">
            <Label htmlFor="file">Service Post Image *</Label>
            <Input id="file" type="file" name="image" accept="image/*" />
            {props.initialData?.imageFileName && (
              <img
                src={Urls.serviceImage(props.initialData?.imageFileName!)}
                alt="This is an image"
              />
            )}
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
