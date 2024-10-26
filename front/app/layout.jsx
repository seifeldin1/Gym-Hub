import '@styles/globals.css'

export const metadata = {
    title: "Fitness Tracker",
    description: "Enhance Your Workout"
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