import Image from 'next/image';
import Amr  from '@public/images/gym-004.png';
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

const About=()=>{
    return(
        <>
        <div className=" flex justify-around mt-10 mb-10 ">
        <div className=" flex items-center ">
            
            <Image className="items-center  ease-in  duration-300 ml-15 hover:scale-110"
        src={Amr.src} // Path to the image
        alt="Description of the image" // Alternative text for accessibility
        width={300} // Width in pixels
        height={300} // Height in pixels    
      />

        </div>

        <div className="items-center mr-12 l-6 my-4 text-center border-none w-1/2 h-full">
        <div className="relative flex flex-col items-center text-center">
        <span className="absolute inset-0  text-[8rem] lg:text-[10rem] font-bold uppercase text-yellow-300 opacity-10 z-0">
        Trainers
    </span>
            <h2 className="text-white relative font-bold text-3xl  z-10">Change Your Body With Our Trainer</h2>
            </div>
            <p className="text-white ml-6 my-4  text-center  lg:w-full">Welcome to <span className="text-yellow-300">GYM HUB</span> , where fitness meets innovation! Our mission is to provide a dynamic and motivating environment for individuals of all fitness levels. With state-of-the-art equipment, professional trainers, and personalized workout plans,
                 we aim to empower you to achieve your health and wellness goals. Whether you're here to build strength, boost endurance, or simply improve your lifestyle, our gym is your ultimate fitness destination. 
                Let’s embark on this journey together—because your progress is our passion!</p>
        
            <button className="text-white border-2 border-solid my-8 ease-in duration-300  hover:border-yellow-400 rounded-[10px] px-3 py-2 bg-black hover:scale-110  hover:bg-yellow-400 hover:text-black">Read More</button>
        </div>
        </div>
        </>
    );
};
const WHY = () =>{
  return(
    <>
<div className="relative text-center h-80  py-3 bg-black ease-in duration-300 hover:scale-110">
  
  <h1 className="absolute inset-0 text-5xl font-serif  font-bold text-yellow-300 opacity-30 z-0">FITNESS </h1>
  <h1 className="relative text-4xl font-bold text-white z-10"> WHY CHOOSE US</h1>
    </div>
    
    </>
  );
};
    const NewsSletter = () =>{
        return(
            <>
            <div className=" h-40 w-full bg-black bpx-border">
            <div className="border flex justify-between border-white h-40 w-90 items-center  ">
              <div className="p-8">
                  <h2 className="text-white font-bold text-3xl">NEWSLETTER SUBSCRIBE</h2>
                  <p className="text-gray-400 text-xs mb-6 max-w-md mx-auto">
                  Stay motivated and informed with our fitness tips, exclusive workout plans, and special offers.
                   Sign up now to unlock the best version of yourself!"
                  </p>
              </div>
              <div className="flex mr-32">
                <input type="email" placeholder='Your Email Address' className="px-4 py-2 text-black rounded-l-lg focus-within:border-yellow-300" />
                <button className="bg-blue-600 text-white px-8 py-2 mx-3rounded-none hover:bg-yellow-300">Send</button>
              </div>
              </div>
            </div>
            </>
        );
    };
    const Gallery = () =>{
      return(
        <>
        <div className="bg-black py-20 sm:flex-col">
        <div className="relative text-center my-5 ">
  
      <h1 className="absolute inset-0 text-5xl font-serif  font-bold text-yellow-300 opacity-30 z-0">TRAINERS</h1>
      <h1 className="relative text-4xl font-bold text-white z-10"> Our Gallery</h1>
        </div>
        <div className="grid grid-cols-4 gap-0 my-8">
       <div className="flex justify-center items-center mb-4"> <Image src={gall1.src}  alt="Gallery-1"  width={300} height={300} className="hover:opacity-50 items-center" /></div> 
        <div className="flex justify-center items-center mb-4"><Image src={gall2.src}  alt="Gallery-2"  width={300} height={300} className="hover:opacity-50"/></div>
        <div className="flex justify-center items-center mb-4"><Image src={gall3.src}  alt="Gallery-3"  width={300} height={300} className="hover:opacity-50" /></div>
        <div className="flex justify-center items-center mb-4"><Image src={gall4.src}  alt="Gallery-4"  width={300} height={300} className="hover:opacity-50"/></div>
       <div className="flex justify-center items-center mb-4"> <Image src={gall5.src}  alt="Gallery-5"  width={300} height={300} className="hover:opacity-50"/></div>
        <div className="flex justify-center items-center mb-4"><Image src={gall6.src}  alt="Gallery-6"  width={300} height={300} className="hover:opacity-50"/></div>
        <div className="flex justify-center items-center mb-4"><Image src={gall7.src}  alt="Gallery-7"  width={300} height={300} className="hover:opacity-50"/></div>
        <div className="flex justify-center items-center mb-4"><Image src={gall8.src}  alt="Gallery-8"  width={300} height={300} className="hover:opacity-50"/></div>
        </div>

        </div>
        </>
      );

    };



const Home = () => {
    return (
        <>
        
        <div className="h-screen pt-6 bg-black ">
            <NavBarPage/>
            <About/>
            <WHY/>
            <Gallery/>
            <NewsSletter/>

        </div>
        </>
    );
};

export default Home;