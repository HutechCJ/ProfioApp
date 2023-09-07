export async function POST(request: Request) {
    return new Response(null, {
      status: 200,
      headers: { 'Set-Cookie': `USER-TOKEN=; path=/; expires=Thu, 01 Jan 1970 00:00:00 GMT` },
    })
}
