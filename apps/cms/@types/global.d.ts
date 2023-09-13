type AxiosRequestConfig = import('axios').AxiosRequestConfig

interface IAuthToken {
    access_token?: string
    refresh_token?: string
    access_token_expires_in?: number
    access_token_expires_at?: number
}

type Nullable<T> = T | null

declare global {
    namespace NodeJS {
        interface ProcessEnv {
            GOOGLE_MAP_API_KEY: string
        }
    }
}

export {}
