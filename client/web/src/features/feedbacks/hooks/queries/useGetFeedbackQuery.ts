import { useQuery, type UseQueryResult } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import type { Feedback } from "@/features/feedbacks/types/feedback";
import { authFetch } from "@/utils/authFetch";

async function fetchFeedback(): Promise<Array<Feedback>> {
  const result = await authFetch(ApiUrls.getAllFeedbackUrl());
  return result.json();
}

export function useFeedbacksQuery(): UseQueryResult<Array<Feedback>, Error> {
  return useQuery<Array<Feedback>, Error>({
    queryKey: [QueryKeys.Feedbacks],
    queryFn: fetchFeedback,
    staleTime: 10000,
  });
}
