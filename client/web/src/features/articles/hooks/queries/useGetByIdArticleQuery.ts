import { useQuery, type UseQueryResult } from "@tanstack/react-query";
import { ApiUrls } from "@/constants/apiUrls";
import { QueryKeys } from "@/constants/queryKeys";
import type { Article } from "@/features/articles/types/article";
import { authFetch } from "@/utils/authFetch";

async function fetchArticleById(id: number): Promise<Article> {
  const result = await authFetch(ApiUrls.getArticleByIdUrl(id), {
    method: "GET",
  });
  return result.json();
}

export function useArticlesByIdQuery(id: number): UseQueryResult<Article> {
  return useQuery<Article>({
    queryKey: [QueryKeys.Articles, id],
    queryFn: () => fetchArticleById(id),
    staleTime: 10000,
  });
}
