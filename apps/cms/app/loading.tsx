import React from 'react'
import { Box, CircularProgress } from '@mui/material'

function Loading() {
    return (
        <Box sx={{ display: 'flex' }}>
            <CircularProgress />
        </Box>
    )
}

export default Loading
