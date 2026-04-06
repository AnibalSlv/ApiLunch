import './App.css'
import Navbar from './modules/navbar'
import ApiCards from './modules/apiCards'
import AddApi from './modules/addApi'
import ModalAddApi from './components/modals/addApi'
import { useState } from 'react'

function App() {
    const [visible, setVisible] = useState<boolean>(false);
    const openModal = () => setVisible(true);
    const closeModal  = () => setVisible(false);

    return (
        <>
            <Navbar/>
            <ApiCards/>
            <AddApi onOpen={openModal}/>
            {visible && <ModalAddApi onClose={closeModal}/>}
        </>
    )
}

export default App
