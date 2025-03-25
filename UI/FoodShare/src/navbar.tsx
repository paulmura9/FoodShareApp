   
   import {NavLink} from "react-router-dom"
   
   
   import "./navbar.css"

    const Navbar =()=>{
        return(
            <nav>
            <ul>
            <li>
                <NavLink to="/">Admin</NavLink>
            </li>
            <li>
                <NavLink to="/donate">Donate</NavLink>
            </li>
            <li>
                <NavLink to="/benefit">Benefit</NavLink>
            </li>
            
            </ul>
            </nav>
        )
    }

    export default Navbar;