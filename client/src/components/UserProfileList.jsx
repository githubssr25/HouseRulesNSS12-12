
import { useEffect, useState } from "react";
import { getAllUserProfiles } from "../managers/userProfileManager";


export const UserProfileList = () => {


const [userProfiles, setUserProfiles] = useState([]);

useEffect(() => {
    getAllUserProfiles().then(setUserProfiles);
  }, []);


  return (
    <div>
      <h1>User Profiles</h1>
      {userProfiles.length === 0 ? (
        <p>No user profiles found</p>
      ) : (
        <table>
          <thead>
            <tr>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Email</th>
              <th>Address</th>
            </tr>
          </thead>
          <tbody>
            {userProfiles.map(profile => (
              <tr key={profile.id}>
                <td>{profile.firstName}</td>
                <td>{profile.lastName}</td>
                <td>{profile.email}</td>
                <td>{profile.address}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}