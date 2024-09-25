import React, { useEffect, useState } from 'react';
import { fetchAirports } from '../services/api'; // Adjust the path as necessary
import { refreshAirports } from '../services/api'; // Import the function
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Typography } from '@mui/material';
import InfiniteScroll from 'react-infinite-scroll-component';

const Airports: React.FC = () => {
    const [airports, setAirports] = useState<any[]>([]);
    const [error, setError] = useState<string | null>(null);
    const [hasMore, setHasMore] = useState(true);
    const [page, setPage] = useState(0); // For pagination

    const handleRefresh = async () => {
        try {
            const response = await refreshAirports();
            const message = response?.data?.message || 'Airports refreshed successfully.';
            alert(message);
        } catch (error) {
            console.error('Error refreshing airports:', error);
            alert('Failed to refresh airports');
        }
    };

    const loadAirports = async (page: number) => {
        try {
            const data = await fetchAirports(page);
            setAirports((prev) => [...prev, ...data]); // Append new data
            setHasMore(data.length > 0); // Check if there are more airports
        } catch (err) {
            setError('Failed to fetch airports');
        }
    };

    useEffect(() => {
        loadAirports(page); // Load initial data
    }, [page]);

    return (
        <div>
            <div>
                <Typography variant="h5">Osvježi popis zračnih luka</Typography>
                <button className='mt-4 m-2 p-2 px-4 btn btn-primary mb-5' onClick={handleRefresh}>Osvježi</button>
            </div>

            {/* <Typography variant="h4">Popis zračnih luka</Typography> */}
            {error && <p>{error}</p>}

            <InfiniteScroll
                dataLength={airports.length} // This is important field to render the next data
                next={() => setPage((prev) => prev + 1)} // Function to fetch next page
                hasMore={hasMore}
                loader={<h4>Loading...</h4>}
                endMessage={
                    <p style={{ textAlign: 'center' }}>
                        <b>No more airports</b>
                    </p>
                }
            >
                <TableContainer className='' component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell className='fw-bolder'>Ime Aerodroma</TableCell>
                                <TableCell className='fw-bolder'>Grad</TableCell>
                                <TableCell className='fw-bolder'>State</TableCell>
                                <TableCell className='fw-bolder'>Država</TableCell>
                                <TableCell className='fw-bolder'>IATA</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {airports.map((airport) => (
                                <TableRow key={airport.iata}>
                                    <TableCell>{airport.name}</TableCell>
                                    <TableCell>{airport.city}</TableCell>
                                    <TableCell>{airport.state || 'N/A'}</TableCell>
                                    <TableCell>{airport.country}</TableCell>
                                    <TableCell>{airport.iata}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            </InfiniteScroll>
        </div>
    );
};

export default Airports;
