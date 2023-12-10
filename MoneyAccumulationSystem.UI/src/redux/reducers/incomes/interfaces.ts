import { IIdProvider, IReference } from 'common/interfaces';
import { INCOME_LIST_FIELDS } from './constants';

export interface IIncomeList extends IIdProvider {
    [INCOME_LIST_FIELDS.AMOUNT]: number;
    [INCOME_LIST_FIELDS.DATE_TIME_OFFSET]?: string;
    [INCOME_LIST_FIELDS.NOTES]: string;
    [INCOME_LIST_FIELDS.INCOME_TYPE]: IReference;
}
