import { useEffect, useMemo, useRef } from 'react'
import debounce from 'lodash/debounce'

/**
 * Custom hook that returns a debounced callback function.
 * The debounced callback will only be invoked after a specified delay since the last invocation.
 *
 * @param callback - The callback function to be debounced.
 * @param wait - The delay in milliseconds before invoking the debounced callback (default: 500ms).
 * @returns The debounced callback function.
 * @link Replicated from: https://www.developerway.com/posts/debouncing-in-react
 */
const useDebounce = (callback: () => void, wait: number = 500) => {
  const ref = useRef<() => void>()

  useEffect(() => {
    ref.current = callback
  }, [callback])

  const debouncedCallback = useMemo(() => {
    const func = () => {
      ref.current?.()
    }

    return debounce(func, wait)
  }, [])

  return debouncedCallback
}

export default useDebounce
