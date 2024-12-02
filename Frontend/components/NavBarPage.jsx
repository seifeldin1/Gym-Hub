import { CgGym } from "react-icons/cg";
import styles from "@styles/navbarpage.module.css"

const NavBarPage = () => {
    return (
        <>
        <div className=" w-5/12 h-16 flex items-center justify-around mx-auto rounded-full text-xl">
            <button className={`${styles.NavButtonsSelected}`}>Home</button>
            <button className={`${styles.NavButtons}`}>About</button>
            <button className={`${styles.NavButtons}`}>Services</button>
            <button className={`${styles.NavButtons}`}>Blog</button>
            <button className={`${styles.NavButtons}`}>Page</button>
            <button className={`${styles.NavButtons}`}>Contact</button>
        </div>
        </>
    )
}

export default NavBarPage