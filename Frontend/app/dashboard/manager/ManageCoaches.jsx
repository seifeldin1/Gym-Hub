import { DashHeader } from '@components/NavBar';

import React from 'react';

const ClientCard = ({ name, bmr, weight, height, workoutPlan, nutritionPlan, startDate, endDate }) => {
  return (
    <>
      <div className="p-5 bg-neutral-800 rounded-xl shadow-xl hover:shadow-2xl border-2 border-neutral-800 hover:border-green-500 my-4 w-[100%] h-fit">

        <div className='flex items-center gap-1 mb-5'>
          <h1 className="text-3xl font-bold text-green-500 mr-[52rem]">{name}</h1>
        </div>

        <div className="">
          <label className="block text-xl font-bold text-white mb-1">
            Assign A Caoch to this client
          </label>
          <select
            id="experience_years"
            name="Experience_Years"
            className="w-full h-[2.5rem] ]text-center py-3 border-none rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent text-black bg-white"
            defaultValue=""
            required
          >
            <option value="" disabled>
              Choose a coach to assign
            </option>
            <option value="0">Coach 1</option>
            <option value="1">Coach 2</option>
            <option value="2">Coach 3</option>
            <option value="3">Coach 4</option>
            <option value="4">Coach 5</option>
          </select>
          <div className=''>
            <button
              type="submit"
              className="p-5 mt-3 mx-[40%] w-fit h-fit text-3xl bg-green-500 text-white font-bold rounded-full shadow-md hover:bg-transparent hover:text-green-500 transition duration-300" onClick={() => setIsModalOpen(true)}>
              Activate Account
            </button>
          </div>

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


const Home = () => {
  return (
    <>
      <DashHeader page_name="Assign Coach to Clients" />
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