'use client';
import { useQuery } from '@tanstack/react-query';
import counterApi, {
  getCounterEntityTypesQueryString,
} from './counter.service';
import { CounterEntity } from './counter.types';

const useEntitiesCounter = (entities: CounterEntity[]) => {
  const queryData = useQuery(
    [`counter/entities${getCounterEntityTypesQueryString(entities)}`],
    {
      queryFn: () => counterApi.countEntities(entities),
      // keepPreviousData: true,
    },
  );

  return queryData;
};
export default useEntitiesCounter;
