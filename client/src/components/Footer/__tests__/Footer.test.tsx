import { render, screen } from '@testing-library/react'

import Footer from '@components/Footer/Footer.tsx'

describe('Footer', () => {
  test('renders current year in copyright', () => {
    // Arrange
    const currentYear = new Date().getFullYear()
    render(<Footer />)

    // Act
    const footerElement = screen.getByText(`© ${currentYear} Copyright:`)

    // Assert
    expect(footerElement).toBeInTheDocument()
  })

  test('renders company name in link', () => {
    // Arrange
    const companyName = 'clearpoint.digital'
    render(<Footer />)

    // Act
    const companyLink = screen.getByText(companyName)

    // Assert
    expect(companyLink).toBeInTheDocument()
  })
})
