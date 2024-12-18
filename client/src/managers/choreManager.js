export const getAllChores = async () => {
    try {
      const response = await fetch('/api/chore', {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("authToken")}`
        }
      });
      if (!response.ok) {
        throw new Error('Failed to fetch chores');
      }
      return await response.json();
    } catch (error) {
      console.error('Error fetching chores:', error);
      return [];
    }
  };

  export const getChoresById = async (id) => {
    try {
      const choreId = parseInt(id, 10); // Parse id to ensure it is an integer
      const response = await fetch(`/api/chore/${choreId}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("authToken")}`
        }
      });
      if(!response.ok){
        throw new Error ("failted to fetch chore based off Id");
      }
      return await response.json();

    } catch (error) {
      console.error("error fetching chore by Id", error);
      return null;
    }
  }
  
  export const deleteChore = async (choreId) => {
    try {
      const response = await fetch(`/api/chore/${choreId}`, {
        method: 'DELETE',
        headers: {
          Authorization: `Bearer ${localStorage.getItem("authToken")}`
        }
      });
      if (!response.ok) {
        throw new Error('Failed to delete chore');
      }
      return true;
    } catch (error) {
      console.error('Error deleting chore:', error);
      return false;
    }
  };

  export const createChore = async (choreData) => {
    console.log("what is choreData structure", choreData);
    try {
      const response = await fetch("/api/chore", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("authToken")}`,
        },
        body: JSON.stringify(choreData),
      });
  
      if (!response.ok) {
        throw new Error("Failed to create a new chore.");
      }
  
      const data = await response.json();
      console.log("Chore successfully created:", data);
      return data;
    } catch (error) {
      console.error("Error creating chore:", error.message);
      return null;
    }
  };
  
  export const completeChore = async (choreId) => {
    try {
      const response = await fetch(`/api/chore/${choreId}/complete`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("authToken")}`
        }
      });
  
      if (!response.ok) {
        throw new Error("Failed to complete the chore.");
      }
  
      console.log(`Chore ${choreId} marked as complete successfully.`);
      return true; // No body is expected for 204 responses
    } catch (error) {
      console.error("Error completing chore:", error.message);
      return false;
    }
  };

  export const assignUserToChore = async (choreId, userId) => {
    try {
      const response = await fetch(`/api/chore/${choreId}/assign?userId=${userId}`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("authToken")}`,
        }
      });
  
      if (!response.ok) {
        throw new Error(`Failed to assign user (User ID: ${userId}) to chore (Chore ID: ${choreId}).`);
      }
  
      console.log(`User (ID: ${userId}) successfully assigned to Chore (ID: ${choreId})`);
      return true; // Indicate success
    } catch (error) {
      console.error("Error assigning user to chore:", error.message);
      return false; // Indicate failure
    }
  };
  

  export const unAssignUserFromChore = async (choreId, userId) => {
    try {
      const response = await fetch(`/api/chore/${choreId}/unassign?userId=${userId}`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("authToken")}`,
        }
      });
  
      if (!response.ok) {
        throw new Error(`Failed to unassign user (User ID: ${userId}) from chore (Chore ID: ${choreId}).`);
      }
  
      console.log(`User (ID: ${userId}) successfully unassigned from Chore (ID: ${choreId})`);
      return true; // Indicate success
    } catch (error) {
      console.error("Error unassigning user from chore:", error.message);
      return false; // Indicate failure
    }
  };
  
  export const updateChore = async (choreId, choreData) => {
    try {
      const response = await fetch(`/api/chore/${choreId}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("authToken")}`
        },
        body: JSON.stringify(choreData),
      });
  
      if (!response.ok) {
        throw new Error("Failed to update the chore.");
      }
  
      const data = await response.json();
      console.log("Chore successfully updated:", data);
      return data;
    } catch (error) {
      console.error("Error updating chore:", error.message);
      return null;
    }
  };
  