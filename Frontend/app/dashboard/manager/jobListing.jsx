import { useState , useEffect } from "react";
import { DashHeader } from "@components/NavBar";
import axiosInstance from "@app/axios";
const ClientCard = ({
    title,
    description,
    date_posted,
    skills_required,
    experience_years_required,
    deadline,
    location,
  }) => {
    // Handle array or string for skills
    const skillsDisplay =
      Array.isArray(skills_required) && skills_required.length
        ? skills_required.join(", ")
        : skills_required || "No Skills Specified";

    return (
      <div className="p-5 bg-neutral-800 rounded-xl shadow-xl hover:shadow-2xl border-2 border-neutral-800 hover:border-green-500 my-4 w-[100%] h-[20rem]">
        <div className="flex justify-between items-center pb-2 border-b-4 border-green-300">
          <div className="flex items-center gap-1">
            <h1 className="text-2xl font-bold text-green-500">{title || "N/A"}</h1>
          </div>
          <div className="flex text-[#0D0D0D] gap-1 flex-row text-center">
            <div className="bg-white font-bold text-sm rounded-lg px-2">{deadline || "No Deadline"}</div>
            <div className="bg-white font-bold text-sm rounded-lg px-2">{location || "No Location"}</div>
            <div className="bg-white font-bold text-sm rounded-lg px-2">{date_posted || "No Date Posted"}</div>
          </div>
        </div>
        <div className="flex flex-row gap-4 mt-3">
          <p className="text-orange-500 font-bold">Experience Years Required: </p>
          <p className="text-white font-bold mb-1">{experience_years_required || "Not Specified"}</p>
        </div>
        <div className="flex flex-col gap-1 mt-3">
          <p className="text-orange-500 font-bold">Skills Required: </p>
          <p className="text-white font-bold mb-1">{skillsDisplay}</p>
        </div>
        <div className="flex flex-col gap-1 mt-3">
          <p className="text-orange-500 font-bold">Description: </p>
          <p className="text-white font-bold">{description || "No Description Provided"}</p>
        </div>
        <button className="bg-red-600 border-2 border-red-600 text-white font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-red-600 transition duration-400 mt-2" onClick={() => setIsModalOpen(false)}>
            Delete Job Post
        </button>
      </div>
    );
  };
