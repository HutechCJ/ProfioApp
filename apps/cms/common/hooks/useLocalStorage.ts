'use client';

import { useState } from 'react';
import localStorageService from '../services/localStorage.service';

function useLocalStorage() {
  const [service] = useState(localStorageService);
  return service;
}

export default useLocalStorage;
