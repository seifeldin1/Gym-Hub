import { useState } from "react";
import { DashHeader } from "@components/NavBar";

const Report = () => {
    const [isModalOpen, setIsModalOpen] = useState(false);

    const Reports = [
        {
            name: "Ahmed Al-Nona",
            progress_summary: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            goals_achieved: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            challenges_faced: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            next_steps: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            name: "Ahmed Al-Nona",
            progress_summary: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            goals_achieved: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            challenges_faced: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            next_steps: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            name: "Ahmed Al-Nona",
            progress_summary: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            goals_achieved: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            challenges_faced: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            next_steps: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            name: "Ahmed Al-Nona",
            progress_summary: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            goals_achieved: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            challenges_faced: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            next_steps: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            name: "Ahmed Al-Nona",
            progress_summary: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            goals_achieved: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            challenges_faced: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            next_steps: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            name: "Ahmed Al-Nona",
            progress_summary: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            goals_achieved: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            challenges_faced: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            next_steps: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            name: "Ahmed Al-Nona",
            progress_summary: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            goals_achieved: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            challenges_faced: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            next_steps: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            name: "Ahmed Al-Nona",
            progress_summary: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            goals_achieved: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            challenges_faced: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            next_steps: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            name: "Ahmed Al-Nona",
            progress_summary: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            goals_achieved: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            challenges_faced: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            next_steps: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            name: "Ahmed Al-Nona",
            progress_summary: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            goals_achieved: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            challenges_faced: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            next_steps: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        }
    ];

    return (
        <>
            <DashHeader page_name="Reports" />

            {/* Modal (Pop-Out) */}
            {isModalOpen && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
                    <div className="bg-white p-6 rounded-lg shadow-lg w-[60%] h-[65%]">
                        <div className="flex flex-row justify-center items-center mb-3">
                            <h2 className="text-2xl font-bold text-black mr-10">Choose A Client</h2>
                            <select
                                id="experience_years"
                                name="Experience_Years"
                                className="w-[15rem] text-center py-3 border-none rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent font-bold text-green-600 bg-transparent"
                                defaultValue=""
                                required
                            >
                                <option value="" disabled>
                                    Select Client
                                </option>
                                <option value="0">Ahmed</option>
                                <option value="1">Nona</option>
                                <option value="2">Salim</option>
                                <option value="3">Fathy</option>
                                <option value="4">Omar</option>
                                <option value="5">Seif</option>
                                <option value="6">Dakroury</option>
                            </select>
                        </div>
                        <p className="text-gray-700"> </p>
                        <form action="">
                            <div>
                                <label htmlFor="progress_summary" className="block text-lg font-semibold text-gray-600">Progress Summary <span className="text-red-500">*</span></label>
                                <input type="text" id="progress_summary" name="Candidate_Name" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Summary About Your Client" required />
                            </div>
                            <div>
                                <label htmlFor="goals_achieved" className="block text-lg font-semibold text-gray-600">Goals Achieved <span className="text-red-500">*</span></label>
                                <input type="text" id="goals_achieved" name="Candidate_Name" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Goals Your Client Have Completed" required />
                            </div>
                            <div>
                                <label htmlFor="challenges_faced" className="block text-lg font-semibold text-gray-600">Challenges Faced <span className="text-red-500">*</span></label>
                                <input type="text" id="challenges_faced" name="Candidate_Name" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Problems Your Client Faced" required />
                            </div>
                            <div>
                                <label htmlFor="next_steps" className="block text-lg font-semibold text-gray-600">Next Steps <span className="text-red-500">*</span></label>
                                <input type="text" id="next_steps" name="Candidate_Name" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-8 text-black" placeholder="What's Next For Your Client" required />
                            </div>
                            <div className="flex justify-between gap-3">
                                <button
                                    className="bg-green-600 border-2 border-green-600 text-white font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-green-600 transition duration-400"
                                    type="submit"
                                >
                                    Add Report
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

            <div className="w-[95%] py-2 grid grid-cols-2 gap-3 max-h-[84%] overflow-y-auto customScroll">
                {Reports.map((report, index) => (
                    <div
                        key={index}
                        className="bg-neutral-800 text-black py-4 px-3 rounded-lg mb-2 ml-6 mr-2 shadow-xl hover:shadow-2xl border-2 border-neutral-800 hover:border-green-500"
                    >
                        <div className="flex justify-between items-center pb-2">
                            <div className="text-green-500">
                                <h2 className="text-3xl font-bold">{report.name}</h2>
                            </div>
                            <div className="flex text-[#0D0D0D] gap-1 flex-col text-center">
                                <div className="bg-white font-bold text-sm rounded-lg px-2">
                                    {report.date_posted}
                                </div>
                            </div>
                        </div>
                        <div className="pt-2 border-t-2 border-[#DBFF55] grid grid-cols-1">
                            <div>
                                <p className="font-bold text-lg text-orange-500">
                                    Progress Summary
                                </p>
                                <p className="text-sm text-white mb-3">
                                    {report.progress_summary}
                                </p>
                            </div>
                            <div>
                                <p className="font-bold text-lg text-orange-500">Goals Achieved</p>
                                <p className="text-sm text-white mb-3">{report.goals_achieved}</p>
                            </div>
                            <div>
                                <p className="font-bold text-lg text-orange-500">
                                    Challenges Faced
                                </p>
                                <p className="text-sm text-white mb-3">
                                    {report.challenges_faced}
                                </p>
                            </div>
                            <div>
                                <p className="font-bold text-lg text-orange-500">Next Steps</p>
                                <p className="text-sm text-white">{report.next_steps}</p>
                            </div>
                        </div>
                    </div>
                ))}
            </div>

            {/* Button to trigger the modal */}
            <div className="flex justify-center my-4">
                <button
                    className="bg-green-600 border-2 border-green-600 text-black font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-white transition duration-400"
                    onClick={() => setIsModalOpen(true)}
                >
                    Add New Report
                </button>
            </div>
        </>
    );
};

export default Report;
