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
    const [error, setError] = useState(null)
    const [success, setSuccess] = useState(null)
    const [darkMode , setDrkMode]= useState(false)
    const [loading, setLoading] = useState(false)
    const [showPassword, setShowPassword] = useState(false)

    const ChangeDarkMode = ()=>{!darkMode}
    const ChangePasswordVisibility = ()=>{!showPassword}
    const LoginHandler= async (event)=>{
        event.preventDefault()
        setError("")
        setSuccess("")
        if(!username || !password){
            if(!username){
                setError("Please enter your username")
                return
            }
            else{
                setError("Please enter your password")
                return 
            }
        }
        setLoading(true)

        // try{
        //     const response = await axios.post(/*api*/ , {
        //         username: username,
        //         password: password
        //     })

        //     setSuccess("Login Successful! Redirecting....")
        //     //TODO: Redirect to the dashboard 
        // }catch(error){
        //     setError("Invalid username or password")
        // }finally{
        //     setLoading(false)
        // } 
    }

    return(
        /*main page */
        <div className={`min-h-screen flex justify-center items-center bg-cover ${style.background}`} >
            {/*form of login*/}
           
            <div className={`relative bg-white p-8 rounded-lg shadow-lg w-full max-w-lg ${darkMode?`bg-slate-700 text-white`: `bg-white text-black` }`}>
                <IconButton
                 onClick ={ChangeDarkMode}
                 color="inherit"
                 className="absolute top-4 right-4"
                 >
                    {darkMode? <DarkModeIcon/> : <LightModeIcon/>}
                 </IconButton>
                 <div className="flex justify-between items-center mb-10">
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
                                className={`${darkMode? `bg-slate-400 text-white`:`bg-white text-black`}`}>
                                </TextField>
                            </div>
                            <div className="mb-4">
                                <TextField
                                label="Password"
                                variant="outlined"
                                type={showPassword? text:password}
                                value={password}
                                fullWidth
                                onChange={(e) => setPassword(e.target.value)}
                                className={`${darkMode? `bg-slate-400 text-white`:`bg-white text-black`}`}>

                                </TextField>
                                <IconButton
                                onClick={() => setShowPassword(!showPassword)}
                                className="absolute left-1/2 transform -translate-x-1/2 mt-2">
                                {showPassword? <VisibilityIcon/> : <VisibilityOffIcon/>}
                                </IconButton>
                            </div>
                        </form>
                        <div>
                            <Button
                            variant="outlined"
                            type="submit"
                            color="primary"
                            disabled={loading}
                            fullwidth>

                            </Button>

                        </div>
                    </div>

                 </div>
            </div>

        </div>
    )
}


export default LoginPage