export interface IIdProvider {
    id: number;
}

export interface IReference extends IIdProvider {
    name: string;
}

export interface IBaseListResponse<T> {
    items: T[];
}

export interface ICurrencyReference extends IReference {
    symbol: string;
}
