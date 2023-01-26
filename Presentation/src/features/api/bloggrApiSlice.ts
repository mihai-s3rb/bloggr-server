import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

import TPost from "../../types/models/TPost";
import TPage from "../../types/models/TPage";
import TComment from "../../types/models/TComment";
import { RootState } from "../../store";
import { TLike } from "../../types/models/TLike";
import TInterest from "../../types/models/TInterest";
import { TLogin } from "../../routes/login/login";
import { TRegister } from "../../routes/register/register";
import TUser from "../../types/models/TUser";
import TUserAuth from "../../types/models/TUserAuth";
import { TMessagesPage } from "../../types/models/TMessagesPage";

export type getPostsArgs = {
  username?: string;
  interests?: string[];
  page?: number;
  sorting?: string;
  input?: string;
  isBookmarked?: boolean;
};

type getPostCommentsArgs = {
  postId: number;
  page?: number;
  sorting?: string;
};

type TUserUpload = Partial<TUser> & {
  profile?: FileList;
  background?: FileList;
};

const queryBuilder = (body: getPostsArgs) => {
  let queries = "";
  if (body?.page) queries += `pageNumber=${body.page}&`;
  if (body?.interests && body.interests.length > 0)
    body.interests.forEach((interest) => {
      queries += `interests=${interest}&`;
    });
  if (body?.sorting) queries += `orderBy=${body.sorting}&`;
  if (body?.username) queries += `username=${body.username}&`;
  if (body?.input) queries += `input=${body.input}&`;
  if (body?.isBookmarked && body.isBookmarked === true)
    queries += `isBookmarked=${true}&`;
  return queries;
};

