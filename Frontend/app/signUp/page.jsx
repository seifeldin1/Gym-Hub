'use client'
import React, { useState } from "react"
import { Button, IconButton, TextField, Tooltip } from "@node_modules/@mui/material"
import VisibilityIcon from '@mui/icons-material/Visibility'
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff'
import LightModeIcon from '@mui/icons-material/LightMode'
import DarkModeIcon from '@mui/icons-material/DarkMode'
import styles from '@styles/signUp.module.css'
import { Typography  , MenuItem , RadioGroup , FormControlLabel , Radio} from "@mui/material";
//import style from '@styles/signup.module.css'
import HomeImage from '@public/images/signUp_photo1.png';


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

                    SelectProps={{
                        MenuProps: {
                            PaperProps: {
                                sx: {
                                    bgcolor: '#002637', // Dropdown background color
                                    '& .MuiMenuItem-root': {
                                        color: 'yellow', // Dropdown text color
                                        '&:hover': {
                                            bgcolor: '#0026', // Background on hover
                                        },
                                    },
                                },
                            },
                        },
                    }}
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
                <Button
                type="submit"
                variant="contained"
                fullWidth
                className="mt-6"
                sx={{ backgroundColor: "blue", color: "white", "&:hover": { backgroundColor: "darkblue" }  }}
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
    