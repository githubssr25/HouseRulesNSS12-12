import { useEffect, useState } from "react";
import { getChoresById } from "../managers/choreManager";
import { useParams, useNavigate } from "react-router-dom";
import {getAllUserProfiles} from "../managers/userProfileManager";
import {assignUserToChore, unAssignUserFromChore, updateChore,}from "../managers/ChoreManager";

export const ChoreDetails = () => {
  const { id } = useParams(); // Get the id from the URL (like /chores/1)
  const [chore, setChore] = useState(null); // Store the entire chore details response
  const [allUsers, setAllUsers] = useState([]);
  const [assignedUserIds, setAssignedUserIds] = useState([]);

  const[formData, setFormData] = useState({
    name: "",
    difficulty: 0,
    choreFrequencyDays: 0,
  });

  const navigate = useNavigate();

useEffect(() => {
  getAllUserProfiles()
  .then((data) => {
    setAllUsers(data);
    console.log("all users received", data);
  })
  .catch((error) => {
    console.error("error fetching user profiles", error.message);
  });
}, [id]);

const handleCheckboxChange = (userId) => {
  const isAlreadyAssigned = assignedUserIds.includes(userId);

  if (!isAlreadyAssigned) {
    assignUserToChore(id, userId).then((response) => {
      if (response) {
        console.log(`Assigned the chore ${id} to user ${userId}`);
        setAssignedUserIds((prevUserIds) => [...prevUserIds, userId]);
      }
    });
  } else {
    unAssignUserFromChore(id, userId).then((response) => {
      if (response) {
        console.log(`Unassigned the chore ${id} from user ${userId}`);
        const updatedChoreIds = assignedUserIds.filter((eachUserId) => parseInt(eachUserId) !== parseInt(userId));
        setAssignedUserIds(updatedChoreIds);
      }
    });
  }
};



  // Fetch chore details on component mount
  useEffect(() => {
    console.log("Attempting to fetch chore with ID:", id);
  
    if (id) {
      getChoresById(id)
        .then((data) => {
          if (data) {
            setFormData({
              name: data.name,
              difficulty: data.difficulty,
              choreFrequency: data.choreFrequency
            });
            setChore(data);
            data.choreAssignment.forEach((chore) => {
              setAssignedUserIds((prevIds) => [...prevIds, chore.userProfileId ])
            });
           
//cant have this in foreach data.choreAssignment.choreAssignmentIdvThis implies that data.choreAssignment is an object with a property called choreAssignmentId, but data.choreAssignment is an array, not an object
//This is invalid syntax. The argument to forEach should be a variable representing each element in the array, not an expression like data.choreAssignment.choreAssignmentI

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

  const handleChange = (event) => {
const {name, value} = event.target;
setFormData((prevForm) => ({
  ...prevForm,
  [name] : value,
}));
 }


  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const updatedChore = await updateChore(id, formData);
      if (updatedChore) {
        console.log("Chore updated successfully:", updatedChore);
        navigate("/chores");
      }
    } catch (error) {
      console.error("Error while updating chore:", error.message);
    }
  };


  return (
    <div>
      <h1>Update Chores --this is the ChoreDetails btw</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Chore Name</label>
          <input
            type="text"
            name="name"
            value={formData.name}
            onChange={handleChange}
            required
          />
        </div>

        <div>
          <label>Difficulty</label>
          <input
            type="number"
            name="difficulty"
            value={formData.difficulty}
            onChange={handleChange}
            min="1"
            max="5"
            required
          />
        </div>

        <div>
          <label>Chore Frequency (Days)</label>
          <input
            type="number"
            name="choreFrequencyDays"
            value={formData.choreFrequencyDays}
            onChange={handleChange}
            min="1"
            max="14"
            required
          />
        </div>

        <button type="submit">Update Chore</button>
      </form>




      {/* Assignees List
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
      )} */}

<h2>All Users - Assign/Unassign</h2>
{allUsers.length > 0 ? (
  allUsers.map((user) => (
    <div key={user.id} style={{ display: "flex", alignItems: "center", gap: "10px", marginBottom: "10px" }}>
      <input
        type="checkbox"
        checked={assignedUserIds.includes(user.id)}
        onChange={() => handleCheckboxChange(user.id)}
      />
      <span>
        <strong>{user.firstName} {user.lastName}</strong> - {user.email}
      </span>
    </div>
  ))
) : (
  <p>No users found.</p>
)}

      {/* Most Recent Completion */}
      <h2>Most Recent Completion</h2>
      {chore?.choreCompletions?.length === 0 ? (
        <p>No completions available</p>
      ) : (
        <div>
          <p><strong>Completed By:</strong> {chore?.choreCompletions[0].firstName} {chore?.choreCompletions[0].lastName}</p>
          <p><strong>Date Completed:</strong> {new Date(chore?.choreCompletions[0].dateCompleted).toLocaleDateString()}</p>
        </div>
      )}
    </div>
  );
}
