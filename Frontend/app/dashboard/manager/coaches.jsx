import { DashHeader } from '@components/NavBar';

import React from 'react';
import { Line } from 'react-chartjs-2';
import { Chart as ChartJS, CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend } from 'chart.js';
import { useState } from "react";
import { IoEyeSharp } from "react-icons/io5";
import { AiFillEyeInvisible } from "react-icons/ai";


const CoachData = () => {
  const [formData, setFormData] = useState({
    phoneNumber: "123-456-7890",
    salary: "$5,000",
    penalties: "5",
    bonuses: "$1,000",
    startDate: "2023-01-01", // Added start date
    contractLength: "2 years",
    endDate: "December 31, 2024",
    nationalNumber: "123456789",
    gender: "Male",
    age: 30,
    email: "example@email.com"
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    let updatedFormData = { ...formData, [name]: value };

    if (name === "startDate" || name === "contractLength") {
      // Calculate end date if start date or contract length changes
      const startDate = new Date(updatedFormData.startDate);
      const contractLength = parseInt(updatedFormData.contractLength.split(" ")[0], 10);

      if (!isNaN(startDate.getTime()) && !isNaN(contractLength)) {
        const endDate = new Date(startDate);
        endDate.setFullYear(startDate.getFullYear() + contractLength);
        updatedFormData.endDate = endDate.toLocaleDateString("en-US", {
          year: "numeric",
          month: "long",
          day: "numeric",
        });
      }
    }

    setFormData(updatedFormData);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    alert("Changes saved!");
  };

  return (
    <>
      <div className="flex flex-col items-center mt-5 space-y-6">
        <div className="bg-white shadow-lg rounded-lg p-8 w-full max-w-9xl">
          <form onSubmit={handleSubmit} className="space-y-8">
            {/* Editable Section */}
            <div className="flex flex-col space-y-4">
              <h3 className="text-lg font-semibold text-gray-700 border-b pb-2">
                Editable Details
              </h3>
              <div className="grid grid-cols-1 sm:grid-cols-4 gap-6">
                {/* Salary */}
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-1">
                    Move Coach
                  </label>
                  <select
                    id="experience_years"
                    name="Experience_Years"
                    className="w-full h-[2.5rem] ]text-center py-3 border-none rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent text-black bg-transparent"
                    defaultValue=""
                    required>
                    <option value="" disabled>
                      Select a branch to move coach to
                    </option>
                    <option value="0">Plan 1</option>
                    <option value="1">Plan 2</option>
                    <option value="2">Plan 3</option>
                    <option value="3">Plan 4</option>
                    <option value="4">Plan 5</option>
                  </select>
                </div>
                {/* Salary */}
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Salary
                  </label>
                  <input
                    type="text"
                    name="salary"
                    value={formData.salary}
                    onChange={handleChange}
                    className="text-black mt-1 block w-full border border-gray-300 rounded-lg shadow-sm focus:ring-green-500 focus:border-green-500 sm:text-sm px-4 py-2"
                  />
                </div>
                {/* Penalties */}
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Penalties
                  </label>
                  <input
                    type="text"
                    name="penalties"
                    value={formData.penalties}
                    onChange={handleChange}
                    className="text-black mt-1 block w-full border border-gray-300 rounded-lg shadow-sm focus:ring-green-500 focus:border-green-500 sm:text-sm px-4 py-2"
                  />
                </div>
                {/* Bonuses */}
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Bonuses
                  </label>
                  <input
                    type="text"
                    name="bonuses"
                    value={formData.bonuses}
                    onChange={handleChange}
                    className="text-black mt-1 block w-full border border-gray-300 rounded-lg shadow-sm focus:ring-green-500 focus:border-green-500 sm:text-sm px-4 py-2"
                  />
                </div>

                {/* Contract Length */}
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Contract Length (years)
                  </label>
                  <input
                    type="number"
                    name="contractLength"
                    value={parseInt(formData.contractLength.split(" ")[0], 10)}
                    onChange={handleChange}
                    className="text-black mt-1 block w-full border border-gray-300 rounded-lg shadow-sm focus:ring-green-500 focus:border-green-500 sm:text-sm px-4 py-2"
                  />
                </div>
              </div>
            </div>

            {/* Static Details */}
            <div className="flex flex-col space-y-4">
              <h3 className="text-lg font-semibold text-gray-700 border-b pb-2">
                Static Details
              </h3>
              <div className="grid grid-cols-5 gap-6">
                {[
                  { label: "Email", value: formData.email },
                  { label: "Salary", value: formData.salary },
                  { label: "Penalties", value: formData.penalties },
                  { label: "Bonuses", value: formData.bonuses },
                  { label: "Start Date", value: formData.startDate },
                  { label: "Contract Length", value: formData.contractLength },
                  { label: "End Date", value: formData.endDate },
                  { label: "National Number", value: formData.nationalNumber },
                  { label: "Gender", value: formData.gender },
                  { label: "Age", value: formData.age },
                ].map((item, idx) => (
                  <div
                    key={idx}
                    className="bg-gray-100 rounded-lg p-4 shadow-sm"
                  >
                    <p className="text-sm font-medium text-gray-500">
                      {item.label}
                    </p>
                    <p className="text-lg font-semibold text-gray-700">
                      {item.value}
                    </p>
                  </div>
                ))}
              </div>
            </div>

            <div className="flex justify-center">
              <button
                type="submit"
                className="px-6 py-3 bg-green-500 text-white font-bold rounded-lg shadow-md hover:bg-transparent hover:text-green-500 transition duration-300"
              >
                Save Changes
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

const clients = [
  {
    name: 'John Doe',
    bmr: '1500',
    weight: '80',
    height: '175',
    workoutPlan: 'Strength',
    nutritionPlan: 'High Protein',
    startDate: 'Jan 2024',
    endDate: 'Dec 2024'
  },
  {
    name: 'Jane Smith',
    bmr: '1500',
    weight: '80',
    height: '175',
    workoutPlan: 'Cardio',
    nutritionPlan: 'Balanced',
    startDate: 'Mar 2024',
    endDate: 'Mar 2025'
  },
  {
    name: 'Jane Smith',
    bmr: '1500',
    weight: '80',
    height: '175',
    workoutPlan: 'Cardio',
    nutritionPlan: 'Balanced',
    startDate: 'Mar 2024',
    endDate: 'Mar 2025'
  },
  {
    name: 'Jane Smith',
    bmr: '1500',
    weight: '80',
    height: '175',
    workoutPlan: 'Cardio',
    nutritionPlan: 'Balanced',
    startDate: 'Mar 2024',
    endDate: 'Mar 2025'
  }
];

const ClientCard = ({ name, bmr, weight, height, workoutPlan, nutritionPlan, startDate, endDate }) => {
  return (
    <>
      <div className="p-5 bg-neutral-800 rounded-xl shadow-xl hover:shadow-2xl border-2 border-neutral-800 hover:border-green-500 my-4 w-[100%] h-[46rem]">

        <div className='flex items-center gap-1 mb-5'>
          <h1 className="text-3xl font-bold text-green-500 mr-[52rem]">{name}</h1>
        </div>

        <div className="">
          <CoachData />
        </div>
      </div>
    </>
  );
};

const Home = () => {
  return (
    <>
      <DashHeader page_name="Coaches" />
      {/*register a new coach */}


      <div className='w-full gap-3 px-6 max-h-[90%] overflow-y-auto customScroll'>
        {clients.map((client, index) => (
          <ClientCard
            key={index}
            name={client.name}
            bmr={client.bmr}
            weight={client.weight}
            height={client.height}
            workoutPlan={client.workoutPlan}
            nutritionPlan={client.nutritionPlan}
            startDate={client.startDate}
            endDate={client.endDate}
          />
        ))}
      </div>

    </>
  )
}

export default Home