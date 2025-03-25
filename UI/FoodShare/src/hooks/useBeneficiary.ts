import { useEffect, useState } from "react"
import Cities from "../models/Cities.ts";
import {IBeneficiary} from "../types/CommonTypes.ts";

const useBeneficiary = () => {
  const [data, setData] = useState<IBeneficiary[]>([])
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    (async () => {
      try {
        setLoading(true) 

        const response = await fetch('http://localhost:5197/api/Beneficiary/GetAllBeneficiaries')
        const beneficiaries = await response.json()
        setData(beneficiaries)
      } catch (err) {
        console.error(err)
      } finally {
        setLoading(false)
      }
    })()
  }, [])

  const getBeneficiary = () => {
    return { data, loading }
  }

  const addBeneficiary = async (newData: BodyInit) => {
    try {
      setLoading(true)
      const response = await fetch('http://localhost:5197/api/Beneficiary/CreateBeneficiary', {
        body: JSON.stringify(newData),
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        }
      })
      const beneficiaries = await response.json()
      const mappedBeneficiaries: IBeneficiary = {
          id: beneficiaries.id,
          name: beneficiaries.name,
          cityName: Cities[beneficiaries.cityId].name,
          address: beneficiaries.address,
          capacity: beneficiaries.capacity
        }
      data.push(mappedBeneficiaries)
      return data
    } catch (err) {
      console.error(err)
    } finally {
      setLoading(false)
    }
  }

  const editBeneficiary = async (updatedData: object) => {
    setLoading(true)
    await fetch(`http://localhost:5197/api/Beneficiary/Edit?id=${updatedData.id}`, {
      method: 'PUT',
      body: JSON.stringify(updatedData),
      headers: {
        'Content-Type': 'application/json'
      }
    })
  }

  const deleteBeneficiary = async (id: string | undefined) => {
    await fetch(`http://localhost:5197/api/Beneficiary/Delete?id=${id}`, {
      method: 'DELETE'
    })
    const index = data.findIndex(element => element.id == id)
    data.splice(index, 1)
    return data
  }

  return {
    getBeneficiary,
    addBeneficiary,
    editBeneficiary,
    deleteBeneficiary
  }
}

export default useBeneficiary