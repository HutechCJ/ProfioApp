'use client';

import Stat, { StatProps } from '@/components/Stat';
import ErrorIcon from '@mui/icons-material/Error';
import InventoryIcon from '@mui/icons-material/Inventory';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import PeopleIcon from '@mui/icons-material/People';
import { Box, Grid, LinearProgress, Paper } from '@mui/material';

import useEntitiesCounter from '@/features/counter/useEntitiesCounter';
import Link from '@/components/Link';

function OverviewStatsCard() {
  const {
    data: counterRes,
    isLoading,
    isError,
  } = useEntitiesCounter(['order', 'incident', 'staff', 'delivery', 'vehicle']);

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
      columns={{ xs: 1, sm: 2, md: 5 }}
    >
      <Grid item xs={1}>
        <Link href="/orders" sx={{ textDecoration: 'none' }}>
          <StatCard
            icon={<InventoryIcon />}
            iconColor="success.main"
            value={counterRes.data.order}
            description="Orders"
          />
        </Link>
      </Grid>
      <Grid item xs={1}>
        <Link href="/incidents" sx={{ textDecoration: 'none' }}>
          <StatCard
            icon={<ErrorIcon />}
            iconColor="red"
            value={counterRes.data.incident}
            description="Incidents"
          />
        </Link>
      </Grid>
      <Grid item xs={1}>
        <Link href="/staffs" sx={{ textDecoration: 'none' }}>
          <StatCard
            icon={<PeopleIcon />}
            iconColor="orange"
            value={counterRes.data.staff}
            description="Staffs"
          />
        </Link>
      </Grid>
      <Grid item xs={1}>
        <Link href="/deliveries" sx={{ textDecoration: 'none' }}>
          <StatCard
            icon={<LocalShippingIcon />}
            iconColor="secondary.main"
            value={counterRes.data.delivery}
            description="Deliveries"
          />
        </Link>
      </Grid>
      <Grid item xs={1}>
        <Link href="/vehicles" sx={{ textDecoration: 'none' }}>
          <StatCard
            icon={<LocalShippingIcon />}
            iconColor="blue"
            value={counterRes.data.vehicle}
            description="Vehicles"
          />
        </Link>
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
