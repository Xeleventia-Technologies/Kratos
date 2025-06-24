import { useQuery, type UseQueryResult } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import type { ArticleResponse } from "@/features/articles/types/article";
import type { ArticleFilterParams } from "@/constants/urlTypes";
import { authFetch } from "@/utils/authFetch";

async function fetchArticle(
  filters: ArticleFilterParams
): Promise<ArticleResponse> {
  const result = await authFetch(ApiUrls.getAllArticlesUrl(filters), {
    method: "GET",
  });
  return result.json();
}

export function useArticlesQuery(
  filters: ArticleFilterParams
): UseQueryResult<ArticleResponse, Error> {
  return useQuery<ArticleResponse, Error>({
    queryKey: [QueryKeys.Articles, filters],
    queryFn: () => fetchArticle(filters),
    staleTime: 10000,
  });
}
