import PeopleIcon from '@mui/icons-material/People';
import LocalShippingIcon from '@mui/icons-material/LocalShipping';
import HubIcon from '@mui/icons-material/Hub';
import InventoryIcon from '@mui/icons-material/Inventory';
import ErrorIcon from '@mui/icons-material/Error';

// Hard navigation routes
const categories = [
  {
    id: 'Quản Lý',
    children: [
      {
        id: 'Xe Vận Chuyển',
        icon: <LocalShippingIcon />,
        href: 'vehicles',
      },
      { id: 'Hub', icon: <HubIcon />, href: 'hubs' },
      { id: 'Đơn Hàng', icon: <InventoryIcon />, href: 'orders' },
      { id: 'Sự Cố', icon: <ErrorIcon />, href: 'accidents' },
      { id: 'Nhân Viên', icon: <PeopleIcon />, href: 'staffs' },
    ],
  },
];

export { categories };
