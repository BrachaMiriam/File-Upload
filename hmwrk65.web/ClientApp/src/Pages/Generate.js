import React, {useState} from "react";
import { useHistory } from "react-router-dom";

const Generate = () => {

    const [amount, setAmount] = useState();
    const history = useHistory();

    const onGenerateClick = async () => {
        console.log(amount);
        window.location.href=`api/peoplecsv/generate?amount=${amount}`;
    }

    return(
        <div className="d-flex w-100 justify-content-center align-self-center">
            <div className='row'>
                <input type='text' className='form-control-lg' onChange={e => setAmount(e.target.value)} placeholder='Amount'/>
            </div>
            <div className="row">
                <div className="col-md-4">
                    <button className="btn btn-primary btn-lg" onClick={onGenerateClick}>Generate</button>
                </div>
            </div>
        </div>
    )  
}

export default Generate;