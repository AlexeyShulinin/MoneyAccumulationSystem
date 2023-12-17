import React from 'react';
import MenuList from './components/MenuList';
import { Col, Row } from 'antd';
import { Outlet } from 'react-router-dom';

function App() {
    return (
        <Row wrap={false}>
            <Col>
                <MenuList />
            </Col>
            <Col flex="auto">
                <h1>Money Accumulation System</h1>
                <Col id="detail" style={{ padding: 20 }}>
                    <Outlet />
                </Col>
            </Col>
        </Row>
    );
}

export default App;
