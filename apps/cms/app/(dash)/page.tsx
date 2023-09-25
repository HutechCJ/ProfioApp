import { Container, Divider } from '@mui/material';
import OverviewStatsCard from './OverviewStats';
import UserGreeting from './UserGreeting';

export default function Index() {
  return (
    <Container maxWidth="xl">
      <UserGreeting />
      <OverviewStatsCard />
      <Divider sx={{ marginY: 2 }} />
    </Container>
  );
}
