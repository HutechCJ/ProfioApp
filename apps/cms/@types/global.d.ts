declare global {
  namespace NodeJS {
    interface ProcessEnv {
      NEXT_PUBLIC_GOOGLE_MAP_API_KEY: string;
      API_BASEURL: string;
      SERVER_HOSTNAME: string;
    }
  }
}

export {};