export const bloggrApi = createApi({
  reducerPath: "bloggrApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "http://localhost:5080",
    prepareHeaders: (headers, { getState }) => {
      const token = (getState() as RootState).user.token;
      if (token) {
        headers.set("authorization", `Bearer ${token}`);
      }
      return headers;
    },
  }),
  tagTypes: [
    "Comments",
    "Posts",
    "Post",
    "Interests",
    "MessagesHistory",
    "User",
  ],
  endpoints: (builder) => ({
    //AUTH endpoints
    login: builder.mutation<TUserAuth, TLogin>({
      query: (body) => ({
        url: "/Users/login",
        method: "POST",
        body: body,
      }),
      transformResponse: (rawResult: TUserAuth) => {
        return rawResult;
      },
      invalidatesTags: ["Posts", "Post", "Comments"],
    }),
    register: builder.mutation<TUserAuth, TRegister>({
      query: (body) => ({
        url: "/Users/register",
        method: "POST",
        body: body,
      }),
      transformResponse: (rawResult: TUserAuth) => {
        return rawResult;
      },
      invalidatesTags: ["Posts", "Post", "Comments"],
    }),
    getPosts: builder.query<TPage<TPost>, getPostsArgs>({
      query: (body) => {
        const queries = queryBuilder(body);
        return `/Posts?${queries}`;
      },
      providesTags: ["Posts"],
      transformResponse: (rawResult: TPage<TPost>) => {
        return rawResult;
      },
    }),
    getPost: builder.query<TPost, number>({
      query: (id) => `/Posts/${id}`,
      transformResponse: (rawResult: TPost) => {
        return rawResult;
      },
      providesTags: ["Post"],
    }),
    addPost: builder.mutation<TPost, Partial<TPost>>({
      query(body) {
        return {
          url: `/Posts`,
          method: "POST",
          body,
        };
      },
      invalidatesTags: ["Posts"],
    }),
    removePost: builder.mutation<TPost, number>({
      query(id) {
        return {
          url: `/Posts/${id}`,
          method: "DELETE",
        };
      },
      invalidatesTags: ["Posts", "Post"],
    }),
    updatePost: builder.mutation<TPost, Partial<TPost>>({
      query({ id, ...body }) {
        return {
          url: `/Posts/${id}`,
          method: "PUT",
          body,
        };
      },
      invalidatesTags: ["Posts", "Post"],
    }),
    getPostComments: builder.query<TPage<TComment>, getPostCommentsArgs>({
      query: ({ postId, ...body }) => {
        let queries = "";
        if (body?.page) queries += `pageNumber=${body.page}&`;
        if (body?.sorting) queries += `orderBy=${body.sorting}&`;
        return `/Posts/${postId}/comments?${queries}`;
      },
      providesTags: ["Comments"],
      transformResponse: (rawResult: TPage<TComment>) => {
        return rawResult;
      },
    }),
    addPostComment: builder.mutation<
      TComment,
      Partial<TComment> & Pick<TPost, "id">
    >({
      query({ id, ...body }) {
        return {
          url: `/Posts/${id}/comments`,
          method: "POST",
          body,
        };
      },
      invalidatesTags: ["Posts", "Post", "Comments"],
    }),
    removePostComment: builder.mutation<
      TComment,
      { postId: number; commentId: number }
    >({
      query({ postId, commentId }) {
        return {
          url: `/Posts/${postId}/comments/${commentId}`,
          method: "DELETE",
        };
      },
      invalidatesTags: ["Posts", "Post", "Comments"],
    }),
    addPostLike: builder.mutation<TLike, number>({
      query(postId) {
        return {
          url: `/Posts/${postId}/likes`,
          method: "POST",
        };
      },
      invalidatesTags: ["Posts", "Post"],
    }),
    removePostLike: builder.mutation<TLike, number>({
      query(postId) {
        return {
          url: `/Posts/${postId}/likes`,
          method: "DELETE",
        };
      },
      invalidatesTags: ["Posts", "Post"],
    }),
    addBookmark: builder.mutation<void, number>({
      query(postId) {
        return {
          url: `/Users/bookmarks?postId=${postId}`,
          method: "POST",
        };
      },
      invalidatesTags: ["Posts", "Post"],
    }),
    removeBookmark: builder.mutation<void, number>({
      query(postId) {
        return {
          url: `/Users/bookmarks?postId=${postId}`,
          method: "DELETE",
        };
      },
      invalidatesTags: ["Posts", "Post"],
    }),
    //user related
    getUser: builder.query<TUser, string>({
      query: (username) => `/Users/username/$${username}`,
      transformResponse: (rawResult: TUser) => {
        return rawResult;
      },
      providesTags: ["User"],
    }),
    updateUser: builder.mutation<TUser, FormData>({
      query(body) {
        return {
          url: `/Users/update`,
          method: "POST",
          body,
        };
      },
      invalidatesTags: ["User"],
    }),
    getInterests: builder.query<TInterest[], void>({
      query: () => "/Interests",
      transformResponse: (rawResult: TInterest[]) => {
        return rawResult;
      },
      providesTags: ["Interests"],
    }),
    addUserInterest: builder.mutation<TInterest, Partial<TInterest>>({
      query(body) {
        return {
          url: `/Users/createdInterests`,
          method: "POST",
          body,
        };
      },
      invalidatesTags: ["Interests"],
    }),
    getMessagesHistory: builder.query<
      TMessagesPage,
      { username: string; cursor: number | null }
    >({
      query: ({ username, cursor }) =>
        `/Users/messagesHistory?username=${username}&cursor=${
          cursor == null ? "" : cursor
        }`,
      transformResponse: (rawResult: TMessagesPage) => {
        return rawResult;
      },
      providesTags: ["MessagesHistory"],
    }),
  }),
});

export const {
  useGetPostsQuery,
  useGetPostQuery,
  useAddPostMutation,
  useGetPostCommentsQuery,
  useAddPostCommentMutation,
  useAddPostLikeMutation,
  useRemovePostLikeMutation,
  useRemovePostCommentMutation,
  useRemovePostMutation,
  useUpdatePostMutation,
  useAddBookmarkMutation,
  useRemoveBookmarkMutation,
  useGetUserQuery,
  useUpdateUserMutation,
  useGetInterestsQuery,
  useAddUserInterestMutation,
  useLoginMutation,
  useRegisterMutation,
  useGetMessagesHistoryQuery,
} = bloggrApi;
