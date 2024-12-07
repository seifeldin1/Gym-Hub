'use client'
import React, { useState } from "react";
import { Button, IconButton, TextField , Tooltip } from "@node_modules/@mui/material";
import VisibilityIcon from '@mui/icons-material/Visibility';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import LightModeIcon from '@mui/icons-material/LightMode';
import DarkModeIcon from '@mui/icons-material/DarkMode';
import style from '@styles/login.module.css'

const LoginPage = () => {
    const [username, setUsername] = useState("")
    const [password, setPassword] = useState("")
    const [UsernameError, setUsernameError] = useState(null)
    const [PasswordError, setPasswordError] = useState(null)
    const [success, setSuccess] = useState(null)
    const [darkMode, setDrkMode] = useState(false)
    const [loading, setLoading] = useState(false)
    const [showPassword, setShowPassword] = useState(false)

    const ChangeDarkMode = () => { setDrkMode(!darkMode) }
    const ChangePasswordVisibility = () => { setShowPassword(!showPassword) }
    const LoginHandler = async (event) => {
        event.preventDefault()
        setUsernameError("")
        setPasswordError("")
        setSuccess("")
        if (!username || !password) {
            if (!username) {
                setUsernameError("Please enter your username")
            }
            if (!password) {
                setPasswordError("Please enter your password")
            }
            return
        }
        setLoading(true)
        try {
            // const response = await axios.post(/*api*/ , {
            //     username: username,
            //     password: password
            // })
            setSuccess("Login Successful! Redirecting....")
            //TODO: Redirect to the dashboard 
        } catch (error) {
            setError("Invalid username or password")
        } finally {
            setLoading(false)
        }
    }

    return (
        /*main page */
        <div className={`min-h-screen flex justify-center p-8 items-center bg-cover ${style.background} transition-colors`} >
            {/*form of login*/}

            <div className={`relative p-4 items-center rounded-lg shadow-lg w-80 max-w-lg transition-colors ${darkMode ? 'rgba(39, 42, 55, 0)' : 'rgba(255, 255, 255, 0.8)'}`} 
            style={{ transform: 'translate(55%, 30%)'}}>
                <IconButton
                    onClick={ChangeDarkMode}
                    style={{
                        maxWidth: '32px', 
                        maxHeight: '32px', 
                        minWidth: '20px',
                        minHeight: '20px',
                        color: darkMode ? 'white' : '#1E1E2A', // Yellow in dark mode
                        backgroundColor: darkMode ? '#1E1E2A ' : '#FCEE02', // Adjust button background
                    }}
                    className={`absolute top-9 left-10 bg-white transition-colors`}
                >
                    {darkMode ? <LightModeIcon /> : <DarkModeIcon />}
                </IconButton>
                <div className={`w-[100%] mb-10`} >
                    <h2 className={`text-xl sm:text-2xl color:#110F10 font-bold mb-5 text-center`}>
                        Log in
                    </h2>
                    <form onSubmit={LoginHandler} className="m-4 mb-8">
                        <div className="mb-4">
                        <Tooltip title="Enter your username" arrow>
                            <TextField
                                label="Username"
                                variant="outlined"
                                placeholder="Enter Username"
                                value={username}
                                fullWidth
                                onChange={(e) => setUsername(e.target.value)}
                                InputLabelProps={{
                                    sx: {
                                        color: darkMode ? 'white' : '#FDE047', // Default label color
                                        '&.Mui-focused': {
                                            color: darkMode ? 'white' : '#FDE047', // Color when focused
                                        },
                                    },
                                }}
                                InputProps={{
                                    style: { color: darkMode ? 'white' : 'yellow', backgroundColor: darkMode ? 'rgba(39, 42, 55, 0.7)' : 'rgba(29, 27, 28, 0.7)', height: '100%' , borderRadius:'8px' },
                                }}
                                className="rounded">
                            </TextField>
                            </Tooltip>
                            {UsernameError && !username && <p className="text-red-700">{UsernameError}</p>}
                        </div>
                        <div className={`flex ${darkMode ? `text-white` : `text-black`}`}>
                        <Tooltip title="Enter your password" arrow>
                            <TextField
                                label="Password"
                                variant="outlined"
                                placeholder="Enter Password"
                                type={showPassword ? 'text' : 'password'}
                                value={password}
                                fullWidth
                                onChange={(e) => setPassword(e.target.value)}
                                InputLabelProps={{
                                    sx: {
                                        color: darkMode ? 'white' : '#FDE047', // Default label color
                                        '&.Mui-focused': {
                                            color: darkMode ? 'white' : '#FDE047', // Color when focused
                                        },
                                    },
                                }}
                                InputProps={{
                                    style: { color: darkMode ? 'white' : 'yellow', backgroundColor: darkMode ? 'rgba(39, 42, 55, 0.7)' : 'rgba(29, 27, 28, 0.7)', height: '100%' , borderRadius:'8px' },
                                    endAdornment: (
                                        <IconButton
                                            onClick={ChangePasswordVisibility}
                                            edge="end"
                                            style={{ color: darkMode ? 'white' : 'yellow' }}
                                        >
                                            {showPassword ? <VisibilityIcon /> : <VisibilityOffIcon />}
                                        </IconButton>
                                    ),
                                }}
                                className="rounded">

                            </TextField>
                            </Tooltip>





                        </div>
                        <div>
                            {PasswordError && !password && <p className="text-red-700">{PasswordError}</p>}

                        </div>








                    </form>
                    <div className="ml-5">
                        <Button
                            variant="contained"
                            type="submit"
                            disabled={loading}
                            onClick={LoginHandler}
                            fullWidth
                            className="w-60 text-black"
                            sx={{
                                backgroundColor: "yellow", // Custom background color
                                color:"black",
                                '&:hover': {
                                    backgroundColor: "black", // Custom hover color
                                    color:"yellow"
                                },
                            }}
                            >

                            {loading ? "Logging in..." : "Login"}
                        </Button>
                    </div>

                    {success && <p className="text-yellow-200 mt-2 p-4">{success}</p>}


                </div>

            </div>

        </div>
    )
}


export default LoginPage