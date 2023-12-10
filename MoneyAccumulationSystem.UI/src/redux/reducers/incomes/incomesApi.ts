import { IBaseListResponse } from 'common/interfaces';
import { IIncomeList } from './interfaces';
import { callApiEndpoint, HTTP_METHOD } from 'api/constants';
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { getHeaders } from '../../../api/utils';

enum INCOMES_TAGS {
    INCOMES = 'incomes',
}

export const incomesApi = createApi({
    reducerPath: 'incomesApi',
    baseQuery: fetchBaseQuery({
        baseUrl: callApiEndpoint('v1/incomes'),
        prepareHeaders: getHeaders(),
    }),
    tagTypes: [INCOMES_TAGS.INCOMES],
    keepUnusedDataFor: 60,
    endpoints: (builder) => ({
        getIncomeListApi: builder.query<IBaseListResponse<IIncomeList>, void>({
            query() {
                return {
                    url: '',
                    method: HTTP_METHOD.GET,
                };
            },
            providesTags: () => [{ type: INCOMES_TAGS.INCOMES }],
        }),
    }),
});

export const resetIncomesApiApiState = () => incomesApi.util.resetApiState();

export const { useGetIncomeListApiQuery } = incomesApi;
