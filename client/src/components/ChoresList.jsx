import { useEffect, useState } from "react";
import { getAllChores, deleteChore, completeChore } from "../managers/choreManager"; // Functions for API calls
import { Button } from "reactstrap"; // Optional, for buttons
import { Link } from "react-router-dom";

/* eslint-disable react/prop-types */

export const ChoresList = ({ loggedInUser }) => {
  // State to store chores
  const [chores, setChores] = useState([]);

  // console.log("structure of loggedinUser", loggedInUser);

  // Fetch all chores when the component loads
  useEffect(() => {
    getAllChores().then((data) => {
      console.log("Response from getAllChores:", data); // Log the data before setting the state
      setChores(data);
    });
  }, []);

  // Function to delete a chore
  const handleDelete = (choreId) => {
    const responseFromDelete = deleteChore(choreId);

    if (responseFromDelete) {
      const filteredChores = chores.map(
        (eachChore) => eachChore.Id !== choreId
      );
      setChores(filteredChores);
    }
  };

   // Function to complete a chore
   const handleComplete = (choreId) => {
    completeChore(choreId, loggedInUser.id)
      .then(() => {
        console.log(`Chore ${choreId} marked as complete by user ${loggedInUser.id}`);
      })
      .catch((error) => {
        console.error(`Failed to complete chore ${choreId}:`, error.message);
      });
  };

  return (
    <div>
      <h1>Chores List</h1>
      {chores.length === 0 ? (
        <p>No chores available</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>Chore Name</th>
              <th>Frequency (Days)</th>
              <th>Difficulty</th>
              {loggedInUser.roles && loggedInUser.roles.includes("Admin") && (
                <th>Actions</th>
              )}
            </tr>
          </thead>
          <tbody>
            {chores.map((chore) => (
              <tr key={chore.id}>
                <td
                style={{color: chore.choresDue ? "red" : "black"}}
                >
                  {chore.name}
                </td>
                <td>{chore.choreFrequencyDays}</td>
                <td>{chore.difficulty}</td>
                {loggedInUser?.roles &&
                  loggedInUser?.roles.includes("Admin") && (
                    <td>
                       <Button 
                    color="primary" 
                    onClick={() => handleComplete(chore.id)} 
                    style={{ marginRight: "10px" }}>
                    Complete
                  </Button>
                      <Button
                        color="danger"
                        onClick={() => handleDelete(chore.id)}
                      >
                        Delete
                      </Button>
                      <Link to={`/chores/${chore.id}`}>
                        {" "}
                        Click Here For Chore Details{" "}
                      </Link>
                      <Link to="/chores/create" style={{ marginLeft: "10px" }}>
                        <Button color="success">Create Chore</Button>
                      </Link>
                    </td>
                  )}
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};
