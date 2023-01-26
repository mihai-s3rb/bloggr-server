import {
  Box,
  CircularProgress,
  IconButton,
  InputLabel,
  MenuItem,
  Pagination,
  Paper,
  Select,
  SelectChangeEvent,
  Typography,
} from "@mui/material";
import { useGetPostCommentsQuery } from "../../features/api/bloggrApiSlice";
import TComment from "../../types/models/TComment";
import TPost from "../../types/models/TPost";
import ProfileImage from "../user-card/profile-image";
import AvatarChip from "../avatar-chip/avatar-chip";
import { useEffect, useState } from "react";
import { createSearchParams, useSearchParams } from "react-router-dom";
import { useAppSelector } from "../../features/hooks";
import { DeleteComment } from "../delete-comment/delete-comment";
type Comments = {
  post: TPost | undefined;
};

const Comments = (props: Comments) => {
  const { id, user } = props.post
    ? props.post
    : { id: 0, user: { username: "deleted" } };
  const [searchParams, setSearchParams] = useSearchParams();
  const defaultSorting = searchParams.get("sort");
  const defaultPage = searchParams.get("page");

  const [page, setPage] = useState<number>(
    Number(defaultPage ? defaultPage : 1)
  );
  const [sorting, setSorting] = useState<string>(
    defaultSorting ? defaultSorting : "asc"
  );
  const { data, isLoading, error } = useGetPostCommentsQuery({
    postId: id,
    page: page,
    sorting: sorting,
  });

  const handleSortingChange = (event: SelectChangeEvent) => {
    setSorting(event.target.value as string);
  };
  const handlePageChange = (
    event: React.ChangeEvent<unknown>,
    value: number
  ) => {
    setPage(value);
  };
  useEffect(() => {
    //change url queries when filtering changes
    setSearchParams(
      createSearchParams({
        sorting: sorting,
        page: String(page),
      }),
      { replace: true }
    );
  }, [sorting, page]);
  if (isLoading) return <CircularProgress />;

  if (error) return <p>Error</p>;

  return (
    <>
      {data?.result && data.result.length > 0 && (
        <Box sx={{ textAlign: "right" }}>
          <Select
            labelId="demo-simple-select-label"
            id="demo-simple-select"
            value={sorting}
            onChange={handleSortingChange}
          >
            <MenuItem value={"asc"}>Newest</MenuItem>
            <MenuItem value={"desc"}>Oldest</MenuItem>
          </Select>
        </Box>
      )}
      {data?.result.map((comment: TComment) => {
        let formatedDate = "";
        if (comment?.creationDate) {
          const date = Date.parse(comment.creationDate);
          const options = { hour: "numeric", month: "short" };
          formatedDate = new Intl.DateTimeFormat("en-GB").format(date);
        }
        return (
          <Paper sx={{ mb: 2, mt: 2 }}>
            <Box sx={{ mt: 1, mb: 1, p: 2 }}>
              <Box
                sx={{
                  display: "flex",
                  justifyContent: "space-between",
                  alignItems: "flex-start",
                  mb: 2,
                }}
              >
                <AvatarChip user={comment.user} />
                <Box>
                  <span>{formatedDate}</span>
                  <DeleteComment comment={comment} />
                </Box>
              </Box>
              <Typography>{comment.content}</Typography>
            </Box>
          </Paper>
        );
      })}
      {data?.result && data.result.length > 0 && (
        <Pagination
          page={page}
          onChange={handlePageChange}
          count={data?.totalPages}
        />
      )}
    </>
  );
};

export default Comments;
