import React, { useState, useEffect } from 'react'
import { createTodo, deleteTodo, getAllTodos, updateTodo } from './services/todos.service'
import {
  Heading,
  useToast,
  VStack,
} from "@chakra-ui/react";
import AddTodo from './components/AddTodo';
import TodoList from './components/TodoList';


const App = () => {

  const [todos, setTodos] = useState([]);

  const toast = useToast();

  const handleAddTodo = async (todo) => {

   const response = await createTodo(todo);

   if(response?.error) {
      setToastError(response.error.message);
      return;
    }  

    setTodos([...todos, todo]);
  }

  const handleUpdateTodo = async (updated, onClose) => {

    const newTodosUpdated = todos.map((todo, i, array) => {
      if(updated.id === todo.id) {
        todo.description = updated.description;
        todo.isCompleted = updated.isCompleted;
      };
      return todo;
    })

    const response = await updateTodo(updated);
   
    if(response?.error) {
       setToastError(response.error.message);
       return;
     }  
 
     setTodos(newTodosUpdated);
  }

  const handleDeleteTodo = async (id) => {

    const response = await deleteTodo(id);

    if(response?.error) {
      setToastError(response.error.message);
      return;
    }  

    const updatedTodos = todos.filter((todo) => {
      return todo.id !== id
    });

    setTodos(updatedTodos);
  }

  const handleToggletComplete = async (todo) => {
    await handleUpdateTodo(todo);
  }

  const setToastError = (message) => {
    toast({
      title: `${message}`,
      position: "top",
      status: "warning",
      isClosable: true,
      duration: 1000
    });
  }

  useEffect(() => {
    const todoList = async () => {

      var response = await getAllTodos();

      setTodos(response.data ?? []);

      if(response.error) {
        setToastError(response.error.message);
      }    
    };

    todoList();
  }, []);

  return (
    <VStack p={4} minH='100vh' pb={28}>
        <Heading>
          Todo list
        </Heading>
        <AddTodo addTodo={handleAddTodo}/>
        <TodoList 
          todoList={todos}
          deleteTodo={handleDeleteTodo}
          updateTodo={handleUpdateTodo}
          toggleComplete={handleToggletComplete}
        />
      </VStack>
  )
}

export default App
