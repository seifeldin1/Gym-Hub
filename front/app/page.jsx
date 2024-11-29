'use client'
import React, { useEffect, useState } from 'react';
import NavBar from '@components/NavBar';

const Home = () => {
    const [isClient, setIsClient] = useState(false);

    useEffect(() => {
        setIsClient(true);
    }, []);

    return (
        <div>
            {isClient ? (
                <div className='bg-[#F5F5F5] w-full h-screen'>
                    <div className='w-[92%] mx-auto pt-4'>
                        <NavBar />
                        
                    </div>
                </div>
            ) : null}
        </div>
    );
};

export default Home;
