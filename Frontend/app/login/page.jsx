'use client'
import React, { useState } from "react";
import { Button, IconButton, TextField , Tooltip } from "@node_modules/@mui/material";
import VisibilityIcon from '@mui/icons-material/Visibility';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import LightModeIcon from '@mui/icons-material/LightMode';
import DarkModeIcon from '@mui/icons-material/DarkMode';
import style from '@styles/login.module.css';
import axiosInstance from "@app/axios";
// import vid1 from "@public/videos/login_bg1.mp4";
// import vid2 from "@public/videos/login_bg2.mp4";
// import vid3 from "@public/videos/login_bg3.mp4";
// import vid4 from "@public/videos/login_bg4.mp4";
// import vid5 from "@public/videos/login_bg5.mp4";


const LoginPage = () => {
    const [username, setUsername] = useState("")
    const [password, setPassword] = useState("")
    const [UsernameError, setUsernameError] = useState(null)
    const [PasswordError, setPasswordError] = useState(null)
    const [success, setSuccess] = useState(null)
    const [darkMode, setDrkMode] = useState(false)
    const [loading, setLoading] = useState(false)
    const [showPassword, setShowPassword] = useState(false)
    const [currentVideo  , setCurrentVideo] = useState(0)
    const [fade , setFade] = useState(false)
    const [error , setError] = useState("")

    const ChangeDarkMode = () => { setDrkMode(!darkMode) }
    const ChangePasswordVisibility = () => { setShowPassword(!showPassword) }
    const LoginHandler = async (event) => {
        event.preventDefault()
        setUsernameError("")
        setPasswordError("")
        setSuccess("")
        setError("")
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
            const response = await axiosInstance.post("/Credentials/login", {
                Username: username,
                Password: password,
            });
            if (response.status === 200 && response.data.success) {
                setSuccess(`Login Successful! Welcome ${response.data.userType}`);
                // Save token and redirect
                localStorage.setItem("token", response.data.token);
                // Example: router.push('/dashboard');
            } else {
                setUsernameError("Invalid Username");
                setPasswordError("Invalid Password");
            }
            
        } catch (error) {
            console.error("Error logging in:", error);
            const serverMessage = error.response.data.message;
            setError(serverMessage || "An error occurred. Please try again.")
        } finally {
            setLoading(false)
        }
    }

    const VideoSources = [
        '/videos/login_bg1.mp4',
    '/videos/login_bg2.mp4',
    '/videos/login_bg3.mp4',
    '/videos/login_bg4.mp4',
    '/videos/login_bg5.mp4',
    ]

    const handleVideoEnd = () => {
        const nextVideoIndex = (currentVideo + 1) % VideoSources.length;
        const nextVideo = document.createElement("video");
        nextVideo.src = VideoSources[nextVideoIndex];
        nextVideo.preload = "auto";
    
        setTimeout(() => {
            setCurrentVideo(nextVideoIndex);
        }, 20); 
    };



    return (
        /*main page */
        <div className={`relative min-h-screen flex justify-center p-8 items-center bg-cover transition-colors`} >
            <div className="absolute inset-0 w-full h-full z-0">
                <video
                    key = {currentVideo}
                    className={`relative inset-0 w-full h-full object-cover video-fade`}                   
                    src={VideoSources[currentVideo]}
                    autoPlay={true}
                    loop={false}
                    muted 
                    onEnded={handleVideoEnd}
                >

                </video>
            </div>
            {/*form of login*/}

            {/*${darkMode ? 'rgba(39, 42, 55, 0)' : 'rgba(255, 255, 255, 0)'} */}
            <div className={`relative p-4 items-center rounded-lg shadow-lg w-80 max-w-lg transition-colors rgba(255, 255, 255, 0) border:none `} 
            style={{ transform: 'translate(0%, 0%)' , border:'none' , boxShadow: 'none'}}>
                <IconButton
                    onClick={ChangeDarkMode}
                    style={{
                        maxWidth: '32px', 
                        maxHeight: '32px', 
                        minWidth: '20px',
                        minHeight: '20px',
                        //color: darkMode ? 'white' : '#1E1E2A', // Yellow in dark mode
                    }}
                    className={`absolute top-9 left-10 bg-white transition-colors`}
                >
                    {/*{darkMode ? <LightModeIcon /> : <DarkModeIcon />}*/}
                </IconButton>
                <div className={`w-[100%] mb-10`} >
                    <h2 style={{ color: 'yellow' }} className={`text-xl sm:text-2xl font-bold mb-5 text-center`}>
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
                                        // color: darkMode ? 'white' : '#FDE047', // Default label color
                                        // '&.Mui-focused': {
                                        //     color: darkMode ? 'white' : '#FDE047', // Color when focused
                                        // },
                                        color: '#FDE047', // Default label color
                                        '&.Mui-focused': {
                                            color: '#FDE047', // Color when focused
                                        },
                                    },
                                }}
                                InputProps={{
                                    style: { color: 'yellow', backgroundColor: 'rgba(29, 27, 28, 0.7)', height: '100%' , borderRadius:'8px' },
                                  //  style: { color: darkMode ? 'white' : 'yellow', backgroundColor: darkMode ? 'rgba(39, 42, 55, 0.7)' : 'rgba(29, 27, 28, 0.7)', height: '100%' , borderRadius:'8px' },
                                }}
                                className="rounded">
                            </TextField>
                            </Tooltip>
                            {UsernameError && !username && <p className="text-red-700">{UsernameError}</p>}
                        </div>
                        {/*${darkMode ? `text-white` : `text-black`}*/}
                        <div className={`flex text-black}`}>
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
                                        color: '#FDE047', // Default label color
                                        '&.Mui-focused': {
                                            color: '#FDE047', // Color when focused
                                        },
                                    },
                                }}
                                InputProps={{
                                    style: { color: 'yellow', backgroundColor: 'rgba(29, 27, 28, 0.7)', height: '100%' , borderRadius:'8px' },

                                    //style: { color: darkMode ? 'white' : 'yellow', backgroundColor: darkMode ? 'rgba(39, 42, 55, 0.7)' : 'rgba(29, 27, 28, 0.7)', height: '100%' , borderRadius:'8px' },
                                    endAdornment: (
                                        <IconButton
                                            onClick={ChangePasswordVisibility}
                                            edge="end"
                                            //style={{ color: darkMode ? 'white' : 'yellow' }}
                                            style={{ color: 'yellow' }}
                                        >
                                            {showPassword ? <VisibilityOffIcon /> :  <VisibilityIcon />}
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

                    {success && <p 
                                    className="text-yellow-400 mt-4 font-bold"
                                    style={{
                                        fontSize: "1.1rem",
                                        textAlign: "center",
                                        backgroundColor: "rgba(26, 31, 2, 0.11)",
                                        padding: "10px",
                                        borderRadius: "5px",
                                    }}>{success}</p>}
                    {error && <p 
                                className="text-red-600 mt-4 font-bold"
                                style={{
                                    fontSize: "1.1rem",
                                    textAlign: "center",
                                    backgroundColor: "rgba(255, 0, 0, 0.1)",
                                    padding: "10px",
                                    borderRadius: "5px",
                                }}>
                    {error}</p>}


                </div>

            </div>

        </div>
    )
}


export default LoginPage