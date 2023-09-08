const axiosConfigs = {
    development: {
        baseURL: 'https://profio-sv1.azurewebsites.net/api/v1/',
        timeout: 10000,
    },
    production: {
        baseURL: 'https://profioapp.azurewebsites.net/api/v1/',
        timeout: 10000,
    },
    test: {
        baseURL: 'https://profio-sv1.azurewebsites.net/api/v1/',
        timeout: 10000,
    },
}
const getAxiosConfig = (): AxiosRequestConfig => {
    const nodeEnv: string = process.env.NODE_ENV
    return axiosConfigs[nodeEnv as keyof typeof axiosConfigs]
}

const axiosConfig = getAxiosConfig()

export default axiosConfig
