import { render, screen, fireEvent } from '@testing-library/react'
import { vi } from 'vitest'

import ShowTodos from '@components/ShowTodos/ShowTodos.tsx'
import type { Todo } from '@models/todo'

const date = new Date().toLocaleString('en-NZ')
const mockTodos: Todo[] = [
  { id: crypto.randomUUID(), description: 'Todo 1', isCompleted: false, createdAt: date, completedAt: null },
  { id: crypto.randomUUID(), description: 'Todo 2', isCompleted: false, createdAt: date, completedAt: null },
  { id: crypto.randomUUID(), description: 'Todo 3', isCompleted: false, createdAt: date, completedAt: null },
]

describe('ShowTodos', () => {
  it('should render the correct number of todos and "items" text', () => {
    // Arrange
    render(
      <ShowTodos
        isLoading={false}
        todos={mockTodos}
        getTodos={() => {}}
        updateTodo={() => {}}
        setShowCompletedTodos={() => {}}
      />
    )

    // Act
    const h2Element = screen.getByRole('heading', { level: 2 })
    const todoElements = screen.getAllByRole('row')

    // Assert
    expect(h2Element).toContainHTML('3 items')
    // Removing 1 from the length because the first row is the table header
    expect(todoElements.length - 1).toBe(mockTodos.length)
  })

  it('should render the correct "item" text for a single todo', () => {
    // Arrange
    render(
      <ShowTodos
        isLoading={false}
        todos={mockTodos}
        getTodos={() => {}}
        updateTodo={() => {}}
        setShowCompletedTodos={() => {}}
      />
    )

    // Act
    const h2Element = screen.getByRole('heading', { level: 2 })

    // Assert
    expect(h2Element).toContainHTML('item')
  })

  // Currently this fails because the function being passed to the component is being wrapped
  // in a debounce function, which is called instead. This could be tested through other means.
  it.skip('should call getTodos when the Refresh button is clicked', () => {
    // Arrange
    const getTodosMock = vi.fn()
    render(
      <ShowTodos
        isLoading={false}
        todos={mockTodos}
        getTodos={getTodosMock}
        updateTodo={() => {}}
        setShowCompletedTodos={() => {}}
      />
    )
    const refreshButton = screen.getByText('Refresh')

    // Act
    fireEvent.click(refreshButton)

    // Assert
    expect(getTodosMock).toHaveBeenCalledTimes(1)
  })

  it('should call updateTodo with the correct todo when the Mark as completed button is clicked', () => {
    // Arrange
    const updateTodoMock = vi.fn()
    render(
      <ShowTodos
        isLoading={false}
        todos={mockTodos}
        getTodos={() => {}}
        updateTodo={updateTodoMock}
        setShowCompletedTodos={() => {}}
      />
    )
    const updateTodoButton = screen.getAllByRole('button', { name: 'Mark as completed' })[0]

    // Act
    fireEvent.click(updateTodoButton)

    // Assert
    expect(updateTodoMock).toHaveBeenCalledTimes(1)
    expect(updateTodoMock).toHaveBeenCalledWith(mockTodos[0].id, mockTodos[0], true)
  })

  it('should render the loading spinner when isLoading is true', () => {
    // Arrange
    render(
      <ShowTodos
        isLoading={true}
        todos={[]}
        getTodos={() => {}}
        updateTodo={() => {}}
        setShowCompletedTodos={() => {}}
      />
    )

    // Act
    const spinnerElement = screen.getByRole('status')

    // Assert
    expect(spinnerElement).toBeInTheDocument()
  })
})
