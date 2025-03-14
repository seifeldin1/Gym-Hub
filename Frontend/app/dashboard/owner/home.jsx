"use client"
import styles from "@styles/dashboard.module.css"
import Image from 'next/image';
import { useEffect, useState } from "react";
import { BsCalendar3 } from "react-icons/bs";
import { CiClock2 } from "react-icons/ci";
import { MdAnnouncement } from "react-icons/md";
import { IoMdAddCircle } from "react-icons/io";
import { DashHeader } from '@components/NavBar';
import { NumStat } from './Statistics';
import { CashflowChart } from './Statistics';
import axiosInstance from '../../axios';


export const NextMeet = () => {
    return (
        <div className={styles.MeetingDiv}>
            <div className="w-[90%] my-4 mx-auto flex flex-col gap-3">
                <h2 className="text-lg">
                    Next Meeeting
                </h2>
                <div className="flex gap-2 items-center text-xs">
                    <div className="flex gap-1">
                        <BsCalendar3 size={15} />
                        11 Nov
                    </div>
                    <div className="flex gap-1">
                        <CiClock2 size={15} />
                        11:00 am
                    </div>
                </div>
                <div className="mx-auto">
                    <Image
                        src="/assets/images/Dashboard/NextMeet.jpg" // Path to your image in the public folder
                        alt="Meeting Image"
                        width={350}
                        height={150}
                        className="rounded-xl"
                    />
                </div>
            </div>
        </div>
    )
}


