import { Suspense } from 'react'
import ThemeRegistry from '../components/ThemeRegistry/ThemeRegistry'
import { NavigationEvents } from '@/components/navigation-events'
import Providers from '@/components/providers/Providers'

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
                <Providers>
                    <ThemeRegistry>
                        {children}
                        <Suspense fallback={null}>
                            <NavigationEvents />
                        </Suspense>
                    </ThemeRegistry>
                </Providers>
            </body>
        </html>
    )
}
