import './App.css'
import React, { useState, useEffect } from 'react'
import { getAllTodos } from './services/todos.service'
import TodoList from './components/TodoList'
import { Container } from 'react-bootstrap'
import AddTask from './components/AddTodo'

const axios = require('axios')

const App = () => {
  const [todos, setTodos] = useState([]);

  useEffect(() => {
    const todoList = () => {
      var list = getAllTodos();
      setTodos(list);
    };
    todoList();
  }, [])

  return (
    <Container>
      <h1>Todo List</h1>
      <AddTask />
      <TodoList list={todos}/>
    </Container>
  )
}

export default App
