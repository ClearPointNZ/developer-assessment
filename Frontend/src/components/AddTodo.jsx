import React from 'react';
import { Button, Form, Row, Col, Container } from 'react-bootstrap';


const AddTask = () => {
    return(
        <Container>
            <Form>
                <Row>
                <Col xs={7}>
                    <Form.Control placeholder='Add your todo'></Form.Control>
                </Col>
                <Col>
                    <Button type="submit">Add</Button>
                </Col>
                </Row>
            </Form>
        </Container>
    )
}

export default AddTask;