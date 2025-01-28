import '../styles/globals.css';
import { config } from '@fortawesome/fontawesome-svg-core';
config.autoAddCss = false; // Disable automatic CSS injection

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
      <body>
        <main>{children}</main>
      </body>
    </html>
  );
}
