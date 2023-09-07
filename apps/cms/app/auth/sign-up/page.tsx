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
import useRegister from '@/features/user/useRegister'
import { useSnackbar } from 'notistack'
import LoadingButton from '@/components/LoadingButton'
import { redirect } from 'next/navigation'

function SignUp() {
    const { enqueueSnackbar } = useSnackbar()
    const {
        mutate: register,
        data: result,
        error,
        isLoading,
        isError,
        isSuccess,
    } = useRegister()

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault()
        const data = new FormData(event.currentTarget)
        register({
            userName: data.get('userName') as string,
            email: data.get('email') as string,
            fullName: data.get('fullName') as string,
            password: data.get('password') as string,
            confirmPassword: data.get('confirmPassword') as string,
        })
    }

    useEffect(() => {
        if (isSuccess) {
            enqueueSnackbar(`Registered! Redirecting you to signin page...`, { variant: 'success' })
            redirect("/auth/sign-in")
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
                    Sign up
                </Typography>
                <Box
                    component="form"
                    noValidate
                    onSubmit={handleSubmit}
                    sx={{ mt: 3 }}
                >
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <TextField
                                autoComplete="given-name"
                                name="fullName"
                                required
                                fullWidth
                                id="fullName"
                                label="Full Name"
                                autoFocus
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                required
                                fullWidth
                                id="userName"
                                label="Username"
                                name="userName"
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                required
                                fullWidth
                                id="email"
                                label="Email Address"
                                name="email"
                                autoComplete="email"
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                required
                                fullWidth
                                name="password"
                                label="Password"
                                type="password"
                                id="password"
                                autoComplete="new-password"
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                required
                                fullWidth
                                name="confirmPassword"
                                label="Confirm Password"
                                type="confirmPassword"
                                id="confirmPassword"
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        value="allowExtraEmails"
                                        color="primary"
                                    />
                                }
                                label="I want to receive inspiration, marketing promotions and updates via email."
                            />
                        </Grid>
                    </Grid>
                    {isError && (
                        <Alert severity="error">
                            <AlertTitle>Error</AlertTitle>
                            <Stack>
                                {Object.values(
                                    (error as any).response.data.data as {
                                        [key: string]: any
                                    }
                                ).flat().map((value, i) => (
                                    <span key={`error_${i}`}>
                                        {value}
                                    </span>
                                ))}
                            </Stack>
                        </Alert>
                    )}
                    <LoadingButton
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                        loading={isLoading}
                    >
                        Sign Up
                    </LoadingButton>
                    <Grid container justifyContent="flex-end">
                        <Grid item>
                            <Link href="/auth/sign-in" variant="body2">
                                Already have an account? Sign in
                            </Link>
                        </Grid>
                    </Grid>
                </Box>
            </Box>
            <Copyright sx={{ mt: 5 }} />
        </Container>
    )
}

export default SignUp
