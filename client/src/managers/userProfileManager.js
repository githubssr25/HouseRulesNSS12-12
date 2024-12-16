


const BASE_URL = "/api/userprofile";

export const getAllUserProfiles = async () => {
  try {
    const response = await fetch(BASE_URL, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("authToken")}`
      }
    });
    if (!response.ok) {
      throw new Error("Failed to fetch user profiles");
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching user profiles:", error);
    return [];
  }
};

export const getUserProfileById = async (id) => {
  try {
    const response = await fetch(`${BASE_URL}/${id}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("authToken")}`
      }
    });
    if (!response.ok) {
      throw new Error("Failed to fetch user profile");
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching user profile:", error);
    return null;
  }
};


export const getUserProfileChoresById = async (id) => {
  try {
    const response = await fetch(`${BASE_URL}/${id}/chores`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("authToken")}`
      }
    });
    if (!response.ok) {
      throw new Error("Failed to fetch user profile");
    }
    return await response.json();
  } catch (error) {
    console.error("Error fetching user profile:", error);
    return null;
  }




}
