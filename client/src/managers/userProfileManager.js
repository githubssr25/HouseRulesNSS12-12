

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
    console.log(`Attempting to fetch user profile with ID: ${id} from ${BASE_URL}/${id}/chores`);

    const response = await fetch(`${BASE_URL}/${id}/chores`, {
      method: 'GET',
      headers: {
        Authorization: `Bearer ${localStorage.getItem("authToken")}`,
        'Content-Type': 'application/json'
      }
    });

    if (!response) {
      console.error('No response received from the server.');
      return null; 
    }

    console.log(`Response status: ${response.status} - ${response.statusText}`);

    if (!response.ok) {
      console.error(`Error: Response returned status: ${response.status} - ${response.statusText}`);
      throw new Error(`Failed to fetch user profile. Status: ${response.status}`);
    }

    const responseData = await response.json();

    if (!responseData) {
      console.warn('Warning: Response data is null or undefined.');
      return null; 
    }

    console.log('Response Data:', responseData);
    return responseData;

  } catch (error) {
    console.error("Error in getUserProfileChoresById:", error.message);
    return null;
  }
};
