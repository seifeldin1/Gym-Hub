'use client'
import React, { useState } from "react";
import { Button, IconButton, TextField } from "@node_modules/@mui/material";
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

            <div className={`relative p-4 items-center rounded-lg shadow-lg w-80 max-w-lg transition-colors ${darkMode ? 'bg-[#272A37] text-white' : 'bg-white text-black'}`}>
                <IconButton
                    onClick={ChangeDarkMode}
                    color="inherit"
                    className={`absolute top-8 right-1 ${darkMode ? `bg-gray-800` : `bg-white`}`}
                >
                    {darkMode ? <LightModeIcon /> : <DarkModeIcon />}
                </IconButton>
                <div className={`w-[100%] mb-10`} >
                    <h2 className={`text-xl sm:text-2xl font-bold mb-5 text-center`}>
                        Log in
                    </h2>
                    <form onSubmit={LoginHandler} className="m-4 mb-8">
                        <div className="mb-4">
                            <TextField
                                label="Username"
                                variant="outlined"
                                value={username}
                                fullWidth
                                onChange={(e) => setUsername(e.target.value)}
                                InputLabelProps={{
                                    sx: {
                                        color: darkMode ? 'grey' : 'black', // Default label color
                                        '&.Mui-focused': {
                                            color: darkMode ? '#1976D2' : 'black', // Color when focused
                                        },
                                    },
                                }}
                                InputProps={{
                                    style: { color: darkMode ? 'white' : 'black', backgroundColor: darkMode ? '#374151' : '#ffffff', height: '100%' },
                                }}
                                className="rounded">
                            </TextField>
                            {UsernameError && !username && <p className="text-red-700">{UsernameError}</p>}
                        </div>
                        <div className={`flex ${darkMode ? `text-white` : `text-black`}`}>
                            <TextField
                                label="Password"
                                variant="outlined"
                                type={showPassword ? 'text' : 'password'}
                                value={password}
                                fullWidth
                                onChange={(e) => setPassword(e.target.value)}
                                InputLabelProps={{
                                    sx: {
                                        color: darkMode ? 'grey' : 'black', // Default label color
                                        '&.Mui-focused': {
                                            color: darkMode ? '#1976D2' : 'black', // Color when focused
                                        },
                                    },
                                }}
                                InputProps={{
                                    style: { color: darkMode ? 'white' : 'black', backgroundColor: darkMode ? '#374151' : '#ffffff', height: '100%' },
                                    endAdornment: (
                                        <IconButton
                                            onClick={ChangePasswordVisibility}
                                            edge="end"
                                            style={{ color: darkMode ? 'white' : 'black' }}
                                        >
                                            {showPassword ? <VisibilityIcon /> : <VisibilityOffIcon />}
                                        </IconButton>
                                    ),
                                }}
                                className="rounded">

                            </TextField>





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
                            className="w-60"
                            sx={{
                                backgroundColor: "#1D90F5", // Custom background color
                                '&:hover': {
                                    backgroundColor: "#1565C0", // Custom hover color
                                },
                            }}
                            >

                            {loading ? "Logging in..." : "Login"}
                        </Button>
                    </div>

                    {success && <p className="text-green-500 mt-2">{success}</p>}


                </div>

            </div>

        </div>
    )
}


export default LoginPage