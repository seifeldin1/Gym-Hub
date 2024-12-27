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
import styles from "@styles/navbarpage.module.css" //it has style htmlFor animation of background
import { MdEmail } from "react-icons/md";
import BottomBar from '@components/BottomBar';


const Form = () => {
    return (
        <>
            <div className="min-h-screen bg-transparent">


                <div className="max-w-5xl w-full bg-gray-600/50 shadow-lg rounded-lg p-8 lg:p-12 flex flex-col lg:flex-row gap-8 mx-auto">
                    <div className="flex-1">
                        <h2 className="text-3xl font-extrabold text-green-500 mb-6">Get in Touch</h2>
                        <form action="#" method="POST" className="space-y-4">
                            <div>
                                <label htmlFor="name" className="block text-lg font-medium text-green-500">Your Name</label>
                                <input type="text" id="name" name="name" required
                                    className="w-full mt-1 px-4 py-2 border border-gray-300 rounded-lg shadow-sm focus:ring-orange-500 focus:border-orange-500" />
                            </div>
                            <div>
                                <label htmlFor="email" className="block text-lg font-medium text-green-500">Your Email</label>
                                <input type="email" id="email" name="email" required
                                    className="w-full mt-1 px-4 py-2 border border-gray-300 rounded-lg shadow-sm focus:ring-orange-500 focus:border-orange-500" />
                            </div>
                            <div>
                                <label htmlFor="message" className="block text-lg font-medium text-green-500">Your Message</label>
                                <textarea id="message" name="message" rows="4" required
                                    className="w-full mt-1 px-4 py-2 border border-gray-300 rounded-lg shadow-sm focus:ring-orange-500 focus:border-orange-500"></textarea>
                            </div>
                            <div>
                                <button type="submit"
                                    className="w-full px-6 py-3 bg-green-500 text-black text-lg font-extrabold rounded-lg shadow hover:bg-transparent hover:text-green-500 transition">
                                    Send Message
                                </button>
                            </div>
                        </form>
                    </div>

                    <div className="flex-1 border-l-2 border-gray-300 pl-6 my-auto">
                        <h2 className="text-3xl font-extrabold text-green-500 mb-2">Contact Information</h2>
                        <div className="space-y-4">

                            <div className="flex items-center">
                                <div className="p-3 bg-green-500 text-black rounded-full text-3xl mt-8"> <MdEmail/> </div>
                                <div className="ml-4">
                                    <h3 className="text-lg font-extrabold text-white mt-8">Phone</h3>
                                    <p className="text-gray-400">+123 456 7890</p>
                                </div>
                            </div>

                            <div className="flex items-center">
                                <div className="p-3 bg-green-500 text-black rounded-full mt-8 text-3xl"> <MdEmail/> </div>
                                <div className="ml-4">
                                    <h3 className="text-lg font-extrabold text-white mt-8">Email</h3>
                                    <p className="text-gray-400">contact@yourdomain.com</p>
                                </div>
                            </div>

                            <div className="flex items-center">
                                <div className="p-3 bg-green-500 text-black rounded-full mt-8 text-3xl"> <MdEmail/> </div>
                                <div className="ml-4 mt-8">
                                    <h3 className="text-lg font-extrabold text-white">Location</h3>
                                    <p className="text-gray-400">123 Main Street, Your City, Country</p>
                                </div>
                            </div>
                            
                        </div>
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
                <NavBarPage page_name="contact" />
                <Form />
                <BottomBar page_name="contact"/>
            </div>
        </>
    );
};

export default Home;