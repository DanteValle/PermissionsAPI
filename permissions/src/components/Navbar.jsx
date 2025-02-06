import React from 'react';
import { Link } from 'react-router-dom';

const Navbar = () => {
  return (
    <nav>
      <Link to="/permiso">Permiso</Link>
      <Link to="/tipo-permiso">Tipo de Permiso</Link>
    </nav>
  );
};

export default Navbar;