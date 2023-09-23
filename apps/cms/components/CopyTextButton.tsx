'use client';

import { IconButton } from '@mui/material';
import ContentCopyIcon from '@mui/icons-material/ContentCopy';
import { useSnackbar } from 'notistack';

interface CopyTextButtonProps {
  text?: string;
}

const CopyTextButton: React.FC<CopyTextButtonProps> = ({ text = '' }) => {
  const { enqueueSnackbar } = useSnackbar();

  const handleClick = () => {
    navigator.clipboard.writeText(text);
    enqueueSnackbar('Copied to clipboard!', {
      variant: 'info',
    });
  };

  return (
    <>
      <IconButton onClick={handleClick} size="medium">
        <ContentCopyIcon sx={{ width: 15, height: 15 }} />
      </IconButton>
    </>
  );
};

export default CopyTextButton;
