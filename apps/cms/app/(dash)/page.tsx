import { Container, Grid } from '@mui/material';
import OverviewStatsCard from './OverviewStats';
import UserGreeting from './UserGreeting';
import VehicleTypePieChart from '@/components/charts/vehicle/VehicleTypePieChart';
// import VehicleStatusBarChart from '@/components/charts/vehicle/VehicleStatusBarChart';
import StaffPositionPieChart from '@/components/charts/staff/StaffPositionPieChart';
import OrderStatusPieChart from '@/components/charts/order/OrderStatusPieChart';

export default function Index() {
  return (
    <Container maxWidth="xl">
      <UserGreeting />
      <OverviewStatsCard />
      <Grid
        minHeight={128}
        container
        direction="row"
        spacing={1}
        columns={{ xs: 1, sm: 1, md: 2, xl: 3 }}
        mb={2}
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
