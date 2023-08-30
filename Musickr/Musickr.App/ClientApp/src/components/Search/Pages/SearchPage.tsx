import React, {useCallback} from "react";

import {
  Heading, 
  IconButton, 
  Tooltip, 
  useDisclosure, 
  VStack
} from "@chakra-ui/react";
import {InfoIcon} from "@chakra-ui/icons";

import {
  createSearchParams, 
  useNavigate
} from "react-router-dom";

import PageContent from "../../Utils/PageContent";
import AboutModal from "../Components/AboutModal";
import SearchBar from "../Components/SearchBar";

const SearchPage = () => {
  const navigate = useNavigate();
  const { isOpen, onClose, onOpen } = useDisclosure();
  
  const onSearchBarChange = useCallback((value: string) => {
    const params = { place: value };
    
    navigate({
      pathname: "/player",
      search: `?${createSearchParams(params)}`
    })
  }, 
  [navigate]);
  
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
        <SearchBar onChange={onSearchBarChange} />
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