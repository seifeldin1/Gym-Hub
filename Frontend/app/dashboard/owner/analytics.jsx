import { DashHeader } from '@components/NavBar';
import React from "react";
import { Line } from "react-chartjs-2";
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend,
} from "chart.js";

// Register Chart.js components
ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend);

const LinearGraph = () => {
    // Data for the chart
    const data = {
        labels: ["January", "February", "March", "April", "May", "June"],
        datasets: [
            {
                label: "Sales",
                data: [65, 59, 80, 81, 56, 55], // Y-axis values
                borderColor: "rgba(75,192,192,1)",
                backgroundColor: "rgba(75,192,192,0.2)", // Fill under the line
                pointBackgroundColor: "rgba(75,192,192,1)",
                pointBorderColor: "#fff",
                tension: 0.4, // Makes the line smooth
            },
        ],
    };

    // Configuration options
    const options = {
        responsive: true,
        plugins: {
            legend: {
                position: "top", // Legend position
            },
            title: {
                display: true,
                text: "Sales Over Time", // Chart title
            },
        },
        scales: {
            x: {
                title: {
                    display: true,
                    text: "Months", // X-axis label
                },
            },
            y: {
                title: {
                    display: true,
                    text: "Sales ($)", // Y-axis label
                },
                beginAtZero: true, // Start Y-axis from zero
            },
        },
    };

    return <Line data={data} options={options} />;
};

const Financial = () => {

    const financialData = [
        {
            name: "client",
            number: 15
        },
        {
            name: "sales",
            number: 20
        },
        {
            name: "sales",
            number: 20
        },
        {
            name: "sales",
            number: 20
        },
        {
            name: "sales",
            number: 20
        },
        {
            name: "sales",
            number: 20
        },
        {
            name: "sales",
            number: 20
        },
        {
            name: "sales",
            number: 20
        },
        {
            name: "sales",
            number: 20
        },
        {
            name: "sales",
            number: 20
        },
    ];

    return (
        <>
            <h1 className='text-3xl py-4'>Financial Data</h1>
            <div className="flex w-full gap-4">
                {financialData.map((item, index) => (
                    <div key={index} className="bg-white w-32 p-4 rounded-md shadow-md text-black">
                        <div className="flex justify-center items-center mb-2">
                            <div className="bg-yellow-200 rounded-full p-3">
                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth="1.5" stroke="currentColor" className="w-6 h-6">
                                    <path strokeLinecap="round" strokeLinejoin="round" d="M11.25 11.25l.041-.02a.75.75 0 011.063.852c.684.621 1.744 1.049 3.01 1.419s2.375.329 3.31.024.903-.614 1.237-1.101A2.251 2.251 0 0018 9.75v-1.5a2.25 2.25 0 00-2.25-2.25H5.25a2.25 2.25 0 00-2.25 2.25v1.5a2.25 2.25 0 002.25 2.25h11.25zm0 0l.041-.02a.75.75 0 00-1.063.852C10.566 11.87 9.426 12.3 8.1 12.681s-1.673.329-2.61-.024-.903-.614-1.237-1.101A2.251 2.251 0 013 9.75v-1.5a2.25 2.25 0 00-2.25-2.25H.75a2.25 2.25 0 00-2.25 2.25v1.5a2.25 2.25 0 002.25 2.25h11.25z" />
                                </svg>
                            </div>
                        </div>
                        <div className="text-center">
                            <h2 className="text-2xl font-semibold">{item.number}+</h2>
                            <p>{item.name.charAt(0).toUpperCase() + item.name.slice(1)}</p>
                        </div>
                    </div>
                ))}
            </div>
        </>
    )
}

