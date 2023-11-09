import { ErrorOr, Response } from "../models";
import axios, { HttpStatusCode } from "axios";
import { useEffect } from "react";
import { AuthMessages, ExceptionMessages } from "../messaging";
import { SnackbarUtilities } from "../utilities";
import { NavigateFunction } from "react-router-dom";
import { PublicRoutes } from "../enviroments";
import { Dispatch } from "@reduxjs/toolkit";
import { resetAccountInfo } from "../redux/states/accountInfo";
import { hideLoading, showLoading } from "../redux/states/loadingData";

const useAxiosApi = (
  abortController: AbortController,
  navigate: NavigateFunction,
  dispatch: Dispatch
) => {
  const MapData = <T>(result: any): Response<T> => {
    const data: T = JSON.parse(JSON.stringify(result.data));
    return new Response<T>(result.status, data, undefined);
  };

  const MapError = <T>(err: any): Response<T> => {
    let data: string = err?.response?.data?.title ?? err?.response?.data ?? "";
    const error: ErrorOr = {
      status: err?.response?.status,
      title: data !== "" ? data : ExceptionMessages.UNKNOWN,
    };
    return new Response<T>(err?.response?.status ?? 500, undefined, error);
  };

  const getService = <T>(url: string): Promise<Response<T>> => {
    return axios
      .get(url)
      .then((res) => {
        return MapData<T>(res);
      })
      .catch((error) => {
        return MapError<T>(error);
      });
  };

  const postService = <T>(
    url: string,
    requestData: any | undefined
  ): Promise<Response<T>> => {
    return axios
      .post(url, requestData)
      .then((res) => {
        return MapData<T>(res);
      })
      .catch((error) => {
        return MapError<T>(error);
      });
  };

  const putService = <T>(
    url: string,
    requestData: any | undefined
  ): Promise<Response<T>> => {
    return axios
      .put(url, requestData)
      .then((res) => {
        return MapData<T>(res);
      })
      .catch((error) => {
        return MapError<T>(error);
      });
  };

  const patchService = <T>(
    url: string,
    requestData: any | undefined
  ): Promise<Response<T>> => {
    return axios
      .patch(url, requestData)
      .then((res) => {
        return MapData<T>(res);
      })
      .catch((error) => {
        return MapError<T>(error);
      });
  };

  const deleteService = <T>(url: string): Promise<Response<T>> => {
    return axios
      .delete(url)
      .then((res) => {
        return MapData<T>(res);
      })
      .catch((error) => {
        return MapError<T>(error);
      });
  };

  const callEndpoint = <T>(
    axiosCall: Promise<Response<T>>,
    setData?: React.Dispatch<React.SetStateAction<T | undefined>>,
    setError?: React.Dispatch<React.SetStateAction<ErrorOr | undefined>>,
    setSuccess?: React.Dispatch<React.SetStateAction<boolean>>,
    onSuccess?: (result: T) => void
  ) => {
    dispatch(showLoading());
    axiosCall
      .then((result) => {
        console.log(result)
        if (result.status === HttpStatusCode.Unauthorized) {
          SnackbarUtilities.info(AuthMessages.EXPIRE_SESION);
          dispatch(resetAccountInfo()); 
          navigate(PublicRoutes.LOGIN, {
            replace: true,
          });
        } else if (
          !!result.data ||
          result.status === HttpStatusCode.NoContent
        ) {
          if (!!setData) {
            setData(result.data);
          }
          if (!!setSuccess) {
            setSuccess(true);
          }
          if (!!onSuccess) {
            onSuccess(result.data!);
          }
          console.log("Call")
        } else if (!!result.error && result.error.title !== "") {
          if (!!setError) {
            setError(result.error);
          }
          SnackbarUtilities.error(result.error.title);
        } else {
          if (!!setError) {
            setError({
              status: 500,
              title: ExceptionMessages.UNKNOWN,
            });
          }
          console.log("UNEXPECTED RESULT:", result);
          SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
        }
      })
      .catch((err) => {
        dispatch(showLoading());
        if (!!setError) {
          setError({
            status: 500,
            title: ExceptionMessages.UNKNOWN,
          });
        }
        console.log("UNKNOWN ERROR:", err);
        SnackbarUtilities.error(ExceptionMessages.UNKNOWN);
        return Promise.reject;
      })
      .finally(() => {
        dispatch(hideLoading());
        cancelEndpoint();
        return Promise.resolve();
      });
  };

  const cancelEndpoint = () => {
    abortController && abortController.abort();
  };

  useEffect(() => {
    return () => {
      cancelEndpoint();
    };
  }, []);

  return {
    callEndpoint,
    getService,
    postService,
    putService,
    patchService,
    deleteService,
  };
};

export default useAxiosApi;
