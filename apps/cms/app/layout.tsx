import Providers from '@/components/providers/Providers';
import ThemeRegistry from '../components/ThemeRegistry/ThemeRegistry';
import { Metadata } from 'next';
import "./global.css"

export const metadata: Metadata = {
  title: 'Profio CMS',
  description:
    'ProfioApp is a repository dedicated to the development and maintenance of the Profio application, aimed at providing efficient and professional solutions for transportation management',
  openGraph: {
    images: '/images/web-preview.jpg',
    siteName: 'Profio CMS',
    locale: 'vi_VN',
    type: 'website',
    description:
      'ProfioApp is a repository dedicated to the development and maintenance of the Profio application, aimed at providing efficient and professional solutions for transportation management',
  },
  viewport:
    'minimum-scale=1, initial-scale=1, width=device-width, shrink-to-fit=no, user-scalable=no, viewport-fit=cover',
  applicationName: 'Profio Logistics CMS Application',
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="vi">
      <body>
        <Providers>
          <ThemeRegistry>{children}</ThemeRegistry>
        </Providers>
      </body>
    </html>
  );
}
