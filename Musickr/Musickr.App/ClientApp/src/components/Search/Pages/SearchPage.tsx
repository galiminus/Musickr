import React from "react";
import {Container, Heading, VStack} from "@chakra-ui/react";
import PageContent from "../../Utils/PageContent";

const SearchPage = () => {
  return (
    <PageContent 
      alignItems="center"
      justify="center"
    >
      <VStack>
        <Heading>
          Musickr
        </Heading>
      </VStack>
      
    </PageContent>
  );
};

export default React.memo(SearchPage);