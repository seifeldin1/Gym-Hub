import { useState } from "react";
import { DashHeader } from "@components/NavBar";
import { IoEyeSharp } from "react-icons/io5";
import { AiFillEyeInvisible } from "react-icons/ai";

const Report = () => {
  const [formData, setFormData] = useState({
    phoneNumber: "123-456-7890",
    password: "mypassword123",
    weight: "70", // Editable weight in kg
    height: "175", // Editable height in cm
    bmr: "1600", // Editable BMR
    membershipLength: "2 years", // Editable Membership Length
    endDate: "December 31, 2024", // Static End Date
    startDate: "January 1, 2023", // Calculated Start Date
    fees: "$500", // Static Membership Fees
    belongToCoach: "John Doe", // Static Coach Name
    membershipType: "Premium", // Static Membership Type
    nationalNumber: "123456789",
    gender: "Male",
    age: 30,
    email: "example@email.com", // Email field now editable
    employeesUnderSupervision: "15", // Static field
    branchName: "Main HQ" // Static field
  });

  const [showPassword, setShowPassword] = useState(false);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    alert("Changes saved!");
  };

  return (
    <>
      <DashHeader page_name="Personal Details" />
      <div className="flex flex-col items-center mt-5 space-y-6">
        <h2 className="text-5xl font-extrabold text-green-500">
        </h2>
        <div className="bg-white shadow-lg rounded-lg p-8 w-full max-w-6xl">
          <form onSubmit={handleSubmit} className="space-y-8">
            {/* Editable Section */}
            <div className="flex flex-col space-y-4">
              <h3 className="text-lg font-semibold text-gray-700 border-b pb-2">
                Editable Details
              </h3>
              <div className="grid grid-cols-3 gap-6">
                {/* Phone Number */}
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Phone Number
                  </label>
                  <input
                    type="text"
                    name="phoneNumber"
                    value={formData.phoneNumber}
                    onChange={handleChange}
                    className="text-black mt-1 block w-full border border-gray-300 rounded-lg shadow-sm focus:ring-green-500 focus:border-green-500 sm:text-sm px-4 py-2"
                  />
                </div>
                {/* Password with View Toggle */}
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Password
                  </label>
                  <div className="relative mt-1">
                    <input
                      type={showPassword ? "text" : "password"}
                      name="password"
                      value={formData.password}
                      onChange={handleChange}
                      className="text-black block w-full border border-gray-300 rounded-lg shadow-sm focus:ring-green-500 focus:border-green-500 sm:text-sm px-4 py-2"
                    />
                    <button
                      type="button"
                      onClick={togglePasswordVisibility}
                      className="absolute inset-y-0 right-0 flex items-center px-4 text-gray-500 hover:text-gray-700"
                    >
                      {showPassword ? <IoEyeSharp /> : <AiFillEyeInvisible />}
                    </button>
                  </div>
                </div>
                {/* Weight */}
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Weight (kg)
                  </label>
                  <input
                    type="number"
                    name="weight"
                    value={formData.weight}
                    onChange={handleChange}
                    className="text-black mt-1 block w-full border border-gray-300 rounded-lg shadow-sm focus:ring-green-500 focus:border-green-500 sm:text-sm px-4 py-2"
                  />
                </div>
                {/* Height */}
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Height (cm)
                  </label>
                  <input
                    type="number"
                    name="height"
                    value={formData.height}
                    onChange={handleChange}
                    className="text-black mt-1 block w-full border border-gray-300 rounded-lg shadow-sm focus:ring-green-500 focus:border-green-500 sm:text-sm px-4 py-2"
                  />
                </div>
                {/* BMR */}
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    BMR
                  </label>
                  <input
                    type="number"
                    name="bmr"
                    value={formData.bmr}
                    onChange={handleChange}
                    className="text-black mt-1 block w-full border border-gray-300 rounded-lg shadow-sm focus:ring-green-500 focus:border-green-500 sm:text-sm px-4 py-2"
                  />
                </div>
              </div>
            </div>

            {/* Static Details */}
            <div className="flex flex-col space-y-4">
              <h3 className="text-lg font-semibold text-gray-700 border-b pb-2">
                Static Details
              </h3>
              <div className="grid grid-cols-5 gap-6">
                {[
                  { label: "Start Date", value: formData.startDate },
                  { label: "End Date", value: formData.endDate },
                  { label: "Membership Length", value: formData.membershipLength },
                  { label: "Fees", value: formData.fees },
                  { label: "Belong to Coach", value: formData.belongToCoach },
                  { label: "Membership Type", value: formData.membershipType },
                  { label: "National Number", value: formData.nationalNumber },
                  { label: "Gender", value: formData.gender },
                  { label: "Age", value: formData.age },
                  { label: "Branch Name", value: formData.branchName },
                ].map((item, idx) => (
                  <div
                    key={idx}
                    className="bg-gray-100 rounded-lg p-4 shadow-sm"
                  >
                    <p className="text-sm font-medium text-gray-500">
                      {item.label}
                    </p>
                    <p className="text-lg font-semibold text-gray-700">
                      {item.value}
                    </p>
                  </div>
                ))}
              </div>
            </div>

            <div className="flex justify-center">
              <button
                type="submit"
                className="px-6 py-3 bg-green-500 text-white font-bold rounded-lg shadow-md hover:bg-transparent hover:text-green-500 transition duration-300"
              >
                Save Changes
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export default Report;
