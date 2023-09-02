import { Navigate } from 'react-router-dom';

export const ProtectRoutes = ({ children } : any) : any => {

  const token = localStorage.getItem("token");

  if (!token)
  {
    return <Navigate replace to='/login' />
  }

  return children;
}