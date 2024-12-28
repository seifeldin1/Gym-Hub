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


export const NextMeet = () => {
    return (
        <div className="bg-[#131313] text-[#F7F7F7] w-[90%] h-[100%] rounded-2xl mx-auto py-1">
            <div className="w-[90%] mt-2 mx-auto flex flex-col gap-3">
                <h2 className="text-2xl mb-4">
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
    const Annoncemnt = [
        {
            name: "Ahmed",
            author_role: "Client",
            title: "lorem ipsum",
            content: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            date_posted: "22-4-2004"
        },
        {
            name: "Kebda",
            author_role: "Manager",
            title: "Shabowl El Donia",
            content: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            date_posted: "22-4-2005"
        }
    ];

    const [isPanelOpen, setIsPanelOpen] = useState(false);

    const handleButtonClick = () => {
        setIsPanelOpen(!isPanelOpen);
    };


    return (
        <>
            {isPanelOpen && (
                <div className="fixed inset-0 flex items-center justify-center bg-gray-900 bg-opacity-50">
                    <div className="bg-white p-6 rounded-lg relative text-black">
                        <button
                            onClick={handleButtonClick}
                            className="absolute top-2 right-2 text-gray-500 hover:text-gray-700"
                        >
                            &times;
                        </button>
                        <h2 className="text-xl font-bold mb-4">Add New Annoncement</h2>
                        <form>
                            <textarea id="myTextarea" className="border-2 border-black rounded-lg px-2 py-2 resize-none h-48 w-full" rows="4" cols="50"></textarea>
                            <button className='bg-[#DBFF55] text-[#1E1E1E] border-2 border-[#DBFF55] hover:bg-transparent duration-150 hover:text-[#DBFF55] flex items-center gap-2 px-2 rounded-lg mx-auto'>
                                <IoMdAddCircle />
                                Add New Annoncement
                            </button>
                        </form>
                    </div>
                </div>
            )}
            <div className='bg-[#131313] text-white h-full w-[90%] mx-auto rounded-xl'>
                <div className='w-[93%] mx-auto'>
                    <h1 className='text-2xl py-4 flex items-center gap-2'>
                        <MdAnnouncement />
                        Annoncements
                    </h1>
                    <button onClick={handleButtonClick} className='bg-[#DBFF55] text-[#1E1E1E] border-2 border-[#DBFF55] hover:bg-transparent duration-150 hover:text-[#DBFF55] flex items-center gap-2 px-2 rounded-lg mx-auto'>
                        <IoMdAddCircle />
                        Add New Annoncement
                    </button>
                </div>
                <div className="w-[90%] mx-auto py-2">
                    {Annoncemnt.map((announcement, index) => (
                        <div key={index} className="bg-[#F7F7F7] text-black py-4 px-3 rounded-lg mb-4 ">
                            <div className="flex justify-between items-center pb-2">
                                <div>
                                    <h2 className="text-xl font-bold">{announcement.title}</h2>
                                    <h3 className="text-xs">{announcement.name}</h3>
                                </div>
                                <div className="flex text-white gap-1 flex-col">
                                    <div className="bg-[#0D0D0D] text-xs rounded-lg px-2 text-center custom-scroll">
                                        {announcement.author_role}
                                    </div>
                                    <div className="bg-[#0D0D0D] text-xs rounded-lg px-2">
                                        {announcement.date_posted}
                                    </div>
                                </div>
                            </div>
                            <div className="text-xs pt-2 border-t-2 border-[#DBFF55]">
                                {announcement.content}
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </>
    );
};

const Clients = () => {
    

    return (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4">
            {clients.map((client, index) => (
                <div
                    key={index}
                    className="p-3 bg-neutral-800 rounded-xl shadow-xl hover:shadow-2xl transition-transform transform hover:-translate-y-2 border border-gray-200 my-4 w-fit flex flex-col"
                >
                    <h2 className="text-2xl font-bold mb-2 text-green-500 text-center">
                        {client.fullName || "Unknown"}
                    </h2>
                    <p className="text-white mb-1 text-sm">
                        <strong>Age:</strong> {client.age || "N/A"}
                    </p>
                    <p className="text-white mb-1 text-sm">
                        <strong>BMR:</strong> {client.bmr || "N/A"}
                    </p>
                    <p className="text-white mb-1 text-sm">
                        <strong>Weight:</strong> {client.weight_kg || "N/A"} kg
                    </p>
                    <p className="text-white mb-1 text-sm">
                        <strong>Height:</strong> {client.height_cm || "N/A"} cm
                    </p>
                    <button className="mt-3 px-3 py-1 bg-blue-500 text-white font-semibold rounded-lg hover:bg-blue-600">
                        View Details
                    </button>
                </div>
            ))}
        </div>
    );
};




const Home = () => {
    const [clients, setClients] = useState([]);

    const FetchClients = async (id) => {
        try {
            const response = await axiosInstance.get('/Coach/ViewMyClients', {
                id:id
            }, {
                headers: {
                    Authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJoZW5rc2hoaCIsInJvbGUiOiJDb2FjaCIsImp0aSI6IjVkMmZjZDkxLTJjYjEtNGE2ZC04ZDE3LThlNGI0MTA5MjVlMiIsIm5iZiI6MTczNTMzMjcwNCwiZXhwIjoxNzM1NDE5MTA0LCJpYXQiOjE3MzUzMzI3MDR9.EZVAumq_mhec5Z63FAU5rbX_KmuLOQm7yiSjAdnz5io',
                }
            });

            // Map response data to match the component's structure
            console.log(response.data);
            const formattedClients = response.data.map((client) => ({
                fullName: client.full_Name,
                age: client.age,
                bmr: client.bmr,
                weight_kg: client.weight_kg,
                height_cm: client.height_cm,
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
        const id=3;
        FetchClients(id);
    }, []);
    return (
        <>

            <DashHeader page_name="Home" />
            <div className='flex gap-3'>


                <div className="flex flex-col w-[75%]">
                    <div className='w-[100%] h-[40%] flex gap-2 flex-row mb-4'>
                        <div className='w-[25%] h-[100%] flex flex-col gap-3'>
                            <NextMeet/>
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
                            <RecentReports/>
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
