import { IndicatorByIdResponse, UpdateIndicatorRequest } from '../models';

const updateIndicatorRequestFromIndicatorByIdResponseAdapter = (
  from: IndicatorByIdResponse
) => {
  const to: UpdateIndicatorRequest = {
    id: from.id,
    code: from.code,
    name: from.name,
    objective: from.objective,
    scope: from.scope,
    formula: from.formula,
    goal: from.goal,
    indicatorTypeId: from.indicatorType.id,
    measurementUnitId: from.measurementUnit.id,
    meaningId: from.meaning.id,
    frequencyId: from.frequency.id,
    displays: from.displays.map((x) => x.id),
    variables: from.variables.map((x) => x.id),
    sources: from.sources.map((x) => x.id),
    actors: from.actors.map((x) => x.id),
  };

  return to;
};

export default {
  updateIndicatorRequestFromIndicatorByIdResponseAdapter,
};
