import PeopleIcon from '@mui/icons-material/People';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import HubIcon from '@mui/icons-material/Hub';
import InventoryIcon from '@mui/icons-material/Inventory';
import ErrorIcon from '@mui/icons-material/Error';

// Hard navigation routes
const categories = [
  {
    id: 'Management',
    children: [
      {
        id: 'Vehicles',
        icon: <LocalShippingIcon />,
        href: 'vehicles',
      },
      { id: 'Hubs', icon: <HubIcon />, href: 'hubs' },
      { id: 'Orders', icon: <InventoryIcon />, href: 'orders' },
      { id: 'Incidents', icon: <ErrorIcon />, href: 'incidents' },
      { id: 'Staffs', icon: <PeopleIcon />, href: 'staffs' },
    ],
  },
];

export { categories };
