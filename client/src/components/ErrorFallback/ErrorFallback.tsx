import { Container } from 'react-bootstrap'

import Header from '@components/Header/Header.tsx'
import Footer from '@components/Footer/Footer.tsx'

type ErrorFallbackProps = {
  error: Error
  resetErrorBoundary: () => void
}

// Some interesting reading: https://www.developerway.com/posts/how-to-handle-errors-in-react#part6
const ErrorFallback = ({ error, resetErrorBoundary }: ErrorFallbackProps) => {
  return (
    <div className="text-center">
      <Container>
        <Header />
        <p>Something went wrong:</p>
        <pre>{error.message}</pre>
        <button onClick={resetErrorBoundary}>Try again</button>
      </Container>
      <Footer />
    </div>
  )
}

export default ErrorFallback
