import Beneficiaries from "../bussiness/Beneficiaries";
import Donors from "../bussiness/Donors";
import Card from "../controls/Card/Card";
import CardBody from "../controls/Card/CardBody";
import CardHeader from "../controls/CardHeader";
import Button from "../controls/button/Button";

const Admin =()=>
{
    return(
<>
<Card>
    <CardHeader>Beneficiaries</CardHeader>
   <CardBody>
    <Beneficiaries/>
  </CardBody>
</Card>
<br></br>

<Card>
    <CardHeader>Donors</CardHeader>
   <CardBody>
    <Donors/>
  </CardBody>
</Card>

</>

    )
}
export default Admin;