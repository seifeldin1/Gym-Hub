import styles from "@styles/navbarpage.module.css"
import GYMlogo from '@public/assets/images/GYM-Logo.png';
import { IoMdLogIn } from "react-icons/io";
import { IoMdPersonAdd } from "react-icons/io";
const NavBarPage = ({ page_name }) => {
    return (
        <>
            <div className={`${styles.DivOFbuttons}`}>
                <img className="ease-in h-8 duration-300 hover:scale-110 mx-52" src={GYMlogo.src} alt="Description of the image" />
                <div className="w-full h-10 flex items-center justify-between rounded-full text-lg font-semibold">
                    <button className={`${page_name === "home" ? styles.NavButtonsSelected : styles.NavButtons}`}>Home</button>
                    <button className={`${page_name === "about" ? styles.NavButtonsSelected : styles.NavButtons}`}>About</button>
                    <button className={`${page_name === "services" ? styles.NavButtonsSelected : styles.NavButtons}`}>Services</button>
                    <button className={`${page_name === "programs" ? styles.NavButtonsSelected : styles.NavButtons}`}>Programs</button>
                    <button className={`${page_name === "contact" ? styles.NavButtonsSelected : styles.NavButtons}`}>Contact</button>
                </div>
                <div className="mx-44 flex">
                    <button className="group flex items-center mx-10 justify-center bg-transparent rounded-full hover:scale-110 transition-transform">
                        <IoMdLogIn className={`${styles.Icon}`} />
                    </button>
                    <button className="group flex items-center mx-10 justify-center bg-transparent rounded-full hover:scale-110 transition-transform">
                        <IoMdPersonAdd className={`${styles.Icon}`}/>
                    </button>
                </div>

            </div>
        </>
    )
}
export default NavBarPage