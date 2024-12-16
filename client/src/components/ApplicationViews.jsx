import { Route, Routes } from "react-router-dom";
import { AuthorizedRoute } from "./auth/AuthorizedRoute";
import Login from "./auth/Login";
import Register from "./auth/Register";
import UserProfileList from "./UserProfileList"
import UserProfileDetails from "./UserProfileDetails"
import ChoreDetails from "./ChoreDetails"
import ChoresList from "./ChoresList"
// import UserProfileList from "./userprofiles/UserProfileList";

import Home from "./Home"

/* eslint-disable react/prop-types */
export default function ApplicationViews({ loggedInUser, setLoggedInUser }) {
  return (
    <Routes>
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
        <Route
        path="userprofiles/:id"
        element={
          <AuthorizedRoute roles={["Admin"]} loggedInUser={loggedInUser}>
            <UserProfileDetails />
          </AuthorizedRoute> }
        />
        <Route
        path="userprofiles"
        element={
          <AuthorizedRoute roles={["Admin"]} loggedInUser={loggedInUser} >
            <UserProfileList />
          </AuthorizedRoute>
        }
        />
      </Route>
      <Route path="*" element={<p>Whoops, nothing here...</p>} />

      <Route path="chores">
        <Route
          index
          element={
            <AuthorizedRoute loggedInUser={loggedInUser}>
              <ChoresList />
            </AuthorizedRoute>
          }
        />
      <Route
        path=":id"  // No need to write "chores/:id" since this is inside Route path="chores"
      //If you used path="/:id", it would be treated as an absolute path and no longer relative to /chores, so React would expect the path /3 (which is not what you want).
        element={
          <AuthorizedRoute roles={["Admin"]} loggedInUser={loggedInUser}>
            <ChoreDetails />
          </AuthorizedRoute> 
          }
        />
      </Route>
    </Routes>
  );
}
