import { AlertCircle } from "lucide-react";
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert";

type props = {
  errorMessage?: string;
};
export function ErrorDisplay(props: props) {
  return (
    <div className="px-4 py-6">
      <Alert variant="destructive">
        <AlertCircle className="h-5 w-5" />
        <AlertTitle>Error while fetching data !</AlertTitle>
        <AlertDescription>
          {props.errorMessage ||
            "Something went wrong. Please try again later."}
        </AlertDescription>
      </Alert>
    </div>
  );
}
