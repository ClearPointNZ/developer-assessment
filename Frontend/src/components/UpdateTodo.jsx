import React, {useState} from 'react';
import {
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalFooter,
    ModalBody,
    ModalCloseButton,
    Button,
    Input,
    FormControl,
    useDisclosure,
    IconButton,
    Checkbox,
    FormLabel,
  } from '@chakra-ui/react';
import {AiTwotoneEdit} from 'react-icons/ai';


const UpdateTodo = ({todo, updateTodo}) => {

    const { isOpen, onOpen, onClose } = useDisclosure();
    const [description, setDescription] = useState('');

    const focusRef = React.useRef();

    const handleUpdate = () => {

        const todoUpdate = {
            id: todo.id,
            isCompleted: todo.isCompleted,
            description: description,
        }

        updateTodo(todoUpdate);

        onClose();
    }

    return(
        <>
        <IconButton icon={<AiTwotoneEdit />} isRound='true' onClick={onOpen} disabled={todo.isCompleted} />
        <Modal
          isCentered
          initialFocusRef={focusRef}
          isOpen={isOpen}
          onClose={onClose}
        >
          <ModalOverlay />
          <ModalContent w='90%'>
            <ModalHeader>Update the descrption</ModalHeader>
            <ModalCloseButton />
            <ModalBody pb={6}>
              <FormControl>
                <Input
                  ref={focusRef}
                  placeholder='Enter the description'
                  defaultValue={todo.description}
                  onChange={(e) => setDescription(e.target.value.trim())}
                  onFocus={(e) => setDescription(e.target.value.trim())}
                />
              </FormControl>
            </ModalBody>
  
            <ModalFooter>
              <Button mr={3} onClick={onClose}>
                Cancel
              </Button>
              <Button
                colorScheme='blue'
                onClick={() => handleUpdate()}
              >
                Save
              </Button>
            </ModalFooter>
          </ModalContent>
        </Modal>
      </>
    )
}

export default UpdateTodo;