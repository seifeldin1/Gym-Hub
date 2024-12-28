import { DashHeader } from "@components/NavBar";
import { useEffect, useState } from "react";
import axiosInstance from '../../axios'; // Import the default export

// Capitalized component name
const CandidateCard = ({
    First_Name,
    Last_Name,
    Age,
    National_Number,
    Phone_Number,
    Email,
    Resume_Link,
    Linkedin_Account,
    Status,
}) => {
    return (
        <div className="p-5 bg-neutral-800 rounded-xl shadow-xl hover:shadow-2xl border-2 border-neutral-800 hover:border-green-500 my-4 w-[100%] h-[16rem]">
            <div className="flex justify-between items-center pb-2 border-b-4 border-green-300">
                <div className='flex items-center gap-1'>
                    <h1 className="text-2xl font-bold text-green-500">{First_Name || "N/A"} {Last_Name || "N/A"}</h1>
                </div>

                <div className="flex text-[#0D0D0D] gap-1 flex-row text-center">
                    <div className="bg-white font-bold text-sm rounded-lg px-2">
                        {Age || "N/A"}
                    </div>
                    <div className="bg-white font-bold text-sm rounded-lg px-2">
                        {Phone_Number || "N/A"}
                    </div>
                </div>
            </div>
            <div className="flex flex-row gap-4 mt-3">
                <p className="text-orange-500 font-bold">National Number: </p>
                <p className="text-white font-bold">{National_Number || "N/A"}</p>
            </div>
            <div className="flex flex-row gap-4 mt-3">
                <p className="text-orange-500 font-bold">Email: </p>
                <p className="text-white font-bold">{Email || "N/A"}</p>
            </div>
            <div className="flex flex-row gap-4 mt-3">
                <p className="text-orange-500 font-bold">Status: </p>
                <p className="text-white font-bold">{Status || "N/A"}</p>
            </div>
            <div className="flex flex-row mt-3">
                <p className="text-orange-500 font-bold">Resume Link: </p>
                <p className="text-white font-bold">{Resume_Link || "N/A"}</p>
            </div>
            <div className="flex flex-row gap-1 mt-3">
                <p className="text-orange-500 font-bold">Linkedin Account: </p>
                <p className="text-white font-bold">{Linkedin_Account || "N/A"}</p>
            </div>
        </div>
    );
};

// Main component where hooks are used
const Candidates = () => {
    const [candidates, setCandidates] = useState([]);

    const FetchCandidates = async () => {
        try {
            const response = await axiosInstance.get("/JobPost", {
                headers: {
                    Authorization: 'Bearer <your-token-here>',
                },
            });

            // Format response data
            const formattedCandidates = response.data.map((candidate) => ({
                First_Name: job.first_name, // Adjust according to actual field names
                Last_Name: job.last_name,
                Age: job.age,
                National_Number: job.national_number,
                Phone_Number: job.phone_number,
                Email: job.email,
                Resume_Link: job.resume_link,
                Linkedin_Account: job.linkedin_account,
                Status: job.status,  // Assuming 'status' is part of the response
            }));
            setCandidates(formattedCandidates);
        } catch (error) {
            if (error.response) {
                console.log(error.response.data);
                console.log(error.response.status);
                console.log(error.response.headers);
            } else {
                console.log(`Error: ${error.message}`);
            }
        }
    };

    useEffect(() => {
        FetchCandidates();
    }, []);

    return (
        <>
            <DashHeader page_name="Candidates" />
            <div className='grid grid-cols-3 gap-3 px-6 max-h-[90%] overflow-y-auto customScroll'>
                {candidates.map((candidate, index) => (
                    <CandidateCard
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
