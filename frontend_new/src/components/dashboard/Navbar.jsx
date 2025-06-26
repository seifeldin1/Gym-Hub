import styles from "@/styles/Dashboard/Navbar.module.css"
import { LogOut, House, Settings, UserRound } from "lucide-react";

const Navbar = () => {
    return (
        <div className={styles.NavBarContainer}>
            <img src="/images/dumbbell.png" alt="dumbbell logo" className={styles.logo} />
            <div className={styles.mainIconsContainer}>
                <div className={styles.iconStyle} >
                    <House />
                </div>
                <div className={styles.iconStyle} >
                    <UserRound />
                </div>
                <div className={styles.iconStyle} >
                    <Settings />
                </div>
            </div>
            <div className={styles.iconStyle} >
                <LogOut />
            </div>
        </div>
    )
}

export default Navbar;