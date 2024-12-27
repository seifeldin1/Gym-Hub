import { useState } from 'react';
import FullCalendar from '@fullcalendar/react';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import { DashHeader } from '@components/NavBar';

const Calendar = () => {
    const [isModalOpen, setIsModalOpen] = useState(false);

    return (
        <>
            <DashHeader page_name="Calendar" />
            
            <div className="mx-auto" style={{ width: '90%', height: '690px' }}>
                <FullCalendar
                    plugins={[dayGridPlugin, timeGridPlugin]}
                    initialView="dayGridMonth"
                    height="100%"  // Ensure FullCalendar takes 100% of its parent container
                    windowResize={() => {
                        // This callback will adjust the height dynamically when the window is resized
                        const calendarApi = this.calendarRef?.getApi();
                        if (calendarApi) {
                            calendarApi.updateSize();
                        }
                    }}
                />
            </div>
            <div className="flex justify-center my-4">
                <button
                    className="bg-green-600 border-2 border-green-600 text-black font-bold py-2 px-4 rounded-full hover:bg-transparent hover:text-white transition duration-400"
                    onClick={() => setIsModalOpen(true)}
                >
                    Add New Interview
                </button>
            </div>

            {isModalOpen && (
                <div className="fixed inset-0 flex items-center justify-center z-50">

                    {/* Modal Content */}
                    <div className="bg-white rounded-lg shadow-lg p-6 w-1/3">
                        <h2 className="text-lg font-bold mb-4 text-black">Add New Interview</h2>
                        <form>
                            <div className="mb-4">
                                <label className="block text-gray-700 font-medium mb-2">Interview Title</label>
                                <input 
                                    type="text" 
                                    placeholder="Enter interview title"
                                    className="w-full border border-gray-300 rounded-lg px-3 py-2 focus:ring-green-500 focus:border-green-500"
                                />
                            </div>
                            <div className="mb-4">
                                <label className="block text-gray-700 font-medium mb-2">Date</label>
                                <input 
                                    type="date" 
                                    className="w-full border border-gray-300 rounded-lg px-3 py-2 focus:ring-green-500 focus:border-green-500"
                                />
                            </div>
                            <div className="mb-4">
                                <label className="block text-gray-700 font-medium mb-2">Time</label>
                                <input 
                                    type="time" 
                                    className="w-full border border-gray-300 rounded-lg px-3 py-2 focus:ring-green-500 focus:border-green-500"
                                />
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
        </>
    );
};

export default Calendar;
