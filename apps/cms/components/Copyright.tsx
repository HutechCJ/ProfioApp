import { Typography, TypographyProps } from '@mui/material'
import Link from './Link'

function Copyright({ ...props }: TypographyProps) {
    return (
        <Typography
            variant="body2"
            color="text.secondary"
            align="center"
            {...props}
        >
            {'Copyright © '}
            <Link color="inherit" href="https://profio.com/">
                Profio
            </Link>{' '}
            {new Date().getFullYear()}.
        </Typography>
    )
}
export default Copyright
