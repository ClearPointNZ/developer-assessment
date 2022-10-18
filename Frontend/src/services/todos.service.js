const baseUrl = process.env.TODO_SERVICE ?? 'https://localhost:5001/';
const apiUrl = baseUrl + 'api/Todo/'

export const getAllTodos = async () => {
    try {
        const response = await fetch(apiUrl, {
            method: "GET"  
        });

        const data = await response.json();

        return ({
            data: data
        });
    } catch {
        return ({
            error: {
                message: "Service is unavailable"
            }
        })
    };
}

export const createTodo = async (todo) => {
    try {
        const response = await fetch(apiUrl, {
            method: "POST",
            body: JSON.stringify(todo),
            headers: {
                'Content-Type': 'application/json',
              }
        });

        const data = await response.json();

        return ({
            data: data
        });

    } catch {
        return ({
            error: {
                message: "Service is unavailable"
            }
        })
    };
}

export const updateTodo = async (todo) => {
    try {
        
        await fetch(apiUrl + `${todo.id}`, {
            method: "PUT",
            body: JSON.stringify(todo),
            headers: {
                'Content-Type': 'application/json',
              }
        })

    } catch (error) {
        return ({
            error: {
                message: error.message
            }
        })
    }
}

export const deleteTodo = async (id) => {
    try {
        
        await fetch(apiUrl + `${id}`, {
            method: "DELETE"
        })

    } catch (error) {
        return ({
            error: {
                message: "Service is unavailable"
            }
        })
    }
}

/**
 * [
        {
            description: "test",
            id: "f1010292-5952-4862-90f8-3c169970f35b",
            isCompleted: false
        },
        {
            description: "tes2",
            id: "f1010292-5952-4862-90f8-3c169970f35a",
            isCompleted: false
        },
        {
            description: "tes3",
            id: "f1010292-5952-4862-90f8-3c169970f35d",
            isCompleted: true
        }
    ]
 */