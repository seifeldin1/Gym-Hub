"use client"
import styles from "@styles/dashboard.module.css"
import Image from 'next/image';
import { BsCalendar3 } from "react-icons/bs";
import { CiClock2 } from "react-icons/ci";
import { MdAnnouncement } from "react-icons/md";
import { IoMdAddCircle } from "react-icons/io";
import { DashHeader } from '@components/NavBar';
import { NumStat } from './Statistics';
import { CashflowChart } from './Statistics';
import { useEffect, useState } from "react";
import axiosInstance from '../../axios';



import React from "react";

export const NextMeet = () => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [formData, setFormData] = useState({
        title: "",
        meetingID: "",
        date: "",
        time: ""
    });
    const [notification, setNotification] = useState(null);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
    
        const coachId = localStorage.getItem("id"); // Get coach ID from local storage
        if (!coachId) {
            setNotification({ type: "error", message: "Coach ID not found in local storage." });
            return;
        }
    
        // Combine date and time into a single datetime string
        const datetime = `${formData.date}T${formData.time}:00`; // ISO format
    
        try {
            // Fetch coach details using the API
            const token = localStorage.getItem("token");
            const coachResponse = await axiosInstance.get(`Coach/Solo?id=${coachId}`, {
                headers: { Authorization: `Bearer ${token}` }, // Replace with actual token
            });
    
            if (coachResponse.status !== 200 || !coachResponse.data) {
                throw new Error("Failed to fetch coach details.");
            }
    
            const coachName = coachResponse.data.fullName; // Adjust based on the actual field returned
    
            // Construct MeetingDetails object
            const meetingData = {
                Coach_ID: parseInt(coachId, 10),
                CoachName: coachName,
                Title: formData.title,
                Time: datetime, // Combined datetime
                Meeting_ID: formData.meetingID
            };
    
            // Send meeting data to the backend
            const response = await axiosInstance.post("Meetings/schedule", meetingData, {
                headers: { Authorization: `Bearer ${token}` }, // Replace with actual token
            });
    
            if (response.status === 200) {
                setNotification({ type: "success", message: "Meeting scheduled successfully!" });
                setFormData({ title: "", meetingID: "", date: "", time: "" }); // Reset form
                setIsModalOpen(false);
            }
        } catch (error) {
            setNotification({ type: "error", message: error.response?.data?.message || "Failed to schedule meeting." });
        }
    };
    

    return (
        <>
            {/* Modal (Pop-Out) */}
            {isModalOpen && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
                    <div className="bg-white p-6 rounded-lg shadow-lg w-[60%] h-fit">
                        <div className="flex flex-row justify-center items-center mb-3">
                            <h2 className="text-2xl font-bold text-black mr-10">Add A New Meeting</h2>
                        </div>
                        <form onSubmit="">
                            <div className="grid grid-cols-4 gap-4">
                                <div>
                                    <label htmlFor="title" className="block text-lg font-semibold text-gray-600">Title <span className="text-red-500">*</span></label>
                                    <input type="text" id="title" name="title" value="" onChange="" className="w-52 mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Meeting title" required />
                                </div>
                                <div>
                                    <label htmlFor="experience_years_required" className="block text-lg font-semibold text-gray-600">Meeting ID <span className="text-red-500">*</span></label>
                                    <input type="number" id="experience_years_required" name="experience_years_required" value="" onChange="" className="w-52 mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="ID of meeting" required />
                                </div>
                                <div>
                                    <label htmlFor="deadline" className="block text-lg font-semibold text-gray-600">Date Deadline</label>
                                    <input type="date" id="deadline" name="deadline" value="" onChange="" className="w-52 mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" />
                                </div>
                                <div>
                                    <label htmlFor="time_deadline" className="block text-lg font-semibold text-gray-600">Time Deadline</label>
                                    <input type="time" id="time_deadline" name="time_deadline" value="" onChange="" className="w-52 mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" />
                                </div>
                            </div>
                            <div className="flex justify-between gap-3">
                                <button className="bg-green-600 border-2 border-green-600 text-white font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-green-600 transition duration-400" type="submit">
                                    Add Meeting
                                </button>
                                <button className="bg-red-600 border-2 border-red-600 text-white font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-red-600 transition duration-400" onClick={() => setIsModalOpen(false)}>
                                    Close
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}

            <div className="bg-[#131313] text-[#F7F7F7] w-[90%] h-[100%] rounded-2xl mx-auto py-1">
                <div className="w-[90%] mt-2 mx-auto flex flex-col gap-3">
                    <div className="flex flex-row justify-between">
                        <h2 className="text-2xl mb-4">
                            Next Meeeting
                        </h2>
                        <button
                            type="submit"
                            className="p-1 w-fit h-fit text-3xl bg-green-500 text-white font-bold rounded-full shadow-md hover:bg-transparent hover:text-green-500 transition duration-300" onClick={() => setIsModalOpen(true)}>
                            <IoMdAddCircle />
                        </button>
                    </div>
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
        </>
    );
};

