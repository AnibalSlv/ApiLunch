import React, { useState, useEffect } from 'react';

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

useEffect(() => {
    if (window.chrome?.webview) {
        const handleWpfMessage = (event: any) => {
            let messageData = event.data;

            // 1. Log para ver qué llega exactamente
            console.log("Mensaje recibido de WPF:", messageData);

            // 2. Lógica de discriminación
            if (typeof messageData === 'string') {
                // Si el string NO empieza con '{', es una ruta directa de archivo/carpeta
                if (!messageData.trim().startsWith('{')) {
                    // Detectamos qué campo actualizar basándonos en el mensaje anterior o el contexto
                    // Si WPF solo manda la ruta tras 'OpenFileDialog', la asignamos al campo correspondiente
                    setData(prev => ({
                        ...prev,
                        // Aquí una lógica simple: si parece ejecutable va a 'execute', si no a 'folder'
                        [messageData.toLowerCase().endsWith('.exe') ? 'execute' : 'folder']: messageData
                    }));
                    return; // Salimos, no hace falta parsear nada
                }

                // Si parece un JSON, intentamos parsearlo
                try {
                    messageData = JSON.parse(messageData);
                } catch (e) {
                    console.error("Error al parsear el supuesto JSON:", e);
                    return;
                }
            }

            // 3. Procesar como objeto estructurado (SYNC_FORM)
            const { type, payload } = messageData;
            if (type === 'SYNC_FORM') {
                setData(prev => ({ ...prev, ...payload }));
            }
        };

        window.chrome.webview.addEventListener('message', handleWpfMessage);
        return () => window.chrome.webview.removeEventListener('message', handleWpfMessage);
    }
}, []);

    const manejarCambio = (event: React.ChangeEvent<HTMLInputElement>) => {
        setData({
            ...data,
            [event.target.name]: event.target.value
        });
    }

    const sendSearchFile = () => {
        console.log("Buscando Archivo...")
        if(window.chrome && window.chrome.webview)
        {
            window.chrome.webview.postMessage({
                action: 'OpenFileDialog',
            })
        }
        else 
        {
            console.error("WebView2 no detectado. ¿Estás en un navegador normal?");
        }
    }

    const sendSearchFolder = () => {
        console.log("Buscando Carpeta...")
        if(window.chrome && window.chrome.webview)
        {
            window.chrome.webview.postMessage({
                action: "OpenFolderDialog",
            })
        } 
        else
        {
            console.error("Webview2 no detectado. ¿Estás en un navegador normal?")
        }
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
                    <div className="div-flex-modal-addApi">
                        <label>Ejecutable de python:</label>
                        <div>
                            <input className="input-modal-addApi" 
                                    name="execute"
                                    type="text" 
                                    placeholder="Seleccione el Ejecutable"
                                    value={data.execute}
                                    onChange={manejarCambio}/>
                            <button type="button" onClick={sendSearchFile}>Agregar</button>
                        </div>
                    </div>
                    <div className="div-flex-modal-addApi">
                        <label>Carpeta Contenedora de la Api:</label>
                        <div>
                        <input className="input-modal-addApi" 
                                name="folder"
                                type="text" 
                                placeholder="Carpeta Contenedora de la Api"
                                value={data.folder}
                                onChange={manejarCambio}/>
                        <button type="button" onClick={sendSearchFolder}>Agregar</button>
                    </div>
                    </div>
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