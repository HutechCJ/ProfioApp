'use client';

import { Box, Typography } from '@mui/material';
import React, { useState, useEffect } from 'react';

const DigitalClock: React.FC = () => {
  const [ctime, setTime] = useState<string>(new Date().toLocaleTimeString());

  useEffect(() => {
    const intervalId = setInterval(() => {
      const newTime = new Date().toLocaleTimeString();
      setTime(newTime);
    }, 1000);

    return () => {
      clearInterval(intervalId);
    };
  }, []);

  return (
    <Box>
      <Typography variant="h5">{ctime}</Typography>
    </Box>
  );
};

export default DigitalClock;
