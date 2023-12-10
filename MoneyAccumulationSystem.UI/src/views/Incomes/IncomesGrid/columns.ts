import type { ColumnsType } from 'antd/es/table';
import { IIncomeList } from '../../../redux/reducers/incomes/interfaces';
import { INCOME_LIST_FIELDS } from '../../../redux/reducers/incomes/constants';
import { getReferenceNameDataIndex } from '../../../utils/stringUtils';

export const columns: ColumnsType<IIncomeList> = [
    {
        key: INCOME_LIST_FIELDS.AMOUNT,
        title: 'Amount',
        dataIndex: INCOME_LIST_FIELDS.AMOUNT,
    },
    {
        key: INCOME_LIST_FIELDS.DATE_TIME_OFFSET,
        title: 'Date',
        dataIndex: INCOME_LIST_FIELDS.DATE_TIME_OFFSET,
    },
    {
        key: INCOME_LIST_FIELDS.INCOME_TYPE,
        title: 'Income Type',
        dataIndex: getReferenceNameDataIndex(INCOME_LIST_FIELDS.INCOME_TYPE),
    },
    {
        key: INCOME_LIST_FIELDS.NOTES,
        title: 'Notes',
        dataIndex: INCOME_LIST_FIELDS.NOTES,
    },
];
