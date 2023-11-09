import {
  CreateVariableIndicatorRequest,
  IndicatorResultPaginationResponse,
  IndicatorTypePaginationResponse,
  VariableIndicatorPaginationResponse,
} from '.';
import {
  ActorByIdResponse,
  Display,
  Frequency,
  Meaning,
  MeasurementUnit,
  Source,
} from '..';

type IndicatorPaginationResponse = {
  id: number;
  code: string;
  name: string;
};

type IndicatorByIdResponse = {
  id: number;
  code: string;
  name: string;
  objective: string;
  scope: string;
  formula: string;
  goal: string;
  indicatorType: IndicatorTypePaginationResponse;
  measurementUnit: MeasurementUnit;
  meaning: Meaning;
  frequency: Frequency;
  results: IndicatorResultPaginationResponse[];
  displays: Display[];
  variables: VariableIndicatorPaginationResponse[];
  sources: Source[];
  actors: ActorByIdResponse[];
};

type CreateIndicatorRequest = {
  code: string;
  name: string;
  objective: string;
  scope: string;
  formula: string;
  goal: string;
  indicatorTypeId: number;
  measurementUnitId: number;
  meaningId: number;
  frequencyId: number;
  displays: number[];
  variables: CreateVariableIndicatorRequest[];
  sources: number[];
  actors: string[];
};

type UpdateIndicatorRequest = {
  id?: number | undefined;
  code?: string | undefined;
  name?: string | undefined;
  objective?: string | undefined;
  scope?: string | undefined;
  formula?: string | undefined;
  goal?: string | undefined;
  indicatorTypeId?: number | undefined;
  measurementUnitId?: number | undefined;
  meaningId?: number | undefined;
  frequencyId?: number | undefined;
  displays?: number[] | undefined;
  variables?: number[] | undefined;
  sources?: number[] | undefined;
  actors?: string[] | undefined;
};

export type {
  IndicatorByIdResponse,
  IndicatorPaginationResponse,
  CreateIndicatorRequest,
  UpdateIndicatorRequest,
};
