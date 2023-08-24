import React from "react";
import {Button, Container, Heading, IconButton, Tooltip, useDisclosure, VStack} from "@chakra-ui/react";
import PageContent from "../../Utils/PageContent";
import {InfoIcon} from "@chakra-ui/icons";
import AboutModal from "../Components/AboutModal";

const SearchPage = () => {
  const { isOpen, onClose, onOpen } = useDisclosure();
  
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
      <Tooltip
        hasArrow
        label="About Musickr"
      >
        <IconButton 
          onClick={onOpen}
          icon={<InfoIcon />} 
          variant="ghost"
          aria-label="about musickr"
          size="lg"
          position="absolute"
          right="4"
          bottom="4"
        />
      </Tooltip>
      <AboutModal
        isOpen={isOpen}
        onClose={onClose}
      />
    </PageContent>
  );
};

export default React.memo(SearchPage);