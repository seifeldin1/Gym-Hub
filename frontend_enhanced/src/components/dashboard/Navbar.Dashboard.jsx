import styles from "@/styles/Dashboard/Navbar.module.css"
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import {
    faHouse,
    faDumbbell,
    faCalendarDays,
    faChalkboardUser,
    faAddressCard,
    faRightFromBracket
} from "@fortawesome/free-solid-svg-icons"

function NavbarDashboard() {
    return (
        <div className={styles.NavBarContainer}>
            <div>
                <FontAwesomeIcon icon={faDumbbell} className="size-12 text-white" />
            </div>
            <div className={styles.iconsContainer}>
                <FontAwesomeIcon icon={faHouse} className={styles.iconStyle} />
                <FontAwesomeIcon icon={faCalendarDays} className={styles.iconStyle} />
                <FontAwesomeIcon icon={faChalkboardUser} className={styles.iconStyle} />
                <FontAwesomeIcon icon={faAddressCard} className={styles.iconStyle} />
            </div>
            <div className="flex flex-col items-center justify-between min-h-[100px]">
                <FontAwesomeIcon icon={faRightFromBracket} className={styles.iconStyle}/>
                <img src="/images/8899746.jpg" className="size-10 rounded-full" alt="" />
            </div>
        </div>
    )
}

export default NavbarDashboard