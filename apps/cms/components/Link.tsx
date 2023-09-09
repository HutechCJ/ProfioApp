import React, { PropsWithChildren } from 'react';

import MUILink, { LinkProps } from '@mui/material/Link';
import NextLink from 'next/link';

function Link({ children, ...props }: PropsWithChildren & LinkProps) {
  return (
    <MUILink component={NextLink} {...props}>
      {children}
    </MUILink>
  );
}

export default Link;
