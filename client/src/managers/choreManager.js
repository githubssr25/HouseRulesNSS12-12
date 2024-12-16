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