import React from 'react';
import { Button } from 'react-bootstrap';

import {AiFillDelete} from 'react-icons/ai';

const DeleteTodo = ({id, completed}) => {

    const handleDelete = () => {    
        alert("delete " + id);
    };

    return(
        <Button disabled={completed} variant="outline-danger" onClick={handleDelete}>
            <AiFillDelete/>
        </Button>
    )
}

export default DeleteTodo;