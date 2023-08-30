import React from "react";
import {
  Link,
  Modal, ModalBody, ModalCloseButton,
  ModalContent,
  ModalHeader,
  ModalOverlay,
  ModalProps, Text, VStack
} from "@chakra-ui/react";
import {ExternalLinkIcon} from "@chakra-ui/icons";

const AboutModal = (props: Omit<ModalProps, "children">) => {
  return (
    <Modal
      isCentered={true}
      {...props}
    >
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          About Musickr
        </ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <VStack my="4">
            <Text>
              Created by T.Ferreira and Marobax
            </Text>
            <Link 
              href="https://github.com/ManedFox/Musickr"
              isExternal
            >
              GitHub link <ExternalLinkIcon mx="2" />
            </Link>
          </VStack>
        </ModalBody>
      </ModalContent>
    </Modal>
  );
};

export default React.memo(AboutModal);