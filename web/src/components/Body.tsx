import { Box, Fab } from "@mui/material";
import { Add, Header, Loading } from ".";
import { GridColDef } from "@mui/x-data-grid";
import { ReactNode, useState } from "react";
import { AppStore } from "../redux/store";
import { useSelector } from "react-redux";
import { Delete, Save } from "@mui/icons-material";

type Props = {
  children: ReactNode;
  title: string;
  slug: string;
  showAdd: boolean;
  isEditing: boolean;
  disableSave?: boolean;
  disableDelete?: boolean;
  setOpen?: React.Dispatch<React.SetStateAction<boolean>>;
  setRefresh?: React.Dispatch<React.SetStateAction<boolean>>;
  subtitle?: string;
  columns?: GridColDef[];
  selectionDataUrl?: string;
  addAdapterFuction?: (data: any) => any;
  onSaveButton?: () => void;
  onDeleteButton?: () => void;
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
        showAdd={props.showAdd}
        title={props.title}
        subtitle={props.subtitle}
        setOpen={setOpen}
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
      <Box
        display={props.isEditing ? "flex" : "none"}
        flexDirection="row"
        gap="1rem"
        position="absolute"
        bottom="0"
        right="0"
        m="1rem"
      >
        <Fab
          size="medium"
          color="success"
          aria-label="save"
          disabled={props.disableSave}
          onClick={props.onSaveButton}
        >
          <Save />
        </Fab>
        <Fab
          size="medium"
          color="error"
          aria-label="save"
          disabled={props.disableDelete}
          onClick={props.onDeleteButton}
        >
          <Delete />
        </Fab>
      </Box>
    </Box>
  );
};

export default Body;
