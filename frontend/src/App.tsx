import { Outlet } from 'react-router-dom'
import './App.css'

function App() {
  return (
    <>
      <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
        <div className="container-fluid">
          <a className="navbar-brand" href="/">Shared Notepad</a>
        </div>
      </nav>
      <div className="container my-4">
        <Outlet />
      </div>
    </>
  )
}

export default App
