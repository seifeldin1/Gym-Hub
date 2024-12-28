'use client'
import styles from '@styles/navbar.module.css'
import { useState, useEffect } from "react";
import { RiHome5Line } from "react-icons/ri";
import { LuCalendar } from "react-icons/lu";
import { BsFillPersonVcardFill } from "react-icons/bs";
import { RiProgress3Fill } from "react-icons/ri";
import { MdLogout } from "react-icons/md";
import GymIcon from '@public/assets/images/image.png';
import Home from './home';
import Calendar from './Calendar';
import Progress from './Progress';
import PersonnalDetails from './personnal_details'

const Dashboard = () => {
    const [activePanel, setActivePanel] = useState("home");
    const [isPanelOpen, setIsPanelOpen] = useState(false);

    const handleButtonClick = () => {
        setIsPanelOpen(!isPanelOpen);
    };

    const handleLogOut = () => {
        // Perform logout actions here, then navigate
        window.location.href = '/'; // This will navigate to the home page with a page reload
    };

    const renderPanel = () => {
        switch (activePanel) {
            case "home":
                return <Home/>;
            case "calendar":
                return <Calendar/>;
            case "Progress":
                return <Progress/>
            case "personnal_details":
                return <PersonnalDetails/>;
            case "log_out":
                handleLogOut()
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
                            onClick={() => setActivePanel("Progress")}
                            role="button"
                            tabIndex={0}
                        >
                            <RiProgress3Fill size={26} />
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
