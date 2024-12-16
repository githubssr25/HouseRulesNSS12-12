import { useEffect, useState } from "react";
import { getChoresById } from "../managers/choreManager";
import { useParams } from "react-router-dom";

export const ChoreDetails = () => {
  const { id } = useParams(); // Get the id from the URL (like /chores/1)
  const [chore, setChore] = useState(null); // Store the entire chore details response

  // Fetch chore details on component mount
  useEffect(() => {
    console.log("Attempting to fetch chore with ID:", id);
  
    if (id) {
      getChoresById(id)
        .then((data) => {
          if (data) {
            setChore(data);
            console.log("Chore details received:", data);
          } else {
            console.warn("No chore data returned for ID:", id);
          }
        })
        .catch((error) => {
          console.error("Error fetching chore details:", error.message);
        });
    }
  }, [id]);

   // Guard Clause: Return early if chore is null
   if (!chore) {
    return <p>Loading chore details...</p>;
  }

  return (
    <div>
      <h1>Chore Details</h1>

      {/* Chore Information */}
      <h2>Chore Information</h2>
      <p><strong>Name:</strong> {chore?.name}</p>
      <p><strong>Difficulty:</strong> {chore?.difficulty}</p>

      {/* Assignees List */}
      <h2>Assigned Users</h2>
      {chore?.choreAssignment?.length === 0 ? (
        <p>No users assigned to this chore</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>First Name</th>
              <th>Last Name</th>
            </tr>
          </thead>
          <tbody>
            {chore?.choreAssignment?.map((assignee, index) => (
              <tr key={index}>
                <td>{assignee.firstName}</td>
                <td>{assignee.lastName}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      {/* Most Recent Completion */}
      <h2>Most Recent Completion</h2>
      {chore?.choreCompletions?.length === 0 ? (
        <p>No completions available</p>
      ) : (
        <div>
          <p><strong>Completed By:</strong> {chore.choreCompletions[0].firstName} {chore.choreCompletions[0].lastName}</p>
          <p><strong>Date Completed:</strong> {new Date(chore.choreCompletions[0].dateCompleted).toLocaleDateString()}</p>
        </div>
      )}
    </div>
  );
};
