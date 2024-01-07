const Footer = () => {
  const currentYear = new Date().getFullYear()

  return (
    <footer className="page-footer font-small teal pt-4">
      <div className="footer-copyright text-center py-3">
        © {currentYear} Copyright:&nbsp;
        <a href="https://clearpoint.digital" target="_blank" rel="noreferrer">
          clearpoint.digital
        </a>
      </div>
    </footer>
  )
}

export default Footer
