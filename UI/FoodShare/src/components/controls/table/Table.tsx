import {ReactNode, useState, useEffect} from 'react'

import { IActions, IBeneficiary } from '../../../types/CommonTypes'

import './Table.css'

/**
 * Table component will generate a table using the HTML `table` tag.
 *
 * @prop header - an array of strings containing the header text for each column
 * @prop actions - an array of actions that can be done on a table row. Example: Add, Delete, View
 * @prop data - an object containing the data content of the table
 */
const Table = ({ header, actions = null, data }: { header: string[], actions: IActions[] | null, data: IBeneficiary[] }) => {
  const [tableData, setTableData] = useState<IBeneficiary[]>([])
  useEffect(() => {
    setTableData(data)
  }, [data]);
  /**
   * This function generated the cells containing the action buttons. On the buttons there is an icon representing
   * the specific action. It is called above when we map on the actions sent to the component as a prop.
   * @param action - the current action for which the row will be generated.
   */
  const generateIconCells = ((action: IActions) => {
    const actionItem: ReactNode[] = []
    switch (action.actionName) {
      case 'Add':
        actionItem.push(<td style={{ textAlign: 'center', }}><button onClick={action.actionHandler}><i className='fa fa-plus'></i></button></td>)
        break
      case 'Edit':
        actionItem.push(<td style={{ textAlign: 'center', }}><button onClick={action.actionHandler}><i className='fa fa-pencil'></i></button ></td >)
        break
      case 'Delete':
        actionItem.push(<td style={{ textAlign: 'center', }}><button onClick={action.actionHandler}><i className='fa fa-trash'></i></button></td>)
        break
    }
    return actionItem
  })


  type GetHeader = () => ReactNode
  const getHeader: GetHeader = () => {
    const mappedHeader = header.map((item, index) => {
      return <th key={index}>{item}</th>
    })

    if (actions) {
      mappedHeader.push(<th colSpan={actions.length}>Actions</th>)
    }

    return <tr>{mappedHeader}</tr>
  }

  /**
   * This function generates a row in the table by mapping on the table `data` and constructing a table row
   * using the `td` HTML tag.
   */
  type GetDataRows = () => ReactNode[]
  const getDataRows: GetDataRows = () => {
    const rowData: ReactNode[] = tableData.map((element: IBeneficiary) => {
      const row = Object.keys(element).map(key => generateDataRows(key, element))
      let actionsRow: ReactNode[] = []
      if (actions) {
        actionsRow = actions.map(generateIconCells)
      }
      return <tr key={element.id} data-key={element.id}>{row}{actionsRow}</tr>
    })

    return rowData
  }

  const generateDataRows = (key: string, element: IBeneficiary) => {
    if (key === 'id') return
    const align = typeof element[key as keyof IBeneficiary] === 'number' ? 'right' : 'left'
    return <td key={key} style={{ textAlign: align }}>{element[key as keyof IBeneficiary]}</td>
  }

  return (
    <>
      <table>
        <thead>
          {getHeader()}
        </thead>
        <tbody>
          {getDataRows()}
        </tbody>
      </table>
    </>
  )
}

export default Table