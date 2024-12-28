import styles from "@styles/navbarpage.module.css"
import GYMlogo from '@public/assets/images/GYM-Logo.png';
import { IoMdLogIn } from "react-icons/io";
import { IoMdPersonAdd } from "react-icons/io";
import Link from 'next/link';  // Import Link from Next.js

const NavBarPage = ({ page_name }) => {
    return (
        <>
            <div className={`${styles.DivOFbuttons}`}>
                <img className="ease-in h-8 duration-300 hover:scale-110 mx-52" src={GYMlogo.src} alt="Description of the image" />
                <div className="w-full h-10 flex items-center justify-between rounded-full text-lg font-semibold">
                    <Link href="/home">
                        <button className={`${page_name === "home" ? styles.NavButtonsSelected : styles.NavButtons}`}>Home</button>
                    </Link>
                    <Link href="/about">
                        <button className={`${page_name === "about" ? styles.NavButtonsSelected : styles.NavButtons}`}>About</button>
                    </Link>
                    <Link href="/services">
                        <button className={`${page_name === "services" ? styles.NavButtonsSelected : styles.NavButtons}`}>Services</button>
                    </Link>
                    <Link href="/programs">
                        <button className={`${page_name === "programs" ? styles.NavButtonsSelected : styles.NavButtons}`}>Programs</button>
                    </Link>
                    <Link href="/contact">
                        <button className={`${page_name === "contact" ? styles.NavButtonsSelected : styles.NavButtons}`}>Contact</button>
                    </Link>
                </div>
                <div className="mx-44 flex">
                    <Link href="/login"> {/* Wrap login button in Link */}
                        <button className="group flex items-center mx-10 justify-center bg-transparent rounded-full hover:scale-110 transition-transform">
                            <IoMdLogIn className={`${styles.Icon}`} />
                        </button>
                    </Link>
                    <Link href="/signUp"> {/* Wrap signup button in Link */}
                        <button className="group flex items-center mx-10 justify-center bg-transparent rounded-full hover:scale-110 transition-transform">
                            <IoMdPersonAdd className={`${styles.Icon}`}/>
                        </button>
                    </Link>
                </div>
            </div>
        </>
    )
}

export default NavBarPage;
