import { DashHeader } from '@components/NavBar';

const Analytics = () => {
    const financial = [
        {
            name: "client",
            number: 15
        },
        {
            name: "client",
            number: 15
        },
    ];
    return (
        <>
            <DashHeader page_name="Analytics" />
            <div className='w-[95%] mx-auto'>
                <h1 className='text-3xl py-2'>Financial</h1>
                <div className="flex w-full">
                    <div className="bg-white p-4 rounded-md shadow-md text-black">
                        <div className="flex justify-center items-center mb-2">
                            <div className="bg-yellow-200 rounded-full p-3">
                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="w-6 h-6">
                                    <path stroke-linecap="round" stroke-linejoin="round" d="M11.25 11.25l.041-.02a.75.75 0 011.063.852c.684.621 1.744 1.049 3.01 1.419s2.375.329 3.31.024.903-.614 1.237-1.101A2.251 2.251 0 0018 9.75v-1.5a2.25 2.25 0 00-2.25-2.25H5.25a2.25 2.25 0 00-2.25 2.25v1.5a2.25 2.25 0 002.25 2.25h11.25zm0 0l.041-.02a.75.75 0 00-1.063.852C10.566 11.87 9.426 12.3 8.1 12.681s-1.673.329-2.61-.024-.903-.614-1.237-1.101A2.251 2.251 0 013 9.75v-1.5a2.25 2.25 0 00-2.25-2.25H.75a2.25 2.25 0 00-2.25 2.25v1.5a2.25 2.25 0 002.25 2.25h11.25z" />
                                </svg>
                            </div>
                        </div>
                        <div className="text-center">
                            <h2 className="text-2xl font-semibold">6k+</h2>
                            <p>Meter Walked</p>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};

export default Analytics;
