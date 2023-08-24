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
        <Heading 
          size="4xl"
          letterSpacing="0.2rem"
        >
          Musickr
        </Heading>
      </VStack>
      
    </PageContent>
  );
};

export default React.memo(SearchPage);