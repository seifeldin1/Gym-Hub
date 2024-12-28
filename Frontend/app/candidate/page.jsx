"use client"
import NavBarPage from "@components/NavBarPage";
import styles from "@styles/navbarpage.module.css"; //it has style for animation of background
import BottomBar from '@components/BottomBar';
import { useEffect, useState } from "react";
import axiosInstance from '../axios'; // Import the default export

import React from "react";



const JobPostCard = ({
    Title,
    Date_Posted,
    Branch_Posted_ID,
    Description,
    Skills_Required,
    Experience_Years_Required,
    Deadline,
    Location,
}) => {
    return (
        <div className="max-w-sm rounded-lg overflow-hidden shadow-lg bg-white border border-gray-200 p-4">
            <h2 className="text-xl font-bold text-gray-800 mb-2">{Title || "Untitled Job"}</h2>
            <p className="text-sm text-gray-500 mb-4">
                Posted on: {Date_Posted ? new Date(Date_Posted).toLocaleDateString() : "N/A"}
            </p>
            <p className="text-gray-700 mb-2">
                <span className="font-semibold">Branch ID:</span> {Branch_Posted_ID || "N/A"}
            </p>
            <p className="text-gray-700 mb-2">
                <span className="font-semibold">Description:</span> {Description || "No description available."}
            </p>
            <p className="text-gray-700 mb-2">
                <span className="font-semibold">Skills Required:</span> {Skills_Required || "None specified"}
            </p>
            <p className="text-gray-700 mb-2">
                <span className="font-semibold">Experience Required:</span> {Experience_Years_Required || "None"} years
            </p>
            <p className="text-gray-700 mb-2">
                <span className="font-semibold">Deadline:</span>{" "}
                {Deadline ? new Date(Deadline).toLocaleDateString() : "No deadline specified"}
            </p>
            <p className="text-gray-700 mb-2">
                <span className="font-semibold">Location:</span> {Location || "Not specified"}
            </p>
        </div>
    );
};



