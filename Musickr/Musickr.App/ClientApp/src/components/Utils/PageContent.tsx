import React, {ReactNode} from "react";
import {Container, Flex, FlexProps, Heading} from "@chakra-ui/react";

const PageContent = ({ 
  children,
  ...props
} : FlexProps) => {
  return (
    <Flex 
      bgColor="red" 
      h="100vh"
      p="4"
      {...props}
    >
      {children}
    </Flex>
  );
};

export default React.memo(PageContent);