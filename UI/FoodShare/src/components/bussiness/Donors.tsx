

import {IActions, IDonor} from '../../types/CommonTypes.ts'
import Table from '../controls/table/Table.tsx'
import Button from '../controls/button/Button.tsx'
//import ModalDialog from '../controls/dialog/ModalDialog.tsx'
import {useEffect, useState} from 'react'
import useDonor from '../../hooks/useDonor.ts'
import DonorTable from '../controls/table/DonorTable.tsx'
//import BeneficiariesForm from './BeneficiariesForm.tsx'

type DonorData = {
  id: number,
  name: string,
  cityName: string,
  address: string,
}

const Donors = () => {
  const { data, loading } = useDonor().getDonor()
  const header = ['Id', 'Name', 'City', 'Address']
  const [donors, setDonors] = useState<IDonor[]>([])
  const [showAddDonorDialog, setShowAddDonorDialog] = useState(false)
  const [donorData, setDonorData] = useState<DonorData>({
    id: 0,
    name: '',
    cityName: '',
    address: '',
  })

  const { addDonor, editDonor, deleteDonor } = useDonor()

   /*  useEffect(() => {
    if (data) {
      setDonors(data)
    }
  }, [data])   */

  const addAction = () => {
    setShowAddDonorDialog(true)
  }
  const editAction = (e: React.MouseEvent<HTMLButtonElement>) => {
    //const id = e.currentTarget?.parentElement?.parentElement?.dataset?.key
    //const currentBeneficiary = beneficiaries.find(beneficiary => beneficiary.id == id)
    //setBeneficiaryData(currentBeneficiary)
    setShowAddDonorDialog(true)
  }
  const removeAction = async  (e: React.MouseEvent<HTMLButtonElement>) => {
    //setBeneficiaries(await deleteBeneficiary(e.currentTarget?.parentElement?.parentElement?.dataset?.key))
  } 
  const actions: IActions[] = [
    {
      actionName: 'Edit',
      // @ts-ignore
      actionHandler: editAction,
    },
    {
      actionName: 'Delete',
      actionHandler: removeAction,
    },
  ]

  const onSubmit = async (submittedData: BodyInit) => {
    if (donorData.id === 0) {
      setDonors(await addDonor(submittedData))
    } else {
      setDonors(await editDonor(submittedData))
    }
  }

  return (<div>
   {loading && <p>Loading...</p>}
    {donors && <DonorTable header={header} data={donors} actions={actions} />}
    <Button onClick={addAction}>Add Donor</Button>
    {/* {showAddBeneficiaryDialog && <ModalDialog closeModal={closeModalHandler} openModal={showAddBeneficiaryDialog}>
     <BeneficiariesForm onSubmit={onSubmit} data={beneficiaryData} />
    </ModalDialog>} */}
  </div>)
}

export default Donors 