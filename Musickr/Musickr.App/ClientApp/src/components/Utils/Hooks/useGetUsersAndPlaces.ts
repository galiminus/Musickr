import {useQuery} from "react-query";

const useGetUsersAndPlaces = (
  q: string
) => {
  return useQuery(
    ['usersAndPlaces', q], 
    () => fetch(`search?q=${q}`).then(res =>
      res.json()
    ),
    { enabled: !!q }
  );
};

export default useGetUsersAndPlaces;