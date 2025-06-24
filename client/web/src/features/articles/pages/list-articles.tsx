import { useState, useEffect } from "react";
import { ArticleDataTable } from "@/features/articles/components/article-data-table";
import { SkeletonLoading } from "@/components/skeleton-loading";
import { ErrorDisplay } from "@/components/error-display";

import {
  createArticleMappings,
  type ChangeApprovalStatus,
} from "@/features/articles/types/article";
import { useArticlesQuery } from "@/features/articles/hooks/queries/useGetArticleQuery";
import type {
  ArticleApprovalStatus,
  ArticleFilterParams,
} from "@/constants/urlTypes";
import { useChangeApprovalStatusMutation } from "@/features/articles/hooks/mutations/useChangeApprovalStatusMutation";

export default function ListArticles() {
  const [pagination, setPagination] = useState({ pageIndex: 0, pageSize: 10 });
  const [search, setSearch] = useState("");
  const [from, setFromDate] = useState("");
  const [to, setToDate] = useState("");
  const [approval, setApprovalStatus] = useState<ArticleApprovalStatus | "">(
    ""
  );
  const [debouncedSearch, setDebouncedSearch] = useState(search);
  const articleColumns = createArticleMappings();

  useEffect(() => {
    const handler = setTimeout(() => {
      setDebouncedSearch(search);
    }, 300);

    return () => {
      clearTimeout(handler);
    };
  }, [search]);

  const filters: ArticleFilterParams = {
    page: pagination.pageIndex + 1,
    count: pagination.pageSize,
    search: debouncedSearch,
    from: from,
    to: to,
    approval: approval || undefined,
  };
  const articleQuery = useArticlesQuery(filters);

  const changeApprovalStatusMutation = useChangeApprovalStatusMutation({
    onSuccess: () => {
      articleQuery.refetch();
    },
    onError: () => {
      articleQuery.refetch();
    },
  });

  function changeApprovalStatus(body: ChangeApprovalStatus) {
    changeApprovalStatusMutation.mutate(body);
  }

  if (articleQuery.isLoading) {
    return <SkeletonLoading />;
  } else if (articleQuery.isError) {
    return <ErrorDisplay errorMessage={articleQuery.error?.message} />;
  }

  return (
    <>
      <ArticleDataTable
        data={articleQuery.data!.articles}
        totalCount={articleQuery.data!.totalCount}
        columns={articleColumns}
        defaultRowsCount={10}
        pagination={pagination}
        setPagination={setPagination}
        filters={{
          search,
          from,
          to,
          approval,
          setSearch,
          setFromDate,
          setToDate,
          setApprovalStatus,
        }}
        changeApprovalStatus={changeApprovalStatus}
      />
    </>
  );
}
