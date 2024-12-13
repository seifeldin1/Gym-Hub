import styles from "@styles/BottomBar.module.css"
import GYMlogo from '@public/assets/images/GYM-Logo.png';
const BottomBar = ({page_name}) => {
    return (
        <>
        <div className="bg-gray-600/40">

        </div>
        <div className={`${styles.DivOFbuttons}`}>
            <img className="h-4 mr-6" src={GYMlogo.src} alt="Description of the image"/>
            <div className="w-[100%] flex items-center gap-4 text-base font-semibold ">
                <button className={`${page_name === "home" ? styles.NavButtonsSelected : styles.NavButtons}`}>Home</button>
                <button className={`${page_name === "about" ? styles.NavButtonsSelected : styles.NavButtons}`}>About</button>
                <button className={`${page_name === "services" ? styles.NavButtonsSelected : styles.NavButtons}`}>Services</button>
                <button className={`${page_name === "programs" ? styles.NavButtonsSelected : styles.NavButtons}`}>Programs</button>
                <button className={`${page_name === "contact" ? styles.NavButtonsSelected : styles.NavButtons}`}>Contact</button>
            </div>
        </div>
        <div className="bg-transparent text-center mt-5">
            <p className="text-gray-400 font-extrabold">©2025 PulseFit. All rights reserved</p>
        </div>
        </>
    )
}
export default BottomBar