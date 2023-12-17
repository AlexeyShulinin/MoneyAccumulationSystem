import React from 'react';
import { createBrowserRouter } from 'react-router-dom';
import { ROUTES } from './routes';
import Incomes from 'views/Incomes';
import CommonErrorPage from '../components/ErrorPages/CommonErrorPage';
import App from '../App';

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        errorElement: <CommonErrorPage />,
        children: [
            {
                path: '/',
                element: <div>Welcome to Money Accumulation System!</div>,
            },
            {
                path: ROUTES.INCOMES,
                element: <Incomes />,
            },
        ],
    },
]);
