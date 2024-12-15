import Image from 'next/image';
import Amr from '@public/images/gym-004.png';
import gall1 from '@public/images/gallery-1.jpg';
import gall2 from '@public/images/gallery-2.jpg';
import gall3 from '@public/images/gallery-3.jpg';
import gall4 from '@public/images/gallery-4.jpg';
import gall5 from '@public/images/gallery-5.jpg';
import gall6 from '@public/images/gallery-6.jpg';
import gall7 from '@public/images/gallery-7.jpg';
import gall8 from '@public/images/gallery-8.jpg';
import NavBar from "@components/NavBar";
import NavBarPage from "@components/NavBarPage";
import WHY from "@components/WHY";
import Newsletter from "@components/Newsletter";
import styles from "@styles/navbarpage.module.css" //it has style for animation of background
import { MdEmail } from "react-icons/md";
import BottomBar from "@components/BottomBar";

const Form = () => {
    return (
        <>
            <div className="relative flex flex-col text-center">
                <span className="absolute inset-0 text-[8rem] lg:text-[5rem] font-bold uppercase text-yellow-300 opacity-10 z-0">PROGRAMS</span>
                <h2 class="text-4xl sm:text-4xl font-extrabold mt-10 text-orange-400 z-10 relative ease-in duration-300 hover:scale-110">Training Workout</h2>
            </div>
            <div className="m-10 bg-transparent mt-14">
                <div className="bg-transparent flex items-center justify-center">
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-6 max-w-6xl w-full">

                        <div className="flex bg-gray-800 shadow-lg rounded-lg overflow-hidden">
                            <img src={gall1.src} alt="Body Building" className="w-1/2 object-cover" />
                            <div className="w-1/2 p-6">
                                <h2 className="text-xl font-bold text-orange-300 mb-4">Body Building</h2>
                                <p className="text-gray-400 text-sm mb-2">Time: <span className="text-white">10:00 AM</span></p>
                                <p className="text-gray-400 text-sm mb-6">Schedule: <span className="text-white">Jan 20, 2021</span></p>
                                <button className="px-4 py-2 bg-transparent border font-bold border-white text-white hover:bg-orange-400 hover:border-orange-400 hover:text-white hover:font-bold transition rounded-full w-[7rem]">Join Now</button>
                            </div>
                        </div>

                        <div className="flex bg-gray-800 shadow-lg rounded-lg overflow-hidden">
                            <img src={gall3.src} alt="Kardio Work" className="w-1/2 object-cover" />
                            <div className="w-1/2 p-6">
                                <h2 className="text-xl font-bold text-orange-300 mb-4">Cardio Work</h2>
                                <p className="text-gray-400 text-sm mb-2">Time: <span className="text-white">10:00 AM</span></p>
                                <p className="text-gray-400 text-sm mb-6">Schedule: <span className="text-white">Jan 20, 2021</span></p>
                                <button className="px-4 py-2 bg-transparent border font-bold border-white text-white hover:bg-orange-400 hover:border-orange-400 hover:text-white hover:font-bold transition rounded-full w-[7rem]">Join Now</button>
                            </div>
                        </div>

                        <div className="flex bg-gray-800 shadow-lg rounded-lg overflow-hidden">
                            <img src={gall7.src} alt="Dynamo Fitness" className="w-1/2 object-cover" />
                            <div className="w-1/2 p-6">
                                <h2 className="text-xl font-bold text-orange-300 mb-4">Dynamo Fitness</h2>
                                <p className="text-gray-400 text-sm mb-2">Time: <span className="text-white">10:00 AM</span></p>
                                <p className="text-gray-400 text-sm mb-6">Schedule: <span className="text-white">Jan 20, 2021</span></p>
                                <button className="px-4 py-2 bg-transparent border font-bold border-white text-white hover:bg-orange-400 hover:border-orange-400 hover:text-white hover:font-bold transition rounded-full w-[7rem]">Join Now</button>
                            </div>
                        </div>

                        <div className="flex bg-gray-800 shadow-lg rounded-lg overflow-hidden">
                            <img src={gall4.src} alt="Bent-over Row" className="w-1/2 object-cover" />
                            <div className="w-1/2 p-6">
                                <h2 className="text-xl font-bold text-orange-300 mb-4">Bent-over Row</h2>
                                <p className="text-gray-400 text-sm mb-2">Time: <span className="text-white">10:00 AM</span></p>
                                <p className="text-gray-400 text-sm mb-6">Schedule: <span className="text-white">Jan 20, 2021</span></p>
                                <button className="px-4 py-2 bg-transparent border font-bold border-white text-white hover:bg-orange-400 hover:border-orange-400 hover:text-white hover:font-bold transition rounded-full w-[7rem]">Join Now</button>
                            </div>
                        </div>

                        <div className="flex bg-gray-800 shadow-lg rounded-lg overflow-hidden">
                            <img src={gall2.src} alt="Bent-over Row" className="w-1/2 object-cover" />
                            <div className="w-1/2 p-6">
                                <h2 className="text-xl font-bold text-orange-300 mb-4">Bench Press</h2>
                                <p className="text-gray-400 text-sm mb-2">Time: <span className="text-white">10:00 AM</span></p>
                                <p className="text-gray-400 text-sm mb-6">Schedule: <span className="text-white">Jan 20, 2021</span></p>
                                <button className="px-4 py-2 bg-transparent border font-bold border-white text-white hover:bg-orange-400 hover:border-orange-400 hover:text-white hover:font-bold transition rounded-full w-[7rem]">Join Now</button>
                            </div>
                        </div>

                        <div className="flex bg-gray-800 shadow-lg rounded-lg overflow-hidden">
                            <img src={gall5.src} alt="Bent-over Row" className="w-1/2 object-cover" />
                            <div className="w-1/2 p-6">
                                <h2 className="text-xl font-bold text-orange-300 mb-4">Machine Squat</h2>
                                <p className="text-gray-400 text-sm mb-2">Time: <span className="text-white">10:00 AM</span></p>
                                <p className="text-gray-400 text-sm mb-6">Schedule: <span className="text-white">Jan 20, 2021</span></p>
                                <button className="px-4 py-2 bg-transparent border font-bold border-white text-white hover:bg-orange-400 hover:border-orange-400 hover:text-white hover:font-bold transition rounded-full w-[7rem]">Join Now</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div className="relative bg-black text-white">
                
                <div className="absolute inset-0">
                    <img
                        src={gall8.src}
                        alt="Background"
                        className="w-full h-full object-cover opacity-50"
                    />
                </div>

                
                <div className="relative max-w-6xl mx-auto px-4 py-20 flex flex-col justify-center h-[50%]">
                    <div className="md:w-1/2">
                        <h1 className="text-4xl font-extrabold mb-6">
                            Join Our Class <br />
                            With Our Professional Trainer
                        </h1>
                        <p className="text-gray-300 mb-8 leading-relaxed">
                        Elevate your fitness journey with expert guidance from our professional trainers. 
                        Whether you're just starting or striving to reach new goals, our personalized classes 
                        provide the perfect environment for growth, motivation, and success. Join us and become the best version of yourself!
                        </p>
                        <button className="px-4 py-2 bg-transparent border font-bold border-white text-white hover:bg-orange-400 hover:border-orange-400 hover:text-white hover:font-bold transition rounded-full">Read More</button>
                    </div>
                </div>
            </div>

        </>
    );

};


const Home = () => {
    return (
        <>
            <div className={`${styles.AnimBackground}`}>
                <NavBarPage page_name="programs"/>
                <Form />
                <Newsletter/>
                <BottomBar page_name="programs"/>
            </div>
        </>
    );
};

export default Home;