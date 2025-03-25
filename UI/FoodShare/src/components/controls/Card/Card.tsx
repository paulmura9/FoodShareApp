import {ReactNode} from "react"
import './Card.css'
import './CardBody.css'
import './CardHeader.css'


const Card=({children}:{children:ReactNode})=>{
    return(
        <>
        <div className="card">
            {children}
        </div>
        </>
    )
}

export default Card;