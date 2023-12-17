import React from 'react';
import { Col, Menu } from 'antd';
import { MENU_ITEMS } from './helpers/constants';
import { useLocation } from 'react-router-dom';

const MenuList = () => {
    const location = useLocation();

    return (
        <Col style={{ height: '100vh' }}>
            <Menu
                theme={'dark'}
                style={{ width: 256, height: '100vh' }}
                mode="inline"
                items={MENU_ITEMS}
                selectedKeys={[location.pathname]}
            />
        </Col>
    );
};

export default MenuList;
