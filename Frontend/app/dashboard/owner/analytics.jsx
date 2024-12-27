import { DashHeader } from '@components/NavBar';
import React from "react";
import { Line } from "react-chartjs-2";
import { useEffect, useState } from "react";
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
import axiosInstance from '../../axios';
import { FaPerson } from "react-icons/fa6";
import { MdSportsMartialArts } from "react-icons/md";
import { GrUserManager } from "react-icons/gr";
import { FaCodeBranch } from "react-icons/fa";
import { FaTools } from "react-icons/fa";
import { GiReceiveMoney } from "react-icons/gi";
import { GiPayMoney } from "react-icons/gi";
import { MdRememberMe } from "react-icons/md";
import { IoPerson } from "react-icons/io5";
import { GiTakeMyMoney } from "react-icons/gi";
import { RiMoneyDollarCircleFill } from "react-icons/ri";
import { RiMoneyEuroCircleLine } from "react-icons/ri";
<RiMoneyEuroCircleLine />






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

    const [finData, setfinData] = useState([]);

    const FetctFinData = async () => {
        try {
            const response = await axiosInstance.get("/Statistics/Financial", {
                headers: {
                    Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjcmlzdGlhbm8ucm9uYWxkbyIsInJvbGUiOiJPd25lciIsImp0aSI6ImVlODY4ZGFiLTVkZDgtNDU5MC1hOTBhLTMyODNhZTEyNGFhYyIsIm5iZiI6MTczNTMyNzU1NSwiZXhwIjoxNzM1NDEzOTU1LCJpYXQiOjE3MzUzMjc1NTV9.V4NMHF7mWvlpC5U-EnyZ-wKDCC_C40XZbOPZ-fHcC9A`,
                },
            });

            const data = response.data;
            console.log(response)

            // Mapping the response data into an array of name and number
            const transformedData = [
                { name: "Income", number: data.total_Income, icon: <GiReceiveMoney size={25} /> },
                { name: "Outcome", number: data.total_Outcome, icon: <GiPayMoney size={25} /> },
                { name: "Membership Fees", number: data.total_Membership_Fees, icon: <MdRememberMe size={25} /> },
                { name: "Salaries", number: data.total_Salaries, icon: <IoPerson size={25} /> },
                { name: "Coach Salary", number: data.total_Coach_Salary, icon: <MdSportsMartialArts size={25} /> },
                { name: "Branch Manager Salary", number: data.total_Branch_Manager_Salary, icon: <GrUserManager size={25} /> },
                { name: "Equipment Purchase Fees", number: data.total_Equipment_Purchase_Fees, icon: <FaTools size={25} /> },
                { name: "Supplements Purchase Fees", number: data.total_Supplements_Purchase_Fees, icon: <GiTakeMyMoney size={25} /> },
                { name: "Supplements Selling Price", number: data.total_Supplements_Selling_Price, icon: <RiMoneyDollarCircleFill size={25} /> },
                { name: "Supplement Fees", number: data.total_Supplement_Fees, icon: <RiMoneyEuroCircleLine size={25} /> }
            ];

            // You can also filter out null values, if necessary
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


    const formatMoney = (amount) => {
        return new Intl.NumberFormat("en-US", {
            style: "currency",
            currency: "USD",
        }).format(amount);
    };


    return (
        <>
            <h1 className='text-3xl py-4'>Financial Data</h1>
            <div className="flex w-full gap-4">
                {finData.map((item, index) => (
                    <div key={index} className="bg-white w-36 p-4 rounded-md shadow-md text-black">
                        <div className="flex justify-center items-center mb-2">
                            <div className="bg-yellow-200 rounded-full p-3">
                                {item.icon}
                            </div>
                        </div>
                        <div className="text-center">
                            <h2 className="text-xl font-semibold">{formatMoney(item.number)}$</h2>
                            <p>{item.name.charAt(0).toUpperCase() + item.name.slice(1)}</p>
                        </div>
                    </div>
                ))}
            </div>
        </>
    )
}

const NumericalNumbers = () => {
    const [numData, setNumData] = useState([]);

    const FetchNumData = async () => {
        try {
            const response = await axiosInstance.get("/Statistics/Numerical", {
                headers: {
                    Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjcmlzdGlhbm8ucm9uYWxkbyIsInJvbGUiOiJPd25lciIsImp0aSI6ImVlODY4ZGFiLTVkZDgtNDU5MC1hOTBhLTMyODNhZTEyNGFhYyIsIm5iZiI6MTczNTMyNzU1NSwiZXhwIjoxNzM1NDEzOTU1LCJpYXQiOjE3MzUzMjc1NTV9.V4NMHF7mWvlpC5U-EnyZ-wKDCC_C40XZbOPZ-fHcC9A`,
                },
            });

            const data = response.data;

            // Map the data and filter out the null values
            const transformedData = [
                { name: "Clients", number: data.total_Number_Of_Clients, icon: <FaPerson size={30} /> },
                { name: "Coaches", number: data.total_Number_Of_Coaches, icon: <MdSportsMartialArts size={30} /> },
                { name: "Managers", number: data.total_Number_Of_Branch_Managers, icon: <GrUserManager size={30} /> },
                { name: "Branches", number: data.total_Number_Of_Branches, icon: <FaCodeBranch size={30} /> },
                { name: "Equipments", number: data.total_Number_Of_Equipments, icon: <FaTools size={30} /> },
                { name: "Coaches Per Branch", number: data.total_Number_Of_Coaches_Per_Branch },
                { name: "Equipments Per Branch", number: data.total_Number_Of_Equipments_Per_Branch },
            ].filter(item => item.number !== null); // Filter out items where number is null

            console.log(transformedData);
            setNumData(transformedData);
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
        FetchNumData();
    }, []);

    // Format number to money format

    return (
        <div className="flex gap-4 flex-wrap py-6 justify-center items-center">
            {numData.map((item, index) => (
                <div key={index} className="bg-white w-32 p-4 rounded-md shadow-md text-black">
                    <div className="flex justify-center items-center mb-2">
                        <div className="bg-yellow-200 rounded-full p-3">
                            {item.icon}
                        </div>
                    </div>
                    <div className="text-center">
                        <h2 className="text-2xl font-semibold">{item.number}</h2>
                        <p className='text-lg'>{item.name.charAt(0).toUpperCase() + item.name.slice(1)}</p>
                    </div>
                </div>
            ))}
        </div>
    );
};


