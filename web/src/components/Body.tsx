import { Box } from "@mui/material";
import { Add, Header, Loading } from ".";
import { GridColDef } from "@mui/x-data-grid";
import { ReactNode, useState } from "react";
import { AppStore } from "../redux/store";
import { useSelector } from "react-redux";

type Props = {
  children: ReactNode;
  title: string;
  slug: string;
  showAdd: boolean;
  setRefresh?: React.Dispatch<React.SetStateAction<boolean>>;
  subtitle?: string;
  columns?: GridColDef[];
  selectionDataUrl?: string;
  addAdapterFuction?: (data: any) => any;
};

const Body = (props: Props) => {
  const loadingData: boolean = useSelector(
    (store: AppStore) => store.loadingData
  );
  const [open, setOpen] = useState(false);

  return loadingData ? (
    <Loading canCancel={false} cancelTitle={undefined} message="Cargando" />
  ) : (
    <Box m="1.5rem 2.5rem">
      <Header
        title={props.title}
        subtitle={props.subtitle}
        setOpen={setOpen}
        showAdd={props.showAdd}
      />
      {props.children}
      {open && props.columns && (
        <Add
          setRefresh={props.setRefresh}
          setOpen={setOpen}
          slug={props.slug}
          columns={props.columns}
          selectionDataUrl={props.selectionDataUrl}
          adapterFuction={(data) =>
            props.addAdapterFuction && props.addAdapterFuction(data)
          }
        />
      )}
    </Box>
  );
};

export default Body;
