'use client'
import styles from '@styles/navbar.module.css'
import { useState } from "react";
import { RiHome5Line } from "react-icons/ri";
import { BsGraphUpArrow } from "react-icons/bs";
import { IoSettingsOutline } from "react-icons/io5";
import { LuCalendar } from "react-icons/lu";
import { TbReport } from "react-icons/tb";
import { BsFillPersonVcardFill } from "react-icons/bs";
import { FaPeopleGroup } from "react-icons/fa6";
import GymIcon from '@public/assets/images/image.png';
import Home from './home';
import Calendar from './Calendar';
import Clients from './clients'
import Report from './report';
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
            case "report":
                return <Report/>;
            case "personnal_details":
                return <PersonnalDetails/>;
            case "settings":
                return <div>Settings Panel</div>;
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
                            onClick={() => setActivePanel("report")}
                            role="button"
                            tabIndex={0}
                        >
                            <TbReport size={26} />
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
                            onClick={() => setActivePanel("settings")}
                            role="button"
                            tabIndex={0}
                        >
                            <IoSettingsOutline size={26} />
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
