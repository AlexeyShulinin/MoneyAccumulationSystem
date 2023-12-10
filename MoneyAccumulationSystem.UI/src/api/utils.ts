import { PrepareHeadersFunc } from './constants';

export const getHeaders = (): PrepareHeadersFunc => (headers) => {
    // TODO: authentication and token refresh

    const token = process.env.REACT_APP_ACCESS_TOKEN;
    headers.set('Authorization', `Bearer ${token}`);

    return headers;
};
