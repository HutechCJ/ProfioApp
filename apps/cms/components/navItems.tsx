import PeopleIcon from '@mui/icons-material/People';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import HubIcon from '@mui/icons-material/Hub';
import InventoryIcon from '@mui/icons-material/Inventory';
import ErrorIcon from '@mui/icons-material/Error';
import AdminPanelSettingsIcon from '@mui/icons-material/AdminPanelSettings';
import AssignmentIndIcon from '@mui/icons-material/AssignmentInd';
import SettingsIcon from '@mui/icons-material/Settings';

// Hard navigation routes
const categories = [
  {
    id: 'People',
    children: [
      {
        id: 'User permissions',
        icon: <AdminPanelSettingsIcon />,
        href: 'users',
      },
      { id: 'Staffs', icon: <PeopleIcon />, href: 'staffs' },
      {
        id: 'Customers',
        icon: <AssignmentIndIcon />,
        href: 'customers',
      },
    ],
  },
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
    ],
  },
  {
    id: 'Account',
    children: [
      {
        id: 'Settings',
        icon: <SettingsIcon />,
        href: 'settings',
      },
    ],
  },
];

export { categories };
