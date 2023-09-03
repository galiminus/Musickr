import {useQuery} from "react-query";
import {createSearchParams} from "react-router-dom";

const useGetSearch = (
  q: string
) => {
  const params = { q: q };
  
  return useQuery(
    ['search', q], 
    () => fetch(`api/search?${createSearchParams(params)}`).then(res =>
      res.json()
    ),
    { enabled: !!q }
  );
};

export default useGetSearch;