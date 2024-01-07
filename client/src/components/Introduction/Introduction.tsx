import { Alert, Row, Col } from 'react-bootstrap'

const Introduction = () => {
  return (
    <Row className="mb-3">
      <Col>
        <Alert variant="success">
          <h1>Todo List App</h1>
          Welcome to the ClearPoint frontend technical test. We like to keep things simple, yet clean so your task(s)
          are as follows:
          <br />
          <br />
          <ol className="text-start">
            <li>Add the ability to add (POST) a Todo Item by calling the backend API</li>
            <li>Display (GET) all the current Todo Items in the below grid and display them in any order you wish</li>
            <li>
              Bonus points for completing the &apos;Mark as completed&apos; button code for allowing users to update and
              mark a specific Todo Item as completed and for displaying any relevant validation errors/ messages from
              the API in the UI
            </li>
            <li>Feel free to add unit tests and refactor the component(s) as best you see fit</li>
          </ol>
        </Alert>
      </Col>
    </Row>
  )
}

export default Introduction
