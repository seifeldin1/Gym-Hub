import styles from "@styles/dashboard.module.css"
import React from 'react';
import { Pie } from 'react-chartjs-2';
import { Bar } from 'react-chartjs-2';
import { Chart as ChartJS, ArcElement, Tooltip, Legend, Title, CategoryScale, LinearScale, BarElement } from 'chart.js';

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
    const data = {
        labels: ['Clients', 'Coaches', 'Managers'],
        datasets: [
            {
                label: 'Number',
                data: [300, 50, 100], // Data values
                backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56'], // Colors for the segments
                hoverBackgroundColor: ['#FF6384', '#36A2EB', '#FFCE56'],
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
                text: 'Total People Distribution', // Text of the title
                font: {
                    size: 15, // Font size of the title
                },
                color: '#333', // Color of the title text
            },
        },
    };
    return <Pie options={options} data={data} />;
};

const TotalMoney = () => {
    const data = {
        labels: ['Income', 'Spent'],
        datasets: [
            {
                label: 'Amount',
                data: [5000, 3000], // Data values
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
                color: '#333', // Color of the title text
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