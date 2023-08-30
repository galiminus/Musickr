import {useQuery} from "react-query";
import {createSearchParams} from "react-router-dom";

const useGetUsersAndPlaces = (
  q: string
) => {
  const params = { q: q };
  
  return useQuery(
    ['usersAndPlaces', q], 
    () => fetch(`search?${createSearchParams(params)}`).then(res =>
      res.json()
    ),
    { enabled: !!q }
  );
};

export default useGetUsersAndPlaces;