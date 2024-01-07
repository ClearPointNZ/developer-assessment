import { Image, Row, Col } from 'react-bootstrap'

import clearPointLogo from '@assets/clearPointLogo.png'

const Header = () => {
  return (
    <header>
      <Row>
        <Col>
          <Image src={clearPointLogo} alt="ClearPoint Logo" fluid rounded />
        </Col>
      </Row>
    </header>
  )
}

export default Header
