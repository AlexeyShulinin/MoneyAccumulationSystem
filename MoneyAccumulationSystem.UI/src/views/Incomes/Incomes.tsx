import React from 'react';
import useIncomes from './hooks/useIncomes';
import IncomesGrid from './IncomesGrid';
import { Button, Col, Row, Spin } from 'antd';

const Incomes = () => {
    const { incomeList, error, isFetching } = useIncomes();

    return (
        <Col>
            {isFetching ? (
                <Spin />
            ) : (
                <>
                    <Row>
                        <Button type="primary" style={{ marginBottom: 16 }}>
                            Add income
                        </Button>
                    </Row>
                    <Row wrap={true}>
                        <Col flex="auto">
                            <IncomesGrid data={incomeList?.items || []} />
                        </Col>
                    </Row>
                </>
            )}
            {/*{incomeList?.items.map((income) => (
                <div key={income.id}>
                    {income.id} | {income.amount} | {income.dateTimeOffset} |{' '}
                    {income.notes} | {income.incomeType.name}
                </div>
            ))}*/}
        </Col>
    );
};

export default Incomes;
