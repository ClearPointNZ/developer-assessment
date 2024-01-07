import { render, screen } from '@testing-library/react'

import Header from '@components/Header/Header.tsx'

describe('Header', () => {
  test('renders ClearPoint logo', () => {
    // Arrange
    render(<Header />)

    // Act
    const logoElement = screen.getByRole('img', { name: 'ClearPoint Logo' })

    // Assert
    expect(logoElement).toBeInTheDocument()
  })
})
