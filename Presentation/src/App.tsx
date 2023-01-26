import { Routes, Route } from "react-router-dom";
import { Navbar } from "./components/navbar/navbar.component";
import { Create } from "./routes/create/create.route";
import { Home } from "./routes/home/home.route";
import { Profile } from "./routes/profile/profile.route";
import { View } from "./routes/view/view.route";
import Blog from "./routes/blog/Blog";
import ProtectedRoute from "./routes/protected-route/protected-route";
import Login from "./routes/login/login";
import Register from "./routes/register/register";

import "./styles/global.scss";
import { Snackbar, Alert, Box, TextField, Button } from "@mui/material";
import React from "react";
import { useAppSelector } from "./features/hooks";
import { Edit } from "./routes/edit/edit.route";
import { About } from "./routes/about/about";
import { Bookmarks } from "./routes/bookmarks/bookmarks";
import { Chat } from "./routes/chat/chat";
import { ChatPopup } from "./components/chat-popup/chat-popup";

function App() {
  const userData = useAppSelector((state) => state.user);
  return (
    <div className="App">
      {userData?.isLoggedIn && <ChatPopup />}
      <Routes>
        <Route path="/" element={<Navbar />}>
          <Route index element={<Home />}></Route>
          <Route
            path="profile"
            element={
              <ProtectedRoute>
                <Profile />
              </ProtectedRoute>
            }
          ></Route>
          <Route path="chat" element={<Chat />}></Route>
          <Route path="feedback" element={<h1>Feedback page</h1>}></Route>
          <Route path="about" element={<About />}></Route>
          <Route path="post/:id" element={<View />}></Route>
          <Route
            path="post"
            element={
              <ProtectedRoute>
                <Create />
              </ProtectedRoute>
            }
          ></Route>
          <Route
            path="edit"
            element={
              <ProtectedRoute>
                <Edit />
              </ProtectedRoute>
            }
          ></Route>
          <Route
            path="bookmarks"
            element={
              <ProtectedRoute>
                <Bookmarks />
              </ProtectedRoute>
            }
          ></Route>
          <Route path=":username" element={<Blog />}></Route>
        </Route>
        <Route path="login" element={<Login />}></Route>
        <Route path="register" element={<Register />}></Route>
      </Routes>
    </div>
  );
}

export default App;
