import React from 'react';
import {
    HStack,
    VStack,
    Text,
    StackDivider,
  } from "@chakra-ui/react";

import DeleteTodo from './DeleteTodo';
import UpdateTodo from './UpdateTodo';

const TodoList = ({
    todoList, 
    updateTodo, 
    deleteTodo,
    toggleComplete}) => {

    const handleToggleComplete = (todo) => {
        todo.isCompleted = !todo.isCompleted;
        toggleComplete(todo);
    }

    if(!todoList.length) {
        return(
            <Text fontSize='xl'>
               Your Todo list is empty
            </Text>
        )
    }
    return(
        <>
            <VStack
            divider={<StackDivider />}
            borderColor='gray.100'
            borderWidth='2px'
            p='5'
            borderRadius='lg'
            w='100%'
            maxW={{ base: "90vw", sm: "80vw", lg: "50vw", xl: "30vw" }}
            alignItems='stretch'
        >
            {todoList.map((todo, i) => (
            <HStack key={i}>
                <Text
                opacity={todo.isCompleted === true ? "0.2" : "1"}
                w='100%'
                p='8px'
                borderRadius='lg'
                as={todo.isCompleted === true ? "del" : ""}
                cursor="pointer"
                onClick={() => handleToggleComplete(todo)}
                >
                {todo.description}
                </Text>
                <UpdateTodo todo={todo} updateTodo={updateTodo}/>
                <DeleteTodo todo={todo} deleteTodo={deleteTodo} />
            </HStack>
            ))}
        </VStack>
        </>
    )
};

export default TodoList;