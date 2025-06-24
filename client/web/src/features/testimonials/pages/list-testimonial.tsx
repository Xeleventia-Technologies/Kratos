import { DataTable } from "@/components/data-table";
import { SkeletonLoading } from "@/components/skeleton-loading";
import { ErrorDisplay } from "@/components/error-display";

import { createTestimonialMappings } from "@/features/testimonials/types/testimonial";
import { useTestimonialsQuery } from "@/features/testimonials/hooks/queries/useGetTestimonialQuery";

export default function ListTestimonials() {
  const testimonialColumns = createTestimonialMappings();
  const testimonialsQuery = useTestimonialsQuery();

  if (testimonialsQuery.isLoading) {
    return <SkeletonLoading />;
  } else if (testimonialsQuery.isError) {
    return <ErrorDisplay errorMessage={testimonialsQuery.error?.message} />;
  }

  return (
    <>
      <DataTable
        data={testimonialsQuery.data || []}
        columns={testimonialColumns}
        defaultRowsCount={10}
      />
    </>
  );
}
