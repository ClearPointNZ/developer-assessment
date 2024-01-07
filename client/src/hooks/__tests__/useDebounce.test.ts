import { act, renderHook } from '@testing-library/react'
import { vi } from 'vitest'

import useDebounce from '@hooks/useDebounce.ts'

vi.useFakeTimers()

describe('useDebounce', () => {
  it('should debounce the callback function', () => {
    // Arrange
    const callback = vi.fn()
    const wait = 500
    const { result } = renderHook(() => useDebounce(callback, wait))

    // Act & Assert
    act(() => {
      result.current()
    })

    expect(callback).not.toHaveBeenCalled()

    act(() => {
      vi.advanceTimersByTime(wait)
    })

    expect(callback).toHaveBeenCalled()
  })
})
