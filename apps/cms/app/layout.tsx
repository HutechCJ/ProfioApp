import { Suspense } from 'react'
import ThemeRegistry from '../components/ThemeRegistry/ThemeRegistry'
import { NavigationEvents } from '@/components/navigation-events'

export const metadata = {
    title: 'Profio CMS',
    description:
        'ProfioApp is a repository dedicated to the development and maintenance of the Profio application, aimed at providing efficient and professional solutions for transportation management',
}

export default function RootLayout({
    children,
}: {
    children: React.ReactNode
}) {
    return (
        <html>
            <body>
                <ThemeRegistry>
                    {children}
                    <Suspense fallback={null}>
                        <NavigationEvents />
                    </Suspense>
                </ThemeRegistry>
            </body>
        </html>
    )
}
