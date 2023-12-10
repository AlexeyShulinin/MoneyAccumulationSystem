import {
    BaseQueryFn,
    FetchArgs,
    FetchBaseQueryMeta,
} from '@reduxjs/toolkit/query';

export enum REDUX_CACHE {
    LIST = 'list',
}

export type BaseQueryFnType = BaseQueryFn<
    string | FetchArgs,
    unknown,
    any,
    FetchBaseQueryMeta
>;