export const RecentReports = () => {
    const [reports, setReports] = useState([]);

    const FetchReports = async () => {
        try {
            const response = await axiosInstance.get("/Reports/GetAllBranchManagerReports", {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`
                },
            });

            // Format reports and set state
            const formattedReports = response.data.map((report) => ({
                managerName: report.managerName || "N/A", // Handle missing manager name
                title: report.title,
                date_posted: report.generatedDate.split("T")[0], // Extract date
                status: report.status,
                type: report.type,
            }));

            setReports(formattedReports); // Set the formatted reports to state
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
        FetchReports(); // Call the FetchReports function to load data when the component mounts
    }, []);

    return (
        <div className="text-white p-2 rounded-lg shadow-md mx-auto">
            <h1 className="text-2xl font-bold text-lime-400 text-center mb-4">
                Recent Reports
            </h1>
            <div className="overflow-y-auto max-h-[27vh] customScroll">
                <table className="w-full table-auto border-collapse">
                    <thead className="bg-[#DBFF55] text-[#4A4A4A]">
                        <tr>
                            <th className="px-4 py-3 text-left">Manager Name</th>
                            <th className="px-4 py-3 text-left">Date</th>
                            <th className="px-4 py-3 text-left">Status</th>
                            <th className="px-4 py-3 text-left">Type</th>
                        </tr>
                    </thead>
                    <tbody>
                        {reports.map((report, index) => (
                            <tr
                                key={index}
                                className={`${index % 2 === 0 ? "bg-gray-800" : "bg-gray-900"
                                    } hover:bg-gray-700 transition-colors`}
                            >
                                <td className="px-4 py-3 align-top">{report.managerName}</td>
                                <td className="px-4 py-3 align-top">{report.date_posted}</td>
                                <td
                                    className={`px-4 py-3 align-top font-bold ${report.status === "Completed"
                                        ? "text-green-400"
                                        : "text-yellow-400"
                                        }`}
                                >
                                    {report.status}
                                </td>
                                <td className="px-4 py-3 align-top text-blue-400 font-medium">
                                    {report.type}
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </div>
    );
};



export const Annoncements = () => {

    //* Fetching Annoncements
    const [annoncements, setAnnoncements] = useState([]);
    const [annoncementId, setAnnoncementID] = useState(0);


    const FetchAnnoncements = async () => {
        try {
            const response = await axiosInstance.get("/Announcements", {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                },
            });
            setAnnoncements(response.data);
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
        FetchAnnoncements();
    }, []); // Only run once when the component mounts

    //* Annoncemnets Panel
    const [isPanelOpen, setIsPanelOpen] = useState(false);
    const [isEditPanelOpen, setIsEditPanelOpen] = useState(false);
    const [selectedAnnouncement, setSelectedAnnouncement] = useState(null);
    const handleButtonClick = () => {
        setIsPanelOpen(!isPanelOpen);
        setSelectedAnnouncement(null); // Reset selection for new announcement
    };

    const handleEditButtonClick = (announcement) => {
        setSelectedAnnouncement(announcement);
        setAnnoncementID(announcement.announcements_ID);
        setIsEditPanelOpen(true);
    };

    const handleCancelEdit = () => {
        setIsEditPanelOpen(false);
        setAnnoncementID(0);
        setSelectedAnnouncement(null);
    };

    const [title, setTitle] = useState('');
    const [content, setContent] = useState('');

    const HandleAnnoncemnetSubmisson = async (e) => {
        e.preventDefault();

        if (!title || !content) {
            console.log('Title and content are required.');
            return;
        }

        try {
            // Dummy values for Author_ID, Author_Role, Date_Posted, and Type
            const authorID = 1; // Example Author ID
            const authorRole = 'Admin'; // Example Author Role
            const datePosted = new Date().toISOString(); // Current date in ISO format
            const type = 'Coach'; // Example Type

            const response = await axiosInstance.post('/Announcements/add', {
                Author_ID: authorID,
                Author_Role: authorRole,
                Title: title,
                Content: content,
                Date_Posted: datePosted,
                Type: type
            }, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`  // Replace `yourToken` with the actual token
                }
            });


            if (response.status === 200) {
                // Assuming the response contains the newly added announcement:
                FetchAnnoncements();
                setTitle('');
                setContent('');
                setIsPanelOpen(false);
            } else {
                console.log('Failed to add the announcement. Please try again.');
            }
        } catch (error) {
            console.log('An error occurred. Please try again later.');
            console.error('Error submitting form:', error);
        }
    };


    const handleEdit = async (e) => {
        e.preventDefault();

        if (!title || !content) {
            console.log('Title and content are required.');
            return;
        }

        try {
            // Send updated data to your API
            console.log(annoncementId)
            const response = await axiosInstance.put(
                "/Announcements",
                {
                    Author_ID: 1,
                    Title: title,
                    Content: content,
                    Type: "Coach",
                    announcements_ID: annoncementId
                },
                {
                    headers: {
                        Authorization: `Bearer ${localStorage.getItem("token")}`
                    }
                }
            );

            if (response.status === 200) {
                setIsEditPanelOpen(false);
                setTitle('');
                setContent('');
                FetchAnnoncements(); // Refresh the list after edit
            } else {
                console.log('Failed to edit the announcement. Please try again.');
            }
        } catch (error) {
            console.log('An error occurred. Please try again later.');
            console.error('Error editing form:', error);
        }
    };

    const handleDelete = async (announcementId) => {
        try {
            const response = await axiosInstance.delete(`/Announcements`, {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`,
                    "Content-Type": "application/json", // Ensures JSON content
                },
                data: {
                    id: announcementId, // Include `id` in the request body
                },
            });
            if (response.status === 200) {
                FetchAnnoncements();
                console.log("Deleted");
            } else {
                console.log("Failed to delete the announcement. Please try again.");
            }
        } catch (error) {
            console.log("An error occurred. Please try again later.", error);
        }
    };

    return (
        <>
            {/* Create Panel */}
            {isPanelOpen && (
                <div className="fixed inset-0 flex items-center justify-center bg-gray-900 bg-opacity-50 transition-all duration-300 ease-in-out">
                    <div className="bg-white p-8 rounded-lg relative text-black max-w-lg w-full shadow-lg">
                        <button
                            onClick={handleButtonClick}
                            className="absolute top-2 right-2 text-gray-500 hover:text-gray-700 focus:outline-none"
                            aria-label="Close"
                        >
                            &times;
                        </button>
                        <h2 className="text-2xl font-semibold mb-6 text-center">Add New Announcement</h2>
                        <form onSubmit={HandleAnnoncemnetSubmisson} className="space-y-4">
                            <div>
                                <label htmlFor="title" className="block text-sm font-medium text-gray-700">Title</label>
                                <input
                                    type="text"
                                    id="title"
                                    name="title"
                                    className="border-2 border-gray-300 rounded-lg w-full px-4 py-2 mt-2 focus:outline-none focus:ring-2 focus:ring-[#DBFF55] focus:border-transparent"
                                    required
                                    onChange={(e) => { setTitle(e.target.value) }}
                                />
                            </div>

                            <div>
                                <label htmlFor="content" className="block text-sm font-medium text-gray-700">Content</label>
                                <textarea
                                    id="content"
                                    name="content"
                                    className="border-2 border-gray-300 rounded-lg w-full px-4 py-2 mt-2 resize-none h-40 focus:outline-none focus:ring-2 focus:ring-[#DBFF55] focus:border-transparent"
                                    required
                                    onChange={(e) => { setContent(e.target.value) }}
                                ></textarea>
                            </div>

                            <div className="flex justify-center">
                                <button
                                    type="submit"
                                    className="bg-[#DBFF55] text-[#1E1E1E] border-2 border-[#DBFF55] hover:bg-transparent duration-150 hover:border-black hover:text-black flex items-center gap-2 px-4 py-2 rounded-lg"
                                >
                                    <IoMdAddCircle className="text-lg" />
                                    Add New Announcement
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}

            {/* Edit Panel */}
            {isEditPanelOpen && selectedAnnouncement && (
                <div className="fixed inset-0 flex items-center justify-center bg-gray-900 bg-opacity-50 transition-all duration-300 ease-in-out">
                    <div className="bg-white p-8 rounded-lg relative text-black max-w-lg w-full shadow-lg">
                        <button
                            onClick={handleCancelEdit}
                            className="absolute top-2 right-2 text-gray-500 hover:text-gray-700 focus:outline-none"
                            aria-label="Close"
                        >
                            &times;
                        </button>
                        <h2 className="text-2xl font-semibold mb-6 text-center">Edit Announcement</h2>
                        <form onSubmit={handleEdit} className="space-y-4">
                            <div>
                                <label htmlFor="title" className="block text-sm font-medium text-gray-700">Title</label>
                                <input
                                    type="text"
                                    id="title"
                                    name="title"
                                    className="border-2 border-gray-300 rounded-lg w-full px-4 py-2 mt-2 focus:outline-none focus:ring-2 focus:ring-[#DBFF55] focus:border-transparent"
                                    required
                                    value={title}
                                    onChange={(e) => setTitle(e.target.value)}
                                />
                            </div>

                            <div>
                                <label htmlFor="content" className="block text-sm font-medium text-gray-700">Content</label>
                                <textarea
                                    id="content"
                                    name="content"
                                    className="border-2 border-gray-300 rounded-lg w-full px-4 py-2 mt-2 resize-none h-40 focus:outline-none focus:ring-2 focus:ring-[#DBFF55] focus:border-transparent"
                                    required
                                    value={content}
                                    onChange={(e) => setContent(e.target.value)}
                                ></textarea>
                            </div>

                            <div className="flex justify-center">
                                <button
                                    type="submit"
                                    className="bg-[#DBFF55] text-[#1E1E1E] border-2 border-[#DBFF55] hover:bg-transparent duration-150 hover:border-black hover:text-black flex items-center gap-2 px-4 py-2 rounded-lg"
                                >
                                    Update Announcement
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}

            <div className='bg-[#131313] text-white h-full w-[90%] mx-auto rounded-xl'>
                <div className='w-[93%] mx-auto'>
                    <h1 className='text-2xl py-4 flex items-center gap-2'>
                        <MdAnnouncement />
                        Announcements
                    </h1>
                    <button onClick={handleButtonClick} className='bg-[#DBFF55] text-[#1E1E1E] border-2 border-[#DBFF55] hover:bg-transparent duration-150 hover:text-[#DBFF55] flex items-center gap-2 px-2 rounded-lg mx-auto'>
                        <IoMdAddCircle />
                        Add New Announcement
                    </button>
                </div>
                <div className="w-[90%] mx-auto py-2">
                    {annoncements.map((announcement, index) => (
                        <div key={index} className="bg-[#F7F7F7] text-black py-4 px-3 rounded-lg mb-4">
                            <div className="flex justify-between items-center pb-2">
                                <div>
                                    <h2 className="text-xl font-bold">{announcement.title}</h2>
                                    <h3 className="text-xs">{announcement.name}</h3>
                                </div>
                                <div className="flex text-white gap-1 flex-col">
                                    <div className="bg-[#0D0D0D] text-xs rounded-lg px-2 text-center">
                                        {announcement.author_Role}
                                    </div>
                                    <div className="bg-[#0D0D0D] text-xs rounded-lg px-2">
                                        {new Date(announcement.date_Posted).toLocaleString('en-US', {
                                            hour: '2-digit',
                                            minute: '2-digit',
                                            hour12: true,
                                            day: '2-digit',
                                            month: '2-digit',
                                            year: 'numeric',
                                        })}
                                    </div>
                                </div>
                            </div>
                            <div className="text-xs pt-2 border-t-2 border-[#DBFF55]">
                                {announcement.content}
                            </div>

                            <button
                                onClick={() => handleEditButtonClick(announcement)}
                                className="mt-2 text-sm text-blue-500"
                            >
                                Edit
                            </button>
                            <button
                                onClick={() => handleDelete(announcement.announcements_ID)}
                                className="mt-2 text-sm text-red-500 px-2"
                            >
                                Delete
                            </button>
                        </div>
                    ))}
                </div>
            </div>
        </>
    );
};



const Home = () => {
    return (
        <>
            <DashHeader page_name="Home" />
            <div className='flex gap-3'>
                <div className='w-[25%] h-[85vh] flex flex-col gap-3'>
                    <NextMeet />
                    <NumStat />
                </div>
                <div className='w-[50%] h-[85vh] flex gap-2 flex-col'>
                    <div className='bg-[#131313] rounded-2xl px-5 py-3 mx-auto w-full'>
                        <CashflowChart />
                    </div>
                    <div className="bg-[#131313] text-white px-2 py-2 rounded-xl flex-grow">
                        <RecentReports />
                    </div>
                </div>
                <div className='w-[25%] h-[85vh]'>
                    <Annoncements />
                </div>
            </div>
        </>
    )
}

export default Home