const NumericalNumbers = () => {
    const NumbersData = [
        {
            name: "client",
            number: 15
        },
        {
            name: "sales",
            number: 20
        },
        {
            name: "sales",
            number: 20
        },
        {
            name: "sales",
            number: 20
        },
        {
            name: "sales",
            number: 20
        },
    ];
    return (
        <div className="flex gap-4 flex-wrap py-6 justify-center items-center">
            {NumbersData.map((item, index) => (
                <div key={index} className="bg-white w-32 p-4 rounded-md shadow-md text-black">
                    <div className="flex justify-center items-center mb-2">
                        <div className="bg-yellow-200 rounded-full p-3">
                            <svg
                                xmlns="http://www.w3.org/2000/svg"
                                fill="none"
                                viewBox="0 0 24 24"
                                strokeWidth="1.5"
                                stroke="currentColor"
                                className="w-6 h-6"
                            >
                                <path
                                    strokeLinecap="round"
                                    strokeLinejoin="round"
                                    d="M11.25 11.25l.041-.02a.75.75 0 011.063.852c.684.621 1.744 1.049 3.01 1.419s2.375.329 3.31.024.903-.614 1.237-1.101A2.251 2.251 0 0018 9.75v-1.5a2.25 2.25 0 00-2.25-2.25H5.25a2.25 2.25 0 00-2.25 2.25v1.5a2.25 2.25 0 002.25 2.25h11.25zm0 0l.041-.02a.75.75 0 00-1.063.852C10.566 11.87 9.426 12.3 8.1 12.681s-1.673.329-2.61-.024-.903-.614-1.237-1.101A2.251 2.251 0 013 9.75v-1.5a2.25 2.25 0 00-2.25-2.25H.75a2.25 2.25 0 00-2.25 2.25v1.5a2.25 2.25 0 002.25 2.25h11.25z"
                                />
                            </svg>
                        </div>
                    </div>
                    <div className="text-center">
                        <h2 className="text-2xl font-semibold">{item.number}+</h2>
                        <p>{item.name.charAt(0).toUpperCase() + item.name.slice(1)}</p>
                    </div>
                </div>
            ))}
        </div>
    );
};


const NumericalTable = () => {
    const BranchDetails = [
        {
            Name: "Branch 1",
            Coaches: 15,
            equipment: 10,
            clients: 20
        },
        {
            Name: "Branch 2",
            Coaches: 15,
            equipment: 10,
            clients: 20
        },
        {
            Name: "Branch 3",
            Coaches: 15,
            equipment: 10,
            clients: 20
        }
    ];

    return (
        <table className="w-full table-auto border-collapse py-5">
            <thead className="bg-[#DBFF55] text-[#4A4A4A] text-sm">
                <tr>
                    <th className="px-4 py-3 text-center">Name</th>
                    <th className="px-4 py-3 text-center"># Coaches</th>
                    <th className="px-4 py-3 text-center"># Equipment</th>
                    <th className="px-4 py-3 text-center"># Clients</th>
                </tr>
            </thead>
            <tbody>
                {BranchDetails.map((Branch, index) => (
                    <tr
                        key={index}
                        className={`${index % 2 === 0 ? "bg-gray-800" : "bg-gray-900"
                            } hover:bg-gray-700 transition-colors`}
                    >
                        <td className="px-4 py-3 text-center text-sm">{Branch.Name}</td>
                        <td className="px-4 py-3 text-center text-sm">{Branch.Coaches}</td>
                        <td className="px-4 py-3 text-center text-sm">{Branch.equipment}</td>
                        <td className="px-4 py-3 text-center text-sm">{Branch.clients}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};


const Analytics = () => {

    return (
        <>
            <DashHeader page_name="Analytics" />
            <div className='w-[95%] mx-auto'>
                <Financial />
                <div className='text-3xl py-4'>
                    <h1>Numerical Data</h1>
                    <div className='w-full flex gap-3'>
                        <div className='w-[50%] px-2 py-4'>
                            <NumericalTable />
                            <NumericalNumbers />
                        </div>
                        <div className='w-[50%] px-2 py-4'>
                            <LinearGraph />
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default Analytics;

