import { useState, useEffect } from "react";
import { DashHeader } from "@components/NavBar";
import { IoEyeSharp } from "react-icons/io5";
import { AiFillEyeInvisible } from "react-icons/ai";
import axiosInstance from "../../axios"; // Import the axios instance

const Report = () => {
  const [formData, setFormData] = useState({
    phoneNumber: "",
    password: "",
    salary: "",
    penalties: "",
    bonuses: "",
    contractLength: "",
    endDate: "",
    nationalNumber: "",
    gender: "",
    age: "",
    email: "", // Email field now editable
  });

  const [showPassword, setShowPassword] = useState(false);

  const FetchReports = async () => {
    try {
      const response = await axiosInstance.get("/Coach", {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`
        },
      });

      const ownerData = response.data[0]; // Take the first owner for simplicity
      // Map the fetched data to formData state
      console.log(ownerData)
      setFormData({
        phoneNumber: ownerData.phone_Number || '', // Default to an empty string if no value
        password: '', // Leave password blank or set a default if needed
        email: ownerData.email || '',
        salary: ownerData.salary || '',
        penalties: ownerData.penalties || '',
        bonuses: ownerData.bonuses || '',
        contractLength: ownerData.contract_Length || '', // Changed to match the fetched data field
        endDate: ownerData.fire_Date || '', // Assuming fire_Date is the end date
        nationalNumber: ownerData.national_Number || '',
        gender: ownerData.gender || '',
        age: ownerData.age || '',
        user_ID: ownerData.user_ID || '', // Ensure user_ID is part of the formData
        sharePercentage: ownerData.share_Percentage * 100 || '', // Assuming this needs to be multiplied by 100
        establishedBranches: ownerData.established_branches || '', // If applicable, include this
        username: ownerData.username || '', // Add if you want to show the username as well
        shiftStart: ownerData.shift_Start || '', // Assuming you want to show shift start
        shiftEnd: ownerData.shift_Ends || '', // Assuming you want to show shift end time
        speciality: ownerData.speciality || '', // Add if you want to include specialty
        status: ownerData.status || '', // If you want to include the coach status
      });

    } catch (error) {
      console.log("Error fetching data:", error);
    }
  };

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axiosInstance.put(
        "/Coach/UpdateCoach",
        {
          User_ID: formData.user_ID,
          phoneNumber: formData.phoneNumber,
          password: formData.password,
          email: formData.email,
          salary: formData.salary,
          penalties: formData.penalties,
          bonuses: formData.bonuses,
          contractLength: formData.contractLength,
          endDate: formData.endDate,
          nationalNumber: formData.nationalNumber,
          gender: formData.gender,
          age: formData.age,
        },
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }
      );

      console.log(response.data)
      // Handle success (e.g., show success message or update the UI)
      alert("Changes saved successfully!");
      console.log(response.data);
    } catch (error) {
      // Handle errors (e.g., display an error message to the user)
      console.error("Error saving changes:", error);
      alert("Failed to save changes. Please try again.");
    }
  };

  useEffect(() => {
    FetchReports()
  }, [])

  return (
    <>
      <DashHeader page_name="Personal Details" />
      <div className="flex flex-col items-center mt-5 space-y-6">
        <h2 className="text-5xl font-extrabold text-green-500">
          Edit Your Details
        </h2>
        <div className="bg-white shadow-lg rounded-lg p-8 w-full max-w-5xl">
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
                      aria-label={showPassword ? "Hide password" : "Show password"}
                      className="absolute inset-y-0 right-0 flex items-center px-4 text-gray-500 hover:text-gray-700"
                    >
                      {showPassword ? (
                        <IoEyeSharp className="text-lg" />
                      ) : (
                        <AiFillEyeInvisible className="text-lg" />
                      )}
                    </button>
                  </div>
                </div>
                {/* Email */}
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Email
                  </label>
                  <input
                    type="email"
                    name="email"
                    value={formData.email}
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
              <div className="grid grid-cols-4 gap-6">
                {[
                  { label: "Salary", value: formData.salary },
                  { label: "Penalties", value: formData.penalties },
                  { label: "Bonuses", value: formData.bonuses },
                  { label: "Contract Length", value: formData.contractLength },
                  { label: "End Date", value: formData.endDate },
                  { label: "National Number", value: formData.nationalNumber },
                  { label: "Gender", value: formData.gender },
                  { label: "Age", value: formData.age },
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
