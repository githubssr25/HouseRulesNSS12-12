const _apiUrl = "/api/auth";

export const login = (email, password) => {
  return fetch(_apiUrl + "/login", {
    method: "POST",
    credentials: "same-origin",
    headers: {
      Authorization: `Basic ${btoa(`${email}:${password}`)}`,
    },
  }).then((res) => {
    if (res.status !== 200) {
      return Promise.resolve(null);
    } else {
      return tryGetLoggedInUser();
    }
  });
};

export const logout = () => {
  return fetch(_apiUrl + "/logout");
};

export const tryGetLoggedInUser = async () => {
  try {
    const response = await fetch("/api/auth/me");
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    // If the server responds with 204 No Content, just return null
    if (response.status === 204) {
      return null;
    }

    return await response.json();
  } catch (error) {
    console.error("Error fetching logged-in user:", error);
    return null;
  }
};


export const register = (userProfile) => {
  userProfile.password = btoa(userProfile.password);
  return fetch(_apiUrl + "/register", {
    credentials: "same-origin",
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(userProfile),
  }).then(() => tryGetLoggedInUser());
};