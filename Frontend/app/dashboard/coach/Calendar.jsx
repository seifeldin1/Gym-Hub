import FullCalendar from '@fullcalendar/react';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import { DashHeader } from '@components/NavBar';

const Calendar = () => {
    return (
        <>
            <DashHeader page_name="Calendar" />
            <div className="mx-auto" style={{ width: '90%', height: '760px' }}>
                <FullCalendar
                    plugins={[dayGridPlugin, timeGridPlugin]}
                    initialView="dayGridMonth"
                    height="100%"  // Ensure FullCalendar takes 100% of its parent container
                    windowResize={() => {
                        // This callback will adjust the height dynamically when the window is resized
                        const calendarApi = this.calendarRef.getApi();
                        calendarApi.updateSize();
                    }}
                />
            </div>
        </>
    );
};

export default Calendar;
