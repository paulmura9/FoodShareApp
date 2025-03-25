import { useEffect, useState } from "react"
import Cities from "../models/Cities.ts";
import { IDonor} from "../types/CommonTypes.ts";

const useDonor= () => {
  const [data, setData] = useState<IDonor[]>([])
  const [loading, setLoading] = useState(false)

  useEffect(() => {
    (async () => {
      try {
        setLoading(true) 

        const response = await fetch('http://localhost:5197/api/Donor/GetAllDonors')
        const donors = await response.json()
        setData(donors)
      } catch (err) {
        console.error(err)
      } finally {
        setLoading(false)
      }
    })()
  }, [])

  const getDonor = () => {
    return { data, loading }
  }

  const addDonor = async (newData: BodyInit) => {
    try {
      setLoading(true)
      const response = await fetch('http://localhost:5197/api/Donor/Create', {
        body: JSON.stringify(newData),
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        }
      })
      const donors = await response.json()
      const mappedDonors: IDonor = {
          id: donors.id,
          name: donors.name,
          cityName: Cities[donors.cityId].name,
          address: donors.address,
        }
      data.push(mappedDonors)
      return data
    } catch (err) {
      console.error(err)
    } finally {
      setLoading(false)
    }
  }

  const editDonor= async (updatedData: object) => {
    setLoading(true)
    await fetch(`http://localhost:5197/api/Donor/Edit?id=${updatedData.id}`, {
      method: 'PUT',
      body: JSON.stringify(updatedData),
      headers: {
        'Content-Type': 'application/json'
      }
    })
  }

  const deleteDonor = async (id: string | undefined) => {
    await fetch(`http://localhost:5197/api/Donor/Delete?id=${id}`, {
      method: 'DELETE'
    })
    const index = data.findIndex(element => element.id == id)
    data.splice(index, 1)
    return data
  }

  return {
    getDonor,
    addDonor,
    editDonor,
    deleteDonor
  }
}

export default useDonor