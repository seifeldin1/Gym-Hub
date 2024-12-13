"use client"
import styles from "@styles/dashboard.module.css"
import { BsCalendar3 } from "react-icons/bs";
import { CiClock2 } from "react-icons/ci";
import Image from 'next/image';

export const NextMeet = () => {
    return (
        <div className={styles.MeetingDiv}>
            <div className="w-[90%] my-4 mx-auto flex flex-col gap-3">
                <h2 className="text-lg">
                    Next Meeeting
                </h2>
                <div className="flex gap-2 items-center text-xs">
                    <div className="flex gap-1">
                        <BsCalendar3 size={15} />
                        11 Nov
                    </div>
                    <div className="flex gap-1">
                        <CiClock2 size={15} />
                        11:00 am
                    </div>
                </div>
                <div className="mx-auto">
                    <Image
                        src="/assets/images/Dashboard/NextMeet.jpg" // Path to your image in the public folder
                        alt="Meeting Image"
                        width={350}
                        height={150}
                        className="rounded-xl"
                    />
                </div>
            </div>
        </div>
    )
}

export const RecentReports = () => {
    const transactions = [
        {
            name: 'Electricity Bill',
            date: '2028-03-01 04:28:48',
            amount: '$295.81',
            note: 'Payment for monthly electricity bill',
            status: 'Failed',
        },
        {
            name: 'Weekly Groceries',
            date: '2028-03-04 04:28:48',
            amount: '$204.07',
            note: 'Groceries shopping at local supermarket',
            status: 'Completed',
        },
        {
            name: 'Movie Night',
            date: '2028-02-27 04:28:48',
            amount: '$97.84',
            note: 'Tickets for movies and snacks',
            status: 'Pending',
        },
    ];

    return (
        <div className="overflow-x-auto bg-white rounded-lg shadow-md">
            <div className="flex justify-between items-center p-4">
                <h2 className="text-xl font-semibold">Recent Transactions</h2>
                <div className="flex space-x-2">
                    <button className="px-4 py-2 bg-green-100 text-green-800 rounded-full">This Month</button>
                    <button className="px-4 py-2 bg-gray-100 text-gray-800 rounded-full">
                        <svg xmlns="http://www.w3.org/2000/svg" className="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 9l-7 7-7-7" />
                        </svg>
                    </button>
                </div>
            </div>
            <table className="min-w-full text-sm text-left text-gray-500">
                <thead className="bg-gray-100">
                    <tr>
                        <th className="px-6 py-3 font-semibold text-gray-700">Transaction Name</th>
                        <th className="px-6 py-3 font-semibold text-gray-700">Date & Time</th>
                        <th className="px-6 py-3 font-semibold text-gray-700">Amount</th>
                        <th className="px-6 py-3 font-semibold text-gray-700">Note</th>
                        <th className="px-6 py-3 font-semibold text-gray-700">Status</th>
                    </tr>
                </thead>
                <tbody>
                    {transactions.map((transaction, index) => (
                        <tr key={index} className={`border-t ${index % 2 === 0 ? 'bg-gray-50' : 'bg-white'}`}>
                            <td className="px-6 py-4">{transaction.name}</td>
                            <td className="px-6 py-4">{transaction.date}</td>
                            <td className="px-6 py-4">{transaction.amount}</td>
                            <td className="px-6 py-4">{transaction.note}</td>
                            <td className="px-6 py-4">
                                <span
                                    className={`px-3 py-1 rounded-full text-sm font-medium ${transaction.status === 'Completed' ? 'bg-green-100 text-green-800' : transaction.status === 'Pending' ? 'bg-yellow-100 text-yellow-800' : 'bg-red-100 text-red-800'}`}
                                >
                                    {transaction.status}
                                </span>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

