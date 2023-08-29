import React, {
  ChangeEvent, 
  useState
} from "react"

import {HStack, Icon, Text} from "@chakra-ui/react";
import {MdPlace} from "react-icons/md";
import {
  AutoComplete, 
  AutoCompleteInput, 
  AutoCompleteItem, 
  AutoCompleteList
} from "@choc-ui/chakra-autocomplete";

import {useDebounce} from "react-use";

import useGetUsersAndPlaces from "../../Utils/Hooks/useGetUsersAndPlaces";

const SearchBar = () => {
  const [searchContent, setSearchContent] = useState("");
  const [searchContentDebounced, setSearchContentDebounced] = useState("");
  
  const handleInput = (changeEvent: ChangeEvent<HTMLInputElement>) => {
    setSearchContent(changeEvent.target.value);
  };

  const [, cancel] = useDebounce(
    () => {
      setSearchContentDebounced(searchContent);
    },
    500,
    [searchContent]
  );
  
  const { isLoading, error, data } = useGetUsersAndPlaces(searchContentDebounced);
  
  return (
    <AutoComplete openOnFocus isLoading={isLoading}>
      <AutoCompleteInput 
        variant="filled"
        h="12"
        w="lg"
        placeholder="Search a place..."
        value={searchContent}
        onChange={handleInput}
      />
      <AutoCompleteList>
        {data?.map((place) => (
          <AutoCompleteItem
            key={place.name}
            value={place.name}
          >
            <HStack justifyContent="center">
              <Icon as={MdPlace} />
              <Text 
                fontSize="lg" 
                m="0"
              >
                {place.name}
              </Text>
            </HStack>
          </AutoCompleteItem>
        ))}
      </AutoCompleteList>
    </AutoComplete>
  )
};

export default React.memo(SearchBar);