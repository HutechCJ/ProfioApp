import { Container, Divider, Grid } from '@mui/material';
import OverviewStatsCard from './OverviewStats';
import UserGreeting from './UserGreeting';
import VehicleTypePieChart from '@/components/charts/vehicle/VehicleTypePieChart';
import VehicleStatusBarChart from '@/components/charts/vehicle/VehicleStatusBarChart';

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
        columns={{ xs: 1, sm: 1, md: 2 }}
      >
        <Grid item xs={1}>
          <VehicleTypePieChart />
        </Grid>
        <Grid item xs={1}>
          <VehicleStatusBarChart />
        </Grid>
      </Grid>
    </Container>
  );
}
