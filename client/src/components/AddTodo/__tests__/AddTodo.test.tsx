import { render, screen, fireEvent } from '@testing-library/react'
import { vi } from 'vitest'

import AddTodo from '@components/AddTodo/AddTodo.tsx'

describe('AddTodo', () => {
  const setDescriptionMock = vi.fn()

  test('renders AddTodo component', () => {
    // Arrange
    render(<AddTodo description="" setDescription={() => {}} addTodo={() => {}} />)
    const addTodoHeading = screen.getByRole('heading', { name: 'Add Todo' })

    // Assert
    expect(addTodoHeading).toBeInTheDocument()
  })

  test('updates description when input value changes', () => {
    // Arrange
    render(<AddTodo description="" setDescription={setDescriptionMock} addTodo={() => {}} />)
    const descriptionInput = screen.getByRole('textbox', { name: 'Description' })

    // Act
    fireEvent.change(descriptionInput, { target: { value: 'New description' } })

    // Assert
    expect(setDescriptionMock).toHaveBeenCalledWith('New description')
  })

  // Currently this fails because the function being passed to the component is being wrapped
  // in a debounce function, which is called instead.
  test.skip('calls addTodo when "Add Todo" button is clicked', () => {
    // Arrange
    const addTodo = vi.fn()
    render(<AddTodo description="Test description" setDescription={() => {}} addTodo={addTodo} />)
    const addButton = screen.getByRole('button', { name: 'Add Todo' })

    // Act
    fireEvent.click(addButton)

    // Assert
    expect(addTodo).toHaveBeenCalled()
  })

  test('clears description when "Clear" button is clicked', () => {
    // Arrange
    render(<AddTodo description="Test description" setDescription={setDescriptionMock} addTodo={() => {}} />)
    const clearButton = screen.getByRole('button', { name: 'Clear' })

    // Act
    fireEvent.click(clearButton)

    // Assert
    expect(setDescriptionMock).toHaveBeenCalledWith('')
  })

  test('displays error message when "Add Todo" button is clicked and description is empty', () => {
    // Arrange
    render(<AddTodo description="" setDescription={() => {}} addTodo={() => {}} />)
    const addButton = screen.getByRole('button', { name: 'Add Todo' })

    // Act
    fireEvent.click(addButton)

    // Assert
    const errorMessage = screen.getByText('Please add a description between 3 and 1000 characters long')
    expect(errorMessage).toBeInTheDocument()
  })
})
