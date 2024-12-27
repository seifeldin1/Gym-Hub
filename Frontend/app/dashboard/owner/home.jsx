"use client"
import styles from "@styles/dashboard.module.css"
import Image from 'next/image';
import { useState } from "react";
import { BsCalendar3 } from "react-icons/bs";
import { CiClock2 } from "react-icons/ci";
import { MdAnnouncement } from "react-icons/md";
import { IoMdAddCircle } from "react-icons/io";
import { DashHeader } from '@components/NavBar';
import { NumStat } from './Statistics';
import { CashflowChart } from './Statistics';
import { axiosInstance } from '../../axios'

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
    const reports = [
        {
            name: "report1",
            date: "2024-12-25",
            description: "lorem ipsum",
            status: "Completed",
            type: "Technical",
        },
        {
            name: "report2",
            date: "2024-12-26",
            description: "dolor sit amet",
            status: "Pending",
            type: "Financial",
        },
    ];

    return (
        <div className=" text-white p-2 rounded-lg shadow-md mx-auto">
            <h1 className="text-2xl font-bold text-lime-400 text-center mb-4">
                Recent Reports
            </h1>
            <div className="overflow-x-auto">
                <table className="w-full table-auto border-collapse">
                    <thead className="bg-[#DBFF55] text-[#4A4A4A]">
                        <tr>
                            <th className="px-4 py-3 text-left">Name</th>
                            <th className="px-4 py-3 text-left">Date</th>
                            <th className="px-4 py-3 text-left">Description</th>
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
                                <td className="px-4 py-3 align-top">{report.name}</td>
                                <td className="px-4 py-3 align-top">{report.date}</td>
                                <td className="px-4 py-3 align-top">{report.description}</td>
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

    const [annoncements, setAnnoncements] = useState();

    
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
                                    <div className="bg-[#0D0D0D] text-xs rounded-lg px-2 text-center">
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
