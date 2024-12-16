import { useEffect, useState } from "react";
import { getUserProfileChoresById } from "../managers/userProfileManager";
import { useParams } from "react-router-dom";

export const UserProfileDetails = () => {

const { id } = useParams();

console.log("what is id params being passed in id", id);

    const [userProfile, setUserProfile] = useState(null);

    useEffect(() => {
      if (!id) return; // No need to proceed if id is undefined/null
  
      console.log("Fetching user profile for ID:", id);
  
      getUserProfileChoresById(id)
        .then((profileData) => {
          if (profileData) {
            console.log("Successfully fetched user profile:", profileData);
            setUserProfile(profileData);
          } else {
            console.warn("Warning: No user profile data returned for ID:", id);
          }
        })
        .catch((error) => {
          console.error("Error while fetching user profile:", error);
        });
    }, [id]);
  
    // If userProfile is null, we assume it hasn't loaded yet or the API failed
    // if (userProfile === null) {
    //   return <p>Loading User Profile Details...</p>;
    // }
  
    // If userProfile is still null after loading, inform the user no profile was found
    // if (!userProfile) {
    //   return <p>No user profile found for ID: {id}</p>;
    // }
    
    
      ///structur we get back   {
    //     FirstName: "John",
    //     LastName: "Doe",
    //     Address: "123 Main St",
    //     Email: "john.doe@example.com",
    //     Roles: ["Admin", "User"], // This is an array of roles for the user
    //     IdentityUserId: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f", // This is the FK to the IdentityUser table
    //     IdentityUser: {
    //         Id: "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
    //         UserName: "JohnD"  },
    //     AssignedChores: [
    //         { 
    //             ChoreName: "Mow the Lawn", 
    //             Difficulty: 4 
    //         },
    //         { 
    //             ChoreName: "Clean the Kitchen", 
    //             Difficulty: 2 
    //         }    ],
    //     CompletedChores: [
    //         { 
    //             ChoreName: "Take Out Trash", 
    //             CompletedOn: "2024-12-11T14:39:25.391032Z" 
    //         },
    //         { 
    //             ChoreName: "Vacuum the House", 
    //             CompletedOn: "2024-12-05T14:39:25.391032Z" 
    //         }  ]}
    
      return (
        <div>
          <h1>User Profile Details</h1>
    
          <h2>Basic Info</h2>
          <p><strong>First Name:</strong> {userProfile.firstName}</p>
<p><strong>Last Name:</strong> {userProfile.lastName}</p>
<p><strong>Address:</strong> {userProfile.address}</p>
<p><strong>Email:</strong> {userProfile.email}</p>
<p><strong>Username:</strong> {userProfile.identityUser?.userName}</p>

    
          <h2>Roles</h2>
          {userProfile.Roles && userProfile.Roles.length > 0 ? (
            <ul>
              {userProfile.Roles.map((role, index) => (
                <li key={index}>{role}</li>
              ))}
            </ul>
          ) : (
            <p>No roles assigned</p>
          )}

          <div>
    
          <h2>Assigned Chores</h2>
          {userProfile.assignedChores && userProfile.assignedChores.length > 0 ? (
            <table>
              <thead>
                <tr>
                  <th>Chore Name</th>
                  <th>Difficulty</th>
                </tr>
              </thead>
              <tbody>
                {userProfile.assignedChores.map((chore, index) => (
                  <tr key={index}>
                    <td>{chore.choreName}</td>
                    <td>{chore.difficulty}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          ) : (
            <p>No assigned chores</p>
          )}
    
          <h2>Completed Chores</h2>
          {userProfile.completedChores && userProfile.completedChores.length > 0 ? (
            <table>
              <thead>
                <tr>
                  <th>Chore Name</th>
                  <th>Completion Date</th>
                </tr>
              </thead>
              <tbody>
                {userProfile.completedChores.map((chore, index) => (
                  <tr key={index}>
                    <td>{chore.choreName}</td>
                    <td>{new Date(chore.completedOn).toLocaleDateString()}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          ) : (
            <p>No completed chores</p>
          )}
        </div>
        </div>
      );
    };




    
    //this is chore completed on "2024-12-05T14:39:25.391032Z"
//const choreCompletedDate = new Date("2024-12-05T14:39:25.391032Z");
//     console.log(choreCompletedDate); 
 // Date object representing: Thu Dec 05 2024 14:39:25 GMT+0000 (UTC)

// const formattedDate = choreCompletedDate.toLocaleDateString();
// console.log(formattedDate); 
 // On US machine: 12/5/2024 
 // On UK machine: 5/12/2024 