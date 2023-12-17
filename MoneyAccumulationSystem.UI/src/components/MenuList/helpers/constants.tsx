import React from 'react';
import { MenuProps } from 'antd';
import { getMenuItem } from './utils';
import { ROUTES } from 'router/routes';
import { NavLink } from 'react-router-dom';

export type MenuItem = Required<MenuProps>['items'][number];

const getLink = (title: string, url: string) => (
    <NavLink to={url}>{title}</NavLink>
);

export const MENU_ITEMS: MenuItem[] = [
    getMenuItem(getLink('Home', ROUTES.ROOT), ROUTES.ROOT),
    getMenuItem('Incomes', ROUTES.INCOMES, '', [
        getMenuItem(
            getLink('Incomes', `/${ROUTES.INCOMES}`),
            `/${ROUTES.INCOMES}`,
        ),
        getMenuItem('Option 2', '3'),
        getMenuItem('Option 3', '4'),
        getMenuItem('Option 4', '5'),
    ]),
];
