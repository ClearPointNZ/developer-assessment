import React from 'react';
import { Col, Collapse, Container, ListGroup, Row } from 'react-bootstrap';
import DeleteTodo from './DeleteTodo';
import UpdateTodo from './UpdateTodo';

const TodoList = ({list}) => {

    const markComplete = () => {
        alert("mark completed");
    }

    if(!list.length) {
        return(
            <>
            <h1>Add a todo</h1>
            </>
        )
    }
    return(
        <Container>
        <ListGroup>
            {list.map((todo, i) => ( 
                <Row key={i}>
                    <Col xs={7}>                
                    <ListGroup.Item
                    key={todo.id}
                    action={!todo.isCompleted}
                    disabled={todo.isCompleted}
                    onClick={markComplete}>
                        {todo.description}
                    </ListGroup.Item></Col>
                    <Col xs={1}><UpdateTodo id={todo.id}/></Col>
                    <Col xs={3}><DeleteTodo id={todo.id} completed={todo.isCompleted}/></Col>
                </Row>         

            ))}
        </ListGroup>
        </Container>
    )
};

export default TodoList;