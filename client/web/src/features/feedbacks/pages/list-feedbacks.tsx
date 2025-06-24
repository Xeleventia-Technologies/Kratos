import { DataTable } from "@/components/data-table";
import { SkeletonLoading } from "@/components/skeleton-loading";
import { ErrorDisplay } from "@/components/error-display";

import { createFeedbackMappings } from "@/features/feedbacks/types/feedback";
import { useFeedbacksQuery } from "@/features/feedbacks/hooks/queries/useGetFeedbackQuery";

export default function ListFeedbacks() {
  const feedbackColumns = createFeedbackMappings();
  const feedbacksQuery = useFeedbacksQuery();

  if (feedbacksQuery.isLoading) {
    return <SkeletonLoading />;
  } else if (feedbacksQuery.isError) {
    return <ErrorDisplay errorMessage={feedbacksQuery.error?.message} />;
  }

  return (
    <>
      <DataTable
        data={feedbacksQuery.data || []}
        columns={feedbackColumns}
        defaultRowsCount={10}
      />
    </>
  );
}
