import { useState } from "react";
import { DashHeader } from "@components/NavBar";

const jobs = [
    {
        title: 'John Doe',
        description: 'I am A nigga destroyed by seif mohamed al molakeb b tutu',
        date_posted: '17 April 2024',
        skills_required: 'Fit On-Time',
        experience_years_required: '3',
        deadline: '24 Jan 2024',
        location: 'Giza, Oula',
    },
    {
        title: 'John Doe',
        description: 'I am A nigga destroyed by seif mohamed al molakeb b tutu',
        date_posted: '17 April 2024',
        skills_required: 'Fit On-Time',
        experience_years_required: '3',
        deadline: '24 Jan 2024',
        location: 'Giza, Oula',
    },
    {
        title: 'John Doe',
        description: 'I am A nigga destroyed by seif mohamed al molakeb b tutu',
        date_posted: '17 April 2024',
        skills_required: 'Fit On-Time',
        experience_years_required: '3',
        deadline: '24 Jan 2024',
        location: 'Giza, Oula',
    },
    {
        title: 'John Doe',
        description: 'I am A nigga destroyed by seif mohamed al molakeb b tutu',
        date_posted: '17 April 2024',
        skills_required: 'Fit On-Time',
        experience_years_required: '3',
        deadline: '24 Jan 2024',
        location: 'Giza, Oula',
    }
];

const ClientCard = ({ title, description, date_posted, skills_required, experience_years_required, deadline, location }) => {
    return (
        <>
            <div className="p-5 bg-neutral-800 rounded-xl shadow-xl hover:shadow-2xl border-2 border-neutral-800 hover:border-green-500 my-4 w-[100%] h-[20rem]">
                <div className="flex justify-between items-center pb-2 border-b-4 border-green-300">
                    <div className='flex items-center gap-1'>
                        <h1 className="text-2xl font-bold text-green-500">{title}</h1>
                    </div>

                    <div className="flex text-[#0D0D0D] gap-1 flex-row text-center">
                        <div className="bg-white font-bold text-sm rounded-lg px-2">
                            {deadline}
                        </div>
                        <div className="bg-white font-bold text-sm rounded-lg px-2">
                            {location}
                        </div>
                        <div className="bg-white font-bold text-sm rounded-lg px-2">
                            {date_posted}
                        </div>
                    </div>
                </div>
                <div className="flex flex-row gap-4 mt-3">
                    <p className="text-orange-500 font-bold">Experience Years Required: </p>
                    <p className="text-white font-bold mb-1">{experience_years_required}</p>
                </div>
                <div className="flex flex-col gap-1 mt-3">
                    <p className="text-orange-500 font-bold">Skills_Required: </p>
                    <p className="text-white font-bold mb-1">{skills_required}</p>
                </div>
                <div className="flex flex-col gap-1 mt-3">
                    <p className="text-orange-500 font-bold">Description: </p>
                    <p className="text-white font-bold">{description}</p>
                </div>
            </div>
        </>
    );
};

const JobListing = () => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    return (
        <>
            <DashHeader page_name="Job Listing" />
            {/* Modal (Pop-Out) */}
            {isModalOpen && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
                    <div className="bg-white p-6 rounded-lg shadow-lg w-[60%] h-fit">
                        <div className="flex flex-row justify-center items-center mb-3">
                            <h2 className="text-2xl font-bold text-black mr-10">Add A New Job Title</h2>
                        </div>
                        <form action="">
                            <div className="grid grid-cols-4">
                                <div>
                                    <label htmlFor="progress_summary" className="block text-lg font-semibold text-gray-600">Title <span className="text-red-500">*</span></label>
                                    <input type="text" id="progress_summary" name="Candidate_Name" className="mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="What is Report About?" required />
                                </div>
                                <div>
                                    <label htmlFor="goals_achieved" className="block text-lg font-semibold text-gray-600">Experience Years <span className="text-red-500">*</span></label>
                                    <input type="number" id="goals_achieved" name="Candidate_Name" className="mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Years of Experience" required />
                                </div>
                                <div>
                                    <label htmlFor="challenges_faced" className="block text-lg font-semibold text-gray-600">Deadline</label>
                                    <input type="date" id="challenges_faced" name="Candidate_Name" className="mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Last date to allow submission" />
                                </div>
                                <div>
                                    <label htmlFor="challenges_faced" className="block text-lg font-semibold text-gray-600">Location</label>
                                    <input type="text" id="challenges_faced" name="Candidate_Name" className="mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Workplace Location" />
                                </div>

                            </div>
                            <div>
                                <label htmlFor="next_steps" className="block text-lg font-semibold text-gray-600">Skills Required <span className="text-red-500">*</span></label>
                                <input type="text" id="next_steps" name="Candidate_Name" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-8 text-black h-[5rem]" placeholder="Skills Needed by a Candidate" required />
                            </div>
                            <div>
                                <label htmlFor="next_steps" className="block text-lg font-semibold text-gray-600">Description <span className="text-red-500">*</span></label>
                                <input type="text" id="next_steps" name="Candidate_Name" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-8 text-black h-[5rem]" placeholder="Describe the job title" required />
                            </div>
                            <div className="flex justify-between gap-3">
                                <button
                                    className="bg-green-600 border-2 border-green-600 text-white font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-green-600 transition duration-400"
                                    type="submit"
                                >
                                    Add Job
                                </button>
                                <button
                                    className="bg-red-600 border-2 border-red-600 text-white font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-red-600 transition duration-400"
                                    onClick={() => setIsModalOpen(false)}
                                >
                                    Close
                                </button>
                            </div>
                        </form>

                    </div>
                </div>
            )}


            <div className='grid grid-cols-3 gap-3 px-6 max-h-[80%] overflow-y-auto customScroll'>

                {jobs.map((jobs, index) => (
                    <ClientCard
                        key={index}
                        title={jobs.title}
                        description={jobs.description}
                        date_posted={jobs.date_posted}
                        skills_required={jobs.skills_required}
                        experience_years_required={jobs.experience_years_required}
                        deadline={jobs.deadline}
                        location={jobs.location}
                    />
                ))}


            </div>
            {/* Button to trigger the modal */}
            <div className="flex justify-center my-4">
                <button
                    className="bg-green-600 border-2 border-green-600 text-black font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-white transition duration-400"
                    onClick={() => setIsModalOpen(true)}
                >
                    Add New Job
                </button>
            </div>

        </>
    );
};

export default JobListing;
