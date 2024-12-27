'use client'
import React, { useState  , useEffect} from "react"
import { Button, IconButton, TextField, Tooltip } from "@node_modules/@mui/material"
import VisibilityIcon from '@mui/icons-material/Visibility'
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff'
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import LightModeIcon from '@mui/icons-material/LightMode'
import DarkModeIcon from '@mui/icons-material/DarkMode'
import styles from '@styles/signUp.module.css'
import { Typography  , MenuItem , RadioGroup , FormControlLabel , Radio} from "@mui/material";
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { Grid } from '@mui/material';
import dayjs from 'dayjs';
//import style from '@styles/signup.module.css'
import HomeImage from '@public/images/signUp_photo1.png';
import axios from 'axios';



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
    const [membershipData, setMembershipData] = useState({
        preferredStart: "",
        membershipPeriod: "",
        membershipType: "",
        BMR: "",
        weight: "",
        height: "",
        membershipFees: 0,
        endDate: ""
    });
    const [joinDate, setJoinDate] = useState("");
    const [showPassword , setShowPassword] = useState(false)
    const [showConfirmPassword , setShowConfirmPassword] = useState(false)
    const [errors , setErrors] = useState({})

    const formatDate = (date) => {
        const d = new Date(date);
        const day = String(d.getDate()).padStart(2, '0'); // Get the day with leading zero
        const month = String(d.getMonth() + 1).padStart(2, '0'); // Get the month with leading zero
        const year = d.getFullYear(); // Get the full year
        return `${day}/${month}/${year}`; // Return the formatted date
      };
    
      // Handler for the date change
      const handleDateChange = (e) => {
        const formattedDate = formatDate(e.target.value); // Format the selected date
        setPreferredStart(formattedDate); // Update the state with the formatted date
      };

    const handleInputChanges = (e)=>{
        const {name , value} = e.target
        setFormData({...formData , [name] : value})
        setErrors({...errors , [name]: ""})
    }
    
    const handleShowPassword = ()=>{setShowPassword(!showPassword)}
    const handleShowConfirmPassword = ()=>{setShowConfirmPassword(!showConfirmPassword)}

    useEffect(() => {
        calculateEndDate();
      }, [membershipData.preferredStart, membershipData.membershipPeriod, membershipData.membershipType]);
    
        const calculateEndDate = () => {
        const { preferredStart, membershipPeriod } = membershipData;
        if (preferredStart && membershipPeriod) {
          const startDate = new Date(preferredStart);
          const endDate = dayjs(startDate).add(parseInt(membershipPeriod), 'month').toDate();
          setMembershipData({ ...membershipData, endDate: endDate.toISOString().split('T')[0] }); 
        } else {
          setMembershipData({ ...membershipData, endDate: "" });
        }
    };
    
    const handleMembershipChanges = (e) => {
        const { name, value } = e.target;

        // Create a copy of the existing membership data
        let updatedData = { ...membershipData, [name]: value };

        // If membership period or type changes, calculate membership fees
        if (name === "membershipPeriod" || name === "membershipType") {
            const periodMultiplier = {
                "1": 1,   // No discount for 1 month
                "3": 0.9, // 10% discount for 3 months
                "6": 0.75, // 25% discount for 6 months
                "12": 0.5  // 50% discount for 12 months
            };
            const baseFees = {
                basic: 250,    // Base fee for basic membership
                silver: 500,   // Base fee for silver membership
                gold: 1500,    // Base fee for gold membership
                platinum: 3000 // Base fee for platinum membership
            };

            const period = updatedData.membershipPeriod; // Duration in months
            const type = updatedData.membershipType;     // Type of membership

            // Calculate fees if both period and type are provided
            if (period && type) {
                const fees = baseFees[type] * parseInt(period) * periodMultiplier[period];
                updatedData.membershipFees = fees.toFixed(2); // Round to 2 decimal places
            }
        }

        // If the period is selected, calculate the end date
        if (name === "membershipPeriod" && updatedData.preferredStart) {
            const startDate = new Date(updatedData.preferredStart);
            const monthsToAdd = parseInt(updatedData.membershipPeriod);
            startDate.setMonth(startDate.getMonth() + monthsToAdd); // Add selected months to start date
            updatedData.endDate = startDate.toISOString().split("T")[0]; // Format as YYYY-MM-DD
        }

        // Update the membership data state
        setMembershipData(updatedData);
    };

    const API_URL = 'http://localhost:5291/api';  // Base API URL
    const checkPhoneNumber = async (phoneNumber) => {
        try {
            const response = await axios.get(`${API_URL}//SignUpChecker/CheckEmail`, {
                params: { phone: phoneNumber }
            });
            return response.data; // Assuming the API returns { isValid: boolean }
        } catch (error) {
            console.error("Error checking phone number:", error);
            throw error;
        }
    };

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
        if(formData.phoneNumber.length < 13)
            newErrors.phoneNumber = "Phone number must be at least 13 characters long"
        if(formData.nationalNumber.length < 14)
            newErrors.nationalNumber = "National number must be at least 14 characters long"
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
        if(!formData.age)
            newErrors.age = "Age is required"
        if(!formData.gender)
            newErrors.gender = "Gender is required"
        if(!membershipData.BMR)
            newErrors.BMR = "BMR is required"
        if(!membershipData.height)
            newErrors.height = "Height is required"
        if(!membershipData.weight)
            newErrors.weight = "Weight is required"
        if(!membershipData.membershipPeriod)
            newErrors.membershipPeriod = "Membership period is required"
        if(!membershipData.membershipType)
            newErrors.membershipType = "Membership type is required"
        if(!membershipData.preferredStart)
            newErrors.preferredStart = "Preferred start date is required"


        if(Object.keys(newErrors).length>0){
            setErrors(newErrors)
            return
        }
        alert("Form submitted successfully!")

    };
            
        
    return (
        <>
        <div className={` ${styles.MainSignUp} ${styles.AnimBackgroundPhoto}`}>
            <div className="relative">
                <img className="z-10" src={HomeImage.src} alt="Description of the image" />
                <div className="absolute inset-0 flex items-center justify-center right-7 ">
                    <span className="text-white text-5xl ">START</span>
                </div>
                <div className="absolute inset-0 flex items-center justify-center right-7 top-32">
                <span className="text-white text-5xl ">TRAINING</span>
                </div>
                <div className="absolute inset-0 flex items-center justify-center right-7 top-64">
                <span className="text-white text-5xl  ">TODAY</span>
                </div>

                
            </div>

            
            <div className="text-center mt-12">
                <h1 className="text-3xl mb-4 text-white">Welcome to Our Gym!</h1>
                <p className="text-xl text-white">Start your fitness journey today with us.</p>
                <p className="text-xl text-white">❝ I hated every minute of training, but I said,</p>
                <p className="text-xl text-white"> ‘Don’t quit. Suffer now and live the rest of your life as a champion.❞ – Muhammad Ali</p>
            </div>  
            <div className="p-8 rounded w-full shadow-md max-w-2xl mt-10">

            
            <form
                className="rounded shadow-md w-full max-w-2xl"
                onSubmit={handleSubmit}
                style={{border:'none' , boxShadow: 'none'}}
            >
            
                <h2 className=" text-3xl mb-8 text-yellow-300 flex items-center justify-center">
                    Sign Up
                </h2>
             
            
                <div className="grid grid-cols-2 gap-4">
                    <TextField
                    label="First Name"
                    name="firstName"
                    variant="outlined"
                    placeholder="Enter your first name"
                    value={formData.firstName}
                    onChange={handleInputChanges}
                    error={!!errors.firstName}
                    helperText={errors.firstName}
                    fullWidth
                    sx={{
                        '& .MuiOutlinedInput-root': {
                            '& fieldset': {
                                borderColor: '#FDE047', // Border color for default state
                            },
                            '&:hover fieldset': {
                                borderColor: '#FFD700', // Border color on hover
                            },
                            '&.Mui-focused fieldset': {
                                borderColor: '#FDE047', // Border color when focused
                            },
                        },
                    }}
                    InputProps={{
                        style: {
                            color: 'yellow', // Input text color
                            
                        },
                        }}
                    InputLabelProps={{
                        sx: {
                            color: '#FDE047', // Default label color
                            '&.Mui-focused': {
                                color: '#FDE047', // Color when focused
                            },
                        },
                    }}
                    />
                    <TextField
                    label="Last Name"
                    name="lastName"
                    variant="outlined"
                    placeholder="Enter your last name"
                    value={formData.lastName}
                    onChange={handleInputChanges}
                    error={!!errors.lastName}
                    helperText={errors.lastName}
                    fullWidth
                    InputProps={{
                        style: {
                            color: 'yellow', // Input text color
                            
                        },
                        }}
                        sx={{
                            '& .MuiOutlinedInput-root': {
                                '& fieldset': {
                                    borderColor: '#FDE047', // Border color for default state
                                },
                                '&:hover fieldset': {
                                    borderColor: '#FFD700', // Border color on hover
                                },
                                '&.Mui-focused fieldset': {
                                    borderColor: '#FDE047', // Border color when focused
                                },
                            },
                        }}
                    InputLabelProps={{
                        sx: {
                            color: '#FDE047', // Default label color
                            '&.Mui-focused': {
                                color: '#FDE047', // Color when focused
                            },
                        },
                    }}
                    />
                    <TextField
                    label="Username"
                    name="username"
                    variant="outlined"
                    placeholder="Choose a username"
                    value={formData.username}
                    onChange={handleInputChanges}
                    error={!!errors.username}
                    helperText={errors.username}
                    fullWidth
                    InputProps={{
                        style: {
                            color: 'yellow', // Input text color
                            
                        },
                        }}
                        sx={{
                            '& .MuiOutlinedInput-root': {
                                '& fieldset': {
                                    borderColor: '#FDE047', // Border color for default state
                                },
                                '&:hover fieldset': {
                                    borderColor: '#FFD700', // Border color on hover
                                },
                                '&.Mui-focused fieldset': {
                                    borderColor: '#FDE047', // Border color when focused
                                },
                            },
                        }}
                    InputLabelProps={{
                        sx: {
                            color: '#FDE047', // Default label color
                            '&.Mui-focused': {
                                color: '#FDE047', // Color when focused
                            },
                        },
                    }}
                    />
                    <TextField
                    label="Email"
                    name="email"
                    variant="outlined"
                    placeholder="example@example.com"
                    value={formData.email}
                    onChange={handleInputChanges}
                    error={!!errors.email}
                    helperText={errors.email}
                    fullWidth
                    InputProps={{
                        style: {
                            color: 'yellow', // Input text color
                            
                        },
                        }}
                        sx={{
                            '& .MuiOutlinedInput-root': {
                                '& fieldset': {
                                    borderColor: '#FDE047', // Border color for default state
                                },
                                '&:hover fieldset': {
                                    borderColor: '#FFD700', // Border color on hover
                                },
                                '&.Mui-focused fieldset': {
                                    borderColor: '#FDE047', // Border color when focused
                                },
                            },
                        }}
                    InputLabelProps={{
                        sx: {
                            color: '#FDE047', // Default label color
                            '&.Mui-focused': {
                                color: '#FDE047', // Color when focused
                            },
                        },
                    }}
                    />
                    <TextField
                    label="Password"
                    type={showPassword? "text" : "password"}
                    name="password"
                    placeholder="Enter your password"
                    value={formData.password}
                    onChange={handleInputChanges}
                    error={!!errors.password}
                    helperText={errors.password}
                    fullWidth
                    
                    
                    sx={{
                        '& .MuiOutlinedInput-root': {
                            '& fieldset': {
                                borderColor: '#FDE047', // Border color for default state
                            },
                            '&:hover fieldset': {
                                borderColor: '#FFD700', // Border color on hover
                            },
                            '&.Mui-focused fieldset': {
                                borderColor: '#FDE047', // Border color when focused
                            },
                        },
                    }}
                    InputLabelProps={{
                        sx: {
                            color: '#FDE047', // Default label color
                            '&.Mui-focused': {
                                color: '#FDE047', // Color when focused
                            },
                        },
                    }}
                    InputProps={{
                        style: {
                            color: 'yellow', // Input text color
                        },                        endAdornment: (
                            <IconButton onClick={handleShowPassword} edge="end" sx={{color:"yellow"}}>
                                {showPassword ? <VisibilityOffIcon /> :  <VisibilityIcon />}
                            </IconButton>
                        )
                    }}
                    />
                    <TextField
                    label="Confirm Password"
                    type={showConfirmPassword? "text" :"password"}
                    name="confirmPassword"
                    placeholder="Re-enter your password"
                    value={formData.confirmPassword}
                    onChange={handleInputChanges}
                    error={!!errors.confirmPassword}
                    helperText={errors.confirmPassword}
                    fullWidth
                    sx={{
                        '& .MuiOutlinedInput-root': {
                            '& fieldset': {
                                borderColor: '#FDE047', // Border color for default state
                            },
                            '&:hover fieldset': {
                                borderColor: '#FFD700', // Border color on hover
                            },
                            '&.Mui-focused fieldset': {
                                borderColor: '#FDE047', // Border color when focused
                            },
                        },
                    }}
                    InputLabelProps={{
                        sx: {
                            
                            color: '#FDE047', // Default label color
                            '&.Mui-focused': {
                                color: '#FDE047', // Color when focused
                            },
                        },
                    }}
                    InputProps={{
                        style: {
                            color: 'yellow',
                         }, // Input text color
                        endAdornment: (
                            <IconButton onClick={handleShowConfirmPassword} edge="end" sx={{color:"yellow"}}>
                                {showConfirmPassword ? <VisibilityOffIcon/> :  <VisibilityIcon/>}
                            </IconButton>
                        )
                    }}
                    />
                    <TextField
                    label="National Number"
                    name="nationalNumber"
                    variant="outlined"
                    placeholder="Enter your national number"
                    value={formData.nationalNumber}
                    onChange={handleInputChanges}
                    error={!!errors.nationalNumber}
                    helperText={errors.nationalNumber}
                    fullWidth
                    InputProps={{
                        style: {
                            color: 'yellow', // Input text color
                            
                        },
                        }}
                        sx={{
                            '& .MuiOutlinedInput-root': {
                                '& fieldset': {
                                    borderColor: '#FDE047', // Border color for default state
                                },
                                '&:hover fieldset': {
                                    borderColor: '#FFD700', // Border color on hover
                                },
                                '&.Mui-focused fieldset': {
                                    borderColor: '#FDE047', // Border color when focused
                                },
                            },
                        }}
                    InputLabelProps={{
                        sx: {
                            color: '#FDE047', // Default label color
                            '&.Mui-focused': {
                                color: '#FDE047', // Color when focused
                            },
                        },
                    }}
                    />
                    <TextField
                    label="Phone Number"
                    name="phoneNumber"
                    variant="outlined"
                    placeholder="Enter your phone number:+20XXXXXXXXXX"
                    value={formData.phoneNumber}
                    onChange={handleInputChanges}
                    error={!!errors.phoneNumber}
                    helperText={errors.phoneNumber}
                    fullWidth
                    InputProps={{
                        style: {
                            color: 'yellow', // Input text color
                            
                        },
                        }}
                        sx={{
                            '& .MuiOutlinedInput-root': {
                                '& fieldset': {
                                    borderColor: '#FDE047', // Border color for default state
                                },
                                '&:hover fieldset': {
                                    borderColor: '#FFD700', // Border color on hover
                                },
                                '&.Mui-focused fieldset': {
                                    borderColor: '#FDE047', // Border color when focused
                                },
                            },
                        }}
                    InputLabelProps={{
                        sx: {
                            color: '#FDE047', // Default label color
                            '&.Mui-focused': {
                                color: '#FDE047', // Color when focused
                            },
                        },
                    }}
                    />
                    <TextField
                    select
                    label="Age"
                    name="age"
                    value={formData.age}
                    onChange={handleInputChanges}
                    error={!!errors.age}
                    helperText={errors.age}
                    fullWidth
                    InputProps={{
                        style: {
                            color: 'yellow', // Input text color
                            
                        },
                        }}
                        sx={{
                            '& .MuiOutlinedInput-root': {
                                '& fieldset': {
                                    borderColor: '#FDE047', // Border color for default state
                                },
                                '&:hover fieldset': {
                                    borderColor: '#FFD700', // Border color on hover
                                },
                                '&.Mui-focused fieldset': {
                                    borderColor: '#FDE047', // Border color when focused
                                },
                                '& svg': {
                                    color: 'yellow', // Dropdown icon color
                                },
                            },
                        }}
                    InputLabelProps={{
                        sx: {
                            color: '#FDE047', // Default label color
                            '&.Mui-focused': {
                                color: '#FDE047', // Color when focused
                            },
                        },
                    }}

                    // SelectProps={{
                    //     MenuProps: {
                    //         PaperProps: {
                    //             sx: {
                    //                 bgcolor: '#002637', // Dropdown background color
                    //                 '& .MuiMenuItem-root': {
                    //                     color: 'yellow', // Dropdown text color
                    //                     '&:hover': {
                    //                         bgcolor: '#0026', // Background on hover
                    //                     },
                    //                 },
                    //             },
                    //         },
                    //     },
                    // }}
                    >
                    {Array.from({ length: 55 }, (_, i) => (
                        <MenuItem key={i + 16} value={i + 16}>
                            {i + 16}
                        </MenuItem>
                    ))}
                    </TextField>
                    
                </div>
                <div className="mt-4 ml-3">
                    <Typography variant="subtitle1" className="mb-2 font-semibold text-yellow-300">
                        Gender
                    </Typography>
                    <RadioGroup
                    row
                    name="gender"
                    value={formData.gender}
                    onChange={handleInputChanges}
                    >
                    <FormControlLabel
                    value="male"
                    className="text-yellow-300"
                    control={<Radio  sx={{ color: 'yellow' }} />}
                    label="Male"
                    />
                    <FormControlLabel
                    value="female"
                    className="text-yellow-300"
                    control={<Radio sx={{ color: 'yellow' }}  />}
                    label="Female"
                    />
                    <FormControlLabel
                    value="engineer"
                    className="text-yellow-300"
                    control={<Radio sx={{ color: 'yellow' }}/>}
                    label="Engineer"
                    />
                    </RadioGroup>
                        {errors.gender && (
                            <Typography color="error" variant="caption">
                                {errors.gender}
                            </Typography>
                        )}
                </div>
                
                <h2 className="mt-20 text-3xl mb-8 text-yellow-300 flex items-center justify-center">Membership Details</h2>
                <form  className="rounded shadow-md w-full max-w-2xl"
                onSubmit={handleSubmit}
                style={{border:'none' , boxShadow: 'none'}}>
                    <div className="grid grid-cols-2 gap-4">
                        {/* Preferred Start Date */}
                        <TextField
                            label="Preferred Start Date"
                            type="date"
                            name="preferredStart"
                            error={!!errors.preferredStart}
                            helperText={errors.preferredStart}
                            value={membershipData.preferredStart}
                            onChange={handleMembershipChanges}
                            fullWidth
                            InputLabelProps={{ 
                                shrink: true,
                                style: { color: 'yellow' },
                            }}
                            sx={{
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': { borderColor: '#FDE047' },
                                    '&:hover fieldset': { borderColor: '#FFD700' },
                                    '&.Mui-focused fieldset': { borderColor: '#FDE047' }
                                },
                                '& .MuiInputBase-input': {
                                    color: 'yellow', // Input text color
                                    '-webkit-text-fill-color': 'yellow', // For Safari support
                                },
                                '& .Mui-disabled .MuiInputBase-input': {
                                    color: 'yellow', // Disabled input color
                                },
                                // Change the color of the calendar icon for the date picker
                                '& input[type="date"]::-webkit-calendar-picker-indicator': {
                                    filter: 'invert(1) sepia(1) saturate(5) hue-rotate(90deg)', // This applies a yellow filter
                                },
                            }}
                        />

                        {/* Membership Period */}
                        <TextField
                            label="Membership Period (Months)"
                            name="membershipPeriod"
                            value={membershipData.membershipPeriod}
                            error={!!errors.membershipPeriod}
                            helperText={errors.membershipPeriod}
                            onChange={handleMembershipChanges}
                            select
                            fullWidth
                            InputLabelProps={{
                               
                                style: { color: 'yellow' },
                            }}
                            sx={{
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': { borderColor: '#FDE047' },
                                    '&:hover fieldset': { borderColor: '#FFD700' },
                                    '&.Mui-focused fieldset': { borderColor: '#FDE047' }
                                },
                                '& .MuiInputBase-input': {
                                    color: 'yellow'
                                },
                                '& .MuiSelect-icon': { // Style the dropdown icon
                                    color: 'yellow',
                                },
                                
                            }}
                        >
                            {["1", "3", "6", "12"].map((option) => (
                                <MenuItem 
                                key={option}
                                value={option}
                                >
                                    {option} Month{option > 1 && "s"}
                                </MenuItem>
                            ))}
                        </TextField>
                        <TextField
                            label="Membership Type"
                            name="membershipType"
                            error={!!errors.membershipType}
                            helperText={errors.membershipType}
                            value={membershipData.membershipType}
                            onChange={handleMembershipChanges}
                            select
                            fullWidth
                            InputLabelProps={{
                               
                                style: { color: 'yellow' },
                            }}
                            sx={{
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': { borderColor: '#FDE047' },
                                    '&:hover fieldset': { borderColor: '#FFD700' },
                                    '&.Mui-focused fieldset': { borderColor: '#FDE047' }
                                },
                                '& .MuiInputBase-input': {
                                    color: 'yellow'
                                },
                                '& .MuiSelect-icon': { // Style the dropdown icon
                                    color: 'yellow',
                                },
                                
                            }}
                        >
                             {["basic", "silver", "gold", "platinum"].map((option) => (
                                <MenuItem 
                                key={option}
                                value={option}
                                >
                                    {option} 
                                </MenuItem>
                            ))}
                           
                        </TextField>
                        <TextField
                            label="Membership Fees"
                            name="membershipFees"
                            error={!!errors.membershipFees}
                            onChange={handleMembershipChanges} 
                            fullWidth
                            type="number" 
                            disabled
                            InputLabelProps={{
                                shrink: true,
                                style: { color: 'yellow' },
                            }}
                            sx={{
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': {
                                        borderColor: '#FDE047', // Border color
                                    },
                                    '&:hover fieldset': {
                                        borderColor: '#FFD700', // Border color on hover
                                    },
                                    '&.Mui-focused fieldset': {
                                        borderColor: '#FDE047', // Border color when focused
                                    },
                                },
                                '& .MuiInputBase-input': {
                                    color: 'yellow', // Input text color is yellow
                                    '-webkit-text-fill-color': 'yellow', // For Safari support
                                },
                                '& .MuiSvgIcon-root': { 
                                    color: 'yellow', // Change the calendar icon color to yellow
                                },
                                // Override styles for the disabled state
                                '& .Mui-disabled': {
                                    color: 'yellow', // Ensure text color stays yellow when disabled
                                    '-webkit-text-fill-color': 'yellow', // For Safari support
                                },
                                '& .Mui-disabled .MuiInputBase-input': {
                                    color: 'yellow', // Text color when input is disabled
                                    '-webkit-text-fill-color': 'yellow', // For Safari
                                },
                                '& .Mui-disabled .MuiOutlinedInput-root': {
                                    backgroundColor: '#2c2c2c', // Optionally change background color for disabled field
                                },
                            }}
                        /> 
                        <TextField
                            label="BMR"
                            name="BMR"
                            value={membershipData.BMR}
                            error={!!errors.BMR}
                            helperText={errors.BMR}
                            onChange={handleMembershipChanges}
                            fullWidth
                            placeholder="BMR"
                            type="number" 
                            InputLabelProps={{
                                
                                style: { color: 'yellow' },
                            }}
                            sx={{
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': { borderColor: '#FDE047' },
                                    '&:hover fieldset': { borderColor: '#FFD700' },
                                    '&.Mui-focused fieldset': { borderColor: '#FDE047' }
                                },
                                '& .MuiInputBase-input': {
                                    color: 'yellow'
                                },
                                '& .MuiSelect-icon': { // Style the dropdown icon
                                    color: 'yellow',
                                },
                                '& input[type=number]::-webkit-outer-spin-button, & input[type=number]::-webkit-inner-spin-button': {
                                    display: 'none',
                                },
                                '& input[type=number]': {
                                    '-moz-appearance': 'textfield', // For Firefox
                                },
                                
                            }}
                        /> 
                        <TextField
                            label="Weight (kg)"
                            name="weight"
                            value={membershipData.weight}
                            error={!!errors.weight}
                            helperText={errors.weight}
                            onChange={handleMembershipChanges}
                            fullWidth
                            placeholder="Weight in kg"
                            type="number" 
                            InputLabelProps={{
                              
                                style: { color: 'yellow' },
                            }}
                            sx={{
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': { borderColor: '#FDE047' },
                                    '&:hover fieldset': { borderColor: '#FFD700' },
                                    '&.Mui-focused fieldset': { borderColor: '#FDE047' }
                                },
                                '& .MuiInputBase-input': {
                                    color: 'yellow'
                                },
                                '& .MuiSelect-icon': { // Style the dropdown icon
                                    color: 'yellow',
                                },
                                '& input[type=number]::-webkit-outer-spin-button, & input[type=number]::-webkit-inner-spin-button': {
                                    display: 'none',
                                },
                                '& input[type=number]': {
                                    '-moz-appearance': 'textfield', // For Firefox
                                },
                                
                            }}
                        />
                        <TextField
                            label="Height (cm)"
                            name="height"
                            value={membershipData.height}
                            error={!!errors.height}
                            helperText={errors.height}
                            onChange={handleMembershipChanges}
                            fullWidth
                            placeholder="Height in cm"
                            type="number" 
                            InputLabelProps={{
                                
                                style: { color: 'yellow' },
                            }}
                            sx={{
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': { borderColor: '#FDE047' },
                                    '&:hover fieldset': { borderColor: '#FFD700' },
                                    '&.Mui-focused fieldset': { borderColor: '#FDE047' }
                                },
                                '& .MuiInputBase-input': {
                                    color: 'yellow'
                                },
                                '& .MuiSelect-icon': { // Style the dropdown icon
                                    color: 'yellow',
                                },
                                '& input[type=number]::-webkit-outer-spin-button, & input[type=number]::-webkit-inner-spin-button': {
                                    display: 'none',
                                },
                                '& input[type=number]': {
                                    '-moz-appearance': 'textfield', // For Firefox
                                },

                                
                            }}
                        />
                        <TextField
                            label="End Date"
                            type="date"
                            name="endDate"
                            value={membershipData.endDate}
                            onChange={handleMembershipChanges} 
                            fullWidth
                            InputLabelProps={{
                            shrink: true,
                            style: { color: 'yellow' },
                            }}
                            disabled
                            sx={{
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': {
                                        borderColor: '#FDE047', // Border color
                                    },
                                    '&:hover fieldset': {
                                        borderColor: '#FFD700', // Border color on hover
                                    },
                                    '&.Mui-focused fieldset': {
                                        borderColor: '#FDE047', // Border color when focused
                                    },
                                },
                                '& .MuiInputBase-input': {
                                    color: 'yellow', // Input text color is yellow
                                    '-webkit-text-fill-color': 'yellow', // For Safari support
                                },
                                '& .MuiSvgIcon-root': { 
                                    color: 'yellow', // Change the calendar icon color to yellow
                                },
                                // Override styles for the disabled state
                                '& .Mui-disabled': {
                                    color: 'yellow', // Ensure text color stays yellow when disabled
                                    '-webkit-text-fill-color': 'yellow', // For Safari support
                                },
                                '& .Mui-disabled .MuiInputBase-input': {
                                    color: 'yellow', // Text color when input is disabled
                                    '-webkit-text-fill-color': 'yellow', // For Safari
                                },
                                '& .Mui-disabled .MuiOutlinedInput-root': {
                                    backgroundColor: '#2c2c2c', // Optionally change background color for disabled field
                                },
                            }}
                        />
                    </div>
                </form>
            
                <Button
                    type="submit"
                    variant="contained"
                    fullWidth
                    className="w-60 text-black mt-20"
                            sx={{
                                backgroundColor: "yellow", // Custom background color
                                color:"black",
                                '&:hover': {
                                    backgroundColor: "black", // Custom hover color
                                    color:"yellow"
                                },
                                marginTop: '25px',
                            }}
                >
                    Sign Up
                </Button>
            </form>
            </div>
            
        </div>
        </>
        
      );
    };
    
    export default SignUpPage;
    