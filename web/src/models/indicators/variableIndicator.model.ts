import { Variable } from "..";

type VariableIndicatorPaginationResponse = {
    id: number;
    datum: number;
    date: Date;
    userId: string;
    variable: Variable
}

type CreateVariableIndicatorRequest = {
    datum: number;
    date: Date;
    userId: string;
    variableId: number;
}

export type {
    VariableIndicatorPaginationResponse,
    CreateVariableIndicatorRequest
}