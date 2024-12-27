'use client'
import styles from '@styles/navbar.module.css'
import { useState } from "react";
import { RiHome5Line } from "react-icons/ri";
import { BsGraphUpArrow } from "react-icons/bs";
import { IoSettingsOutline } from "react-icons/io5";
import { LuCalendar } from "react-icons/lu";
import { TbReport } from "react-icons/tb";
import { BsFillPersonVcardFill } from "react-icons/bs";
import { AiOutlineUsergroupDelete } from "react-icons/ai";
import { FaRectangleList } from "react-icons/fa6";
import { MdLogout } from "react-icons/md";
import { FaPeopleGroup } from "react-icons/fa6";
import GymIcon from '@public/assets/images/image.png';
import Home from './home';
import Calendar from './Calendar';
import Clients from './clients'
import Report from './report';
import Coaches from './coaches';
import JobListing from './jobListing';
import PersonnalDetails from './personnal_details'

const Dashboard = () => {
    const [activePanel, setActivePanel] = useState("home");
    const [isPanelOpen, setIsPanelOpen] = useState(false);

    const handleButtonClick = () => {
        setIsPanelOpen(!isPanelOpen);
    };

    const renderPanel = () => {
        switch (activePanel) {
            case "home":
                return <Home/>;
            case "calendar":
                return <Calendar/>;
            case "clients":
                return <Clients/>
            case "coaches":
                return <Coaches/>
            case "report":
                return <Report/>;
            case "JobListing":
                return <JobListing/>;
            case "personnal_details":
                return <PersonnalDetails/>;
            case "log_out":
                return <div> </div>;
            default:
                return <div>Select a Panel</div>;
        }
    };
    return (
        <>
            <div className='flex bg-[#131313]'>
                <div className='h-[94.69vh] flex w-20 flex-col pt-5 bg-transparent justify-center m-6'>
                    <img
                        className="ease-in w-[50%] duration-300 hover:scale-110 mx-auto mb-5"
                        src={GymIcon.src}
                        alt="Description of the image"
                    />
                    <div className={`${styles.container} gap-10`}>
                        <div
                            className={styles.icon}
                            onClick={() => setActivePanel("home")}
                            role="button"
                            tabIndex={0}
                        >
                            <RiHome5Line size={26} />
                        </div>
                        <div
                            className={styles.icon}
                            onClick={() => setActivePanel("calendar")}
                            role="button"
                            tabIndex={0}
                        >
                            <LuCalendar size={26} />
                        </div>
                        <div
                            className={styles.icon}
                            onClick={() => setActivePanel("clients")}
                            role="button"
                            tabIndex={0}
                        >
                            <FaPeopleGroup size={26} />
                        </div>
                        <div
                            className={styles.icon}
                            onClick={() => setActivePanel("coaches")}
                            role="button"
                            tabIndex={0}
                        >
                            <AiOutlineUsergroupDelete size={26} />
                        </div>
                        <div
                            className={styles.icon}
                            onClick={() => setActivePanel("report")}
                            role="button"
                            tabIndex={0}
                        >
                            <TbReport size={26} />
                        </div>
                        <div
                            className={styles.icon}
                            onClick={() => setActivePanel("JobListing")}
                            role="button"
                            tabIndex={0}
                        >
                            <FaRectangleList size={26} />
                        </div>
                        <div
                            className={styles.icon}
                            onClick={() => setActivePanel("personnal_details")}
                            role="button"
                            tabIndex={0}
                        >
                            <BsFillPersonVcardFill size={26} />
                        </div>
                        <div
                            className={styles.icon}
                            onClick={() => setActivePanel("log_out")}
                            role="button"
                            tabIndex={0}
                        >
                            <MdLogout size={26} />
                        </div>
                    </div>
                </div>
                <div className='flex-1 flex-row rounded-2xl bg-[#1E1E1E] text-white h-[94.69vh] overflow-hidden my-auto mr-3'>
                    {renderPanel()}
                </div>
            </div>
        </>
    );
};


export default Dashboard;
