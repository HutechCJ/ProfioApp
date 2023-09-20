import { Container, Divider } from '@mui/material';
import OverviewStatsCard from './OverviewStats';
import UserGreeting from './UserGreeting';
import OrderStatisticsCard from './orders/OrderStatisticsCard';
import VehicleStatisticsCard from './vehicles/VehicleStatisticsCard';
import StaffStatisticsCard from './staffs/StaffStatisticsCard';

export default function Index() {
  return (
    <Container maxWidth="xl">
      <UserGreeting />
      <OverviewStatsCard />
      <Divider sx={{ marginY: 2 }} />
      <OrderStatisticsCard />
      <Divider sx={{ marginY: 2 }} />
      <VehicleStatisticsCard />
      <Divider sx={{ marginY: 2 }} />
      <StaffStatisticsCard />
    </Container>
  );
}
