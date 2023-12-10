import { IIdProvider } from 'common/interfaces';
import { REDUX_CACHE } from './constats';

export const getListProvidesTags = <T>(
    data: T[],
    tag: any,
    deepFieldName = '',
) => {
    return data
        ? [
              ...data.map((item) => ({
                  type: tag,
                  id: deepFieldName
                      ? (
                            item[
                                deepFieldName as keyof typeof item
                            ] as IIdProvider
                        ).id
                      : (item as IIdProvider).id,
              })),
              { type: tag, id: REDUX_CACHE.LIST },
          ]
        : [{ type: tag, id: REDUX_CACHE.LIST }];
};
