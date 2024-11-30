'use client'
import React , {useState} from "react";
import { Button, IconButton, TextField } from "@node_modules/@mui/material";
import VisibilityIcon from '@mui/icons-material/Visibility';
import VisibilityOffIcon from '@mui/icons-material/VisibilityOff';
import LightModeIcon from '@mui/icons-material/LightMode';
import DarkModeIcon from '@mui/icons-material/DarkMode';
import style from '@styles/login.module.css'

const LoginPage= ()=>{
    const [username, setUsername]= useState("") 
    const [password , setPassword] = useState("")
    const [UsernameError, setUsernameError] = useState(null)
    const [PasswordError, setPasswordError] = useState(null)
    const [success, setSuccess] = useState(null)
    const [darkMode , setDrkMode]= useState(false)
    const [loading, setLoading] = useState(false)
    const [showPassword, setShowPassword] = useState(false)

    const ChangeDarkMode = ()=>{setDrkMode(!darkMode)}
    const ChangePasswordVisibility = ()=>{setShowPassword(!showPassword)}
    const LoginHandler= async (event)=>{
        event.preventDefault()
        setUsernameError("")
        setPasswordError("")
        setSuccess("")
        if(!username || !password){
            if(!username){
                setUsernameError("Please enter your username")
                
            }
            if(!password){
                setPasswordError("Please enter your password")
                
            }
            return 
        }
        setLoading(true)

        try{
            // const response = await axios.post(/*api*/ , {
            //     username: username,
            //     password: password
            // })

            setSuccess("Login Successful! Redirecting....")
            //TODO: Redirect to the dashboard 
        }catch(error){
            setError("Invalid username or password")
        }finally{
            setLoading(false)
        } 
    }

    return(
        /*main page */
        <div className={`min-h-screen flex justify-start p-8 items-center bg-cover ${style.background}`} >
            {/*form of login*/}
           
            <div className={`relative bg-white p-8 rounded-lg shadow-lg w-full max-w-lg ${darkMode?`bg-slate-700 text-black`: `bg-white text-black` }`}>
                <IconButton
                 onClick ={ChangeDarkMode}
                 color="inherit"
                 className={`absolute top-4 right-4 ${darkMode? `bg-slate-300`:`bg-white`}`}
                 >
                    {darkMode? <DarkModeIcon/> : <LightModeIcon/>}
                 </IconButton>
                 <div className= "flex justify-between items-center mb-10 " >
                    <div className="flex1">
                        <h2 className={`text-xl sm:text-2xl font-bold mb-5 text-center ${darkMode? `text-white`:`text-black`}`}>
                            Log in 
                        </h2>
                        <form onSubmit={LoginHandler}>
                            <div className="mb-4">
                                <TextField
                                label="Username"
                                variant="outlined"
                                value={username}
                                fullWidth
                                onChange={(e) => setUsername(e.target.value)}
                                className={`${darkMode? `text-white`:`text-black`}`}>
                                </TextField>
                                {UsernameError && !username && <p className="text-red-700">{UsernameError}</p>}
                            </div>
                            <div className="mb-4">
                                <TextField
                                label="Password"
                                variant="outlined"
                                type={showPassword? 'text':'password'}
                                value={password}
                                fullWidth
                                onChange={(e) => setPassword(e.target.value)}
                                className={`${darkMode? `text-white`:`text-black`}`}>

                                </TextField>
                                <div>
                                {PasswordError && !password && <p className="text-red-700">{PasswordError}</p>}

                                <IconButton
                                onClick={ChangePasswordVisibility}
                                className="absolute left-1/2 transform translate-x-1/2 mt-2">
                                {showPassword? <VisibilityIcon/> : <VisibilityOffIcon/>}
                                </IconButton>
                                </div>
                            </div>
                            <div>
                            <Button
                            variant="outlined"
                            type="submit"
                            color="primary"
                            disabled={loading}
                            onClick={LoginHandler}
                            fullwidth>
                                {loading ? "Logging in..." : "Log in"}
                            </Button>
                           


                        </div>
                        </form>
                        {success && <p className="text-green-500 mt-2">{success}</p>}

                        
                    </div>

                 </div>
            
            </div>

        </div>
    )
}


export default LoginPage