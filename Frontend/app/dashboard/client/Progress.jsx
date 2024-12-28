import { DashHeader } from "@components/NavBar";

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



const Progress = () => {


    return (
        <>
            <div className="overflow-y-auto max-h-[100%]">
                <div className="w-full">
                    <p className="text-5xl text-green-600 font-bold ml-7 mr-6">Ahmed Mohamed</p>
                    <div className='flex items-center gap-1 ml-7 mb-5'>
                        <p className="mr-5"><strong>BMR: </strong>1500</p>
                        <p className="mr-5"><strong>Weight: </strong> 80 kg</p>
                        <p className="mr-5"><strong>Height: </strong> 175 cm</p>
                        <p className="mr-5"><strong>Coach: </strong> Mo Salah</p>
                    </div>

                    <div className="ml-7">

                    </div>
                </div>

                <div className="flex flex-row ml-7">

                    <div className="w-[50%]">
                        {/*From Table of Recommendation*/}
                        <p className="font-bold text-2xl mb-3">Recommendations By Coach</p>
                        <div className="flex flex-row gap-6">

                            <div className="mr-10">
                                <p className="text-orange-500 font-bold text-lg">Plan Name</p>
                                <select
                                    id="experience_years"
                                    name="Experience_Years"
                                    className="w-[15rem] text-center py-3 border-none rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent text-black bg-gray-300"
                                    defaultValue=""
                                    required
                                >
                                    <option value="" disabled>
                                        Select a Recommended Plan
                                    </option>
                                    <option value="0">Plan 1</option>
                                    <option value="1">Plan 2</option>
                                    <option value="2">Plan 3</option>
                                    <option value="3">Plan 4</option>
                                    <option value="4">Plan 5</option>
                                </select>
                            </div>
                            <div className="mr-10">
                                <p className="text-orange-500 font-bold text-lg">Supplement Name</p>
                                <select
                                    id="experience_years"
                                    name="Experience_Years"
                                    className="w-[15rem] text-center py-3 border-none rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent text-black bg-gray-300"
                                    defaultValue=""
                                    required
                                >
                                    <option value="" disabled>
                                        Select a Recommended Plan
                                    </option>
                                    <option value="0">Plan 1</option>
                                    <option value="1">Plan 2</option>
                                    <option value="2">Plan 3</option>
                                    <option value="3">Plan 4</option>
                                    <option value="4">Plan 5</option>
                                </select>
                            </div>
                        </div>

                        {/*From Table of Diet*/}
                        <p className="font-bold text-2xl mb-3 mt-10">My Diet</p>
                        <div className="grid grid-cols-2 gap-6">
                            <div className="mr-10">
                                <p className="text-orange-500 font-bold text-lg">Nutrition Plan</p>
                                <p className="text-white text-lg">NUTRITION_PLAN_NAME</p>
                            </div>
                            <div className="mr-10">
                                <p className="text-orange-500 font-bold text-lg">Supplement Name</p>
                                <p className="text-white text-lg">SUPPLEMENT_NAME</p>
                            </div>
                            <div className="mr-10">
                                <p className="text-orange-500 font-bold text-lg">Status</p>
                                <p className="text-white text-lg">STATUS</p>
                            </div>
                            <div className="mr-10">
                                <p className="text-orange-500 font-bold text-lg">Start Date</p>
                                <input disabled type="date" id="challenges_faced" name="Candidate_Name" className="w-52 mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Last date to allow submission" />
                            </div>
                            <div className="mr-10">
                                <p className="text-orange-500 font-bold text-lg">End Date</p>
                                <input disabled type="date" id="challenges_faced" name="Candidate_Name" className="w-52 mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Last date to allow submission" />
                            </div>
                        </div>

                        {/* */}
                        <p className="font-bold text-2xl mb-3 mt-10">Rate Your Coach</p>
                        <div className="flex flex-col"> 
                            <form action="">
                                <input type="number" id="challenges_faced" max="10" name="Candidate_Name" className="w-52 mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 mr-5 text-black" placeholder="Give a Rating (1 -10)" required/>
                                <button
                                    className="bg-green-600 border-2 border-green-600 text-white font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-green-600 transition duration-400"
                                    type="submit"
                                >
                                    Rate
                                </button>
                            </form>

                        </div>
                    </div>
                    <div className="flex flex-col items-center w-[50%] justify-center">
                        <h2 className="text-center text-2xl font-semibold mb-4 text-green-500">Weight Tracker</h2>
                        <Line data={data} options={options} />
                    </div>
                </div>

            </div>

        </>
    );
};
export {Progress};


const ProgressTab = () => {
    return (
        <>
            <DashHeader page_name="Progress" />
            <Progress/>
        </>
    );
};
export default ProgressTab;
