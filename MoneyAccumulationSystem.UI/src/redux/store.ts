import { configureStore } from '@reduxjs/toolkit';
import { setupListeners } from '@reduxjs/toolkit/query';
import { incomesApi } from './reducers/incomes/incomesApi';

export const store = configureStore({
    reducer: {
        [incomesApi.reducerPath]: incomesApi.reducer,
    },
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware({
            serializableCheck: false,
        }).concat(incomesApi.middleware),
});

setupListeners(store.dispatch);
