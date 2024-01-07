// jest-dom adds custom jest matchers for asserting on DOM nodes.
// allows you to do things like:
// expect(element).toHaveTextContent(/react/i)
// learn more: https://github.com/testing-library/jest-dom
import { expect } from 'vitest'
import matchers from '@testing-library/jest-dom/matchers'

expect.extend(matchers)
