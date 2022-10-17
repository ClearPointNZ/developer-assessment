import React from 'react';
import { Button } from 'react-bootstrap';

import {AiTwotoneEdit} from 'react-icons/ai';

const UpdateTodo = ({id}) => {

    const handleUpdate = () => {
        alert("update " + id);
    };

    return(
        <Button variant="outline-info" onClick={handleUpdate}>
            <AiTwotoneEdit/>
        </Button>
    )
}

export default UpdateTodo;