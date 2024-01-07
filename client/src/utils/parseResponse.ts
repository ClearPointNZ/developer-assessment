import { AxiosResponse } from 'axios'
import { ZodType } from 'zod'

/**
 * Parses the response data using a given Zod schema.
 * @template T - The type of the response data.
 * @param {ZodType} schema - The schema used to parse the response data.
 * @returns {(res: AxiosResponse<T>) => T} A function that takes an AxiosResponse and returns the parsed data.
 * @link Adapted from: https://timdeschryver.dev/blog/why-we-should-verify-http-response-bodies-and-why-we-should-use-zod-for-this#using-the-zod-schema-within-the-service:~:text=parse%2Dresponse.operator.ts
 */
export default function parseResponse<T>(schema: ZodType): (res: AxiosResponse<T>) => T {
  return (res: AxiosResponse<T>) => {
    // When in development, we want to throw an error so we can fix it immediately
    if (!import.meta.env.PROD) {
      // https://github.com/colinhacks/zod?tab=readme-ov-file#parse
      return schema.parse(res.data)
    }

    // When in production, we want to log the error and be alerted
    // https://github.com/colinhacks/zod?tab=readme-ov-file#safeparse
    const parsed = schema.safeParse(res.data)

    if (!parsed.success) {
      // Log to service for alerts
      console.error(parsed.error)
    }

    return parsed
  }
}
