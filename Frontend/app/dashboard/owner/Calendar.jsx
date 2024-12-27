import FullCalendar from '@fullcalendar/react';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import { DashHeader } from '@components/NavBar';
import axiosInstance from '../../axios';
import { useEffect, useState, useRef } from "react";

const Calendar = () => {
    const [calEvents, setCalEvents] = useState([]);
    const [eventsLoaded, setEventsLoaded] = useState(false);  // New state to track data loading
    const calendarRef = useRef(null);  // Create a reference to FullCalendar

    useEffect(() => {
        // Prevent fetching events again if already loaded
        if (eventsLoaded) return;

        const fetchEvents = async () => {
            try {
                const responseEvents = await axiosInstance.get("/Events");
                const responseHolidays = await axiosInstance.get("/Holiday");

                // Log the response data to inspect the format
                console.log('Response from Events:', responseEvents.data);
                console.log('Response from Holidays:', responseHolidays.data);
                setCalEvents([...responseEvents.data, ...responseHolidays.data]);
                setEventsLoaded(true);
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

        fetchEvents();
    }, [eventsLoaded]);  // Depend on eventsLoaded state

    // Format events for FullCalendar (ensure start and end dates are valid)
    const formattedEvents = calEvents.map(event => ({
        title: event.title,
        start: event.start_Date,  // Ensure the start date format is correct
        end: event.end_Date,      // Ensure the end date format is correct
        description: event.description,
        location: event.location,
        id: event.event_ID,
    }));

    // Conditionally render FullCalendar once events are loaded
    return (
        <>
            <DashHeader page_name="Calendar" />
            <div className="mx-auto" style={{ width: '90%', height: '760px' }}>
                <FullCalendar
                    ref={calendarRef}  // Attach the ref to FullCalendar
                    plugins={[dayGridPlugin, timeGridPlugin]}
                    initialView="dayGridMonth"
                    height="100%"  
                    events={formattedEvents}  // Pass the formatted events to FullCalendar
                    windowResize={() => {
                        if (calendarRef.current) {
                            const calendarApi = calendarRef.current.getApi();
                            calendarApi.updateSize();
                        }
                    }}
                    key={JSON.stringify(formattedEvents)}  // Force re-render when events change
                />
            </div>
        </>
    );
};

export default Calendar;
