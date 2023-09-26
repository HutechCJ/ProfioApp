import Swal from 'sweetalert2';
import withReactContent from 'sweetalert2-react-content';

function useSwal() {
  return withReactContent(Swal);
}

export default useSwal;
