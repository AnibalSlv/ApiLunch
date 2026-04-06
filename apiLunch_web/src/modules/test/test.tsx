import React, { useEffect, useState } from 'react';

interface Datas {
    NameApi: string;
    Execute: string;
    Folder: string;
    Port: string;
}

const ReceptorWPF: React.FC = () => {
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
        
        // Actualizamos el estado
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
    <div style={{ padding: '20px' }}>
        <pre>{JSON.stringify(data, null, 2)}</pre> {/* Esto mostrará el JSON crudo en pantalla */}
        
                <h1>Pantalla de React</h1>
        <h3>Depuración:</h3>
        <p>¿data es null?: {data === null ? "SÍ" : "NO"}</p>
        
        
        
        {Array.isArray(data) && data.map((data, index) => (
        /* Key sirve como identificador asi React sabe quien es quien */
        <div key={index}>
            <p><strong>Nombre:</strong> {data.NameApi}</p>
            <p><strong>Mensaje:</strong> {data.Execute}</p>
            <p><strong>Carpeta:</strong> {data.Folder}</p>
            <p><strong>Puerto:</strong> {data.Port}</p>
        </div>
    ))}
    </div>
);
};

export default ReceptorWPF;