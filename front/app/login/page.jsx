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
        <div className={`min-h-screen flex justify-center  p-8 items-center bg-cover ${style.background} `} >
            {/*form of login*/}
           
            <div className={`relative bg-white p-4 items-center rounded-lg shadow-lg w-80 max-w-lg ${darkMode? 'bg-slate-800 text-white': 'bg-slate-700 text-black' }`}>
                <IconButton
                 onClick ={ChangeDarkMode}
                 color="inherit"
                 className={`absolute top-8 right-1 ${darkMode? `bg-gray-800`:`bg-white`}`}
                 >
                    {darkMode? <LightModeIcon/> : <DarkModeIcon/>}
                 </IconButton>
                 <div className= {`flex justify-between w-64 items-center mb-10 ${darkMode? `bg-gray-800 text-white`:`text-black`} `} >
                    <div className="flex1">
                        <h2 className={`text-xl sm:text-2xl font-bold mb-5 text-center ${darkMode? `bg-gray-800 text-white`:`text-black`}`}>
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
                                    style: { color: darkMode ? 'grey' : 'black' },
                                }}
                                InputProps={{
                                    style: { color: darkMode ? 'white' : 'black', backgroundColor: darkMode ? '#374151' : '#ffffff' , height:'50px'},
                                }}
                                className="rounded">
                                </TextField>
                                {UsernameError && !username && <p className="text-red-700">{UsernameError}</p>}
                            </div>
                            <div className={`flex ${darkMode? `text-white`:`text-black`}`}>
                                <TextField
                                label="Password"
                                variant="outlined"
                                type={showPassword? 'text':'password'}
                                value={password}
                                fullWidth
                                onChange={(e) => setPassword(e.target.value)}
                                InputLabelProps={{
                                    style: { color: darkMode ? 'grey' : 'black' },
                                }}
                                InputProps={{
                                    style: { color: darkMode ? 'white' : 'black', backgroundColor: darkMode ? '#374151' : '#ffffff' , height:'50px' },
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
                            color="105269"
                            disabled={loading}
                            onClick={LoginHandler}
                            fullwidth
                            className="w-60">
                            
                                {loading ? "Logging in..." : "Login"}
                            </Button>
                        </div>
                       
                        {success && <p className="text-green-500 mt-2">{success}</p>}

                        
                    </div>

                 </div>
            
            </div>

        </div>
    )
}


export default LoginPage