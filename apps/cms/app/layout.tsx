import Providers from '@/components/providers/Providers';
import ThemeRegistry from '../components/ThemeRegistry/ThemeRegistry';

export const metadata = {
  title: 'Profio CMS',
  description:
    'ProfioApp is a repository dedicated to the development and maintenance of the Profio application, aimed at providing efficient and professional solutions for transportation management',
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
