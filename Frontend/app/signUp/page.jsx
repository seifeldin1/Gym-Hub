'use client'
import React, { useState } from "react"
import { Button, IconButton, TextField, Tooltip } from "@node_modules/@mui/material"
import VisibilityIcon from '@mui/icons-material/Visibility'
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff'
import LightModeIcon from '@mui/icons-material/LightMode'
import DarkModeIcon from '@mui/icons-material/DarkMode'
//import style from '@styles/signup.module.css'

const SignUpPage = ()=>{
    const[formData , setFormData] = useState({
        username : "",
        password : "",
        confirmPassword : "",
        firstName : "", 
        lastName : "" , 
        email : "" , 
        phoneNumber : "" , 
        gender : "", 
        age: "", 
        nationalNumber: ""
    })
    const [showPassword , setShowPassword] = useState(false)
    const [showConfirmPassword , setShowConfirmPassword] = useState(false)
    const [errors , setErrors] = useState({})

    const handleInputChanges = (e)=>{
        const {name , value} = e.target
        setFormData({...formData , [name] : value})
        setErrors({...errors , [name]: ""})
    }
    
    const handleShowPassword = ()=>{setShowPassword(!showPassword)}
    const handleShowConfirmPassword = ()=>{setShowConfirmPassword(!showConfirmPassword)}

    const handleSubmit = (e)=>{
        e.preventDefault()
        const newErrors= {}
        if(formData.password !== formData.confirmPassword)
            newErrors.confirmPassword = "Passwords do not match"
        if(formData.password.length < 8)
            newErrors.password = "Password must be at least 8 characters long"
        if(formData.firstName.length < 3)
            newErrors.firstName = "First name must be at least 3 characters long"
        if(formData.lastName.length < 3)
            newErrors.lastName = "Last name must be at least 3 characters long"
        if(formData.email.length < 5)
            newErrors.email = "Email must be at least 5 characters long"
        if(formData.phoneNumber.length < 10)
            newErrors.phoneNumber = "Phone number must be at least 10 characters long"
        if(formData.nationalNumber.length < 14)
            newErrors.nationalNumber = "National number must be at least 14 characters long"
        if(formData.age < 16)
            newErrors.age = "Age must be at least 16 years old"
        if(!formData.username)
            newErrors.username = "Username is required"
        if(!formData.password)
            newErrors.password = "Password is required"
        if(!formData.confirmPassword)
            newErrors.confirmPassword = "Confirm password is required"
        if(!formData.firstName)
            newErrors.firstName = "First name is required"
        if(!formData.lastName)
            newErrors.lastName = "Last name is required"
        if(!formData.email)
            newErrors.email = "Email is required"
        if(!formData.phoneNumber)
            newErrors.phoneNumber = "Phone number is required"
        if(!formData.nationalNumber)
            newErrors.nationalNumber = "National number is required"
        if(formData.age && isNaN(formData.age))
            newErrors.age = "Age must be a number"

        if(Object.keys(newErrors)>0){
            setErrors(newErrors)
            return
        }
        if (Object.keys(newErrors)==0)alert("Form submitted successfully!")

    };
            
        
    return(
        <div className="min-h-screen flex justify-center items-center bg-gray-100 p-4">
            <form className="bg-white p-6 rounded shadow-md w-full max-w-md " onSubmit={handleSubmit}>
                <h2 className="text-2xl font-bold text-gray-800 text-center mb-4">
                    Sign Up
                </h2>
                <TextField
                id="username"
                label="Username"
                name = "username"
                variant = "outlined"
                className="mb-4"
                onChange={handleInputChanges}
                value={formData.username}
                fullWidth
                error={!!errors.username}
                helperText={errors.username}
                />
                <TextField
                label="Password"
                variant="outlined"
                type={showPassword? "text" : "password"}
                name="password"
                value={formData.password}
                onChange={handleInputChanges}
                fullWidth
                className="mb-2"
                error={!!errors.password}
                helperText={errors.password}
                />

                <TextField
                label="Confirm Password"
                variant="outlined"
                type={showPassword? "text" : "password"}
                name="confirmPassword"
                value={formData.confirmPassword}
                onChange={handleInputChanges}
                fullWidth
                className="mb-2"
                error={!!errors.confirmPassword}
                helperText={errors.confirmPassword}
                />

                <TextField
                label="First Name"
                variant="outlined"
                name="firstName"
                value={formData.firstName}
                onChange={handleInputChanges}
                fullWidth
                className="mb-2"
                error={!!errors.firstName}
                helperText={errors.firstName}
                />

                <TextField
                label="Last Name"
                variant="outlined"
                name="lastName"
                value={formData.lastName}
                onChange={handleInputChanges}
                fullWidth
                className="mb-2"
                error={!!errors.lastName}
                helperText={errors.lastName}
                />

                <TextField
                label="Email"
                variant="outlined"
                name="email"
                type = "email"
                value={formData.email}
                onChange={handleInputChanges}
                fullWidth
                className="mb-2"
                error={!!errors.email}
                helperText={errors.email}
                />

                <TextField
                label="Phone Number"
                variant="outlined"
                name="phoneNumber"
                value={formData.phoneNumber}
                onChange={handleInputChanges}
                fullWidth
                className="mb-2"
                error={!!errors.phoneNumber}
                helperText={errors.phoneNumber}
                />

                <TextField
                label="Gender"
                variant="outlined"
                name="gender"
                value={formData.gender}
                onChange={handleInputChanges}
                fullWidth
                className="mb-2"
                />

                <TextField
                label="Age"
                variant="outlined"
                name="age"
                value={formData.age}
                onChange={handleInputChanges}
                fullWidth
                className="mb-2"
                error={!!errors.age}
                helperText={errors.age}
                />

                <TextField
                label="National Number"
                variant="outlined"
                name="nationalNumber"
                value={formData.nationalNumber}
                onChange={handleInputChanges}
                fullWidth
                className="mb-2"
                error={!!errors.nationalNumber}
                helperText={errors.nationalNumber}
                />

                <Button
                type="submit"
                variant="contained"
                fullWidth
                className="mt-4"
                sx={{
                        backgroundColor: "blue",
                        color: "white",
                        "&:hover": { backgroundColor: "darkblue" },
                }}
                >
                        Sign Up
                </Button>

            </form>

        </div>
    )

}

export default SignUpPage