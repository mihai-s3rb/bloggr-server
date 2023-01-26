import React from "react";
import { useNavigate } from "react-router-dom";
import { useAppSelector } from "../../features/hooks";
import { enqueueSnackbar } from "notistack";

type ProtectedComponent = {
  children: React.ReactNode;
};

const ProtectedComponent = ({ children }: ProtectedComponent) => {
  const { isLoggedIn } = useAppSelector((state) => state.user);
  const navigate = useNavigate();
  const handleClick = (e: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
    e.stopPropagation();
    if (!isLoggedIn) {
      navigate("/login", { replace: true });
      enqueueSnackbar("Please log in first", { variant: "warning" });
    }
  };

  return <div onClick={(e) => handleClick(e)}>{children}</div>;
};

export default ProtectedComponent;
