import React from 'react';
import type { ColumnsType } from 'antd/es/table';
import { IIncomeList } from '../../../redux/reducers/incomes/interfaces';
import { INCOME_LIST_FIELDS } from '../../../redux/reducers/incomes/constants';
import { getReferenceNameDataIndex } from '../../../utils/stringUtils';
import { Button, Space } from 'antd';
import { DeleteOutlined, EditOutlined } from '@ant-design/icons';

export const columns: ColumnsType<IIncomeList> = [
    {
        key: INCOME_LIST_FIELDS.AMOUNT,
        title: 'Amount',
        dataIndex: INCOME_LIST_FIELDS.AMOUNT,
        render: (amount, income) => (
            <>
                {amount} {income[INCOME_LIST_FIELDS.CURRENCY]?.symbol}
            </>
        ),
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
    {
        key: 'Action',
        title: '',
        fixed: 'right',
        width: 100,
        dataIndex: INCOME_LIST_FIELDS.ID,
        render: (id) => (
            <Space size={8}>
                <span>{id}</span>
                <Button type="primary">
                    <EditOutlined />
                </Button>
                <Button type="dashed" danger>
                    <DeleteOutlined />
                </Button>
            </Space>
        ),
    },
];
