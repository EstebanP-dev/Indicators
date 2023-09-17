function useQuery(useLocation: any) {
    return new URLSearchParams(useLocation().search);
}

export default useQuery;