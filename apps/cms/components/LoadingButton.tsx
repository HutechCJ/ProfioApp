import { Button, ButtonProps, CircularProgress } from '@mui/material'
import React from 'react'

function LoadingButton({
    loading = false,
    children,
    size,
    ...props
}: ButtonProps & { loading?: boolean }) {
    const getProgressSize = () => {
        return size === 'large' ? 30 : size === 'small' ? 10 : 20
    }
    return (
        <Button disabled={loading} {...props}>
            {loading ? <CircularProgress size={getProgressSize()} /> : children}
        </Button>
    )
}

export default LoadingButton