const JobPosts = () => {
    const [jobPosts, setJobPosts] = useState([]);

    const FetchJobPosts = async () => {
        try {
            const response = await axiosInstance.get("/JobPost", {
                headers: {
                    Authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJoZW5rc2hoaCIsInJvbGUiOiJDb2FjaCIsImp0aSI6IjVkMmZjZDkxLTJjYjEtNGE2ZC04ZDE3LThlNGI0MTA5MjVlMiIsIm5iZiI6MTczNTMzMjcwNCwiZXhwIjoxNzM1NDE5MTA0LCJpYXQiOjE3MzUzMzI3MDR9.EZVAumq_mhec5Z63FAU5rbX_KmuLOQm7yiSjAdnz5io'
                },
            });

            // Map response data to match the component's structure
            console.log(response.data);
            const formattedJobPosts = response.data.map((job) => ({
                id: job.post_ID,
                branchId: job.branch_Posted_ID,
                title: job.title,
                description: job.description,
                skills: job.skills_Required,
                experienceYears: job.experience_Years_Required,
                postedDate: job.date_Posted.split("T")[0], // Extract date
                deadline: job.deadline.split("T")[0], // Extract date
                location: job.location,
            }));
            setJobPosts(formattedJobPosts);
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
        FetchJobPosts();
    }, []);

    
    const [availableSlots, setAvailableSlots] = useState([]);

    const FetchAvailableTimes = async () => {
        try {
            const response = await axiosInstance.get("/Interview", {
                headers: {
                    Authorization: "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJoZW5rc2hoaCIsInJvbGUiOiJDb2FjaCIsImp0aSI6IjVkMmZjZDkxLTJjYjEtNGE2ZC04ZDE3LThlNGI0MTA5MjVlMiIsIm5iZiI6MTczNTMzMjcwNCwiZXhwIjoxNzM1NDE5MTA0LCJpYXQiOjE3MzUzMzI3MDR9.EZVAumq_mhec5Z63FAU5rbX_KmuLOQm7yiSjAdnz5io",
                },
            });

            console.log("Available Times Response:", response.data);

            // Format and set available slots
            const formattedSlots = response.data.map((slot) => ({
                dateTime: slot.free_Interview_Date, // ISO 8601 format
            }));

            setAvailableSlots(formattedSlots);
        } catch (error) {
            if (error.response) {
                console.error("Error Response:", error.response.data);
            } else {
                console.error(`Error: ${error.message}`);
            }
        }
    };

    useEffect(() => {
        FetchAvailableTimes();
    }, []);

    


    return (
        <>
    <div className="grid grid-cols-3 gap-y-6 mx-auto my-7 ml-12">
    {jobPosts.map((job, index) => (
        <JobPostCard
            key={index}
            Post_ID={job.id} // Mapped from job.post_ID
            Title={job.title} // Mapped from job.title
            Branch_Posted_ID={job.branchId} // Mapped from job.branch_Posted_ID
            Date_Posted={job.postedDate} // Mapped from job.date_Posted
            Description={job.description} // Mapped from job.description
            Experience_Years_Required={job.experienceYears} // Mapped from job.experience_Years_Required
            Deadline={job.deadline} // Mapped from job.deadline
            Location={job.location} // Mapped from job.location
        />
    ))}
</div>


            <div className="min-h-screen bg-transparent">
                <section className="flex items-center justify-center min-h-screen bg-transparent mb-10">
                    <div className="w-full max-w-3xl px-6 py-12 bg-white rounded-3xl shadow-2xl">
                        <h2 className="text-4xl font-extrabold text-center text-gray-700 mb-8">Job Application</h2>
                        <p className="text-center text-gray-500 mb-10">
                            Fill in your details below to apply for the position. Fields marked with <span className="text-red-500">*</span> are mandatory.
                        </p>
                        <form id="applicationForm" className="space-y-6">
                            <div>
                                <label htmlFor="candidate_name" className="block text-sm font-semibold text-gray-600">Candidate Name <span className="text-red-500">*</span></label>
                                <input type="text" id="candidate_name" name="Candidate_Name" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" placeholder="John Doe" required />
                            </div>

                            <div>
                                <label htmlFor="email" className="block text-sm font-semibold text-gray-600">Email <span className="text-red-500">*</span></label>
                                <input type="email" id="email" name="Email" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" placeholder="johndoe@example.com" required />
                            </div>

                            <div>
                                <label htmlFor="phone" className="block text-sm font-semibold text-gray-600">Phone <span className="text-red-500">*</span></label>
                                <input type="tel" id="phone" name="Phone" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" placeholder="+123 456 7890" required />
                            </div>

                            <div>
                                <label htmlFor="position_applied" className="block text-sm font-semibold text-gray-600">Position Applied <span className="text-red-500">*</span></label>
                                <select id="position_applied" name="Position_Applied" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" required>
                                    <option value="" disabled selected>Select a position</option>
                                    <option value="Coach">Coach</option>
                                    <option value="Manager">Manager</option>
                                </select>
                            </div>

                            <div>
                                <label htmlFor="status" className="block text-sm font-semibold text-gray-600">Status <span className="text-red-500">*</span></label>
                                <input type="text" id="status" name="Status" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" placeholder="e.g., Full-time, Part-time" required />
                            </div>

                            <div>
                                <label htmlFor="expected_salary" className="block text-sm font-semibold text-gray-600">Expected Salary <span className="text-red-500">*</span></label>
                                <input type="number" id="expected_salary" name="Expected_Salary" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" placeholder="e.g., 50000" required />
                            </div>

                            <div>
                                <label htmlFor="experience_years" className="block text-sm font-semibold text-gray-600">Experience (Years) <span className="text-red-500">*</span></label>
                                <select
                                    id="experience_years"
                                    name="Experience_Years"
                                    className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                                    defaultValue=""
                                    required
                                >
                                    <option value="" disabled>
                                        Select experience
                                    </option>
                                    <option value="0">0</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                    <option value="5+">5+</option>
                                </select>
                            </div>

                            <div>
                                <label htmlFor="skills" className="block text-sm font-semibold text-gray-600">Skills <span className="text-red-500">*</span></label>
                                <textarea id="skills" name="Skills" rows="4" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" placeholder="List your skills here..." required></textarea>
                            </div>
                            <div>
            <label
                htmlFor="interview_datetime"
                className="block text-sm font-semibold text-gray-600"
            >
                Interview Date and Time <span className="text-red-500">*</span>
            </label>
            <div className="mt-2">
                {/* Dropdown to select available slots */}
                <select
                    id="interview_datetime"
                    name="Interview_DateTime"
                    className="w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    required
                >
                    <option value="" disabled>
                        Select a Date and Time
                    </option>
                    {availableSlots.map((slot) => (
                        <option key={slot.id} value={slot.dateTime}>
                            {new Date(slot.dateTime).toLocaleString()} 
                        </option>
                    ))}
                </select>
            </div>
        </div>


                            <div className="text-center">
                                <button type="submit" className="w-full py-4 px-6 bg-orange-400 text-white font-semibold rounded-lg shadow-lg hover:bg-transparent hover:text-orange-400 duration-75">
                                    Submit Application
                                </button>
                            </div>
                        </form>
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
                <NavBarPage />
                <JobPosts/>
                <BottomBar />
            </div>
        </>
    );
};

export default Home;
