import NavBar from "@components/NavBar";
import NavBarPage from "@components/NavBarPage";
import styles from "@styles/navbarpage.module.css" //it has style for animation of background
import HomeImage from '@public/assets/images/HomePage/Home-image.png';

const Header = () => {
    return (
        <>
        <div className="flex justify-center">
            <p className="text-[#fffc68] font-bold italic text-[1500%] mt-10 z-0">FIT</p>
            <img className="ml-[-8%] mr-[-5%] my-32 h-full z-10" src={HomeImage.src} alt="Description of the image"/>
            <p className="text-[#fffc68] font-bold italic text-[1500%] mt-10 z-0">SS</p>
        </div>
        
        
            
        </>
    );
}

const Home = () => {
    return (
        <>
            <div className={`${styles.AnimBackground}`}>
                <NavBarPage page_name="home"/>
                <Header/>
            </div>
        </>
    );
};

export default Home;