import { MaybePromise } from '@reduxjs/toolkit/dist/query/tsHelpers';
import { BaseQueryApi } from '@reduxjs/toolkit/query';

const BASE_URL = process.env.REACT_APP_API_BASE_URL;
export const callApiEndpoint = (uri: string) => BASE_URL + uri;

export enum HTTP_METHOD {
    GET = 'GET',
    POST = 'POST',
    PUT = 'PUT',
    DELETE = 'DELETE',
    PATCH = 'PATCH',
}

export type PrepareHeadersFunc = (
    headers: Headers,
    api: Pick<
        BaseQueryApi,
        'getState' | 'extra' | 'endpoint' | 'type' | 'forced'
    >,
) => MaybePromise<Headers>;
