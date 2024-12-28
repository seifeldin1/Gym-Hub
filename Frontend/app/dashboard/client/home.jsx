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
import {Progress} from './Progress'


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
                <div className="fixed inset-0 flex items-center justify-center bg-gray-900 bg-opacity-50 ">
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
            <div className='bg-[#131313] text-white h-full w-[90%] mx-auto rounded-xl max-h-[100%] overflow-y-auto customScroll'>
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
                        <div key={index} className="bg-[#F7F7F7] text-black py-4 px-3 rounded-lg mb-4">
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



const ClientCard = ({ name, workoutPlan, nutritionPlan, startDate, endDate }) => {
    return (
        <>
        <div className="p-3 bg-neutral-800 rounded-xl shadow-xl hover:shadow-2xl transition-transform transform hover:-translate-y-2 border border-gray-200 my-4 w-fit flex flex-col">
            <h2 className="text-2xl font-bold mb-2 text-green-500 text-center">{name}</h2>
            <p className="text-white mb-1 text-sm"><strong>Workout Plan:</strong> {workoutPlan}</p>
            <p className="text-white mb-1 text-sm"><strong>Nutrition Plan:</strong> {nutritionPlan}</p>
            <p className="text-white mb-1 text-sm"><strong>Start Date:</strong> {startDate}</p>
            <p className="text-white mb-1 text-sm"><strong>End Date:</strong> {endDate}</p>
            <button className="mt-3 px-3 py-1 bg-blue-500 text-white font-semibold rounded-lg hover:bg-blue-600">View Details</button>
        </div>
        </>
    );
};





const Home = () => {
    return (
        <>
            <DashHeader page_name="Home" />
            <div className='flex gap-3'>

                <div className="flex flex-col w-[75%]">
                    <div className='bg-[#131313] rounded-2xl px-5 py-3 mx-auto w-[100%] h-[92%] ml-5'>
                        <h1 className="text-2xl text-white">
                            Progress
                        </h1>
                        <div className="inline-flex space-x-6 max-w-full overflow-y-auto max-h-[95%] customScroll">
                            <Progress/>
                        </div>
                    </div>
                </div>

                <div className="flex flex-col w-[25%]">
                    <div className='w-[100%] h-[47%] mb-7'>
                        <Annoncements/>
                    </div>
                    <div className='w-[100%] h-[41.5%] flex flex-col'>
                        <NextMeet/>
                    </div>
                </div>

            </div>
        </>
    )
}

export default Home
