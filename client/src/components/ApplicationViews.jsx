import { Route, Routes } from "react-router-dom";
import { AuthorizedRoute } from "./auth/AuthorizedRoute";
import Login from "./auth/Login";
import Register from "./auth/Register";
import UserProfileList from "./UserProfileList"
import {UserProfileDetails} from "./UserProfileDetails"
import {ChoreDetails} from "./ChoreDetails"
import {ChoresList} from "./ChoresList"
import {CreateChore} from "./CreateChore"
import {AssignedChores} from "./AssignedChores"
// import UserProfileList from "./userprofiles/UserProfileList";

import Home from "./Home";

/* eslint-disable react/prop-types */
export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
      {/* HOME, LOGIN, REGISTER */}
      <Route path="/">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <Home />
            </AuthorizedRoute>
          }
        />
        <Route
          path="login"
          element={<Login setLoggedInUser={setLoggedInUser} />}
        />
        <Route
          path="register"
          element={<Register setLoggedInUser={setLoggedInUser} />}
        />
      </Route>

      {/* USER PROFILES GROUP */}
      <Route path="userprofiles">
        <Route
          index
          element={
            <AuthorizedRoute roles={["Admin"]} loggedInUser={loggedInUser}>
              <UserProfileList loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          }
        />
        <Route
          path=":id"
          element={
            <AuthorizedRoute roles={["Admin"]} loggedInUser={loggedInUser}>
              <UserProfileDetails loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          }
        />
      </Route>

      {/* CHORES GROUP */}
      <Route path="chores">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <ChoresList loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          }
        />
        <Route
          path=":id"
          element={
            <AuthorizedRoute roles={["Admin"]} loggedInUser={loggedInUser}>
              <ChoreDetails loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          }
        />
        <Route
          path="create"
          element={
            <AuthorizedRoute roles={["Admin"]} loggedInUser={loggedInUser}>
              <CreateChore loggedInUser={loggedInUser} />
            </AuthorizedRoute>
          } // this will be http://localhost:5173/chores/create path
        />
        <Route 
        path="assigned"
        element={
          <AuthorizedRoute loggedInUser={loggedInUser}>
            <AssignedChores loggedInUser={loggedInUser} />
          </AuthorizedRoute>
        } 

        />
      </Route>

      {/* 404 PAGE */}
      <Route path="*" element={<p>Whoops, nothing here...</p>} />
    </Routes>
  );
}
