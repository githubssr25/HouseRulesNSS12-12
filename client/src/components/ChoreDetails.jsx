import { useEffect, useState } from "react";
import { getChoresById } from "../managers/choreManager";
import { useParams } from "react-router-dom";

export const ChoreDetails = () => {
  const { id } = useParams(); // Get the id from the URL (like /chores/1)
  const [chore, setChore] = useState(null); // Store the entire chore details response

  // Fetch chore details on component mount
  useEffect(() => {
    getChoresById(id)
      .then((data) => {
        setChore(data);
      })
      .catch((error) => {
        console.error("Error fetching chore details:", error);
      });
  }, [id]);

  return (
    <div>
      <h1>Chore Details</h1>

      {/* Chore Information */}
      <h2>Chore Information</h2>
      <p><strong>Name:</strong> {chore.Name}</p>
      <p><strong>Difficulty:</strong> {chore.Difficulty}</p>

      {/* Assignees List */}
      <h2>Assigned Users</h2>
      {chore.ChoreAssignment.length === 0 ? (
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
            {chore.ChoreAssignment.map((assignee) => (
              <tr key={assignee.ChoreAssignmentId}>
                <td>{assignee.FirstName}</td>
                <td>{assignee.LastName}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      {/* Most Recent Completion */}
      <h2>Most Recent Completion</h2>
      {chore.ChoreCompletions.length === 0 ? (
        <p>No completions available</p>
      ) : (
        <div>
          <p><strong>Completed By:</strong> {chore.ChoreCompletions[0].FirstName} {chore.ChoreCompletions[0].LastName}</p>
          <p><strong>Date Completed:</strong> {new Date(chore.ChoreCompletions[0].DateCompleted).toLocaleDateString()}</p>
        </div>
      )}
    </div>
  );
};
