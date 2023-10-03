// external imports
import axios from "axios";

async function makeApiCallWithNumRetries<RequestType, ResponseType>(
    url: string,
    method: string,
    data: RequestType | undefined,
    retryCount: number): Promise<ResponseType | undefined> {
    let currRetryCount = 0;
    return await makeApiCallWithRetry<RequestType, ResponseType>(url, method, data, () => {
        currRetryCount++;
        return currRetryCount < retryCount;
    });
}

async function makeApiCallWithRetry<RequestType, ResponseType>(
    url: string,
    method: string,
    data: RequestType | undefined,
    shouldRetry: () => boolean): Promise<ResponseType | undefined> {
    try {
        const callResponse = await axios.request({
            method: method,
            url: url,
            data: data
        });

        if (callResponse?.data) {
            const response: ResponseType = callResponse.data;
            return response;
        }
    }
    catch (error: any) {
        console.error(`Error when making network call to ${url}: ${error}`);
        if (error?.response?.data) {
            const errorResponse: ResponseType = error.response.data;
            return errorResponse;
        }
    }

    // If the response was invalid or there was an error, and we need to retry
    if (shouldRetry()) {
        return await makeApiCallWithRetry(url, method, data, shouldRetry);
    }
}

export {
    makeApiCallWithRetry,
    makeApiCallWithNumRetries
}