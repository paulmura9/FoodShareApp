import {ChangeEvent, useState } from "react"
import { BeneficiaryForm } from '../../types/BeneficiaryTypes.ts'
import './BeneficiariesForm.css'
import CitiesModel from "../../models/Cities.ts";
import MyDropdown from "../controls/dropdown/DropDown.tsx";

/**
 * SubmitHandler type is called when the user submits the form
 */
export type SubmitHandler = (formData: BeneficiaryForm) => void

/**
 * This component implements the Beneficiaries form used to handle the Add and Update operations
 *
 * @param onSubmit - callback function implemented by the caller to handle data submission
 * @param data
 */
const BeneficiariesForm = ({ onSubmit, data }: { onSubmit: SubmitHandler, data: never }) => {
    const cities = CitiesModel
  const [formData, setFormData] = useState(data)

  /**
   * Function called when one of the controls in the form changes it's value. This function updates
   * the formData state of the form so that it will contain allways updated data.
   *
   * @param event
   */
  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target
    setFormData(prevFormData => ({ ...prevFormData, [name]: value }))
  }

  const onCityChange = (event: ChangeEvent<HTMLSelectElement>) => {
      const {value} = event.target
      setFormData(prevFormData => ({...prevFormData, cityId: value }))
  }


  return (
    <>
      <form method="dialog" onSubmit={() => onSubmit(formData)}>
        <label htmlFor="name">Name</label>
        <input id="name" name="name" value={formData.name} onChange={handleChange} required />
        <label>City</label>
        <MyDropdown data={cities} onChange={onCityChange} /> 
        <label htmlFor="address">Address</label>
        <input id="address" name="address" value={formData.address} onChange={handleChange} required />
        <label htmlFor="capacity">Capacity</label>
        <input id="capacity" name="capacity" type="number" value={formData.capacity} onChange={handleChange} required />

        <button type="submit" className="button">Add Beneficiary</button>
      </form>
    </>
  )
}

export default BeneficiariesForm