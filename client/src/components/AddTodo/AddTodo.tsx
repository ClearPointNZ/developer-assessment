import { useState } from 'react'
import { Button, Container, Row, Col, Form, Stack } from 'react-bootstrap'

import useDebounce from '@hooks/useDebounce'

type AddTodoProps = {
  description: string
  setDescription: (description: string) => void
  addTodo: () => void
}

const AddTodo = ({ description, setDescription, addTodo }: AddTodoProps) => {
  const [isDescriptionValidated, setIsDescriptionValidated] = useState(false)

  const addTodoWithDebounce = useDebounce(addTodo, 500)

  const handleOnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setIsDescriptionValidated(true)
    setDescription(e.target.value)
  }

  const handleAddTodo = () => {
    setIsDescriptionValidated(true)

    if (description.length >= 3 && description.length <= 1000) {
      setIsDescriptionValidated(false)
      addTodoWithDebounce()
    }
  }

  const handleClear = () => {
    setIsDescriptionValidated(true)
    setDescription('')
  }

  return (
    <Row className="mb-4">
      <Col>
        <Container>
          <h2>Add Todo</h2>
          <Form.Group as={Row} className="mb-2 offset-md-1" controlId="formAddTodo">
            <Form.Label column sm="2">
              Description
            </Form.Label>
            <Col md="6">
              <Form.Control
                type="text"
                placeholder="Enter description..."
                value={description}
                minLength={3}
                maxLength={1000}
                isInvalid={isDescriptionValidated && (description.length < 3 || description.length > 1000)}
                onChange={handleOnChange}
              />
              <Form.Control.Feedback className="text-start" type="invalid">
                Please add a description between 3 and 1000 characters long
              </Form.Control.Feedback>
            </Col>
          </Form.Group>
          <Form.Group as={Row} className="mb-3 offset-md-3" controlId="formAddTodo">
            <Stack direction="horizontal" gap={2}>
              {/* Could add a conditional here to show some loading state when adding a todo */}
              <Button variant="primary" onClick={handleAddTodo}>
                Add Todo
              </Button>
              <Button variant="secondary" onClick={handleClear}>
                Clear
              </Button>
            </Stack>
          </Form.Group>
        </Container>
      </Col>
    </Row>
  )
}

export default AddTodo
