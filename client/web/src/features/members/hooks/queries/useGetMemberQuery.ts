import { useQuery, type UseQueryResult } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import type { Member } from "@/features/members/types/member";
import { authFetch } from "@/utils/authFetch";

async function fetchMember(): Promise<Array<Member>> {
  const result = await authFetch(ApiUrls.getAllMembersUrl());
  return result.json();
}

export function useMembersQuery(): UseQueryResult<Array<Member>, Error> {
  return useQuery<Array<Member>, Error>({
    queryKey: [QueryKeys.Members],
    queryFn: fetchMember,
    staleTime: 10000,
  });
}
