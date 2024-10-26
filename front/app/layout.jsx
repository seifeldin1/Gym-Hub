import '@styles/globals.css'

export const metadata = {
    title: "Company Management",
    description: "Dashboard For Company"
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