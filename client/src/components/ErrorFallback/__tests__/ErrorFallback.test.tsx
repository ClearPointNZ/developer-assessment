import { render, screen, fireEvent } from '@testing-library/react'
import { vi } from 'vitest'

import ErrorFallback from '@components/ErrorFallback/ErrorFallback.tsx'

describe('ErrorFallback', () => {
  const mockError = new Error('Test error')
  const mockResetErrorBoundary = vi.fn()

  beforeEach(() => {
    render(<ErrorFallback error={mockError} resetErrorBoundary={mockResetErrorBoundary} />)
  })

  it('should render the error message', () => {
    // Arrange
    const errorMessage = screen.getByText('Something went wrong:')
    const errorText = screen.getByText(mockError.message)

    // Assert
    expect(errorMessage).toBeInTheDocument()
    expect(errorText).toBeInTheDocument()
  })

  it('should call resetErrorBoundary when the "Try again" button is clicked', () => {
    // Arrange
    const tryAgainButton = screen.getByRole('button', { name: 'Try again' })

    // Act
    fireEvent.click(tryAgainButton)

    // Assert
    expect(mockResetErrorBoundary).toHaveBeenCalled()
  })
})
