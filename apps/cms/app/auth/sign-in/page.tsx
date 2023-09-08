'use client'

import {
    Alert,
    AlertTitle,
    Avatar,
    Box,
    Button,
    Checkbox,
    Container,
    FormControlLabel,
    Grid,
    Stack,
    TextField,
    Typography,
} from '@mui/material'
import LockOutlinedIcon from '@mui/icons-material/LockOutlined'
import React, { useEffect } from 'react'
import Link from '@/components/Link'
import Copyright from '@/components/Copyright'
import useLogin from '@/features/user/useLogin'
import LoadingButton from '@/components/LoadingButton'
import { useSnackbar } from 'notistack'
import { redirect } from 'next/navigation'

function SignIn() {
    const { enqueueSnackbar } = useSnackbar()
    const {
        mutate: login,
        data: result,
        isLoading,
        isSuccess,
        error,
        isError,
    } = useLogin()

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault()
        const data = new FormData(event.currentTarget)
        login({
            userName: data.get('userName') as string,
            password: data.get('password') as string,
        })
    }

    useEffect(() => {
        if (isSuccess) {
            enqueueSnackbar(`Logged in!`, { variant: 'success' })
            alert(`${result.data.toString()}`)
            window.location.reload()
        }
    }, [isSuccess])

    return (
        <Container component="main" maxWidth="xs">
            <Box
                sx={{
                    marginTop: 8,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}
            >
                <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Sign in
                </Typography>
                <Box
                    component="form"
                    onSubmit={handleSubmit}
                    noValidate
                    sx={{ mt: 1 }}
                >
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="userName"
                        label="Email"
                        name="userName"
                        autoComplete="userName"
                        autoFocus
                    />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        name="password"
                        label="Password"
                        type="password"
                        id="password"
                        autoComplete="current-password"
                    />
                    <FormControlLabel
                        control={<Checkbox value="remember" color="primary" />}
                        label="Remember me"
                    />
                    {isError && (
                        <Alert severity="error">
                            <AlertTitle>Error</AlertTitle>
                            <Stack>{(error as any).response?.data?.data || 'Unknown Error'}</Stack>
                        </Alert>
                    )}
                    <LoadingButton
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                        loading={isLoading}
                    >
                        Sign In
                    </LoadingButton>
                    <Grid container>
                        <Grid item xs>
                            <Link href="#" variant="body2">
                                Forgot password?
                            </Link>
                        </Grid>
                        <Grid item>
                            <Link href="/auth/sign-up" variant="body2">
                                {"Don't have an account? Sign Up"}
                            </Link>
                        </Grid>
                    </Grid>
                </Box>
            </Box>
            <Copyright sx={{ mt: 8, mb: 4 }} />
        </Container>
    )
}

export default SignIn
