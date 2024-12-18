import { getAssignedDueChores, completeChore2 } from "../managers/choreManager";
import { useEffect, useState } from "react";

/* eslint-disable react/prop-types */

export const AssignedChores = ({loggedInUser}) => {

const userId = parseInt(loggedInUser.id);

console.log("what is userId and loggedInUser", userId, loggedInUser);



const [chores, setChores] = useState([]);


useEffect(() => {
    getAssignedDueChores(userId)
    .then((data) => {
        console.log("assigned chores data we get back for this user", data);
        setChores(data);
    });
}, [userId]);

const handleComplete = (choreId) => {

    completeChore2(choreId, userId)
    .then((data) => {
        console.log("chore completed successfully", data);
        return getAssignedDueChores(userId);
        //JUST IMMEADIATELY CALL THE METHOD TO GET MOST UPDATED STATE AFTER WE CHANGE IT AND NO LONGER NEED TO COMPLETE
        //THENYOU CAN CHANGE THE STATE IN SETCHORES WITH THAT THIS IS A GOOD TRICK
    })
    .then((updatedData) => {
        console.log("updated assigned chores data", updatedData);
        setChores(updatedData); // Update state with re-fetched chores
    })
    .catch((error) => {
        console.error("error fetching chores", error);
    })

getAssignedDueChores(userId)
.then((data) => {
    setChores(data);
})
.catch((error) => {
    console.error("error refetching", error);
})
}

return (
<>
<h1>My Assigned Chores</h1>
      {chores.length === 0 ? (
        <p>No assigned chores to complete.</p>
      ) : (
        <table>
            <thead>
            <tr>
            <th>Chore Name</th>
              <th>Difficulty</th>
              <th>Due</th>
              <th>Action</th>
            </tr>
            </thead>
            <tbody>
                {chores.map((chore, index) => (
                    <tr key={index}>
                        <td>  {chore.choreName} </td>
                        <td> {chore.difficulty}</td>
                        <td> {chore.choreDue ? "yes" : "No"} </td>
                        <td>
                            <button
                            onClick={() => handleComplete(chore.choreId)}
                            disabled={!chore.choreDue}
                            >
                                {chore.choreDue ? "Mark Complete" : "Not Due Yet"}
                            </button>
                        </td>
                    </tr>
            ))}
            </tbody>
        </table>
      )}


</>




)}