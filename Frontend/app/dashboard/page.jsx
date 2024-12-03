import NavBar from '@components/NavBar';
import Home from '@app/dashboard/home';
const Dashboard = () => {
    return (
        <div>
            <div className='bg-[#F5F5F5] w-full h-screen'>
                <div className='w-[95%] mx-auto pt-4'>
                    <NavBar />
                </div>
                <div className='flex w-[95%] mx-auto mt-4 h-[88%] gap-8 rounded-lg'>
                    <Home />
                </div>
            </div>
        </div>
    );
};

export default Dashboard;
