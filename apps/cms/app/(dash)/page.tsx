'use client'

import useUser from "@/features/user/useUser"

export default function Index() {
    const user = useUser()
    if (!user) return null
    return <div>Hello {`${user?.fullName}`}!</div>
}
