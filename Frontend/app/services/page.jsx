import Image from 'next/image';
import NavBar from "@components/NavBar";
import services1 from '@public/images/fitness.png';
import NavBarPage from "@components/NavBarPage";
const Serv = () =>{
return(
    <>
    <div className="flex items-center justify-center mt-20">
        <h1 className="font-bold text-yellow-200 italic align-middle text-9xl ">FIT</h1>
        <div className="bg-black"><Image src={services1.src} alt="services 1" width={400} height={400} className="bg-black  "/></div>
        <h1 className="font-bold text-yellow-200 italic align-middle text-9xl ">SS</h1>
        
    </div>
    </>
);
};
const OpenClass= () =>{
    return(
        <>
       <div className="mt-16 h-full">
       <div className="relative text-center h-80  py-3 bg-black ease-in duration-300 ">
  
    <h1 className="absolute inset-0 text-5xl font-serif  font-bold text-yellow-300 opacity-30 z-0">BODY BUILDING </h1>
    <h1 className="relative text-4xl font-bold text-white z-10"> OPEN CLASS FITNESS</h1>
    </div>
    <div className="bg-black h-full grid grid-cols-3 pl-6">
        <div className="border-t-8 border-yellow-400 flex flex-col items-center justify-center w-3/4 h-3/4 bg-gray-700 p-8">
            <h1 className="text-white font-bold font-2xl">CARDIO FITNESS</h1>
            <p className="text-white">Boost your endurance and burn calories with our cardio classes! Perfect for all fitness levels, 
                these sessions improve heart health, build stamina, 
                and energize your day.</p>
            <button className="border-b-4 border-white pt-12 text-white ease-in duration-150 hover:border-yellow-300 hover:text-yellow-300 hover:scale-110">Open Class</button>
        </div>
        <div className="border-t-8 border-yellow-400 flex flex-col items-center justify-center w-3/4 h-3/4 bg-gray-700 p-8">
            <h1 className="text-white font-bold font-2xl">CORPORATE FITNESS</h1>
            <p className="text-white">Boost productivity and well-being with our Corporate Fitness programs! 
                Designed to energize teams, improve health, 
                and foster a positive work culture
                 </p>
            <button className="border-b-4 border-white pt-12 text-white ease-in duration-150 hover:border-yellow-300 hover:text-yellow-300 hover:scale-110">Open Class</button>
        </div>
        <div className="border-t-8 border-yellow-400 flex flex-col items-center justify-center w-3/4 h-3/4 bg-gray-700 p-8">
            <h1 className="text-white font-bold font-2xl">GROUP TRAINING</h1>
            <p className="text-white">Achieve your goals together with Group Training! Fun, motivating, 
                and designed for all fitness levels to help you stay inspired and succeed.</p>
            <button className="border-b-4 border-white pt-12 text-white ease-in duration-150 hover:border-yellow-300 hover:text-yellow-300 hover:scale-110">Open Class</button>
        </div>
        <div className="border-t-8 border-yellow-400 flex flex-col items-center justify-center w-3/4 h-3/4 bg-gray-700 p-8">
            <h1 className="text-white font-bold font-2xl">PERSONAL TRAINING</h1>
            <p className="text-white">Get personalized guidance with Personal Training!
                 Tailored workouts and expert coaching to help you reach your fitness goals faster.</p>
            <button className="border-b-4 border-white pt-12 text-white ease-in duration-150 hover:border-yellow-300 hover:text-yellow-300 hover:scale-110">Open Class</button>
        </div>
        <div className="border-t-8 border-yellow-400 flex flex-col items-center justify-center w-3/4 h-3/4 bg-gray-700 p-8">
            <h1 className="text-white font-bold font-2xl">STRENGTH TRAINING</h1>
            <p className="text-white">Build muscle and boost power with Strength Training! Perfect for all levels,
                 designed to enhance strength, endurance, and confidence.</p>
            <button className="border-b-4 border-white pt-12 text-white ease-in duration-150 hover:border-yellow-300 hover:text-yellow-300 hover:scale-110">Open Class</button>
        </div>
        <div className="border-t-8 border-yellow-400 flex flex-col items-center justify-center w-3/4 h-3/4 bg-gray-700 p-8">
            <h1 className="text-white font-bold font-2xl">SPORTS CONDITIONING</h1>
            <p className="text-white">Elevate your performance with Sports Conditioning! Tailored training to improve strength, speed,
                 and agility for athletes of all levels.</p>
            <button className="border-b-4 border-white pt-12 text-white ease-in duration-150 hover:border-yellow-300 hover:text-yellow-300 hover:scale-110">Open Class</button>
        </div>
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
            <Serv/>
            <OpenClass/>
            
            

        </div>
        </>
    );
};

export default Home;