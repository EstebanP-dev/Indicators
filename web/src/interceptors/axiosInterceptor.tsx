import axios, { InternalAxiosRequestConfig } from "axios"
import { AccountInfo } from "../models";
import { enviroment } from "../enviroments";

export const AxiosInterceptor = () => {
    const updateHeaders = (request: InternalAxiosRequestConfig<any>) => {
        const accountInfo: AccountInfo = JSON.parse(localStorage.getItem("accountInfo") as string);
        const token = accountInfo?.token ?? "";
        request.baseURL = enviroment.api;
        request.headers.Authorization = `Bearer ${token}`;
        request.headers["Content-Type"] = "application/json";
        console.log("Starting request:", request);
        return request;
    }

    axios.interceptors.request.use((request) => {
        return updateHeaders(request);
    })

    axios.interceptors.response.use(
        (response) => {
            console.log("Ending request:", response);
            return response;
        },
        (error) => {
            console.log("Error:", error);
            return Promise.reject(error);
        }
    )
}