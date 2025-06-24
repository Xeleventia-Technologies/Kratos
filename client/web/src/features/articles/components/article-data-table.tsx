import * as React from "react";
import {
  IconChevronLeft,
  IconChevronRight,
  IconChevronsLeft,
  IconChevronsRight,
} from "@tabler/icons-react";
import {
  type ColumnFiltersState,
  type SortingState,
  type VisibilityState,
  getCoreRowModel,
  getFacetedRowModel,
  getFacetedUniqueValues,
  getFilteredRowModel,
  getPaginationRowModel,
  getSortedRowModel,
  useReactTable,
} from "@tanstack/react-table";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Tabs } from "@/components/ui/tabs";
import { Input } from "@/components/ui/input";
import type { Column } from "@/types/column";
import type { Article, ChangeApprovalStatus } from "@/features/articles/types/article";
import { ArticleApprovalStatus } from "@/constants/urlTypes";

export function ArticleDataTable(props: {
  data: Article[];
  totalCount: number;
  columns: Column[];
  defaultRowsCount?: number;
  pagination: {
    pageIndex: number;
    pageSize: number;
  };
  setPagination: React.Dispatch<
    React.SetStateAction<{
      pageIndex: number;
      pageSize: number;
    }>
  >;
  filters: {
    search?: string;
    from?: string;
    to?: string;
    approval?: ArticleApprovalStatus | "";
    setSearch: (val: string) => void;
    setFromDate: (val: string) => void;
    setToDate: (val: string) => void;
    setApprovalStatus: (val: ArticleApprovalStatus | "") => void;
  };
  changeApprovalStatus: (body: ChangeApprovalStatus) => void;
}) {
  const [rowSelection, setRowSelection] = React.useState({});
  const [columnVisibility, setColumnVisibility] =
    React.useState<VisibilityState>({});
  const [columnFilters, setColumnFilters] = React.useState<ColumnFiltersState>(
    []
  );

  const [sorting, setSorting] = React.useState<SortingState>([]);

  const table = useReactTable({
    data: props.data,
    columns: props.columns,
    state: {
      sorting,
      columnVisibility,
      rowSelection,
      columnFilters,
      pagination: props.pagination,
    },
    getRowId: (row) => row.id.toString(),
    enableRowSelection: true,
    onRowSelectionChange: setRowSelection,
    onSortingChange: setSorting,
    onColumnFiltersChange: setColumnFilters,
    onColumnVisibilityChange: setColumnVisibility,
    onPaginationChange: props.setPagination,
    getCoreRowModel: getCoreRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    getPaginationRowModel: getPaginationRowModel(),
    getSortedRowModel: getSortedRowModel(),
    getFacetedRowModel: getFacetedRowModel(),
    getFacetedUniqueValues: getFacetedUniqueValues(),

    pageCount: Math.ceil(props.totalCount / props.pagination.pageSize),
    manualPagination: true,
  });

  return (
    <Tabs defaultValue="outline" className="w-full flex-col gap-6">
      <div className="flex flex-wrap items-center justify-between gap-4 px-4 lg:px-6 w-full">
        <div className="flex flex-wrap items-center gap-4 w-full lg:w-auto">
          <Input
            placeholder="Search..."
            value={props.filters.search || ""}
            onChange={(e) => props.filters.setSearch(e.target.value)}
            className="w-full sm:w-48"
            autoFocus={true}
          />
          <Input
            type="date"
            value={props.filters.from || ""}
            onChange={(e) => props.filters.setFromDate(e.target.value)}
            className="w-full sm:w-44"
          />
          <Input
            placeholder="To"
            type="date"
            value={props.filters.to || ""}
            onChange={(e) => props.filters.setToDate(e.target.value)}
            className="w-full sm:w-44"
          />
          <Select
            value={props.filters.approval || ""}
            onValueChange={props.filters.setApprovalStatus}
          >
            <SelectTrigger className="w-full sm:w-40">
              <SelectValue placeholder="Approval Status" />
            </SelectTrigger>
            <SelectContent>
              {Object.entries(ArticleApprovalStatus).map(([key, value]) => (
                <SelectItem key={key} value={value}>
                  {key}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </div>
      </div>
      <div className="relative flex flex-col gap-4 overflow-auto px-4 lg:px-6">
        <div className="overflow-hidden rounded-lg border">
          <Table>
            <TableHeader className="bg-muted sticky top-0 z-10">
              <TableRow>
                {props.columns.map((column, index) => (
                  <TableHead className="w-8" key={index}>
                    {column.header}
                  </TableHead>
                ))}
                <TableHead className="w-8">Current Status</TableHead>
                <TableHead className="w-8">Action</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody className="**:data-[slot=table-cell]:first:w-8">
              {table.getRowModel().rows.length > 0 ? (
                table.getRowModel().rows.map((row) => {
                  const currentStatus = row.original.approvalStatus;
                  return (
                    <TableRow key={row.id}>
                      {row.getVisibleCells().map((cell) => (
                        <TableCell key={cell.id}>
                          {cell.renderValue() as string}
                        </TableCell>
                      ))}

                      <TableCell className="flex flex-col gap-1 lg:flex-row">
                        <Button
                          size="sm"
                          variant={
                            currentStatus === "Approved" ? "default" : "outline"
                          }
                          className={`${
                            currentStatus === "Approved"
                              ? "bg-green-600 text-white"
                              : "text-green-600 border-green-600 hover:bg-green-50"
                          }`}
                          onClick={() =>
                            props.changeApprovalStatus({
                              id: row.original.id,
                              approvalStatus: "approved",
                            })
                          }
                        >
                          Approved
                        </Button>
                        <Button
                          size="sm"
                          variant={
                            currentStatus === "Pending" ? "default" : "outline"
                          }
                          className={`${
                            currentStatus === "Pending"
                              ? "bg-yellow-600 text-white"
                              : "text-yellow-600 border-yellow-600 hover:bg-yellow-50"
                          }`}
                          onClick={() =>
                            props.changeApprovalStatus({
                              id: row.original.id,
                              approvalStatus: "pending",
                            })
                          }
                        >
                          Pending
                        </Button>
                        <Button
                          size="sm"
                          variant={
                            currentStatus === "Rejected" ? "default" : "outline"
                          }
                          className={`${
                            currentStatus === "Rejected"
                              ? "bg-red-400 text-white"
                              : "text-red-400 border-red-400 hover:bg-red-50"
                          }`}
                          onClick={() =>
                            props.changeApprovalStatus({
                              id: row.original.id,
                              approvalStatus: "rejected",
                            })
                          }
                        >
                          Rejected
                        </Button>
                      </TableCell>
                    </TableRow>
                  );
                })
              ) : (
                <TableRow>
                  <TableCell
                    colSpan={props.columns.length}
                    className="h-24 text-center"
                  >
                    No results.
                  </TableCell>
                </TableRow>
              )}
            </TableBody>
          </Table>
        </div>
        <div className="flex items-center justify-between px-4">
          <div className="text-muted-foreground hidden flex-1 text-sm lg:flex">
            {table.getFilteredSelectedRowModel().rows.length} of{" "}
            {table.getFilteredRowModel().rows.length} row(s) selected.
          </div>
          <div className="flex w-full items-center gap-8 lg:w-fit">
            <div className="hidden items-center gap-2 lg:flex">
              <Label htmlFor="rows-per-page" className="text-sm font-medium">
                Rows per page
              </Label>
              <Select
                value={`${table.getState().pagination.pageSize}`}
                onValueChange={(value) => {
                  table.setPageSize(Number(value));
                }}
              >
                <SelectTrigger size="sm" className="w-20" id="rows-per-page">
                  <SelectValue
                    placeholder={table.getState().pagination.pageSize}
                  />
                </SelectTrigger>
                <SelectContent side="top">
                  {[10, 20, 30, 40, 50].map((pageSize) => (
                    <SelectItem key={pageSize} value={`${pageSize}`}>
                      {pageSize}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
            </div>
            <div className="flex w-fit items-center justify-center text-sm font-medium">
              Page {table.getState().pagination.pageIndex + 1} of{" "}
              {table.getPageCount()}
            </div>
            <div className="ml-auto flex items-center gap-2 lg:ml-0">
              <Button
                variant="outline"
                className="hidden h-8 w-8 p-0 lg:flex"
                onClick={() => table.setPageIndex(0)}
                disabled={!table.getCanPreviousPage()}
              >
                <span className="sr-only">Go to first page</span>
                <IconChevronsLeft />
              </Button>
              <Button
                variant="outline"
                className="size-8"
                size="icon"
                onClick={() => table.previousPage()}
                disabled={!table.getCanPreviousPage()}
              >
                <span className="sr-only">Go to previous page</span>
                <IconChevronLeft />
              </Button>
              <Button
                variant="outline"
                className="size-8"
                size="icon"
                onClick={() => table.nextPage()}
                disabled={!table.getCanNextPage()}
              >
                <span className="sr-only">Go to next page</span>
                <IconChevronRight />
              </Button>
              <Button
                variant="outline"
                className="hidden size-8 lg:flex"
                size="icon"
                onClick={() => table.setPageIndex(table.getPageCount() - 1)}
                disabled={!table.getCanNextPage()}
              >
                <span className="sr-only">Go to last page</span>
                <IconChevronsRight />
              </Button>
            </div>
          </div>
        </div>
      </div>
    </Tabs>
  );
}
