import { MenuItem } from './constants';
import { Key, ReactNode } from 'react';

export function getMenuItem(
    label: ReactNode,
    key?: Key | null,
    icon?: ReactNode,
    children?: MenuItem[],
    type?: 'group',
): MenuItem {
    return {
        key,
        icon,
        children,
        label,
        type,
    } as MenuItem;
}
