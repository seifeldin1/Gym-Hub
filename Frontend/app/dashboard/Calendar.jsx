import React, { useEffect, useRef } from 'react';
import dynamic from 'next/dynamic';
import 'tui-calendar/dist/tui-calendar.css'; // Import calendar styles

// Dynamically import the calendar to avoid SSR issues
const TuiCalendar = dynamic(() => import('tui-calendar'), {
    ssr: false, // Disable SSR for this component
});

const Calendar = () => {
    const calendarRef = useRef(null); // Reference to hold the calendar container

    useEffect(() => {
        // Wait for the calendar to be mounted in the DOM
        const calendar = new TuiCalendar(calendarRef.current, {
            defaultView: 'month', // Set the initial view of the calendar
            taskView: true,
            scheduleView: ['time', 'allday'],
        });

        // Example: Adding an event to the calendar
        calendar.createSchedules([
            {
                id: '1',
                calendarId: '1',
                title: 'Sample Event',
                category: 'time',
                start: '2024-12-04T10:00:00',
                end: '2024-12-04T12:00:00',
            },
        ]);

        // Return cleanup function to destroy the calendar when component unmounts
        return () => {
            calendar.destroy();
        };
    }, []);

    return <div ref={calendarRef} style={{ height: '500px' }}></div>; // Calendar container
};

export default Calendar;
