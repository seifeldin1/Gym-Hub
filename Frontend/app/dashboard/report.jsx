import { DashHeader } from '@components/NavBar';

export const Reports = () => {
    return (
        <div className='bg-[#131313] text-white h-full w-[90%] mx-auto rounded-xl'>
            <div className='w-[93%] mx-auto'>
                <h1 className='text-2xl py-4 flex items-center gap-2'>
                    <MdAnnouncement />
                    Annoncements
                </h1>
                <button className='bg-[#DBFF55] text-[#1E1E1E] border-2 border-[#DBFF55] hover:bg-transparent duration-150 hover:text-[#DBFF55] flex items-center gap-2 px-2 rounded-lg mx-auto'>
                    <IoMdAddCircle />
                    Add New Annoncement
                </button>
            </div>
            <div className="w-[90%] mx-auto py-2">
                {Annoncemnt.map((announcement, index) => (
                    <div key={index} className="bg-[#F7F7F7] text-black py-4 px-3 rounded-lg mb-4 ">
                        <div className="flex justify-between items-center pb-2">
                            <div>
                                <h2 className="text-xl font-bold">{announcement.title}</h2>
                                <h3 className="text-xs">{announcement.name}</h3>
                            </div>
                            <div className="flex text-white gap-1 flex-col">
                                <div className="bg-[#0D0D0D] text-xs rounded-lg px-2 text-center">
                                    {announcement.author_role}
                                </div>
                                <div className="bg-[#0D0D0D] text-xs rounded-lg px-2">
                                    {announcement.date_posted}
                                </div>
                            </div>
                        </div>
                        <div className="text-xs pt-2 border-t-2 border-[#DBFF55]">
                            {announcement.content}
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

const Report = () => {
    const Reports = [
        {
            name: "Ahmed",
            title: "lorem ipsum",
            content: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            date_posted: "22-4-2004",
            status: "lol",
            type: "mid",
        },
        {
            name: "Ahmed",
            title: "lorem ipsum",
            content: "Lorem ipsum dolor sit amet consectetur adipisicing elit. Aspernatur eum omnis fugit in vel? Magni, modi obcaecati tempora animi delectus magnam iure nemo repudiandae, illum, corrupti fuga non quis asperiores.",
            date_posted: "22-4-2004",
            status: "lol",
            type: "mid",
        },
    ];

    return (
        <>
            <DashHeader page_name="Reports" />
            <div className="w-[95%] mx-auto py-2 grid grid-cols-4 gap-4">
                {Reports.map((report, index) => (
                    <div key={index} className="bg-[#F7F7F7] text-black py-4 px-3 rounded-lg mb-4 ">
                        <div className="flex justify-between items-center pb-2">
                            <div>
                                <h2 className="text-xl font-bold">{report.title}</h2>
                                <h3 className="text-xs">{report.name}</h3>
                            </div>
                            <div className="flex text-white gap-1 flex-col text-center">
                                <div className="bg-[#0D0D0D] text-xs rounded-lg px-2">
                                    {report.status}
                                </div>
                                <div className="bg-[#0D0D0D] text-xs rounded-lg px-2">
                                    {report.date_posted}
                                </div>
                                <div className="bg-[#0D0D0D] text-xs rounded-lg px-2">
                                    {report.status}
                                </div>
                            </div>
                        </div>
                        <div className="text-xs pt-2 border-t-2 border-[#DBFF55]">
                            {report.content}
                        </div>
                    </div>
                ))}
            </div>
        </>
    );
};

export default Report;
