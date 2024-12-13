'use client'
import NavBar from '@components/NavBar';
import { DashHeader } from '@components/NavBar';
import { NextMeet } from './home';
import { NumStat } from './Statistics';
import { CashflowChart } from './Statistics';
import { RecentReports } from './home';

const RecentActivity = () => {
    const activities = [
        { time: 'Today, 16:05', user: 'Jamie Smith', action: 'updated account settings' },
        { time: 'Today, 13:05', user: 'Alex Johnson', action: 'logged in' },
        { time: 'Today, 02:05', user: 'Morgan Lee', action: 'added a new savings goal for vacation' },
        { time: 'Yesterday, 21:05', user: 'Taylor Green', action: 'reviewed recent transactions' },
        { time: 'Yesterday, 09:05', user: 'Wilson Baptista', action: 'transferred funds to emergency fund' },
    ];
    return (
        <div className="bg-gray-100 rounded-lg shadow-md p-4">
            <h2 className="text-lg font-semibold mb-2">Recent Activity</h2>
            <ul className="space-y-2">
                {activities.map((activity, index) => (
                    <li key={index} className="flex items-center">
                        <span className="text-gray-500 mr-2">{activity.time}</span>
                        <span className="text-sm">{activity.user} {activity.action}</span>
                    </li>
                ))}
            </ul>
        </div>
    );
};


const Dashboard = () => {
    return (
        <div className='flex bg-[#FEED02]/35'>
            <NavBar />
            <div className='flex-1 flex-row rounded-bl-2xl rounded-tl-2xl bg-white'>
                <DashHeader />
                <div className='flex'>
                    <div className='w-[25%] h-screen flex flex-col gap-3'>
                        <NextMeet />
                        <NumStat />
                    </div>
                    <div className='w-[50%] h-fit flex gap-2 flex-col'>
                        <div className='bg-[#FEED02]/40 rounded-2xl px-5 py-3 mx-auto w-full'>
                            <CashflowChart />
                        </div>
                        <RecentReports />
                    </div>
                    <div className='w-[25%] h-fit'>
                        <RecentActivity />
                    </div>
                </div>
            </div>
        </div>
    );
};


export default Dashboard;