export const RecentReports = () => {
    const clients = [
        {
            id: '001',
            name: 'John Doe',
            progress: '70%',
            goalsAchieved: '5/7',
            challenges: 'Time management',
            nextSteps: 'Increase endurance'
        },
        {
            id: '001',
            name: 'John Doe',
            progress: '70%',
            goalsAchieved: '5/7',
            challenges: 'Time management',
            nextSteps: 'Increase endurance'
        },
    ];

    return (
        <div className=" text-white p-2 rounded-lg shadow-md mx-auto">
            <h1 className="text-2xl text-white mb-4">
                Client Reports
            </h1>
            <div className="overflow-x-auto rounded-lg">
                <table className="w-full table-auto border-collapse">
                    <thead className="bg-[#DBFF55] text-[#4A4A4A]">
                        <tr>
                            <th className="px-4 py-3 text-left">ID</th>
                            <th className="px-4 py-3 text-left">Name</th>
                            <th className="px-4 py-3 text-left">Progress</th>
                            <th className="px-4 py-3 text-left">Gaols Achieved</th>
                            <th className="px-4 py-3 text-left">Challanges Faced</th>
                            <th className="px-4 py-3 text-left">Next Steps</th>
                        </tr>
                    </thead>
                    <tbody>
                        {clients.map((client, index) => (
                            <tr key={index} className={`${index % 2 === 0 ? 'bg-gray-800' : 'bg-gray-900'} hover:bg-gray-700 transition-colors`}>
                                <td className="px-4 py-3 align-top">{client.id}</td>
                                <td className="px-4 py-3 align-top">{client.name}</td>
                                <td className="px-4 py-3 align-top">{client.progress}</td>
                                <td className="px-4 py-3 align-top">{client.goalsAchieved}</td>
                                <td className="px-4 py-3 align-top">{client.challenges}</td>
                                <td className="px-4 py-3 align-top">{client.nextSteps}</td>
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
                    Authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmYWRpLmlicmFoaW0iLCJyb2xlIjoiQ29hY2giLCJqdGkiOiJlNDM5ODhiMS0xNmU4LTRhN2QtOGFkNC1mNzAxNjUxNGRiZWIiLCJuYmYiOjE3MzUzMjExOTYsImV4cCI6MTczNTQwNzU5NiwiaWF0IjoxNzM1MzIxMTk2fQ.b3e7akvDFMwQ-P8h9C5ret-soZRG79eMLxYZGw_pMOI'  // Replace `yourToken` with the actual token
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

const ClientCard = ({ fullName, age, bmr, weight_kg, height_cm, workoutPlan, nutritionPlan, startDate, endDate }) => {
    return (
        <div className="p-3 bg-neutral-800 rounded-xl shadow-xl hover:shadow-2xl transition-transform transform hover:-translate-y-2 border border-gray-200 my-4 w-fit flex flex-col">
            <h2 className="text-2xl font-bold mb-2 text-green-500 text-center">
                {fullName || "Unknown"}
            </h2>
            <p className="text-white mb-1 text-sm">
                <strong>Age:</strong> {age || "N/A"}
            </p>
            <p className="text-white mb-1 text-sm">
                <strong>BMR:</strong> {bmr || "N/A"}
            </p>
            <p className="text-white mb-1 text-sm">
                <strong>Weight:</strong> {weight_kg || "N/A"} kg
            </p>
            <p className="text-white mb-1 text-sm">
                <strong>Height:</strong> {height_cm || "N/A"} cm
            </p>
            <p className="text-white mb-1 text-sm">
                <strong>Workout Plan:</strong> {workoutPlan || "N/A"}
            </p>
            <p className="text-white mb-1 text-sm">
                <strong>Nutrition Plan:</strong> {nutritionPlan || "N/A"}
            </p>
            <p className="text-white mb-1 text-sm">
                <strong>Start Date:</strong> {startDate || "N/A"}
            </p>
            <p className="text-white mb-1 text-sm">
                <strong>End Date:</strong> {endDate || "N/A"}
            </p>
            <button className="mt-3 px-3 py-1 bg-blue-500 text-white font-semibold rounded-lg hover:bg-blue-600">
                View Details
            </button>
        </div>
    );
};





const Home = () => {
    const [clients, setClients] = useState([]);

    const FetchClients = async (id) => {
        try {
            const token = localStorage.getItem("token");
            const response = await axiosInstance.get('/Coach/ViewMyClients', {
                params: {
                    id: 5  // Passing the ID as a query parameter
                },
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`
                }
            });

            // Map response data to match the component's structure
            console.log(response.data);
            const formattedClients = response.data.map((client) => ({
                fullName: client.fullName,
                age: client.age,
                bmr: client.bmr,
                weight_kg: client.weight_kg,
                height_cm: client.height_cm,
                workoutPlan: client.workoutPlan,
                nutritionPlan: client.nutritionPlan,
                startDate: client.startDate,
                endDate: client.endDate,
            }));
            setClients(formattedClients);
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
        const id = 3;
        FetchClients(id);
    }, []);

    return (
        <>
            <DashHeader page_name="Home" />
            <div className='flex gap-3'>
                <div className="flex flex-col w-[75%]">
                    <div className='w-[100%] h-[40%] flex gap-2 flex-row mb-4'>
                        <div className='w-[25%] h-[100%] flex flex-col gap-3'>
                            <NextMeet />
                        </div>
                        <div className='bg-[#131313] rounded-2xl px-5 py-3 mx-auto w-[75%] h-[100%]'>
                            <h1 className="text-2xl text-white">
                                Clients
                            </h1>
                            <div className="inline-flex space-x-6 max-w-full overflow-x-auto whitespace-nowrap customScroll">
                                {clients.map((client, index) => (
                                    <ClientCard
                                        key={index}
                                        fullName={client.fullName}
                                        age={client.age}
                                        bmr={client.bmr}
                                        weight_kg={client.weight_kg}
                                        height_cm={client.height_cm}
                                        workoutPlan={client.workoutPlan}
                                        nutritionPlan={client.nutritionPlan}
                                        startDate={client.startDate}
                                        endDate={client.endDate}
                                    />
                                ))}
                            </div>
                        </div>
                    </div>

                    <div className="bg-[#131313] text-white px-2 py-2 rounded-xl flex-grow ml-4">
                        <RecentReports />
                    </div>
                </div>

                <div className='w-[25%] h-[85vh]'>
                    <Annoncements />
                </div>
            </div>
        </>
    );
};


export default Home
