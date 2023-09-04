/* eslint-disable @typescript-eslint/ban-ts-comment */
import {
    AxiosError,
    AxiosInstance,
    AxiosRequestConfig,
    AxiosResponse,
} from 'axios'
import axios from 'axios'
import localStorageService from './localStorage.service'
import StoreKeys from '@/common/constants/storekeys'
import HttpStatusCode from '@/common/constants/httpStatusCode'
import axiosConfig from '@/common/configs/api.config'
import _omitBy from 'lodash/omitBy'

/** @class */
export default class HttpService {
    private instance: AxiosInstance

    constructor(config = axiosConfig) {
        const axiosConfigs = config

        const instance = axios.create({ ...axiosConfigs })
        Object.assign(instance, this.setupInterceptorsTo(instance))
        this.instance = instance
    }

    private async onRefreshToken() {
        const { refresh_token }: IAuthToken = localStorageService.get(
            StoreKeys.ACCESS_TOKEN,
            ''
        )
        if (refresh_token) {
            // TODO: handle refresh token
            return ''
        }
    }

    private onRequest = async (config: AxiosRequestConfig) => {
        return config
    }

    private onRequestError = (error: AxiosError): Promise<AxiosError> => {
        console.error(`[request error] [${JSON.stringify(error)}]`)
        return Promise.reject(error)
    }

    private onResponse = (response: AxiosResponse) => {
        return response.data
    }

    private onResponseError = (error: AxiosError): Promise<AxiosError> => {
        const statusCode = error?.response?.status
        switch (statusCode) {
            case HttpStatusCode.UNAUTHORIZED: {
                if (
                    typeof window !== 'undefined' &&
                    !window.location.pathname.startsWith('/auth')
                )
                    window.location.replace('/auth/sign-in')
                break
            }
        }
        return Promise.reject(error)
    }

    private setupInterceptorsTo(axiosInstance: AxiosInstance): AxiosInstance {
        axiosInstance.interceptors.request.use(
            // @ts-ignore
            this.onRequest,
            this.onRequestError
        )
        axiosInstance.interceptors.response.use(
            this.onResponse,
            this.onResponseError
        )
        return axiosInstance
    }

    public async get<T>(url: string, config?: AxiosRequestConfig): Promise<T> {
        return (await this.instance.get<T>(`${url}`, config)) as T
    }

    public async post<T>(url: string, data?: any, config?: AxiosRequestConfig) {
        return await this.instance.post<T>(url, data, config)
    }

    public async put<T>(url: string, data?: any, config?: AxiosRequestConfig) {
        return await this.instance.put<T>(url, data, config)
    }

    public async patch<T>(url: string, data: any, config?: AxiosRequestConfig) {
        return await this.instance.patch<T>(url, data, config)
    }

    public async delete(url: string, config?: AxiosRequestConfig) {
        return await this.instance.delete(url, config)
    }

    public setHttpConfigs(config?: Partial<AxiosRequestConfig>) {
        if (config?.baseURL) {
            this.instance.defaults.baseURL = config.baseURL
        }

        this.instance.defaults = {
            ...this.instance.defaults,
            ..._omitBy(config, 'BaseURL'),
        }
    }
}
