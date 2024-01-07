import { vi } from 'vitest'
import { SafeParseReturnType, ZodError, ZodType } from 'zod'
import { AxiosResponse } from 'axios'

import parseResponse from '@utils/parseResponse.ts'

describe('parseResponse', () => {
  const mockSchema = {
    parse: vi.fn(),
    safeParse: vi.fn(),
  } as unknown as ZodType<any>

  const mockResponse = {
    data: {},
  } as unknown as AxiosResponse<any>

  beforeEach(() => {
    vi.clearAllMocks()
  })

  it('should call schema.parse when not in production', () => {
    // Arrange
    import.meta.env.PROD = false

    // Act
    parseResponse(mockSchema)(mockResponse)

    // Assert
    expect(mockSchema.parse).toHaveBeenCalledWith(mockResponse.data)
  })

  it('should return the parsed result when safeParse is successful', () => {
    // Arrange
    import.meta.env.PROD = true
    const parsedResult = { success: true, data: {} } as SafeParseReturnType<any, any>
    vi.spyOn(mockSchema, 'safeParse').mockReturnValue(parsedResult)

    // Act
    const result = parseResponse(mockSchema)(mockResponse)

    // Assert
    expect(result).toEqual(parsedResult)
  })

  // Replace console.error with service call
  it('should log the error when safeParse is unsuccessful', () => {
    // Arrange
    import.meta.env.PROD = true
    const error = new ZodError([])
    const parsedResult = { success: false, error } as SafeParseReturnType<any, any>
    vi.spyOn(mockSchema, 'safeParse').mockReturnValue(parsedResult)

    console.error = vi.fn()

    // Act
    parseResponse(mockSchema)(mockResponse)

    // Assert
    expect(console.error).toHaveBeenCalledWith(error)
  })
})
