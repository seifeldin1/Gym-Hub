import NavBarPage from "@components/NavBarPage";
import styles from "@styles/navbarpage.module.css" //it has style for animation of background
import BottomBar from '@components/BottomBar';


const Form = () => {
    return (
        <>
            <div className="min-h-screen bg-transparent">
                <section className="flex items-center justify-center min-h-screen bg-transparent mb-10">
                    <div className="w-full max-w-3xl px-6 py-12 bg-white rounded-3xl shadow-2xl">
                        <h2 className="text-4xl font-extrabold text-center text-gray-700 mb-8">Job Application</h2>
                        <p className="text-center text-gray-500 mb-10">
                            Fill in your details below to apply for the position. Fields marked with <span className="text-red-500">*</span> are mandatory.
                        </p>
                        <form id="applicationForm" className="space-y-6">

                            <div>
                                <label htmlFor="candidate_name" className="block text-sm font-semibold text-gray-600">Candidate Name <span className="text-red-500">*</span></label>
                                <input type="text" id="candidate_name" name="Candidate_Name" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" placeholder="John Doe" required />
                            </div>

                            <div>
                                <label htmlFor="email" className="block text-sm font-semibold text-gray-600">Email <span className="text-red-500">*</span></label>
                                <input type="email" id="email" name="Email" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" placeholder="johndoe@example.com" required />
                            </div>

                            <div>
                                <label htmlFor="phone" className="block text-sm font-semibold text-gray-600">Phone <span className="text-red-500">*</span></label>
                                <input type="tel" id="phone" name="Phone" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" placeholder="+123 456 7890" required />
                            </div>

                            <div>
                                <label htmlFor="position_applied" className="block text-sm font-semibold text-gray-600">Position Applied <span className="text-red-500">*</span></label>
                                <select id="position_applied" name="Position_Applied" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" required>
                                    <option value="" disabled selected>Select a position</option>
                                    <option value="Coach">Coach</option>
                                    <option value="Manager">Manager</option>
                                </select>
                            </div>

                            <div>
                                <label htmlFor="status" className="block text-sm font-semibold text-gray-600">Status <span className="text-red-500">*</span></label>
                                <input type="text" id="status" name="Status" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" placeholder="e.g., Full-time, Part-time" required />
                            </div>

                            <div>
                                <label htmlFor="expected_salary" className="block text-sm font-semibold text-gray-600">Expected Salary <span className="text-red-500">*</span></label>
                                <input type="number" id="expected_salary" name="Expected_Salary" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" placeholder="e.g., 50000" required />
                            </div>

                            <div>
                                <label htmlFor="experience_years" className="block text-sm font-semibold text-gray-600">Experience (Years) <span className="text-red-500">*</span></label>
                                <select
                                    id="experience_years"
                                    name="Experience_Years"
                                    className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                                    defaultValue=""
                                    required
                                >
                                    <option value="" disabled>
                                        Select experience
                                    </option>
                                    <option value="0">0</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                    <option value="5+">5+</option>
                                </select>


                            </div>

                            <div>
                                <label htmlFor="skills" className="block text-sm font-semibold text-gray-600">Skills <span className="text-red-500">*</span></label>
                                <textarea id="skills" name="Skills" rows="4" className="mt-2 w-full px-4 py-3 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent" placeholder="List your skills here..." required></textarea>
                            </div>

                            <div className="text-center">
                                <button type="submit" className="w-full py-4 px-6 bg-orange-400 text-white font-semibold rounded-lg shadow-lg hover:bg-transparent hover:text-orange-400 duration-75">
                                    Submit Application
                                </button>
                            </div>
                        </form>
                    </div>
                </section>

            </div>
        </>
    );

};


const Home = () => {
    return (
        <>
            <div className={`${styles.AnimBackground}`}>
                <NavBarPage />
                <Form />
                <BottomBar />
            </div>
        </>
    );
};

export default Home;