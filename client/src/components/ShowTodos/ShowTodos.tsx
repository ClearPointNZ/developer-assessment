import { Row, Col, Button, Table, Spinner, Stack, Form } from 'react-bootstrap'

import type { Todo } from '@models/todo'
import useDebounce from '@hooks/useDebounce.ts'

type ShowTodosProps = {
  isLoading: boolean
  todos: Todo[]
  getTodos: () => void
  updateTodo: (id: string, todo: Todo, isCompleted: boolean) => void
  setShowCompletedTodos: (showCompletedTodos: boolean) => void
}

const ShowTodos = ({ isLoading, todos, getTodos, updateTodo, setShowCompletedTodos }: ShowTodosProps) => {
  const todoCount = todos?.length ?? 0
  const todoText = todos?.length === 1 ? 'item' : 'items'

  const getTodosWithDebounce = useDebounce(getTodos, 500)

  const handleToggleShowCompletedTodos = (e: React.ChangeEvent<HTMLInputElement>) => {
    setShowCompletedTodos(e.target.checked)
    getTodosWithDebounce()
  }

  return (
    <Row>
      <Col>
        <Stack direction="horizontal" gap={2} className="justify-content-center mb-1">
          <h2>
            Showing {todoCount} {todoText}&nbsp;
          </h2>
          <Button variant="primary" className="pull-right" onClick={getTodosWithDebounce}>
            Refresh
          </Button>
          <Form.Check
            reverse
            type="switch"
            label="Show Completed Todos"
            className="ms-2"
            onChange={handleToggleShowCompletedTodos}
          ></Form.Check>
        </Stack>

        {isLoading && todoCount === 0 ? (
          <Spinner animation="border" role="status">
            <span className="visually-hidden">Loading...</span>
          </Spinner>
        ) : (
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>Description</th>
                <th>Action</th>
              </tr>
            </thead>
            <tbody>
              {todos?.map((todo) => (
                <tr key={todo.id}>
                  <td className="text-break">{todo.description}</td>
                  <td className="w-25">
                    {!todo.isCompleted ? (
                      <Button variant="warning" size="sm" onClick={() => updateTodo(todo.id, todo, true)}>
                        Mark as completed
                      </Button>
                    ) : (
                      <Button variant="danger" size="sm" onClick={() => updateTodo(todo.id, todo, false)}>
                        Mark as incomplete
                      </Button>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        )}
      </Col>
    </Row>
  )
}

export default ShowTodos
