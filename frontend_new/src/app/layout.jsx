import '../styles/globals.css';

export const metadata = {
  title: 'Pulse Fit',
  description: 'Gym Management System',
};

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <head>
        {/* Include metadata */}
        <title>{metadata.title}</title>
        <meta name="description" content={metadata.description} />
      </head>
      <body cz-shortcut-listen="true">
        <main>{children}</main>
      </body>
    </html>
  );
}
