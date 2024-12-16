import { useState } from "react";
import { useNavigate } from "react-router-dom";
import {createChore}  from "../managers/choreManager";


/* eslint-disable react/prop-types */

export const CreateChore = () => {
    const [formData, setFormData] = useState({
      name: "",
      difficulty: 1,
      choreFrequencyDays: 1,
    });
  
    const navigate = useNavigate();
  
    const handleSubmit = async (e) => {
      e.preventDefault();
  
      try {
        const newChore = await createChore(formData);
        if (newChore) {
          console.log("New chore created successfully:", newChore);
          navigate("/chores"); // Navigate back to Chores List after success
        }
      } catch (error) {
        console.error("Error while creating chore:", error.message);
      }
    };
  
    const handleChange = (e) => {
      const { name, value } = e.target;
      setFormData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    };

    return (
        <div>
          <h1>Create a New Chore</h1>
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
                required
              />
            </div>
    
            <button type="submit" color="primary">
              Create Chore
            </button>
    
            <button 
              type="button" 
              color="secondary" 
              onClick={() => navigate("/chores")} 
              style={{ marginLeft: "10px" }}>
              Cancel
            </button>
          </form>
        </div>
      );
    };