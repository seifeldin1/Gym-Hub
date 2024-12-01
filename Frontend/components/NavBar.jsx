import styles from '@styles/navbar.module.css'
import { RiHome5Line } from "react-icons/ri";
import { BsGraphUpArrow } from "react-icons/bs";
import { IoIosGitBranch } from "react-icons/io";
import { IoSettingsOutline } from "react-icons/io5";
import { LuCalendar } from "react-icons/lu";
import { CiSearch } from "react-icons/ci";
import { IoNotificationsOutline } from "react-icons/io5";
import profile from '@public/assets/images/profile.jpg'


const NavBar = () => {
    return (
        <div className='flex justify-between'>
            <div className={`${styles.container} gap-6`}>
                    <div className={styles.icon}>
                        <RiHome5Line size={20}/>
                        Home
                    </div>
                    <div className={styles.icon}>
                        <LuCalendar size={18}/>
                        Calendar
                    </div>
                    <div className={styles.icon}>
                        <IoIosGitBranch size={18}/>
                        Branches
                    </div>
                    <div className={styles.icon}>
                        <BsGraphUpArrow size={18}/>
                        Statistics
                    </div>
                    <div className={styles.icon}>
                        <IoSettingsOutline size={18}/>
                        Settings
                    </div>
            </div>
                <div className={`${styles.container} gap-2`}>
                    <div className='flex items-center gap-2 bg-[#F5F5F5] px-2 rounded-full py-1'>
                        <CiSearch size={20}/>
                        <input className='bg-[#F5F5F5] focus:outline-none' type='text' placeholder='Search here...'/>
                    </div>
                    <div className='bg-[#F5F5F5] p-1 rounded-full cursor-pointer hover:bg-[#F6643C] hover:text-white'>
                        <IoNotificationsOutline size={20}/>
                    </div>
                    <div className='rounded-full'>
                        <img src={profile.src} className='w-[35px]'/>
                    </div>
                </div>
        </div>
    )
}

export default NavBar