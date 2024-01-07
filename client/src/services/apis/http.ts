import axios, { AxiosInstance } from 'axios'

// Factory for creating an axios instance per given
// base url. This will allow us to create multiple
// instances for different clients.
const httpFactory = (baseURL: string): AxiosInstance => {
  const http = axios.create({ baseURL })

  // Wiring up an axios interceptor to be able to handle errors globally
  // This block can be altered to handle different errors
  // depending on the use case. For example, once authn
  // is implemented perhaps redirecting a user to login
  // again if the BE returns a 401.
  http.interceptors.response.use(undefined, (err) => {
    const statusCode = err.response?.status

    if (statusCode === 401) {
      localStorage.clear()
      window.location.href = '/login'
    }

    return Promise.reject(err)
  })

  return http
}

export default httpFactory
