import { Table } from 'antd';
import { columns } from './columns';
import React from 'react';
import { IIncomeList } from '../../../redux/reducers/incomes/interfaces';

const IncomesGrid = (props: { data: IIncomeList[] }) => {
    const { data } = props;
    return <Table columns={columns} dataSource={data} />;
};

export default IncomesGrid;
