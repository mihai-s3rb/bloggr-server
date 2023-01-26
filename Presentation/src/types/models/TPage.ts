import TFeedPost from "./TFeedPost";

interface TPage<TResult> {
  totalCount: number;
  totalPages: number;
  pageSize: number;
  pageNumber: number;
  result: TResult[];
}

export default TPage;