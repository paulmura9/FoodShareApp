import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import {BrowserRouter }from "react-router-dom"
import Navbar from './navbar.tsx'


ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <BrowserRouter>
<Navbar>

</Navbar>
    <App />
    </BrowserRouter>
  </React.StrictMode>,
)
