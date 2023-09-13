import { NextRequest } from 'next/server'

export async function GET(request: NextRequest) {
    request.nextUrl.searchParams.append('key', process.env.GOOGLE_MAP_API_KEY)
    console.log(process.env.GOOGLE_MAP_API_KEY)
    const url = new URL(
        `https://www.google.com/maps/embed/v1/directions?${request.nextUrl.searchParams}`
    )
    const res = await fetch(url)

    return res
}
