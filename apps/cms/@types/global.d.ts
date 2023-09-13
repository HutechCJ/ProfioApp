declare global {
  namespace NodeJS {
    interface ProcessEnv {
      GOOGLE_MAP_API_KEY: string;
    }
  }
}

export {};
