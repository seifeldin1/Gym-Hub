import styles from "@styles/dashboard.module.css"
import React from 'react';
import { Pie } from 'react-chartjs-2';
import { Bar } from 'react-chartjs-2';
import { Chart as ChartJS, ArcElement, Tooltip, Legend, Title, CategoryScale, LinearScale, BarElement } from 'chart.js';
import { useEffect, useState } from "react";
import axiosInstance from '../../axios';

// Register Chart.js components
ChartJS.register(ArcElement, Tooltip, Legend, Title, CategoryScale, LinearScale, BarElement);

export const CashflowChart = () => {
    const data = {
        labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        datasets: [
            {
                label: 'Income',
                data: [4000, 5000, 3000, 7000, 5000, 6000, 4000, 4500, 7000, 6000, 5000, 5500],
                backgroundColor: '#064E3B', // Dark green
                borderRadius: 5, // Rounded corners
            },
            {
                label: 'Expense',
                data: [-3000, -4000, -2000, -5000, -3000, -4000, -2500, -3000, -5000, -4500, -3500, -4000],
                backgroundColor: '#86EFAC', // Light green
                borderRadius: 5, // Rounded corners
            },
        ],
    };

    const options = {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                position: 'top',
                labels: {
                    boxWidth: 20,
                    font: {
                        size: 14,
                    },
                },
            },
        },
        scales: {
            x: {
                grid: {
                    display: false,
                },
            },
            y: {
                ticks: {
                    callback: (value) => `${value / 1000}K`,
                },
                beginAtZero: true,
            },
        },
    };

    return (
        <div style={{ height: '400px' }}>
            <Bar options={options} data={data} />
        </div>
    );
};

const NumEmployee = () => {
    const [numData, setNumData] = useState([]);
    const [chartData, setChartData] = useState({
        labels: ['Clients', 'Coaches', 'Managers'],
        datasets: [
            {
                label: 'Number',
                data: [0, 0, 0], // Default data values, will be updated with API response
                backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56'],
                hoverBackgroundColor: ['#FF6384', '#36A2EB', '#FFCE56'],
                cutout: '70%',
                borderWidth: 2,
                borderColor: '#fff',
            },
        ],
    });

    const FetchNumData = async () => {
        try {
            const response = await axiosInstance.get("/Statistics/Numerical", {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                },
            });

            const data = response.data;
            console.log(data)
            // Map the data and filter out the null values
            const transformedData = [
                { name: "Clients", number: data.total_Number_Of_Clients },
                { name: "Coaches", number: data.total_Number_Of_Coaches },
                { name: "Managers", number: data.total_Number_Of_Branch_Managers },
                { name: "Branches", number: data.total_Number_Of_Branches },
                { name: "Equipments", number: data.total_Number_Of_Equipments },
                { name: "Coaches Per Branch", number: data.total_Number_Of_Coaches_Per_Branch },
                { name: "Equipments Per Branch", number: data.total_Number_Of_Equipments_Per_Branch },
            ].filter(item => item.number !== null); // Filter out items where number is null

            console.log(transformedData);
            setNumData(transformedData);

            // Update pie chart data with the number of clients, coaches, and managers
            const clients = data.total_Number_Of_Clients || 0;
            const coaches = data.total_Number_Of_Coaches || 0;
            const managers = data.total_Number_Of_Branch_Managers || 0;

            setChartData(prevData => ({
                ...prevData,
                datasets: [
                    {
                        ...prevData.datasets[0],
                        data: [clients, coaches, managers], // Update the chart data
                    },
                ],
            }));

            console.log(chartData);
        } catch (error) {
            if (error.response) {
                console.log(error.response.data);
                console.log(error.response.status);
                console.log(error.response.headers);
            } else {
                console.log(`Error: ${error.message}`);
            }
        }
    };

    useEffect(() => {
        FetchNumData(); // Fetch the data when the component mounts
    }, []);

    const options = {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                position: 'top',
            },
            tooltip: {
                enabled: true,
            },
            title: {
                display: true,
                text: 'Total People Distribution',
                font: {
                    size: 15,
                },
                color: 'white',
            },
        },
    };

    return <Pie options={options} data={chartData} />;
};


const TotalMoney = () => {
    const [finData, setfinData] = useState([]);
    
    const FetctFinData = async () => {
        try {
            const response = await axiosInstance.get("/Statistics/Financial", {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                },
            });

            const data = response.data;
            console.log(response);

            // Map only the necessary data for Income and Outcome
            const transformedData = [
                { name: "Income", number: data.total_Income},
                { name: "Outcome", number: data.total_Outcome}
            ];

            // Filter out null values
            const filteredData = transformedData.filter(item => item.number !== null);
            console.log(filteredData);
            setfinData(filteredData);
        } catch (error) {
            if (error.response) {
                console.log(error.response.data);
                console.log(error.response.status);
                console.log(error.response.headers);
            } else {
                console.log(`Error: ${error.message}`);
            }
        }
    };

    useEffect(() => {
        FetctFinData();
    }, []);

    // Get income and outcome values from fetched data
    const income = finData.find(item => item.name === "Income")?.number || 0;
    const outcome = finData.find(item => item.name === "Outcome")?.number || 0;

    const data = {
        labels: ['Income', 'Outcome'],
        datasets: [
            {
                label: 'Amount',
                data: [income, outcome], // Dynamic data for Income and Outcome
                backgroundColor: ['#FF6384', '#36A2EB'], // Colors for the segments
                hoverBackgroundColor: ['#FF6384', '#36A2EB'],
                cutout: '70%', // Creates the white center (70% of the chart's radius is cut out)
                borderWidth: 2, // Optional: Add a border for each segment
                borderColor: '#fff', // Optional: White border for a cleaner look
            },
        ],
    };

    const options = {
        responsive: true, // Ensures the chart adjusts to the container
        maintainAspectRatio: false, // Allows flexibility with the aspect ratio
        plugins: {
            legend: {
                position: 'top', // Position the legend
            },
            tooltip: {
                enabled: true, // Enables tooltips
            },
            title: {
                display: true, // Enables the title
                text: 'Total Money Distribution', // Text of the title
                font: {
                    size: 15, // Font size of the title
                },
                color: 'white', // Color of the title text
            },
        },
    };

    return <Pie options={options} data={data} />;
};

export const NumStat = () => {
    return (
        <div className={styles.QuickStat}>
            <h2 className="mx-auto w-[90%] pt-3 text-xl">Quick Stats</h2>
            <div className="w-full mx-auto h-[170px]">
                <NumEmployee />
            </div>
            <div className="w-full mx-auto h-[170px]">
                <TotalMoney />
            </div>
        </div>
    );
};