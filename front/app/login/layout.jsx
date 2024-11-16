import '@styles/globals.css'

export const metadata = {
    title: "Login",
    description: "Login For Gym Hub"
}

const RootLayout = ({children}) => {
  return (
    <html lang='en'>
        <body>
            <main>
                {children}
            </main>
        </body>
    </html>
  )
}

export default RootLayout