'use client';

import React from 'react';
import {
  QueryClientProvider,
  QueryClient,
  MutationCache,
} from '@tanstack/react-query';
// import { ReactQueryDevtools } from '@tanstack/react-query-devtools';
import { ReactQueryStreamedHydration } from '@tanstack/react-query-next-experimental';
import { useSnackbar } from 'notistack';

function Providers({ children }: React.PropsWithChildren) {
  const { enqueueSnackbar } = useSnackbar();
  const [client] = React.useState(
    new QueryClient({
      mutationCache: new MutationCache({
        onError(error, _variables, _context, mutation) {
          if (mutation.options.onError) return;

          enqueueSnackbar(`${(error as any).message}`, {
            variant: 'error',
          });
        },
      }),
    }),
  );

  return (
    <QueryClientProvider client={client}>
      {/* <RouterTransition/> */}
      <ReactQueryStreamedHydration>{children}</ReactQueryStreamedHydration>
      {/* <ReactQueryDevtools initialIsOpen={false} /> */}
    </QueryClientProvider>
  );
}

export default Providers;
