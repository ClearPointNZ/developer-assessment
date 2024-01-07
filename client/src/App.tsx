import { useState, useEffect } from 'react'
import { Container } from 'react-bootstrap'

import Header from '@components/Header/Header.tsx'
import Introduction from '@components/Introduction/Introduction.tsx'
import AddTodo from '@components/AddTodo/AddTodo.tsx'
import ShowTodos from '@components/ShowTodos/ShowTodos.tsx'
import Footer from '@components/Footer/Footer.tsx'

import todosApi from '@services/apis/todosApi.ts'
import { todoErrors } from '@utils/constants.ts'
import { showErrorNotification } from '@utils/notifications.ts'
import type { Todo } from '@models/todo'

import '@/App.css'

const App = () => {
  const [isLoading, setIsLoading] = useState(false)
  const [showCompletedTodos, setShowCompletedTodos] = useState(false)
  const [todos, setTodos] = useState<Todo[]>([])
  const [description, setDescription] = useState('')

  const getAllTodos = async () => {
    try {
      setIsLoading(true)
      const todos = await todosApi.getAll(showCompletedTodos)
      setTodos(todos)
    } catch (err) {
      // Log to service for alerts
      showErrorNotification(todoErrors.getAllError)
    }

    setIsLoading(false)
  }

  const addTodo = async () => {
    try {
      const newItem = await todosApi.post({
        description,
        isCompleted: false,
      })
      setTodos([newItem, ...todos])
      setDescription('')
    } catch (err) {
      // Log to service for alerts
      showErrorNotification(todoErrors.addError)
    }
  }

  const updateTodo = async (id: string, todo: Todo, isCompleted: boolean) => {
    try {
      await todosApi.put(id, { ...todo, isCompleted })

      if (showCompletedTodos) {
        setTodos(todos.map((item: Todo) => (item.id === id ? { ...item, isCompleted } : item)))
        return
      }

      setTodos(todos.filter((todo: Todo) => todo.id !== id))
    } catch (err) {
      // Log to service for alerts
      showErrorNotification(todoErrors.updateError)
    }
  }

  useEffect(() => {
    getAllTodos()
  }, [])

  return (
    <div className="text-center">
      <Container>
        <Header />
        <main>
          <Introduction />
          <AddTodo description={description} setDescription={setDescription} addTodo={addTodo} />
          <ShowTodos
            isLoading={isLoading}
            todos={todos}
            getTodos={getAllTodos}
            updateTodo={updateTodo}
            setShowCompletedTodos={setShowCompletedTodos}
          />
        </main>
      </Container>
      <Footer />
    </div>
  )
}

export default App
