// Definimos la interfaz para que TS no se queje
declare global {
    interface Window {
        chrome: any;
    }
}

const AddApi = () => {
    const executeOpenDialogFile = () => {
        // El objeto window.chrome.webview solo existe 
        // si la página se está ejecutando dentro de un control WebView2 de Microsoft.
        if (window.chrome && window.chrome.webview) 
        {
            // Enviamos un objeto JSON o un string al C#
            // Es el método que lanza un "paquete de datos" a través del túnel hacia C#.
            window.chrome.webview.postMessage({
                action: 'OpenDialogFile',
            });
        } 
        else 
        {
            console.error("WebView2 no detectado. ¿Estás en un navegador normal?");
        }
    };

    return (
            <button 
                className="AddApi"
                onClick={executeOpenDialogFile}>
                Agregar Api
            </button>
        );
};

export default AddApi