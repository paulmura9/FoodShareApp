import {Cities} from "../../../models/Cities.ts"
import './DropDown.css'

const MyDropdown = ({data, onChange}: {data: Cities[], onChange: React.ChangeEvent<HTMLSelectElement>}) => {
    return (
        <select onChange={onChange}>
            {data.map(city => <option id={city.name} key={city.id} value={city.id}>{city.name}</option>)}
        </select>
    )
}

export default MyDropdown