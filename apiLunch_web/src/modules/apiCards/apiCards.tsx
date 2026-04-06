import React, { useEffect, useState } from 'react';
interface Datas {
    NameApi: string;
    Execute: string;
    Folder: string;
    Port: string;
}

const runApi = (api: Datas) => {
    console.log("Datos Enviados para ejecutar:", api);

    if (window.chrome && window.chrome.webview) 
    {
        // Enviamos un objeto JSON o un string al C#
        // Es el método que lanza un "paquete de datos" a través del túnel hacia C#.
        window.chrome.webview.postMessage({
            action: "RunApi",
            nameApi: api.NameApi,
            execute: api.Execute,
            folder: api.Folder,
            port: api.Port
        });
    } 
    else 
    {
        console.error("WebView2 no detectado. ¿Estás en un navegador normal?");
    }

}

const ApiCards: React.FC = () => {
    const [data, setdata] = useState<Datas | null>(null);
    
    useEffect(() => {
        const handleMessage = (event: any) => {
        let dataRecibida = event.data;
        if (typeof event.data === 'string') {
            try {
                dataRecibida = JSON.parse(event.data);
            } catch (e) {
                console.error("Error parseando JSON", e);
            }
        }
        
        // El estado se actualiza
        setdata(dataRecibida);
        };

        const webview = window.chrome?.webview;
        if (webview) {
            webview.addEventListener('message', handleMessage);
        }
        
        return () => {
            if (webview) webview.removeEventListener('message', handleMessage);
        };
    }, []); // [] significa: "Ejecuta esto solo cuando el componente nazca"}

    return (
    <div style={{ display: 'flex' }}>
        {Array.isArray(data) && data.map((element, index) => (
        /* Key sirve como identificador asi React sabe quien es quien */
        <div key={index} className="container-card">
            <p className="title-card">{element.NameApi}</p>
            <p className="state-card">la carpeta:{element.Folder}</p>
            <div className="body-card">
                <div className="port-card">
                    <p className="subtitle-card">{element.Port}</p>
                    <p>5000</p>
                </div>
                <div className="cpu-card">
                    <p className="subtitle-card">CPU</p>
                    <p>12%</p>
                </div>
                <div className="ram-card">
                    <p className="subtitle-card">RAM</p>
                    <p>150MB</p>
                </div>
            </div>
            <div className="options-card">
                <button className="logs-card">Logs</button>
                <button className="run-card" onClick={() => runApi(element)}>Run</button>
                <button className="stop-card">Stop</button>
            </div>
        </div>
    ))}
    </div>
);
};

export default ApiCards