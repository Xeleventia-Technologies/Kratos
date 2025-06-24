import { Skeleton } from "@//components/ui/skeleton";

export function SkeletonLoading() {
  return (
    <div className="space-y-4 px-4 py-6">
      {[...Array(10)].map((_, i) => (
        <div key={i} className="flex items-center gap-4">
          <Skeleton className="h-4 w-[20%]" />
          <Skeleton className="h-4 w-[30%]" />
          <Skeleton className="h-4 w-[25%]" />
          <Skeleton className="h-4 w-[15%]" />
        </div>
      ))}
    </div>
  );
}
