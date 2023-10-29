export class AddColDef {
  field: string = '';
  headerName: string = '';
  type: string = '';
  slug?: string;
  value?: any | null;
  options?: ReadonlyArray<any> = [];
  getOptionLabel?: (option: any) => string;

  /**
   *
   */
  constructor() {}

  // fetchData() {
  //   const abortController: AbortController = loadAbort();
  //   const dispatch = useDispatch();
  //   const navigate = useNavigate();
  //   const { callEndpoint, getService } = useAxiosApi(
  //     abortController,
  //     navigate,
  //     dispatch
  //   );

  //   callEndpoint(
  //     getService<any>(`${this.slug}?page=0&rows=100`),
  //     undefined,
  //     undefined,
  //     undefined,
  //     (result) => {
  //       this.options = result;
  //     }
  //   );
  // }
}
