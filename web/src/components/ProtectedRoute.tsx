import { useSelector } from 'react-redux';
import { PublicRoutes } from '../enviroments'
import { Navigate } from 'react-router-dom'
import { AppStore } from '../redux/store';
import { AccountInfo } from '../models';

const ProtectedRoute = ({ children } : any) => {

  const accountInfo: AccountInfo = useSelector((_store: AppStore) => _store.accountInfo);

  if (accountInfo.token === '') {
    return <Navigate replace to={PublicRoutes.LOGIN} />
  }

  return children;
}

export default ProtectedRoute;