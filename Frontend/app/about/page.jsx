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
import BottomBar from '@components/BottomBar';

const About = () => {
  return (
    <>
      <div className=" flex justify-center mt-10 mb-10 gap-40 ">
        <div className=" flex items-center ">

          <Image className="items-center  ease-in  duration-300 ml-15 hover:scale-110"
            src={Amr.src} // Path to the image
            alt="Description of the image" // Alternative text for accessibility
            width={300} // Width in pixels
            height={300} // Height in pixels    
          />

        </div>

        <div className="items-center mr-12 l-6 my-4 border-none w-1/3 h-full">
          <div className="relative flex flex-col">
            <span className="absolute inset-0 text-[8rem] lg:text-[10rem] font-bold uppercase text-yellow-300 opacity-10 z-0">Trainers</span>
            <h2 class="text-4xl sm:text-5xl font-extrabold mt-20 text-orange-400 z-10 relative ease-in duration-300 hover:scale-110">Change Your Body With Our Trainer</h2>
          </div>
          <p class="my-9 text-lg sm:text-xl text-gray-300">
            Welcome to <span class="text-orange-400 font-bold">PulseFit</span>, where fitness meets innovation! Our mission is to provide a dynamic and motivating environment for individuals of all fitness levels. With state-of-the-art equipment, professional trainers, and personalized workout plans,
            we aim to empower you to achieve your health and wellness goals. Whether you're here to build strength, boost endurance, or simply improve your lifestyle, our gym is your ultimate fitness destination. 
            Let’s embark on this journey together — because <span class="text-orange-400 font-bold">your progress</span> is our passion!
          </p>
          <button className="text-white text-lg border-2 border-solid my-4 ease-in duration-300 hover:border-orange-400 px-3 py-2 bg-transparent hover:scale-110 hover:bg-orange-400 hover:text-gray-100">Read More</button>          </div>
        </div>
        </>
    );
};


const Gallery = () => {
  return (
    <>
      <div className="bg-transparent sm:flex-col mt-[25%]">
        <h2 class="text-center my-5 text-4xl sm:text-5xl font-extrabold mt-24 text-orange-400 relative ease-in duration-300 hover:scale-110">Our Gallery</h2>
        <section class="max-w-7xl mx-auto px-6 sm:px-12 py-16">
            <div class="grid grid-cols-4 gap-8">
              <div className="flex justify-center items-center mb-4"><Image src={gall1.src}  alt="Gallery-1"  width={300} height={300} className="hover:opacity-50 items-center"/></div> 
              <div className="flex justify-center items-center mb-4"><Image src={gall2.src}  alt="Gallery-2"  width={300} height={300} className="hover:opacity-50"/></div>
              <div className="flex justify-center items-center mb-4"><Image src={gall3.src}  alt="Gallery-3"  width={300} height={300} className="hover:opacity-50"/></div>
              <div className="flex justify-center items-center mb-4"><Image src={gall4.src}  alt="Gallery-4"  width={300} height={300} className="hover:opacity-50"/></div>
              <div className="flex justify-center items-center mb-4"><Image src={gall5.src}  alt="Gallery-5"  width={300} height={300} className="hover:opacity-50"/></div>
              <div className="flex justify-center items-center mb-4"><Image src={gall6.src}  alt="Gallery-6"  width={300} height={300} className="hover:opacity-50"/></div>
              <div className="flex justify-center items-center mb-4"><Image src={gall7.src}  alt="Gallery-7"  width={300} height={300} className="hover:opacity-50"/></div>
              <div className="flex justify-center items-center mb-4"><Image src={gall8.src}  alt="Gallery-8"  width={300} height={300} className="hover:opacity-50"/></div>
            </div>
          </section>
        </div>
    </>
  );

};


const Home = () => {
  return (
    <>
      <div className={`${styles.AnimBackground}`}>
        <NavBarPage page_name="about"/>
        <About/>
        <WHY/>
        <Gallery/>
        <Newsletter/>
        <BottomBar page_name="about"/>
      </div>
    </>
  );
};

export default Home;