import styles from '@styles/navbar.module.css'
import { RiHome5Line } from "react-icons/ri";
import { BsGraphUpArrow } from "react-icons/bs";
import { IoIosGitBranch } from "react-icons/io";
import { IoSettingsOutline } from "react-icons/io5";
import { LuCalendar } from "react-icons/lu";
import { CiSearch } from "react-icons/ci";
import { IoNotificationsOutline } from "react-icons/io5";
import profile from '@public/assets/images/profile.jpg'
import GymIcon from '@public/assets/images/image.png';

export const DashHeader = ({page_name}) => {
    return (
        <div className='flex justify-between w-full px-6 py-4 items-center'>
            <h1 className='text-2xl font-semibold'>{page_name}</h1>
            <div className={`flex items-center gap-2`}>
                <div className='flex items-center gap-2 bg-[#131313] text-white px-2 rounded-full py-1'>
                    <CiSearch size={18} />
                    <input className='bg-[#131313] focus:outline-none' type='text' placeholder='Search here...' />
                </div>
                <div className='bg-[#F5F5F5] p-1 rounded-full cursor-pointer hover:bg-[#DBFF55] text-[#7B817E] hover:text-black'>
                    <IoNotificationsOutline size={20} />
                </div>
                <div className='rounded-full overflow-hidden'>
                    <img src={profile.src} className='w-[30px]' />
                </div>
            </div>
        </div>
    )
}