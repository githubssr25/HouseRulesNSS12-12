/* eslint-disable react/prop-types */


import { Navigate } from "react-router-dom";

// This component returns a Route that either display the prop element
// or navigates to the login. If roles are provided, the route will require
// all of the roles when all is true, or any of the roles when all is false
export const AuthorizedRoute = ({ children, loggedInUser, roles, all }) => {
  let authed = false;
  if (loggedInUser) {
    if (roles && roles.length) {
      authed = all
        ? roles.every((r) => loggedInUser.roles.includes(r))
        : roles.some((r) => loggedInUser.roles.includes(r));
    } else {
      authed = true;
    }
  }

  return authed ? children : <Navigate to="/login" />;
};

//if you wanted to pass the props from AuthorizedRoutes to its children this is how youd do it
// Inject loggedInUser into children components
// return authed
// ? React.cloneElement(children, { loggedInUser })
// : <Navigate to="/login" />;
// };