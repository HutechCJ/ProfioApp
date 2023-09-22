import { Typography, TypographyProps } from '@mui/material';
import Link from './Link';

function Copyright({ ...props }: TypographyProps) {
  return (
    <Typography
      variant="body2"
      color="text.secondary"
      align="center"
      {...props}
    >
      {'Copyright © '}
      <Link
        color="#b7ec2"
        href="https://profio-cms.onrender.com/"
        style={{
          textDecoration: 'none',
          fontWeight: 'bold',
        }}
      >
        Profio
      </Link>{' '}
      {new Date().getFullYear()}. All rights reserved.
    </Typography>
  );
}
export default Copyright;
