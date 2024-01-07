import { render, screen, waitFor, fireEvent } from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import { vi } from 'vitest'

import App from '@/App'

import todosApi from '@services/apis/todosApi.ts'
import type { Todo } from '@models/todo'

const date = new Date().toLocaleString('en-NZ')
const mockTodos: Todo[] = [
  { id: crypto.randomUUID(), description: 'Todo 1', isCompleted: false, createdAt: date, completedAt: null },
  { id: crypto.randomUUID(), description: 'Todo 2', isCompleted: false, createdAt: date, completedAt: null },
  { id: crypto.randomUUID(), description: 'Todo 3', isCompleted: false, createdAt: date, completedAt: null },
]

describe('App', () => {
  beforeAll(() => {
    // Mock the todosApi.getAll function to return the mockTodos
    todosApi.getAll = vi.fn().mockResolvedValue(mockTodos)
  })

  test('renders App component', async () => {
    // Arrange
    render(<App />)
    const headerComponent = screen.getByRole('img', { name: 'ClearPoint Logo' })
    const introductionComponent = screen.getByText(/Todo List App/i)
    const addTodoComponent = screen.getByRole('heading', { name: 'Add Todo' })
    const showTodosComponent = screen.getByText(/Showing/i)
    const footerComponent = screen.getByText(/clearpoint.digital/i)

    // Assert
    expect(headerComponent).toBeInTheDocument()
    expect(introductionComponent).toBeInTheDocument()
    expect(addTodoComponent).toBeInTheDocument()
    expect(showTodosComponent).toBeInTheDocument()
    expect(footerComponent).toBeInTheDocument()

    expect(todosApi.getAll).toHaveBeenCalledTimes(1)

    await waitFor(() => {
      mockTodos.forEach((todo) => expect(screen.getByText(todo.description)).toBeInTheDocument())
    })
  })

  test('adds a new todo', async () => {
    // Arrange
    const newTodo: Todo = {
      id: crypto.randomUUID(),
      description: 'New Todo',
      isCompleted: false,
      createdAt: date,
      completedAt: null,
    }

    // Mock the todosApi.post function to return the newTodo
    todosApi.post = vi.fn().mockResolvedValue(newTodo)

    render(<App />)
    const descriptionInput = screen.getByRole('textbox', { name: 'Description' })
    const addButton = screen.getByRole('button', { name: 'Add Todo' })

    // Act
    fireEvent.change(descriptionInput, { target: { value: newTodo.description } })
    await userEvent.click(addButton)

    // Assert
    await waitFor(() => {
      const createdTodo = screen.getByText(newTodo.description)
      expect(createdTodo).toBeInTheDocument()
    })
  })

  test('marks a todo as complete', async () => {
    // Arrange
    const existingTodo = mockTodos[0]

    // Mock the todosApi.put function
    todosApi.put = vi.fn()

    render(<App />)

    // Wait for the existing todos to be loaded
    await waitFor(() => {
      mockTodos.forEach((todo) => expect(screen.getByText(todo.description)).toBeInTheDocument())
    })

    const todo = screen.getByText(existingTodo.description)
    const completeButton = screen.getAllByRole('button', { name: 'Mark as completed' })[0]

    // Act
    await userEvent.click(completeButton)

    // Assert
    expect(todosApi.put).toHaveBeenCalledTimes(1)

    await waitFor(() => {
      expect(todo).not.toBeInTheDocument()
    })
  })
})
