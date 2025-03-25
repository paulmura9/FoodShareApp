
import { Route, Routes } from 'react-router-dom'
import Admin from './components/pages/Admin'
import Donate from './components/pages/Donate'
import Benefit from './components/pages/Benfit'

function App() {

  return (
   <Routes>
    <Route path='/' element={<Admin/>}></Route>
    <Route path='/donate' element={<Donate/>}></Route>
    <Route path='/benefit' element={<Benefit/>}></Route>
   </Routes>
  )
}

export default App
