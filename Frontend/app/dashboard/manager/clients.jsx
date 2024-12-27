import { DashHeader } from '@components/NavBar';

import React from 'react';
import { Line } from 'react-chartjs-2';
import { Chart as ChartJS, CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend } from 'chart.js';

// Register required components for Chart.js
ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend);


    // Sample data (Replace this with dynamic data if needed)
    const weightData = [
      { date: '2024-12-01', weight: 70 },
      { date: '2024-12-05', weight: 71 },
      { date: '2024-12-10', weight: 70.5 },
      { date: '2024-12-15', weight: 69.8 },
      { date: '2024-12-20', weight: 70.2 },
    ];
  
    // Extracting dates and weights for chart data
    const dates = weightData.map(entry => entry.date);
    const weights = weightData.map(entry => entry.weight);
  
    // Chart.js configuration
    const data = {
        labels: dates,
        datasets: [
          {
            label: 'Weight (kg)',
            data: weights,
            borderColor: 'rgba(75, 192, 192, 1)',
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderWidth: 2,
            fill: true,
            tension: 0.3,  // Smooth curve
            pointRadius: 5,
          },
        ],
      };
    
      const options = {
        responsive: true,
        scales: {
          x: {
            title: {
              display: true,
              text: 'Date',
              color: 'rgba(255, 99, 132, 1)', // Change color of x-axis title
            },
            ticks: {
              color: 'rgba(54, 162, 235, 1)', // Change color of x-axis ticks
            },
            grid: {
              color: 'rgba(255, 99, 132, 0.2)', // Change color of grid lines for x-axis
            },
          },
          y: {
            title: {
              display: true,
              text: 'Weight (kg)',
              color: 'rgba(255, 159, 64, 1)', // Change color of y-axis title
            },
            ticks: {
              color: 'rgba(54, 162, 235, 1)', // Change color of y-axis ticks
            },
            grid: {
              color: 'rgba(255, 159, 64, 0.2)', // Change color of grid lines for y-axis
            },
          },
        },
        plugins: {
          legend: {
            display: true,
            position: 'top',
          },
        },
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
                    <p className=""><strong>BMR</strong> {bmr}</p>
                    <p className=""><strong>Weight</strong> {weight} kg</p>
                    <p className=""><strong>Height</strong> {height} cm</p>
                    <p className="text-white"><strong>Workout Plan</strong> {workoutPlan}</p>
                    <p className="text-white"><strong>Nutrition Plan</strong> {nutritionPlan}</p>
                    <p className="text-white"><strong>Start Date</strong> {startDate}</p>
                    <p className="text-white"><strong>End Date</strong> {endDate}</p>
                </div>

                <div className="flex flex-row">
                    <div className="w-[50%] mr-10">
                        <div className="flex gap-4 mb-5 items-center">
                            <p className="font-bold text-2xl">Select Report</p>
                            <select
                                id="experience_years"
                                name="Experience_Years"
                                className="w-[15rem] text-center py-3 border-none rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent text-green-600 bg-transparent"
                                defaultValue=""
                                required
                            >
                                <option value="" disabled>
                                    Select Report Date
                                </option>
                                <option value="0">12 Jan 2024</option>
                                <option value="1">15 Feb 2024</option>
                                <option value="2">25 March 2024</option>
                                <option value="3">30 April 2024</option>
                                <option value="4">7 May 2024</option>
                                <option value="5">24 June 2024</option>
                                <option value="6">1 July 2024</option>
                            </select>
                        </div>
                        <div>
                            <p className="text-xl font-bold text-green-500 mb-1">Progress Summary</p>
                            <textarea disabled className="bg-transparent border border-white rounded-md p-2 w-[100%] h-32 mb-5">This is a readonly textarea.</textarea>
                            <p className="text-xl font-bold text-green-500 mb-1">Goals Acheived</p>
                            <textarea disabled className="bg-transparent border border-white rounded-md p-2 w-[100%] h-32 mb-5">This is a readonly textarea.</textarea>
                            <p className="text-xl font-bold text-green-500 mb-1">Challanges Faced</p>
                            <textarea disabled className="bg-transparent border border-white rounded-md p-2 w-[100%] h-32 mb-5">This is a readonly textarea.</textarea>
                        </div>
                    </div>


                    <div className="flex flex-col items-center w-[50%] justify-center">
                        <h2 className="text-center text-2xl font-semibold mb-4 text-green-500">Weight Tracker</h2>
                        <Line data={data} options={options}/>
                    </div>
                </div>
            </div>
        </>
    );
};


const Home = () => {
    return (
        <>
            <DashHeader page_name="Clients"/>
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