import { useQuery, type UseQueryResult } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import type { Testimonial } from "@/features/testimonials/types/testimonial";
import { authFetch } from "@/utils/authFetch";

async function fetchTestimonial(): Promise<Array<Testimonial>> {
  const result = await authFetch(ApiUrls.getAllTestimonialsUrl());
  return result.json();
}

export function useTestimonialsQuery(): UseQueryResult<Array<Testimonial>, Error> {
  return useQuery<Array<Testimonial>, Error>({
    queryKey: [QueryKeys.Testimonials],
    queryFn: fetchTestimonial,
    staleTime: 20000,
  });
}
