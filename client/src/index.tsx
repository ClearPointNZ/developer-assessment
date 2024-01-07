import React from 'react'
import { createRoot } from 'react-dom/client'
import { ErrorBoundary } from 'react-error-boundary'

import App from '@/App.jsx'
import ErrorFallback from '@components/ErrorFallback/ErrorFallback.tsx'

import 'bootstrap/dist/css/bootstrap.min.css'
import '@/index.css'

const root = createRoot(document.getElementById('root')!)
root.render(
  <React.StrictMode>
    <ErrorBoundary FallbackComponent={ErrorFallback} onReset={document.location.reload}>
      <App />
    </ErrorBoundary>
  </React.StrictMode>
)
