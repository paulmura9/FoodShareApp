import { MouseEventHandler, ReactNode } from "react";
import "./Button.css"

const Button=({onClick,children}:{onClick:MouseEventHandler<HTMLButtonElement>,children:ReactNode})=>{
    return(
   <button onClick={onClick} className="button">{children}</button>
    )
}
export default Button