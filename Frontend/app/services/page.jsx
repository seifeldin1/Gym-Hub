import Image from 'next/image';
import NavBar from "@components/NavBar";
import services1 from '@public/images/fitness.png';
import NavBarPage from "@components/NavBarPage";
import  Schedule  from "@components/Schedule";
import Newwsletter from "@components/Newsletter";
import Newsletter from '@components/Newsletter';
import styles from "@styles/navbarpage.module.css" //it has style for animation of background
import BottomBar from "@components/BottomBar";

const Serv = () =>{
return(
    <>
    <div className="flex justify-center my-20">
        <p className="text-[#fffc68] font-bold italic text-[1250%] mt-10 z-0">CON</p>
        <img className="ml-[-8%] mr-[-5%] m-12 h-96 z-10" src={services1.src} alt="Description of the image"/>
        <p className="text-[#fffc68] font-bold italic text-[1250%] mt-10 z-0">CT</p>
    </div>
    </>
);
};
const OpenClass= () =>{
    return(
        <>
        <div className="relative flex flex-col text-center">
            <span className="absolute inset-0 text-[8rem] lg:text-[5rem] font-bold uppercase text-yellow-300 opacity-10 z-0">BODY BUILDING</span>
            <h2 class="text-4xl sm:text-4xl font-extrabold mt-10 text-orange-400 z-10 relative ease-in duration-300 hover:scale-110"> OPEN CLASS FITNESS</h2>
        </div>

        <section class="max-w-7xl mx-auto px-6 sm:px-12 py-16">
            <div className="bg-transparent grid grid-cols-3 pl-6 mx-auto text-center">
                <div className="border-t-8 border-yellow-400 flex flex-col items-center mb-8 justify-center w-3/4 h-4/4 bg-gray-700/30 p-8">
                    <h1 className="text-orange-400 font-extrabold text-xl mb-4">CARDIO FITNESS</h1>
                    <p className="text-gray-300">
                        Boost your endurance and burn calories with our cardio classes! Perfect for all fitness levels, 
                        these sessions improve heart health, build stamina, 
                        and energize your day.</p>
                    <button className="font-bold border-2 border-yellow mt-12 p-2 text-yellow-300 border-yellow-300 ease-in duration-150 hover:border-white hover:bg-white hover:text-black hover:scale-110">Open Class</button>
                </div>

                <div className="border-t-8 border-yellow-400 flex flex-col items-center mb-8 justify-center w-3/4 h-4/4 bg-gray-700/30 p-8">
                    <h1 className="text-orange-400 font-extrabold text-xl mb-4">CORPORATE FITNESS</h1>
                    <p className="text-gray-300">
                        Boost productivity and well-being with our Corporate Fitness programs! 
                        Designed to energize teams, improve health, 
                        and foster a positive work culture
                        </p>
                    <button className="font-bold border-2 border-yellow mt-12 p-2 text-yellow-300 border-yellow-300 ease-in duration-150 hover:border-white hover:bg-white hover:text-black hover:scale-110">Open Class</button>
                </div>

                <div className="border-t-8 border-yellow-400 flex flex-col items-center mb-8 justify-center w-3/4 h-4/4 bg-gray-700/30 p-8">
                    <h1 className="text-orange-400 font-extrabold text-xl mb-4">GROUP TRAINING</h1>
                    <p className="text-gray-300">
                        Achieve your goals together with Group Training! Fun, motivating, 
                        and designed for all fitness levels to help you stay inspired and succeed.</p>
                    <button className="font-bold border-2 border-yellow mt-12 p-2 text-yellow-300 border-yellow-300 ease-in duration-150 hover:border-white hover:bg-white hover:text-black hover:scale-110">Open Class</button>
                </div>

                <div className="border-t-8 border-yellow-400 flex flex-col items-center mb-8 justify-center w-3/4 h-4/4 bg-gray-700/30 p-8">
                    <h1 className="text-orange-400 font-extrabold text-xl mb-4">PERSONAL TRAINING</h1>
                    <p className="text-gray-300">
                        Get personalized guidance with Personal Training!
                        Tailored workouts and expert coaching to help you reach your fitness goals faster.</p>
                    <button className="font-bold border-2 border-yellow mt-12 p-2 text-yellow-300 border-yellow-300 ease-in duration-150 hover:border-white hover:bg-white hover:text-black hover:scale-110">Open Class</button>
                </div>

                <div className="border-t-8 border-yellow-400 flex flex-col items-center mb-8 justify-center w-3/4 h-4/4 bg-gray-700/30 p-8">
                    <h1 className="text-orange-400 font-extrabold text-xl mb-4">STRENGTH TRAINING</h1>
                    <p className="text-gray-300">
                        Build muscle and boost power with Strength Training! Perfect for all levels,
                        designed to enhance strength, endurance, and confidence.</p>
                    <button className="font-bold border-2 border-yellow mt-12 p-2 text-yellow-300 border-yellow-300 ease-in duration-150 hover:border-white hover:bg-white hover:text-black hover:scale-110">Open Class</button>
                </div>

                <div className="border-t-8 border-yellow-400 flex flex-col items-center mb-8 justify-center w-3/4 h-4/4 bg-gray-700/30 p-8">
                    <h1 className="text-orange-400 font-extrabold text-xl mb-4">SPORTS CONDITIONING</h1>
                    <p className="text-gray-300">
                        Elevate your performance with Sports Conditioning! Tailored training to improve strength, speed,
                        and agility for athletes of all levels.</p>
                    <button className="font-bold border-2 border-yellow mt-12 p-2 text-yellow-300 border-yellow-300 ease-in duration-150 hover:border-white hover:bg-white hover:text-black hover:scale-110">Open Class</button>
                </div>
            </div>
        </section>
        </>
    );
    };


const Home = () => {
    return (
        <>
        <div className={`${styles.AnimBackground}`}>
            <NavBarPage page_name="services"/>
            <Serv/>
            <OpenClass/>
            <Schedule/>
            <Newsletter/>
            <BottomBar page_name="programs"/>
        </div>
        </>
    );
};

export default Home;