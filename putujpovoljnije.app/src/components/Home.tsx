import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
// Import your image from assets
import logo from '../assets/c4df9e_europska-zrakoplovom.jpg'; // Adjust the path based on your folder structure

const Home: React.FC = () => {
    return (
        <div className="text-center"> {/* Centering content using Bootstrap */}
            <h1>Dobro došli na Putuj Povoljnije</h1>
            {/* Display the image */}
            <img src={logo} alt="Flight Search Logo" className="img-fluid mt-3" />
            <h3 className='mt-3'>Ovdje vaše putovanje počinje</h3>
        </div>
    );
};

export default Home;
