function ApiCards(){
    return(
        <div className="apiCards">
            <div className="container-card">
                <p className="title-card">C# Api</p>
                <p className="state-card">State</p>
                <div className="body-card">
                    <div className="port-card">
                        <p className="subtitle-card">Port</p>
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
                    <button className="run-card">Run</button>
                    <button className="stop-card">Stop</button>
                </div>
            </div>
        </div>
    )
}

export default ApiCards