import FullCalendar from '@fullcalendar/react';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import { DashHeader } from '@components/NavBar';
import axiosInstance from '../../axios';
import { useEffect, useState, useRef, useCallback } from "react";

const Calendar = () => {
    const [calEvents, setCalEvents] = useState([]);
    const [eventsLoaded, setEventsLoaded] = useState(false);
    const calendarRef = useRef(null);

    const fetchEvents = useCallback(async () => {
        if (eventsLoaded) return;

        try {
            const [responseEvents, responseHolidays] = await Promise.all([
                axiosInstance.get("/Events", {
                    headers: { Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmYWRpLmlicmFoaW0iLCJyb2xlIjoiQ29hY2giLCJqdGkiOiJlNDM5ODhiMS0xNmU4LTRhN2QtOGFkNC1mNzAxNjUxNGRiZWIiLCJuYmYiOjE3MzUzMjExOTYsImV4cCI6MTczNTQwNzU5NiwiaWF0IjoxNzM1MzIxMTk2fQ.b3e7akvDFMwQ-P8h9C5ret-soZRG79eMLxYZGw_pMOI` },
                }),
                axiosInstance.get("/Holiday", {
                    headers: { Authorization: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmYWRpLmlicmFoaW0iLCJyb2xlIjoiQ29hY2giLCJqdGkiOiJlNDM5ODhiMS0xNmU4LTRhN2QtOGFkNC1mNzAxNjUxNGRiZWIiLCJuYmYiOjE3MzUzMjExOTYsImV4cCI6MTczNTQwNzU5NiwiaWF0IjoxNzM1MzIxMTk2fQ.b3e7akvDFMwQ-P8h9C5ret-soZRG79eMLxYZGw_pMOI` },
                }),
            ]);

            setCalEvents([...responseEvents.data, ...responseHolidays.data]);
            setEventsLoaded(true);
        } catch (error) {
            console.error(error);
        }
    }, [eventsLoaded]);

    useEffect(() => {
        fetchEvents();
    }, [fetchEvents]);

    const formattedEvents = calEvents.map(event => ({
        title: event.title,
        start: new Date(event.start_Date).toISOString(),
        end: event.end_Date ? new Date(event.end_Date).toISOString() : null,
        description: event.description,
        location: event.location,
        id: event.event_ID,
    }));

    return (
        <>
            <DashHeader page_name="Calendar" />
            <div className="mx-auto" style={{ width: '90%', height: '760px' }}>
                <FullCalendar
                    ref={calendarRef}
                    plugins={[dayGridPlugin, timeGridPlugin]}
                    initialView="dayGridMonth"
                    height="100%"
                    events={formattedEvents}
                    windowResize={() => {
                        if (calendarRef.current) {
                            const calendarApi = calendarRef.current.getApi();
                            calendarApi.updateSize();
                        }
                    }}
                />
            </div>
        </>
    );
};

export default Calendar;