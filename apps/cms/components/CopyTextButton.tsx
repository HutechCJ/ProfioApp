'use client';

import { IconButton, Tooltip } from '@mui/material';
import ContentCopyIcon from '@mui/icons-material/ContentCopy';
import { useSnackbar } from 'notistack';

interface CopyTextButtonProps {
  text?: string;
  titleTooltip?: string;
}

const CopyTextButton: React.FC<CopyTextButtonProps> = ({
  text = '',
  titleTooltip = 'Copy',
}) => {
  const { enqueueSnackbar } = useSnackbar();

  const handleClick = () => {
    navigator.clipboard.writeText(text);
    enqueueSnackbar('Copied to clipboard!', {
      variant: 'info',
    });
  };

  return (
    <>
      <Tooltip title={titleTooltip} arrow>
        <IconButton
          onClick={handleClick}
          size="medium"
          sx={{ '&:hover': { color: '#007dc3' } }}
        >
          <ContentCopyIcon sx={{ width: 15, height: 15 }} />
        </IconButton>
      </Tooltip>
    </>
  );
};

export default CopyTextButton;
