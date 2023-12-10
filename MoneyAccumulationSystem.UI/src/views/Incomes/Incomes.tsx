import React from 'react';
import useIncomes from './hooks/useIncomes';
import IncomesGrid from './IncomesGrid';
import { Spin } from 'antd';

const Incomes = () => {
    const { incomeList, error, isFetching } = useIncomes();

    return (
        <>
            {isFetching ? (
                <Spin />
            ) : (
                <IncomesGrid data={incomeList?.items || []} />
            )}
            {/*{incomeList?.items.map((income) => (
                <div key={income.id}>
                    {income.id} | {income.amount} | {income.dateTimeOffset} |{' '}
                    {income.notes} | {income.incomeType.name}
                </div>
            ))}*/}
        </>
    );
};

export default Incomes;
