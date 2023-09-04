import {useQuery} from "react-query";
import {createSearchParams} from "react-router-dom";

const useGetPhotos = (
  q: string
) => {
  const params = { q: q };

  return useQuery(
    ['photos', q],
    () => fetch(`api/photos?${createSearchParams(params)}`).then(res =>
      res.json()
    ),
    { enabled: !!q }
  );
};

export default useGetPhotos;