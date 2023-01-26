import styled from "@emotion/styled";

const ProfileImage = styled.div<{backgroundImage: string;}>`
  background: url(${props => props.backgroundImage});
  background-repeat: no-repeat;
  background-position: center center;
  background-size: contain;
  background-origin: content-box;  
  width: 100px;
  height: 100px;
  border-radius: 50%;
  margin: 2rem 0;
`;

export default ProfileImage;