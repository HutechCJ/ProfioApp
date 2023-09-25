import { Container, Divider, Grid } from '@mui/material';
import OverviewStatsCard from './OverviewStats';
import UserGreeting from './UserGreeting';
import VehicleTypePieChart from '@/components/charts/vehicle/VehicleTypePieChart';
// import VehicleStatusBarChart from '@/components/charts/vehicle/VehicleStatusBarChart';
import StaffPositionPieChart from '@/components/charts/staff/StaffPositionPieChart';
import OrderStatusPieChart from '@/components/charts/order/OrderStatusPieChart';
import StaffStatisticsCard from './staffs/StaffStatisticsCard';
import OrderStatisticsCard from './orders/OrderStatisticsCard';

export default function Index() {
  return (
    <Container maxWidth="xl">
      <UserGreeting />
      <OverviewStatsCard />
      <Divider sx={{ marginY: 2 }} />
      <Grid
        minHeight={128}
        container
        direction="row"
        spacing={1}
        columns={{ xs: 1, sm: 1, md: 2, xl: 3 }}
      >
        <Grid item xs={1}>
          <OrderStatusPieChart />
        </Grid>
        <Grid item xs={1}>
          <StaffPositionPieChart />
        </Grid>
        <Grid item xs={1}>
          <VehicleTypePieChart />
        </Grid>
        {/* <Grid item xs={1}>
          <VehicleStatusBarChart />
        </Grid> */}
      </Grid>
    </Container>
  );
}
