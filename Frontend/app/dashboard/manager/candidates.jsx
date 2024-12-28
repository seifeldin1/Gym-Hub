import { useState } from "react";
import { DashHeader } from "@components/NavBar";

const candidates = [
    {
        First_Name: 'Ahmed',
        Last_Name: 'Mohamed',
        Age: '26',
        National_Number: '123456789',
        Phone_Number: '123-456-789',
        Email: 'example@gmail.com',
        Status: 'Status',
        Resume_Link: 'resume.com',
        Linkedin_Account:'linkedin.com',
    },
    {
        First_Name: 'Ahmed',
        Last_Name: 'Mohamed',
        Age: '26',
        National_Number: '123456789',
        Phone_Number: '123-456-789',
        Email: 'example@gmail.com',
        Status: 'Status',
        Resume_Link: 'resume.com',
        Linkedin_Account:'linkedin.com',
    },
    {
        First_Name: 'Ahmed',
        Last_Name: 'Mohamed',
        Age: '26',
        National_Number: '123456789',
        Phone_Number: '123-456-789',
        Email: 'example@gmail.com',
        Status: 'Status',
        Resume_Link: 'resume.com',
        Linkedin_Account:'linkedin.com',
    },
    {
        First_Name: 'Ahmed',
        Last_Name: 'Mohamed',
        Age: '26',
        National_Number: '123456789',
        Phone_Number: '123-456-789',
        Email: 'example@gmail.com',
        Status: 'Status',
        Resume_Link: 'resume.com',
        Linkedin_Account:'linkedin.com',
    },
    {
        First_Name: 'Ahmed',
        Last_Name: 'Mohamed',
        Age: '26',
        National_Number: '123456789',
        Phone_Number: '123-456-789',
        Email: 'example@gmail.com',
        Status: 'Status',
        Resume_Link: 'resume.com',
        Linkedin_Account:'linkedin.com',
    },
    {
        First_Name: 'Ahmed',
        Last_Name: 'Mohamed',
        Age: '26',
        National_Number: '123456789',
        Phone_Number: '123-456-789',
        Email: 'example@gmail.com',
        Status: 'Status',
        Resume_Link: 'resume.com',
        Linkedin_Account:'linkedin.com',
    },
    {
        First_Name: 'Ahmed',
        Last_Name: 'Mohamed',
        Age: '26',
        National_Number: '123456789',
        Phone_Number: '123-456-789',
        Email: 'example@gmail.com',
        Status: 'Status',
        Resume_Link: 'resume.com',
        Linkedin_Account:'linkedin.com',
    }
];

const ClientCard = ({ First_Name, Last_Name, Age, National_Number, Phone_Number, Email, Status, Resume_Link, Linkedin_Account}) => {
    return (
        <>
            <div className="p-5 bg-neutral-800 rounded-xl shadow-xl hover:shadow-2xl border-2 border-neutral-800 hover:border-green-500 my-4 w-[100%] h-[16rem]">
                <div className="flex justify-between items-center pb-2 border-b-4 border-green-300">
                    <div className='flex items-center gap-1'>
                        <h1 className="text-2xl font-bold text-green-500">{First_Name} {Last_Name}</h1>
                    </div>

                    <div className="flex text-[#0D0D0D] gap-1 flex-row text-center">
                        <div className="bg-white font-bold text-sm rounded-lg px-2">
                            {Age}
                        </div>
                        <div className="bg-white font-bold text-sm rounded-lg px-2">
                            {Phone_Number}
                        </div>
                    </div>
                </div>
                <div className="flex flex-row gap-4 mt-3">
                    <p className="text-orange-500 font-bold">National Number: </p>
                    <p className="text-white font-bold">{National_Number}</p>
                </div>
                <div className="flex flex-row gap-4 mt-3">
                    <p className="text-orange-500 font-bold">Email: </p>
                    <p className="text-white font-bold">{Email}</p>
                </div>
                <div className="flex flex-row gap-4 mt-3">
                    <p className="text-orange-500 font-bold">Status: </p>
                    <p className="text-white font-bold">{Status}</p>
                </div>
                <div className="flex flex-row mt-3">
                    <p className="text-orange-500 font-bold">Resume Link: </p>
                    <p className="text-white font-bold">{Resume_Link}</p>
                </div>
                <div className="flex flex-row gap-1 mt-3">
                    <p className="text-orange-500 font-bold">Linkedin Account: </p>
                    <p className="text-white font-bold">{Linkedin_Account}</p>
                </div>
            </div>
        </>
    );
};

const Candidates = () => {
    return (
        <>
            <DashHeader page_name="Candidates" />
            <div className='grid grid-cols-3 gap-3 px-6 max-h-[90%] overflow-y-auto customScroll'>

                {candidates.map((candidate, index) => (
                    <ClientCard
                        key={index}
                        First_Name={candidate.First_Name}
                        Last_Name={candidate.Last_Name}
                        Age={candidate.Age}
                        National_Number={candidate.National_Number}
                        Phone_Number={candidate.Phone_Number}
                        Email={candidate.Email}
                        Status={candidate.Status}
                        Resume_Link={candidate.Resume_Link}
                        Linkedin_Account={candidate.Linkedin_Account}
                    />
                ))}


            </div>

        </>
    );
};

export default Candidates;
