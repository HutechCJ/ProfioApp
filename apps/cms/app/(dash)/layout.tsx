import Paperbase from '../../components/paperbase/Paperbase';

export default function DashLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return <Paperbase>{children}</Paperbase>;
}
