import { useEffect, useState } from "react";
import { getUserProfileChoresById } from "../managers/userProfileManager";
import { useParams } from "react-router-dom";

export const UserProfileDetails = () => {

const { id } = useParams();

    const [userProfile, setUserProfiles] = useState([]);

    useEffect(() => {
        getUserProfileChoresById(id).then(setUserProfiles);
      }, [id]);
    
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
          <p><strong>First Name:</strong> {userProfile.FirstName}</p>
          <p><strong>Last Name:</strong> {userProfile.LastName}</p>
          <p><strong>Address:</strong> {userProfile.Address}</p>
          <p><strong>Email:</strong> {userProfile.Email}</p>
          <p><strong>Username:</strong> {userProfile.IdentityUser?.UserName}</p>
    
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
    
          <h2>Assigned Chores</h2>
          {userProfile.AssignedChores && userProfile.AssignedChores.length > 0 ? (
            <table>
              <thead>
                <tr>
                  <th>Chore Name</th>
                  <th>Difficulty</th>
                </tr>
              </thead>
              <tbody>
                {userProfile.AssignedChores.map((chore, index) => (
                  <tr key={index}>
                    <td>{chore.ChoreName}</td>
                    <td>{chore.Difficulty}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          ) : (
            <p>No assigned chores</p>
          )}
    
          <h2>Completed Chores</h2>
          {userProfile.CompletedChores && userProfile.CompletedChores.length > 0 ? (
            <table>
              <thead>
                <tr>
                  <th>Chore Name</th>
                  <th>Completion Date</th>
                </tr>
              </thead>
              <tbody>
                {userProfile.CompletedChores.map((chore, index) => (
                  <tr key={index}>
                    <td>{chore.ChoreName}</td>
                    <td>{new Date(chore.CompletedOn).toLocaleDateString()}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          ) : (
            <p>No completed chores</p>
          )}
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
