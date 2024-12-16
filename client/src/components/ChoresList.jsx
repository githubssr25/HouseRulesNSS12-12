import { useEffect, useState } from "react";
import { getAllChores, deleteChore } from "../managers/choreManager"; // Functions for API calls
import { Button } from "reactstrap"; // Optional, for buttons
import { Link } from "react-router-dom";

/* eslint-disable react/prop-types */

export const ChoresList = ({ loggedInUser }) => {
  // State to store chores
  const [chores, setChores] = useState([]);

  console.log("structure of loggedinUser", loggedInUser);

  // Fetch all chores when the component loads
  useEffect(() => {
    getAllChores().then(setChores);
  }, [chores]);

  // Function to delete a chore
  const handleDelete = (choreId) => {

    const responseFromDelete = deleteChore(choreId);

    if(responseFromDelete){
      const filteredChores = chores.map(eachChore => eachChore.Id !== choreId);
      setChores(filteredChores);
    }


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
                <td>{chore.name}</td>
                <td>{chore.choreFrequencyDays}</td>
                <td>{chore.difficulty}</td>
                {loggedInUser?.roles && loggedInUser?.roles.includes("Admin") && (
                  <td>
                    <Button color="danger" onClick={() => handleDelete(chore.id)}>
                      Delete
                    </Button>
                    <Link to={`/chores/${chore.id}`}> Click Here For Chore Details </Link>
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