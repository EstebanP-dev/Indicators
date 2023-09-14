import { AccountInfo, ErrorOr, Response } from "../models";
import axios, { AxiosRequestConfig } from "axios";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { AppStore } from "../redux/store";
import { ExceptionMessages } from "../messaging";

const useAxiosApi = (abortController: AbortController) => {
    const accountInfoStored: AccountInfo = useSelector((store: AppStore) => store.accountInfo);
    const [loading, setLoading] = useState(false);
    
    const configuration: AxiosRequestConfig<any> = {
        signal: abortController.signal,
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': `Bearer ${accountInfoStored?.token ?? ''}`
        }
    };

    const MapData = <T>(result: any): Response<T> => {
        const data: T = JSON.parse(JSON.stringify(result.data));
        return new Response<T>(result.status, data, undefined);
    }
    
    const MapError = <T>(err: any): Response<T> => {
        console.log(err);
        const data: ErrorOr = JSON.parse(JSON.stringify(err?.response?.data ?? {
            status: err?.response?.status,
            title: ExceptionMessages.UNKNOWN
        }));
        return new Response<T>(err?.response?.status ?? 500, undefined, data);
    }

    const getService = <T>(url: string): Promise<Response<T>> => {
        return axios.get(url, configuration)
        .then((res) => {
            return MapData<T>(res);
        })
        .catch((error) => {
            return MapError<T>(error);
        })
    }

    const postService = <T>(url: string, requestData: any | undefined): Promise<Response<T>> => {
        return axios.post(url, requestData, configuration)
        .then((res) => {
            return MapData<T>(res);
        })
        .catch((error) => {
            return MapError<T>(error);
        })
    }
    
    const putService = <T>(url: string, requestData: any | undefined): Promise<Response<T>> => {
        return axios.put(url, requestData, configuration)
        .then((res) => {
            return MapData<T>(res);
        })
        .catch((error) => {
            return MapError<T>(error);
        })
    }
    
    const patchService = <T>(url: string, requestData: any | undefined): Promise<Response<T>> => {
        return axios.patch(url, requestData, configuration)
        .then((res) => {
            return MapData<T>(res);
        })
        .catch((error) => {
            return MapError<T>(error);
        })
    }
    
    const deleteService = <T>(url: string): Promise<Response<T>> => {
        return axios.delete(url, configuration)
        .then((res) => {
            return MapData<T>(res);
        })
        .catch((error) => {
            return MapError<T>(error);
        })
    }

    const cancelEndpoint = () => {
        setLoading(false);
        abortController && abortController.abort();
    };

    const callEndpoint = async <T>(axiosCall: Promise<T>) => {
        setLoading(true);

        let result: T;
        try {
            result = await axiosCall;
        }
        catch (err: any) {
            setLoading(false);
            throw err;
        }
        finally {
            setLoading(false);
        }

        return result
    };

    useEffect(() => {
        return () => {
            cancelEndpoint();
        };
    }, []);

    return {
        loading,
        callEndpoint,
        cancelEndpoint,
        getService,
        postService,
        putService,
        patchService,
        deleteService
    }
}

export default useAxiosApi;