'use client'

import useUser from '@/features/user/useUser'
import { Typography, Divider } from '@mui/material'

function UserGreeting() {
    const user = useUser()
    if (!user) return null
    return (
        <>
            <Typography variant="h5" component="h5">
                {`Hello ${user.fullName}!`}
            </Typography>
            <Divider sx={{ marginBottom: 2 }} />
        </>
    )
}

export default UserGreeting
