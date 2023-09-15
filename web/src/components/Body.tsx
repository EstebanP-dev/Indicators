import { Box } from "@mui/material"
import { Add, Header } from "."
import { GridColDef } from "@mui/x-data-grid"
import { ReactNode, useState } from "react";

type Props = {
    children: ReactNode,
    title: string,
    slug: string,
    showAdd: boolean,
    setRefresh: React.Dispatch<React.SetStateAction<boolean>>;
    subtitle?: string,
    columns?: GridColDef[],
}

const Body = (props: Props) => {
    const [open, setOpen] = useState(false);

  return (
    <Box m="1.5rem 2.5rem">
        <Header title={props.title} subtitle={props.subtitle} setOpen={setOpen} />
        {props.children}
        {open && props.columns && <Add setRefresh={props.setRefresh} setOpen={setOpen} slug={props.slug} columns={props.columns}/>}
    </Box>
  )
}

export default Body