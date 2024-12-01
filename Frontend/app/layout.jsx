import '@styles/globals.css'

export const metadata = {
    title: "Gym Hub",
    description: "Landpage of Gym Hub"
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