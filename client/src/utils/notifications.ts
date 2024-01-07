import { notification } from 'antd'

// Temporary solution for surfacing errors
// to the FE
export const showErrorNotification = (err: string) => {
  notification.error({
    placement: 'bottomRight',
    description: err,
    message: undefined
  })
}
