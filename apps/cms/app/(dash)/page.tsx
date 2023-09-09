'use client';

import useUser from '@/features/user/useUser';

export default function Index() {
  const user = useUser();
  return <div>Hello {`${user?.fullName}`}!</div>;
}
