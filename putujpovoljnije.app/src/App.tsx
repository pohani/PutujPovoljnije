import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import FlightSearch from './components/FlightSearch';
import Airports from './components/Airports';
import Home from './components/Home.tsx';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';


const App: React.FC = () => {
    return (
        <Router>
            <div className="d-flex flex-column min-vh-100" style={{ backgroundColor: '#f8f9fa' }}>

                <div className="d-flex justify-content-center bg-warning py-3 ">
                    <div className="w-75">

                        <Link className=" navbar-brand fs-1 fw-bold " to="/">Putuj Povoljnije</Link>
                    </div>
                </div>

                <nav className="navbar navbar-expand-lg navbar-dark bg-dark fw-bold">
                    <div className="container w-75">
                        <button
                            className="navbar-toggler"
                            type="button"
                            data-bs-toggle="collapse"
                            data-bs-target="#navbarNav"
                            aria-controls="navbarNav"
                            aria-expanded="false"
                            aria-label="Toggle navigation"
                        >
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="collapse navbar-collapse justify-content-center" id="navbarNav">
                            <ul className="navbar-nav">
                                <li className="nav-item mx-2">
                                    <Link className="nav-link" to="/">Početna</Link>
                                </li>
                                <li className="nav-item mx-2">
                                    <Link className="nav-link" to="/search">Pretraži letove</Link>
                                </li>
                                <li className="nav-item mx-2">
                                    <Link className="nav-link" to="/airports">Zračne luke</Link>
                                </li>
                                {/* <li className="nav-item mx-2">
                                    <Link className="nav-link" to="/">O nama</Link>
                                </li> */}
                            </ul>
                        </div>
                    </div>
                </nav>
                {/* Content */}
                <div className="container flex-grow-1 py-4 w-75"    >
                    <Routes>
                        <Route path="/search" element={<FlightSearch />} />
                        <Route path="/airports" element={<Airports />} />
                        <Route path="/refresh-airports" element={<Home />} />
                        <Route path="/" element={<Home />} />
                    </Routes>
                </div>
            </div>
        </Router >
    );
};

export default App;
