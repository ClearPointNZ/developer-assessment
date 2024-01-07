import { z } from 'zod'

/**
 * Represents the request schema for a todo.
 */
export const TodoRequestSchema = z.object({
  description: z.string(),
  isCompleted: z.boolean(),
})

/**
 * Represents a todo request.
 */
export type TodoRequest = z.infer<typeof TodoRequestSchema>

/**
 * Represents the response schema for a todo.
 */
export const TodoResponseSchema = z.object({
  id: z.string().uuid(),
  description: z.string(),
  isCompleted: z.boolean(),
  createdAt: z.string(),
  completedAt: z.string()?.nullable(),
})

/**
 * Represents the response schema for an array of todo items.
 */
export const TodosResponseSchema = z.array(TodoResponseSchema)

/**
 * Represents a todo.
 */
export type Todo = z.infer<typeof TodoResponseSchema>
