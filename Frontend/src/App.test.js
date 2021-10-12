import { render, screen } from '@testing-library/react'
import App from './App'

test('renders the footer text', () => {
  render(<App />)
  const footerElement = screen.getByText(/clearpoint.digital/i)
  expect(footerElement).toBeInTheDocument()
})
