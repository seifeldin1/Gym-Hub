import NavBar from "@components/NavBar";
import NavBarPage from "@components/NavBarPage";
import BottomBar from "@components/BottomBar";
import styles from "@styles/navbarpage.module.css" //it has style for animation of background
import HomeImage from '@public/assets/images/HomePage/Home-image.png';
import Amr from '@public/images/gym-004.png';
import strong from '@public/assets/images/HomePage/strong.png';
import Newsletter from "@components/Newsletter";

const Header = () => {
    return (
        <>
            <div className="flex justify-center">
                <p className="text-[#fffc68] font-bold italic text-[1500%] mt-10 z-0">FIT</p>
                <img className="ml-[-8%] mr-[-5%] my-32 h-full z-10" src={HomeImage.src} alt="Description of the image" />
                <p className="text-[#fffc68] font-bold italic text-[1500%] mt-10 z-0">SS</p>
            </div>

            <div className=" flex justify-center mt-10 mb-10 gap-40 ">
                <div className=" flex items-center ">

                    <img className="items-center  ease-in  duration-300 ml-15 hover:scale-110"
                        src={strong.src} // Path to the image
                        alt="Description of the image" // Alternative text for accessibility
                        width={500} // Width in pixels
                        height={500} // Height in pixels    
                    />

                </div>

                <div className="items-center mr-12 l-6 my-4 border-none w-1/3 h-full">
                    <div className="relative flex flex-col">
                        <span className="absolute inset-0 text-[8rem] lg:text-[10rem] font-bold uppercase text-green-300 opacity-10 z-0">service</span>
                        <h2 class="text-4xl sm:text-5xl font-extrabold mt-20 text-[#b2d438] z-10 relative ease-in duration-300 hover:scale-110">Providing solution for Fitness</h2>
                    </div>
                    <p class="my-9 text-lg sm:text-xl text-gray-300">
                        Welcome to <span class="text-[#b2d438] font-bold">PulseFit</span> where we are dedicated to providing comprehensive
                        fitness solutions tailored to your needs. From expert guidance and diverse workout plans to a supportive community, we’re
                        here to help you achieve your health and wellness goals with ease and confidence."
                    </p>
                    <button className="text-white text-lg border-2 font-bold border-solid my-4 ease-in duration-300 hover:border-green-300 px-3 py-2 bg-transparent hover:scale-110 hover:bg-green-300 hover:text-black">Read More</button>          </div>
            </div>

            <div className="bg-transparent py-10">
                <div className="max-w-7xl mx-auto px-4 grid grid-cols-1 md:grid-cols-3 gap-6">
                    
                    <div className="bg-yellow-300 text-black p-6 text-center rounded-md">
                        <h3 className="text-2xl font-extrabold uppercase text-orange-700">Basic</h3>
                        <p className="text-5xl font-extrabold mt-4">
                            $20<span className="text-lg align-top">99</span>
                        </p>
                        <p className="text-lg mt-1">Monthly</p>
                        <div className="mt-6 space-y-3">
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Personal Trainer
                            </p>
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Cardio Fitness
                            </p>
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Weight Loss & Toning
                            </p>
                            <p className="line-through">Weight & Resistance Training</p>
                            <p className="line-through">Sports Conditioning</p>
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Specialty Services
                            </p>
                        </div>
                        <button className="mt-6 bg-black font-extrabold text-white px-4 py-2 rounded hover:bg-white hover:text-black transition">
                            Join Now
                        </button>
                    </div>

                    
                    <div className="bg-yellow-300 text-black p-6 text-center rounded-md">
                        <h3 className="text-2xl font-extrabold uppercase text-orange-700">Middle</h3>
                        <p className="text-5xl font-extrabold mt-4">
                            $30<span className="text-lg align-top">99</span>
                        </p>
                        <p className="text-lg mt-1">Monthly</p>
                        <div className="mt-6 space-y-3">
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Personal Trainer
                            </p>
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Cardio Fitness
                            </p>
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Weight Loss & Toning
                            </p>
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Weight & Resistance Training
                            </p>
                            <p className="line-through">Sports Conditioning</p>
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Specialty Services
                            </p>
                        </div>
                        <button className="mt-6 bg-black font-extrabold text-white px-4 py-2 rounded hover:bg-white hover:text-black transition">
                            Join Now
                        </button>
                    </div>

                    
                    <div className="bg-yellow-300 text-black p-6 text-center rounded-md">
                        <h3 className="text-2xl font-extrabold uppercase text-orange-700">High</h3>
                        <p className="text-5xl font-extrabold mt-4">
                            $50<span className="text-lg align-top">99</span>
                        </p>
                        <p className="text-lg mt-1">Monthly</p>
                        <div className="mt-6 space-y-3">
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Personal Trainer
                            </p>
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Cardio Fitness
                            </p>
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Weight Loss & Toning
                            </p>
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Weight & Resistance Training
                            </p>
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Sports Conditioning
                            </p>
                            <p className="flex items-center justify-center gap-2">
                                <span>&#10004;</span> Specialty Services
                            </p>
                        </div>
                        <button className="mt-6 bg-black font-extrabold text-white px-4 py-2 rounded hover:bg-white hover:text-black transition">
                            Join Now
                        </button>
                    </div>
                </div>
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
                <Newsletter/>
                <BottomBar page_name="home"/>
            </div>
        </>
    );
};

export default Home;