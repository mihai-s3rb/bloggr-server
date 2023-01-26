import React from "react";
import { Navigate, Outlet, redirect } from "react-router-dom";
import { useAppSelector } from "../../features/hooks";

type ProtectedRoute = {
  children?: JSX.Element;
};

const ProtectedRoute = ({ children }: ProtectedRoute) => {
  const { isLoggedIn } = useAppSelector((state) => state.user);
  if (!isLoggedIn) return <Navigate to="/" replace />;
  return children ? children : <Outlet />;
};

export default ProtectedRoute;
