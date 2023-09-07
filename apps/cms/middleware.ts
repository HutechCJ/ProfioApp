import { NextRequest, NextResponse } from 'next/server'

// Limit the middleware to paths starting with `/api/`
export const config = {
    matcher: ['/((?!api|_next/static|_next/image|favicon.ico).*)'],
}

export function middleware(request: NextRequest) {
    const userToken = request.cookies.get('USER-TOKEN')?.value
    const authPath = '/auth'
    const signInPath = authPath + '/sign-in'

    if (!userToken && request.nextUrl.pathname.includes('/auth')) {
        return NextResponse.next()
    }

    if (userToken) {
        return request.nextUrl.pathname.includes("/auth")
            ? NextResponse.redirect(new URL('/', request.url))
            : NextResponse.next()
    }

    return NextResponse.redirect(new URL("/auth/sign-in", request.url))
}
