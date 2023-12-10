import { useGetIncomeListApiQuery } from '../../../redux/reducers/incomes/incomesApi';

const useIncomes = () => {
    const { data: incomeList, error, isFetching } = useGetIncomeListApiQuery();

    return {
        incomeList: incomeList || null,
        error,
        isFetching,
    };
};

export default useIncomes;