const JobListing = () => {
    const [jobs, setJobs] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [newJob, setNewJob] = useState({
        title: "",
        experience_years_required: "",
        deadline: "", // Date part
        location: "",
        skills_required: "",
        description: "",
    });
    const [deadlineTime, setDeadlineTime] = useState(""); // Time part

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNewJob({ ...newJob, [name]: value });
    };

    const handleTimeChange = (e) => {
        setDeadlineTime(e.target.value);
    };

    const fetchJobs = async () => {
        try {
            const response = await axiosInstance.get('/JobPost');
            console.log("API Response:", response.data);
            setJobs(response.data); // Assuming API returns an array of jobs
        } catch (error) {
            console.error("Error fetching jobs:", error);
        }
    };

    const addJob = async (e) => {
        e.preventDefault();
        try {
            const token = localStorage.getItem("token"); // Retrieve token from localStorage
            const deadline = `${newJob.deadline}T${deadlineTime}`; // Combine date and time into ISO 8601 format

            const response = await axiosInstance.post(
                "/JobPost", 
                {
                    Branch_Posted_ID: 1, // Assuming a placeholder value for Branch_Posted_ID
                    Title: newJob.title,
                    Experience_Years_Required: newJob.experience_years_required,
                    Deadline: deadline,
                    Location: newJob.location,
                    Skills_Required: newJob.skills_required,
                    Description: newJob.description,
                    Date_Posted: new Date().toISOString().split("T")[0], // Use today's date
                },
                {
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: `Bearer ${token}`,
                    },
                }
            );

            if (response.status === 201 || response.status === 200) {
                setJobs([...jobs, response.data]); // Add the new job to the state
                setIsModalOpen(false); // Close the modal
                setNewJob({
                    title: "",
                    experience_years_required: "",
                    deadline: "",
                    location: "",
                    skills_required: "",
                    description: "",
                });
                setDeadlineTime(""); // Reset time
            } else {
                console.error("Error adding job:", response.statusText);
            }
        } catch (error) {
            console.error("Error adding job:", error.response || error.message);
        }
    };

    // Fetch jobs when the component loads
    useEffect(() => {
        fetchJobs();
    }, []);

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
                        <form onSubmit={addJob}>
                            <div className="grid grid-cols-5 gap-4">
                                <div>
                                    <label htmlFor="title" className="block text-lg font-semibold text-gray-600">Title <span className="text-red-500">*</span></label>
                                    <input type="text" id="title" name="title" value={newJob.title} onChange={handleInputChange} className="w-52 mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Job Title" required />
                                </div>
                                <div>
                                    <label htmlFor="experience_years_required" className="block text-lg font-semibold text-gray-600">Experience Years <span className="text-red-500">*</span></label>
                                    <input type="number" id="experience_years_required" name="experience_years_required" value={newJob.experience_years_required} onChange={handleInputChange} className="w-52 mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Years of Experience" required />
                                </div>
                                <div>
                                    <label htmlFor="deadline" className="block text-lg font-semibold text-gray-600">Date Deadline</label>
                                    <input type="date" id="deadline" name="deadline" value={newJob.deadline} onChange={handleInputChange} className="w-52 mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" />
                                </div>
                                <div>
                                    <label htmlFor="time_deadline" className="block text-lg font-semibold text-gray-600">Time Deadline</label>
                                    <input type="time" id="time_deadline" name="time_deadline" value={deadlineTime} onChange={handleTimeChange} className="w-52 mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" />
                                </div>
                                <div>
                                    <label htmlFor="location" className="block text-lg font-semibold text-gray-600">Location</label>
                                    <input type="text" id="location" name="location" value={newJob.location} onChange={handleInputChange} className="w-52 mt-2 px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Workplace Location" />
                                </div>
                            </div>
                            <div>
                                <label htmlFor="skills_required" className="block text-lg font-semibold text-gray-600">Skills Required <span className="text-red-500">*</span></label>
                                <input type="text" id="skills_required" name="skills_required" value={newJob.skills_required} onChange={handleInputChange} className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-8 text-black h-[5rem]" placeholder="Required Skills" required />
                            </div>
                            <div>
                                <label htmlFor="description" className="block text-lg font-semibold text-gray-600">Description <span className="text-red-500">*</span></label>
                                <textarea id="description" name="description" value={newJob.description} onChange={handleInputChange} className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-8 text-black h-[5rem]" placeholder="Job Description" required />
                            </div>
                            <div className="flex justify-between gap-3">
                                <button className="bg-green-600 border-2 border-green-600 text-white font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-green-600 transition duration-400" type="submit">
                                    Add Job
                                </button>
                                <button className="bg-red-600 border-2 border-red-600 text-white font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-red-600 transition duration-400" onClick={() => setIsModalOpen(false)}>
                                    Close
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
            <div className="grid grid-cols-3 gap-3 px-6 max-h-[80%] overflow-y-auto customScroll">
                {jobs.map((job, index) => (
                    <ClientCard
                        key={index}
                        title={job.title}
                        description={job.description}
                        date_posted={job.date_posted}
                        skills_required={job.skills_required}
                        experience_years_required={job.experience_years_required}
                        deadline={job.deadline}
                        location={job.location}
                    />
                ))}
            </div>
            <div className="flex justify-center my-4">
                <button className="bg-green-600 border-2 border-green-600 text-black font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-white transition duration-400" onClick={() => setIsModalOpen(true)}>
                    Add New Job
                </button>
            </div>
        </>
    );
};

export default JobListing;