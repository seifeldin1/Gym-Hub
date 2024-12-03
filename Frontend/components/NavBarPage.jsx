import styles from "@styles/navbarpage.module.css"

const NavBarPage = ({page_name}) => {
    return (
        <>
        <div className=" w-5/12 h-16 flex items-center justify-around mx-auto rounded-full text-xl">
            <button className={`${page_name === "home" ? styles.NavButtonsSelected : styles.NavButtons}`}>Home</button>
            <button className={`${page_name === "about" ? styles.NavButtonsSelected : styles.NavButtons}`}>About</button>
            <button className={`${page_name === "services" ? styles.NavButtonsSelected : styles.NavButtons}`}>Services</button>
            <button className={`${page_name === "blog" ? styles.NavButtonsSelected : styles.NavButtons}`}>Blog</button>
            <button className={`${page_name === "page" ? styles.NavButtonsSelected : styles.NavButtons}`}>Page</button>
            <button className={`${page_name === "contact" ? styles.NavButtonsSelected : styles.NavButtons}`}>Contact</button>
        </div>
        </>
    )
}

export default NavBarPage