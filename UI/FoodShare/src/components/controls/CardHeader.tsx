import { ReactNode } from "react";

const CardHeader=({children}:{children:ReactNode})=>{
    return (
        <>
        <div className="card-header">
            {children}
        </div>
        </>
    )
}
export default CardHeader;