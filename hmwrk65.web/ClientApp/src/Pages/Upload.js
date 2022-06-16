import React, {useRef} from "react";
import axios from 'axios';
import { useHistory } from "react-router-dom";

const Upload = () => {

    const toBase64 = file => new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });

    const history = useHistory();
    const fileInputRef = useRef(null);

    const onUpLoadClick = async () => {
        const file = fileInputRef.current.files[0];
        const base64 = await toBase64(file);
        await axios.post('/api/peoplecsv/upload', {base64});
        history.push('/');
    }

    return (
        <div className="d-flex w-100 justify-content-center align-self-center">
            <div className="row">
                <div className="col-md-10">
                    <input ref={fileInputRef} type='file' className="form=control-lg"></input>
                </div>
                <div className="col-md-12">
                    <button className="btn btn-primary btn-lg" onClick={onUpLoadClick}>Upload</button>
                </div>
            </div>
        </div>
    )
}
export default Upload;




