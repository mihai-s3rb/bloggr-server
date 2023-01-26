import styled from "@emotion/styled";

const PostFeedImg = styled.div<{ backgroundImage: string | undefined }>`
  background: url(${(props) => props.backgroundImage});
  background-repeat: no-repeat;
  background-position: center center;
  background-size: contain;
  background-origin: content-box;
  width: 100%;
  height: 100%;
`;

export default PostFeedImg;
