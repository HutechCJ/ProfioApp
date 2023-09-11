'use client';

import Stat, { StatProps } from '@/components/Stat';
import ErrorIcon from '@mui/icons-material/Error';
import InventoryIcon from '@mui/icons-material/Inventory';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import PeopleIcon from '@mui/icons-material/People';
import { Box, Grid, LinearProgress, Paper } from '@mui/material';

import useEntitiesCounter from '@/features/counter/useEntitiesCounter';

function OverviewStatsCard() {
  const {
    data: counterRes,
    isLoading,
    isError,
  } = useEntitiesCounter(['order', 'incident', 'staff', 'delivery']);

  if (isLoading) {
    return (
      <Box sx={{ width: '100%' }}>
        <LinearProgress />
      </Box>
    );
  }

  if (isError || !counterRes) {
    return null;
  }

  return (
    <Grid
      minHeight={128}
      container
      direction="row"
      spacing={1}
      columns={{ xs: 1, sm: 2, md: 4 }}
    >
      <Grid item xs={1}>
        <StatCard
          icon={<InventoryIcon />}
          iconColor="success.main"
          value={counterRes.data.order}
          description="Orders"
        />
      </Grid>
      <Grid item xs={1}>
        <StatCard
          icon={<ErrorIcon />}
          iconColor="red"
          value={counterRes.data.incident}
          description="Incidents"
        />
      </Grid>
      <Grid item xs={1}>
        <StatCard
          icon={<PeopleIcon />}
          iconColor="grey"
          value={counterRes.data.staff}
          description="Staffs"
        />
      </Grid>
      <Grid item xs={1}>
        <StatCard
          icon={<LocalShippingIcon />}
          iconColor="secondary.main"
          value={counterRes.data.delivery}
          description="Deliveries"
        />
      </Grid>
    </Grid>
  );
}

export default OverviewStatsCard;

function StatCard({ ...props }: StatProps) {
  return (
    <Paper
      sx={{
        padding: 4,
        height: '100%',
      }}
    >
      <Stat {...props} />
    </Paper>
  );
}
