import React from "react";

import PageContent from "../../Utils/PageContent";
import {useSearchParams} from "react-router-dom";

const PlayerPage = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  
  const place = searchParams.get("place");
  
  return (
    <PageContent>
      Content for {place} here !
    </PageContent>
  );
};

export default React.memo(PlayerPage);