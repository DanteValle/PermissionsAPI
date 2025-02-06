import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './components/Login';
import Navbar from './components/Navbar';
import Permiso from './components/Permiso';
function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false)
  const handleLogin = () => {
    setIsLoggedIn(true);
  };
  return (
    <Router>
      {isLoggedIn && <Navbar />}
      <Routes>
        <Route
          path="/"
          element={!isLoggedIn ? <Login onLogin={handleLogin} /> : <Permiso />}
        />
        <Route path="/permiso" element={<Permiso />} />
      </Routes>
    </Router>
  )
}

export default App
