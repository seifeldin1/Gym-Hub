import { useState } from "react";
import { DashHeader } from "@components/NavBar";

const Report = () => {
    const [isModalOpen, setIsModalOpen] = useState(false);

    const Reports = [
        {
            Title: "Report Title",
            Type: "Type",
            Status: "Status",
            content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            Title: "Report Title",
            Type: "Type",
            Status: "Status",
            content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            Title: "Report Title",
            Type: "Type",
            Status: "Status",
            content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            Title: "Report Title",
            Type: "Type",
            Status: "Status",
            content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            Title: "Report Title",
            Type: "Type",
            Status: "Status",
            content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            Title: "Report Title",
            Type: "Type",
            Status: "Status",
            content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            Title: "Report Title",
            Type: "Type",
            Status: "Status",
            content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            Title: "Report Title",
            Type: "Type",
            Status: "Status",
            content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            Title: "Report Title",
            Type: "Type",
            Status: "Status",
            content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        },
        {
            Title: "Report Title",
            Type: "Type",
            Status: "Status",
            content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas justo augue, pulvinar id elit ac, imperdiet iaculis nunc. Cras nec.",
            date_posted: "22-4-2004",
        }
    ];

    return (
        <>
            <DashHeader page_name="Reports"/>

            {/* Modal (Pop-Out) */}
            {isModalOpen && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
                    <div className="bg-white p-6 rounded-lg shadow-lg w-[60%] h-[53%]">
                        <div className="flex flex-row justify-center items-center mb-3">
                            <h2 className="text-2xl font-bold text-black mr-10">Choose A Client</h2>
                        </div>
                        <form action="">
                            <div className="flex flex-row gap-32">
                                <div>
                                    <label htmlFor="progress_summary" className="block text-lg font-semibold text-gray-600">Title <span className="text-red-500">*</span></label>
                                    <input type="text" id="progress_summary" name="Candidate_Name" className="mt-2 w-[150%] px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="What is Report About?" required />
                                </div>
                                <div>
                                    <label htmlFor="goals_achieved" className="block text-lg font-semibold text-gray-600">Type <span className="text-red-500">*</span></label>
                                    <input type="text" id="goals_achieved" name="Candidate_Name" className="mt-2 w-[150%] px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Daily, Weekly, Monthly" required />
                                </div>
                                <div>
                                    <label htmlFor="challenges_faced" className="block text-lg font-semibold text-gray-600">Status</label>
                                    <input disabled type="text" id="challenges_faced" name="Candidate_Name" className="mt-2 w-[150%] px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-4 text-black" placeholder="Sent"/>
                                </div>

                            </div>
                            <div>
                                <label htmlFor="next_steps" className="block text-lg font-semibold text-gray-600">Content <span className="text-red-500">*</span></label>
                                <input type="text" id="next_steps" name="Candidate_Name" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent mb-8 text-black h-[10rem]" placeholder="Your Report" required />
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
                                <h2 className="text-3xl font-bold">{report.Title}</h2>
                            </div>
                            <div className="flex text-[#0D0D0D] gap-1 flex-row text-center">
                                <div className="bg-white font-bold text-sm rounded-lg px-2">
                                    {report.Type}
                                </div>
                                <div className="bg-white font-bold text-sm rounded-lg px-2">
                                    {report.Status}
                                </div>
                                <div className="bg-white font-bold text-sm rounded-lg px-2">
                                    {report.date_posted}
                                </div>
                            </div>
                        </div>
                        <div className="pt-2 border-t-2 border-[#DBFF55] grid grid-cols-1">
                            <p className="text-sm text-white mb-3">
                                {report.content}
                            </p>
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
