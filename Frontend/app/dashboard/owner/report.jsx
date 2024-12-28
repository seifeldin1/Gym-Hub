import { DashHeader } from '@components/NavBar';
import axiosInstance from '../../axios';
import { useEffect, useState } from "react";

const Report = () => {
    const [reports, setReports] = useState([]);
    const [managers, setManagers] = useState([]);  // To store managers
    const [selectedManager, setSelectedManager] = useState(""); // Track selected manager name

    const FetchReports = async () => {
        try {
            const response = await axiosInstance.get("/Reports/GetAllBranchManagerReports", {
                headers: {
                    Authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjcmlzdGlhbm8ucm9uYWxkbyIsInJvbGUiOiJPd25lciIsImp0aSI6ImVlODY4ZGFiLTVkZDgtNDU5MC1hOTBhLTMyODNhZTEyNGFhYyIsIm5iZiI6MTczNTMyNzU1NSwiZXhwIjoxNzM1NDEzOTU1LCJpYXQiOjE3MzUzMjc1NTV9.V4NMHF7mWvlpC5U-EnyZ-wKDCC_C40XZbOPZ-fHcC9A'
                },
            });

            // Get unique managers based on managerName to populate the dropdown
            const uniqueManagers = Array.from(
                new Set(response.data
                    .filter((report) => report.managerName || report.managerName === "N/A") // Include "N/A" managers
                    .map((report) => report.managerName))
            ).map((name) => {
                return response.data.find((report) => report.managerName === name);
            });

            // Ensure "N/A" is included as an option
            if (!uniqueManagers.some((manager) => manager.managerName === "N/A")) {
                uniqueManagers.push({ managerName: "N/A", managerId: "N/A" });
            }

            // Format reports and set state
            const formattedReports = response.data.map((report) => ({
                managerName: report.managerName || "N/A", // Storing managerName
                managerId: report.managerId, // Still storing managerId for reference
                title: report.title,
                content: report.content,
                date_posted: report.generatedDate.split("T")[0], // Extract date
                status: report.status,
                type: report.type,
            }));

            setReports(formattedReports);
            setManagers(uniqueManagers);  // Set unique managers for the dropdown
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
        FetchReports();
    }, []);

    const handleSelectChange = (e) => {
        setSelectedManager(e.target.value); // Update selected manager name
    };

    // Filter reports based on selected manager name (including "N/A")
    const filteredReports = selectedManager
        ? reports.filter((report) => report.managerName === selectedManager)
        : reports;

    return (
        <>
            <DashHeader page_name="Reports" />
            <div className='w-[95%] mx-auto flex justify-between'>
                <h2 className='text-2xl'>
                    View Reports
                </h2>
                <select
                    onChange={handleSelectChange}
                    value={selectedManager}
                    className="border border-gray-300 px-4 py-2 rounded-md text-black bg-white shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                >
                    <option value="">All Managers</option>
                    {managers.map((manager) => (
                        <option key={manager.managerId} value={manager.managerName}>
                            {manager.managerName || "N/A"}
                        </option>
                    ))}
                </select>
            </div>
            <div className="w-[95%] mx-auto py-2 grid grid-cols-4 gap-4 overflow-y-auto max-h-[96vh]">
                {filteredReports.map((report, index) => (
                    <div key={index} className="bg-[#F7F7F7] text-black py-4 px-3 rounded-lg mb-4">
                        <div className="flex justify-between items-center pb-2">
                            <div>
                                <h2 className="text-xl font-bold">{report.title}</h2>
                                <h3 className="text-xs">{report.managerName}</h3>
                            </div>
                            <div className="flex text-white gap-1 flex-col text-center">
                                <div className="bg-[#0D0D0D] text-xs rounded-lg px-2">
                                    {report.status}
                                </div>
                                <div className="bg-[#0D0D0D] text-xs rounded-lg px-2">
                                    {report.date_posted}
                                </div>
                                <div className="bg-[#0D0D0D] text-xs rounded-lg px-2">
                                    {report.type}
                                </div>
                            </div>
                        </div>
                        <div className="text-xs pt-2 border-t-2 border-[#DBFF55]">
                            {report.content}
                        </div>
                    </div>
                ))}
            </div>
        </>
    );
};

export default Report;
