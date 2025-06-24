import { useQuery, type UseQueryResult } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import type { Service } from "@/features/services/types/service";
import { authFetch } from "@/utils/authFetch";

async function fetchService(): Promise<Array<Service>> {
  const result = await authFetch(ApiUrls.getAllServicesUrl());
  return result.json();
}

export function useServicesQuery(): UseQueryResult<Array<Service>, Error> {
  return useQuery<Array<Service>, Error>({
    queryKey: [QueryKeys.Services],
    queryFn: fetchService,
    staleTime: 10000,
  });
}
