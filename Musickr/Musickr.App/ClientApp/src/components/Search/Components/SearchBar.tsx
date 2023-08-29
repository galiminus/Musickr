import React, {ChangeEvent, useEffect, useState} from "react"
import {AutoComplete, AutoCompleteInput, AutoCompleteItem, AutoCompleteList} from "@choc-ui/chakra-autocomplete";
import {useQuery} from "react-query";
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
  
  console.log(data)
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
      </AutoCompleteList>
    </AutoComplete>
  )
};

export default React.memo(SearchBar);