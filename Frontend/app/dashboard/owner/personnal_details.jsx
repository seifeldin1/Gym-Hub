import { useState, useEffect } from "react";
import { DashHeader } from "@components/NavBar";
import { IoEyeSharp } from "react-icons/io5";
import { AiFillEyeInvisible } from "react-icons/ai";
import axiosInstance from '../../axios';

const Report = () => {
  const [formData, setFormData] = useState({
    phoneNumber: '',
    password: '',
    email: '',
    salary: '',
    sharePercentage: '',
    establishedBranches: '',
    nationalNumber: '',
    gender: '',
    age: '',
    user_ID: '',  // Add user_ID to formData for consistency
  });

  const [initialData, setInitialData] = useState(null); // To store initial fetched data

  const FetchReports = async () => {
    try {
      const response = await axiosInstance.get("/Owner", {
        headers: {
          Authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjcmlzdGlhbm8ucm9uYWxkbyIsInJvbGUiOiJPd25lciIsImp0aSI6ImVlODY4ZGFiLTVkZDgtNDU5MC1hOTBhLTMyODNhZTEyNGFhYyIsIm5iZiI6MTczNTMyNzU1NSwiZXhwIjoxNzM1NDEzOTU1LCJpYXQiOjE3MzUzMjc1NTV9.V4NMHF7mWvlpC5U-EnyZ-wKDCC_C40XZbOPZ-fHcC9A'
        },
      });

      const ownerData = response.data[0]; // Take the first owner for simplicity
      setInitialData(ownerData); // Set the initial data when fetched

      // Map the fetched data to formData state
      setFormData({
        phoneNumber: ownerData.phone_Number,
        password: '', // You can leave password blank or set a default if needed
        email: ownerData.email,
        salary: ownerData.salary || '',
        sharePercentage: ownerData.share_Percentage * 100 || '',
        establishedBranches: ownerData.established_branches || '',
        nationalNumber: ownerData.national_Number,
        gender: ownerData.gender,
        age: ownerData.age,
        user_ID: ownerData.user_ID, // Ensure user_ID is part of the formData
      });
    } catch (error) {
      console.log("Error fetching data:", error);
    }
  };

  useEffect(() => {
    FetchReports();
  }, []);

  const [showPassword, setShowPassword] = useState(false);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!initialData) {
      alert("Initial data not loaded.");
      return;
    }

    // Prepare the updated data only with changed fields
    const updatedData = { user_ID: formData.user_ID }; // Always include user_ID

    // Check if the password has been changed
    if (formData.password && formData.password !== initialData.password) {
      updatedData.passwordHashed = formData.password; // Use 'passwordHashed' instead of 'password'
    }

    // Compare each field with the initial data and add it to updatedData if changed
    Object.keys(formData).forEach((key) => {
      // Exclude the password field from being checked here since it's handled above
      if (key !== 'password' && formData[key] !== initialData[key]) {
        // Match the property names from the response (e.g., 'phoneNumber' -> 'phone_Number')
        const mappedKey = {
          phoneNumber: 'phone_Number',
          salary: 'salary',
          sharePercentage: 'share_Percentage',
          establishedBranches: 'established_branches',
          nationalNumber: 'national_Number',
          gender: 'gender',
          age: 'age',
          email: 'email',
        }[key] || key;

        updatedData[mappedKey] = formData[key];
      }
    });

    console.log(updatedData);
    try {
      const response = await axiosInstance.put("/Owner", updatedData, {
        headers: {
          Authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjcmlzdGlhbm8ucm9uYWxkbyIsInJvbGUiOiJPd25lciIsImp0aSI6ImVlODY4ZGFiLTVkZDgtNDU5MC1hOTBhLTMyODNhZTEyNGFhYyIsIm5iZiI6MTczNTMyNzU1NSwiZXhwIjoxNzM1NDEzOTU1LCJpYXQiOjE3MzUzMjc1NTV9.V4NMHF7mWvlpC5U-EnyZ-wKDCC_C40XZbOPZ-fHcC9A',
        },
      });

      if (response.status === 200) {
        alert("Changes saved successfully!");
      } else {
        alert("Failed to save changes.");
      }
    } catch (error) {
      console.log("Error saving changes:", error);
      alert("An error occurred while saving changes.");
    }
  };


  return (
    <>
      <DashHeader page_name="Personal Details" />
      <div className="flex flex-col items-center mt-5 space-y-6">
        <h2 className="text-5xl font-extrabold text-green-500">
          Edit Your Details
        </h2>
        <div className="bg-white shadow-lg rounded-lg p-8 w-full max-w-4xl">
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
              <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                {[
                  { label: "Salary", value: formData.salary },
                  { label: "Share Percentage", value: formData.sharePercentage },
                  { label: "Established Branches", value: formData.establishedBranches },
                  { label: "National Number", value: formData.nationalNumber },
                  { label: "Gender", value: formData.gender },
                  { label: "Age", value: formData.age }
                ].map((item, idx) => (
                  <div key={idx} className="bg-gray-100 rounded-lg p-4 shadow-sm">
                    <p className="text-sm font-medium text-gray-500">{item.label}</p>
                    <p className="text-lg font-semibold text-gray-700">{item.value}</p>
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
