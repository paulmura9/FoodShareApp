import useBeneficiary from '../../hooks/useBeneficiary.ts'
import {IActions, IBeneficiary} from '../../types/CommonTypes.ts'
import Table from '../controls/table/Table.tsx'
import Button from '../controls/button/Button.tsx'
import ModalDialog from '../controls/dialog/ModalDialog.tsx'
import {useEffect, useState} from 'react'
import BeneficiariesForm from './BeneficiariesForm.tsx'

type BeneficiaryData = {
  id: number,
  name: string,
  cityName: string,
  address: string,
  capacity: number
}

const Beneficiaries = () => {
  const { data, loading } = useBeneficiary().getBeneficiary()
  const header = ['Name', 'City', 'Address', 'Capacity']
  const [beneficiaries, setBeneficiaries] = useState<IBeneficiary[]>([])
  const [showAddBeneficiaryDialog, setShowAddBeneficiaryDialog] = useState(false)
  const [beneficiaryData, setBeneficiaryData] = useState<BeneficiaryData>({
    id: 0,
    name: '',
    cityName: '',
    address: '',
    capacity: 0
  })

  const { addBeneficiary, editBeneficiary, deleteBeneficiary } = useBeneficiary()

   useEffect(() => {
    if (data) {
      setBeneficiaries(data)
    }
  }, [data]) 

  const addAction = () => {
    setShowAddBeneficiaryDialog(true)
  }
  const editAction = (e: React.MouseEvent<HTMLButtonElement>) => {
    const id = e.currentTarget?.parentElement?.parentElement?.dataset?.key
    const currentBeneficiary = beneficiaries.find(beneficiary => beneficiary.id == id)
    setBeneficiaryData(currentBeneficiary)
    setShowAddBeneficiaryDialog(true)
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
    if (beneficiaryData.id === 0) {
      setBeneficiaries(await addBeneficiary(submittedData))
    } else {
      setBeneficiaries(await editBeneficiary(submittedData))
    } 
      setShowAddBeneficiaryDialog(false)
    
  }

  return (<div>
   {loading && <p>Loading...</p>}
    {beneficiaries && <Table header={header} data={beneficiaries} actions={actions} />}
    <Button onClick={addAction}>Add Beneficiary</Button>
    {showAddBeneficiaryDialog && <ModalDialog  openModal={showAddBeneficiaryDialog}>
     <BeneficiariesForm onSubmit={onSubmit} data={beneficiaryData} />
    </ModalDialog>} 
  </div>)
}

export default Beneficiaries 