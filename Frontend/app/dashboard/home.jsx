"use client"
import styles from "@styles/dashboard.module.css"
import { BsCalendar3 } from "react-icons/bs";
import { CiClock2 } from "react-icons/ci";
import { GoArrowRight } from "react-icons/go";
import Image from 'next/image';
import profile from '@public/assets/images/profile.jpg';

import { Pie } from 'react-chartjs-2';
import { Chart as ChartJS, Tooltip, Legend, ArcElement, plugins } from 'chart.js';


import { useEffect } from 'react';
import FullCalendar from '@fullcalendar/react';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction'; // for drag & drop functionality

const FullCalendarComponent = () => {
    useEffect(() => {
        // Any necessary side effects when the calendar is mounted can go here
    }, []);

    return (
        <div className="w-full h-full overflow-hidden"> {/* Ensure parent div has overflow hidden */}
            <FullCalendar
                plugins={[dayGridPlugin, interactionPlugin]}
                initialView="dayGridMonth"
                contentHeight="auto"
                events={[
                    { title: 'Event 1', date: '2024-12-01' },
                    { title: 'Event 2', date: '2024-12-02' },
                ]}
                // You can also use maxHeight or fixed height if needed
                height="100%"  // Ensures the calendar stays inside the parent container
            />
        </div>
    );
};

ChartJS.register(Tooltip, Legend, ArcElement);

const pieChartData = {
    labels: ["Clients", "Coaches", "Branch Managers"],
    datasets: [
        {
            label: "Number of People",
            data: [30, 20, 10],
            backgroundColor: [
                "#4A90E2",
                "#50E3C2",
                "#F5A623"
            ]
        }
    ]

}

const pieChartDataMoney = {
    labels: ["Spent", "Earned"],
    datasets: [
        {
            label: "Number of People",
            data: [30, 20],
            backgroundColor: [
                "#28A745",
                "#DC3545",
            ]
        }
    ]

}

const ReportCard = () => {
    return (
        <div className="w-1/3 shadow-2xl bg-[#547c97] text-white px-3 py-4 rounded-xl flex flex-col gap-1">
            <h3 className="text-2xl font-semibold">Report Title</h3>
            <div className="flex items-center gap-2 mb-3">
                <div className='rounded-full'>
                    <img src={profile.src} className='w-5 rounded-full' />
                </div>
                <h4 className="text-xs">Sent by: The Duck</h4>
            </div>
            <p className="mb-3">
                Lorem ipsum dolor sit amet consectetur adipisicing elit.
                Possimus expedita, adipisci fugiat eos ipsum, dolor tenetur earum
                ab odio optio fugit ratione quidem hic magni! Id amet deleniti minima rerum?
            </p>
            <button className="bg-[#fc8764] rounded-md w-fit px-2 py-1 hover:bg-[#e57154] flex items-center gap-2">
                Read more
                <GoArrowRight />
            </button>
        </div>
    );
}

const Home = () => {
    const options = {
        responsive: true, // Chart scales with container size
        maintainAspectRatio: false, // Allow custom aspect ratio
    };

    return (
        <>
            <div className="bg-white w-1/2 rounded-xl">
                <div className={styles.HomeImg}>
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
                                    src="/assets/images/Dashboard/Meet.jpg" // Path to your image in the public folder
                                    alt="Meeting Image"
                                    width={350}
                                    height={150}
                                    className="rounded-xl"
                                />
                            </div>
                        </div>
                    </div>
                </div>
                <div className="w-[95%] mx-auto mt-6 overflow-hidden h-80">
                    <FullCalendarComponent />
                </div>
            </div>
            <div className="bg-white w-1/2 rounded-xl">
                <div className="w-[95%] mx-auto py-4">
                    <h2 className="text-4xl font-bold pb-3">Recent Reports</h2>
                    <div className="flex gap-4">
                        <ReportCard />
                        <ReportCard />
                        <ReportCard />
                    </div>
                    <div className="mt-8">
                        <h2 className="mb-4 text-3xl">Quick Statistics</h2>
                        <div className="flex">
                            <div className="w-1/2 h-60 font-semibold">
                                <Pie options={options} data={pieChartData} />
                            </div>
                            <div className="w-1/2 h-60">
                                <Pie options={options} data={pieChartDataMoney} />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Home;