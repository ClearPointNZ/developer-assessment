import { useState } from "react";
import { Button, HStack, Input, useToast } from "@chakra-ui/react";


const AddTodo = ({addTodo}) => {

    const [description, setDescription] = useState("");
    const [statusInput, setStatusInput] = useState(true);

    const toast = useToast();


    const handleSubmit = (evt) => {

        evt.preventDefault();

        const text = description.trim();

        if(!text) {
            toast({
                title: "Enter a description",
                position: "top",
                status: "warning",
                isClosable: true,
              });

            setStatusInput(false);
            return setDescription("");
        }

        const todo = {
            id: crypto.randomUUID(),
            description: text,
            isCompleted: false
        }

        addTodo(todo);

        setDescription("");
    };

    if(description && !statusInput) {
        setStatusInput(true);
    }

    return(
        <form onSubmit={handleSubmit}>
        <HStack mt='4' mb='4'>
          <Input
            h='46'
            borderColor={!statusInput ? "red" : "transparent"}
            variant='filled'
            placeholder='Add your todo'
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
          <Button
            colorScheme='twitter'
            px='8'
            pl='10'
            pr='10'
            h='46'
            type='submit'
          >
            Add
          </Button>
        </HStack>
      </form>
    )
}

export default AddTodo;