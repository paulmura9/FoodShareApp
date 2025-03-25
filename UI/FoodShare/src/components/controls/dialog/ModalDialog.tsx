import { ReactNode, useEffect, useRef } from 'react'

import './ModalDialog.css'

/**
 * ModalDialog component will display a dialog on the screen containing forms to enter
 * data needed on other components. It will be mainly used in CRUD operations to display
 * the form for entering data.
 * @param openModal it decides if modal will be displayed or not
 * @param closeModal callback funtion that will receive data from the modal
 * @param children ReactNode to display any other info in the dailog as required by the parent component
 */
const ModalDialog = ({ openModal, /*closeModal,*/ children }: { openModal: boolean; /*closeModal: CloseHandler;*/ children: ReactNode }) => {
  // store a reference to the dialog element
  const ref = useRef<HTMLDialogElement>(null)

  // when parent component changes the openModal parameter
  // the dialog will be displayed
  useEffect(() => {
    if (openModal) {
      ref.current?.showModal()
    }
  }, [openModal])

  return (
    <>
      <dialog ref={ref} /*onCancel={handleModalClose}*/>
        <div className='header'>Add Beneficiary</div>
        {children}
      </dialog>
    </>
  )
}

export default ModalDialog