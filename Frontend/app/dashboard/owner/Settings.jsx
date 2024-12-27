import React, { useState } from "react";
import { DashHeader } from '@components/NavBar';

const Settings = () => {
    const [formData, setFormData] = useState({
        name: "",
        email: "",
        password: "",
        confirmPassword: "",
        profilePicture: null,
        firstName: "",
        lastName: "",
        phoneNumber: "",
        nationalNumber: "",
        age: "",
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleFileChange = (e) => {
        setFormData({ ...formData, profilePicture: e.target.files[0] });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        if (formData.password !== formData.confirmPassword) {
            alert("Passwords do not match");
            return;
        }
        // Handle form submission, such as sending the data to an API
        console.log("Form data submitted:", formData);
    };

    return (
        <>
            <DashHeader page_name="Settings" />
            <div className="w-[95%] mx-auto rounded-lg bg-orange-500">
                <h2 className="text-xl font-semibold text-center mb-6">Edit Your Settings</h2>
                <form onSubmit={handleSubmit} className="space-y-4 flex gap-2">
                    <div className="w-[50%]">
                        <div className="w-[95%] mx-auto">
                            <fieldset className="mb-4">
                                <legend className="text-sm font-medium mb-2">Personal Information</legend>
                                <label htmlFor="firstName" className="block text-sm font-medium my-2">
                                    First Name
                                </label>
                                <input
                                    type="text"
                                    id="firstName"
                                    name="firstName"
                                    value={formData.firstName}
                                    onChange={handleChange}
                                    placeholder="Enter New First Name"
                                    className="w-[90%] px-2 rounded-md"
                                    required
                                />

                                <label htmlFor="lastName" className="block text-sm font-medium my-2">
                                    Last Name
                                </label>
                                <input
                                    type="text"
                                    id="lastName"
                                    name="lastName"
                                    value={formData.lastName}
                                    onChange={handleChange}
                                    placeholder="Enter New Last Name"
                                    className="w-[90%] px-2 rounded-md"
                                    required
                                />

                                <label htmlFor="phoneNumber" className="block text-sm font-medium my-2">
                                    Phone Number
                                </label>
                                <input
                                    type="text"
                                    id="phoneNumber"
                                    name="phoneNumber"
                                    value={formData.phoneNumber}
                                    onChange={handleChange}
                                    placeholder="Enter New Phone Number"
                                    className="w-[90%] px-2 rounded-md"
                                    required
                                />

                                <label htmlFor="nationalNumber" className="block text-sm font-medium my-2">
                                    National Number
                                </label>
                                <input
                                    type="text"
                                    id="nationalNumber"
                                    name="nationalNumber"
                                    value={formData.nationalNumber}
                                    onChange={handleChange}
                                    placeholder="Enter New National Number"
                                    className="w-[90%] px-2 rounded-md"
                                    required
                                />

                                <label htmlFor="age" className="block text-sm font-medium my-2">
                                    Age
                                </label>
                                <input
                                    type="number"
                                    id="age"
                                    name="age"
                                    value={formData.age}
                                    onChange={handleChange}
                                    placeholder="Enter New Age"
                                    className="w-[90%] px-2 rounded-md"
                                    required
                                />
                            </fieldset>

                            <fieldset className="mb-4">
                                <legend className="text-sm font-medium mb-2">Account Information</legend>
                                <label htmlFor="name" className="block text-sm font-medium my-2">
                                    Username
                                </label>
                                <input
                                    type="text"
                                    id="name"
                                    name="name"
                                    value={formData.name}
                                    onChange={handleChange}
                                    placeholder="Enter New Username"
                                    className="w-[90%] px-2 rounded-md"
                                    required
                                />

                                <label htmlFor="email" className="block text-sm font-medium my-2">
                                    Email
                                </label>
                                <input
                                    type="email"
                                    id="email"
                                    name="email"
                                    value={formData.email}
                                    onChange={handleChange}
                                    placeholder="Enter New Email"
                                    className="w-[90%] px-2 rounded-md"
                                    required
                                />

                                <label htmlFor="password" className="block text-sm font-medium my-2">
                                    Password
                                </label>
                                <input
                                    type="password"
                                    id="password"
                                    name="password"
                                    value={formData.password}
                                    onChange={handleChange}
                                    placeholder="Enter New Password"
                                    className="w-[90%] px-2 rounded-md"
                                    required
                                />

                                <label htmlFor="confirmPassword" className="block text-sm font-medium my-2">
                                    Confirm Password
                                </label>
                                <input
                                    type="password"
                                    id="confirmPassword"
                                    name="confirmPassword"
                                    value={formData.confirmPassword}
                                    onChange={handleChange}
                                    placeholder="Confirm New Password"
                                    className="w-[90%] px-2 rounded-md"
                                    required
                                />
                            </fieldset>

                            <fieldset className="mb-4">
                                <legend className="text-sm font-medium mb-2">Profile Picture</legend>
                                <input
                                    type="file"
                                    id="profilePicture"
                                    name="profilePicture"
                                    onChange={handleFileChange}
                                    className="w-[90%] px-2 rounded-md"
                                />
                            </fieldset>

                            <button
                                type="submit"
                                className="w-[90%] py-2 bg-blue-600 text-white rounded-md"
                            >
                                Submit
                            </button>
                        </div>
                    </div>
                </form>
            </div>
        </>
    );
};

export default Settings;
