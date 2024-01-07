import { type TodoRequest, type Todo, TodoResponseSchema, TodosResponseSchema } from '@models/todo'
import httpFactory from '@services/apis/http.ts'
import parseResponse from '@utils/parseResponse.ts'

const http = httpFactory(import.meta.env.VITE_TODO_API_BASE_URL)
const todosBaseUrlPart = '/v1/todos'

// Creating a facade to abstract away axios concerns
// Todo: Wire up standardised error message handling
const get = (id: string) => http.get<Todo>(`${todosBaseUrlPart}/${id}`).then(parseResponse(TodoResponseSchema))
const getAll = (showCompletedTodos: boolean) =>
  http.get<Todo[]>(`${todosBaseUrlPart}?showCompleted=${showCompletedTodos}`).then(parseResponse(TodosResponseSchema))
const post = (data: TodoRequest) => http.post<Todo>(todosBaseUrlPart, data).then(parseResponse(TodoResponseSchema))
const put = (id: string, data: TodoRequest) => http.put(`${todosBaseUrlPart}/${id}`, data)

export default {
  get,
  getAll,
  post,
  put,
}
