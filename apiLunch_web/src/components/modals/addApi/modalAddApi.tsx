import React, { useState } from "react"

interface ModalProps {
    onClose: () => void
}

interface FormData{
    nameApi: string,
    execute: string,
    folder: string,
    port: string,
}

function ModalAddApi ({ onClose }: ModalProps){
    const [data, setData] = useState<FormData>({
            nameApi: '',
            execute: '',
            folder: '',
            port: ''
        });

    const manejarCambio = (event: React.ChangeEvent<HTMLInputElement>) => {
        setData({
            ...data,
            [event.target.name]: event.target.value
        });
    }

    

    //! SI COLOCAS UN BTN TYPE SUBMIT DENTRO DEL FORM SE EJECUTA ESTA FUNICON CUANDO SE LE DE CLICK AL BOTON
    const sendForm = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log("Datos Enviados:", data);

        if (window.chrome && window.chrome.webview) 
        {
            // Enviamos un objeto JSON o un string al C#
            // Es el método que lanza un "paquete de datos" a través del túnel hacia C#.
            window.chrome.webview.postMessage({
                action: 'SaveAddApi',
                nameApi: data.nameApi,
                execute: data.execute,
                folder: data.folder,
                port: data.port
            });
        } 
        else 
        {
            console.error("WebView2 no detectado. ¿Estás en un navegador normal?");
        }
        onClose();
    }

    return(
        <form onSubmit={sendForm}>
            <div className="container-modal-addApi">
                <div className="modal-addApi">
                    <label>Nombre de la Api:</label>
                    <input className="input-modal-addApi"
                            name="nameApi" 
                            type="text" 
                            placeholder="Nombre de la Api"
                            value={data.nameApi}
                            onChange={manejarCambio}/>
                    <label>Ejecutable de python:</label>
                    <input className="input-modal-addApi" 
                            name="execute"
                            type="text" 
                            placeholder="Seleccione el Ejecutable"
                            value={data.execute}
                            onChange={manejarCambio}/>
                    <label>Carpeta Contenedora de la Api:</label>
                    <input className="input-modal-addApi" 
                            name="folder"
                            type="text" 
                            placeholder="Carpeta Contenedora de la Api"
                            value={data.folder}
                            onChange={manejarCambio}/>
                    <label>Puerto:</label>
                    <input className="input-modal-addApi" 
                            name="port"
                            type="number" 
                            placeholder="8000"
                            value={data.port}
                            onChange={manejarCambio}/>
                    <div className="container-btn-modal-addApi">
                        <button className="btn-cancel-modal-addApi" onClick={onClose}>Cancelar</button>
                        <button type="submit" className="btn-save-modal-addApi">Guardar</button>
                    </div>
                </div>
            </div>
        </form>
    )
}

export default ModalAddApi