const NumericalTable = () => {
    const [numData, setNumData] = useState([]);

    const FetchNumData = async () => {
        try {
            const response = await axiosInstance.get("/Statistics/Numerical/AllBranch", {
                headers: {
                    Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjcmlzdGlhbm8ucm9uYWxkbyIsInJvbGUiOiJPd25lciIsImp0aSI6ImVlODY4ZGFiLTVkZDgtNDU5MC1hOTBhLTMyODNhZTEyNGFhYyIsIm5iZiI6MTczNTMyNzU1NSwiZXhwIjoxNzM1NDEzOTU1LCJpYXQiOjE3MzUzMjc1NTV9.V4NMHF7mWvlpC5U-EnyZ-wKDCC_C40XZbOPZ-fHcC9A`,  // Please use the correct token here
                },
            });

            const data = response.data;

            // Clean data to remove null values
            const cleanedData = data.map(branch => {
                return Object.fromEntries(
                    Object.entries(branch).filter(([key, value]) => value !== null)
                );
            });

            console.log(cleanedData);
            setNumData(cleanedData);

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
        FetchNumData();
    }, []);

    return (
        <div className="overflow-x-auto max-h-96">
            <table className="w-full table-auto border-collapse py-5">
                <thead className="bg-[#DBFF55] text-[#4A4A4A] text-sm">
                    <tr>
                        <th className="px-4 py-3 text-center">Name</th>
                        <th className="px-4 py-3 text-center"># Coaches</th>
                        <th className="px-4 py-3 text-center"># Equipment</th>
                    </tr>
                </thead>
                <tbody>
                    {numData.map((Branch, index) => (
                        <tr
                            key={index}
                            className={`${index % 2 === 0 ? "bg-gray-800" : "bg-gray-900"
                                } hover:bg-gray-700 transition-colors`}
                        >
                            <td className="px-4 py-3 text-center text-sm">{Branch.branch_ID}</td>
                            <td className="px-4 py-3 text-center text-sm">{Branch.total_Number_Of_Coaches_Per_Branch}</td>
                            <td className="px-4 py-3 text-center text-sm">{Branch.total_Number_Of_Equipments_Per_Branch}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
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

