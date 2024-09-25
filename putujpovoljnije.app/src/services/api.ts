// src/services/api.ts
import axios from 'axios';

const API_URL = 'https://localhost:7137/api'; // Adjust based on your API structure

const api = axios.create({
    baseURL: API_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

export const fetchAirports = async (page: number, limit: number = 20) => {
    try {
        const response = await api.get('/airports/GetPageable', {
            params: {
                page: page,
                limit: limit
            }
        });
        return response.data;
    } catch (error) {
        console.error('Error fetching airports:', error);
        throw error;
    }
};


export const fetchAllAirports = async () => {
    try {
        const response = await api.get('/airports/GetAll');
        return response.data;
    } catch (error) {
        console.error('Error fetching airports:', error);
        throw error;
    }
};


export const refreshAirports = async () => {
    const response = await api.post('/DataRefresh/airports');
    return response.data;
};

// New function for searching flights
export const searchFlights = async (searchData: {
    departureAirport: string;
    destinationAirport: string;
    departureDate: string;
    returnDate?: string;
    adults: number;
    children: number;
    currency: string;
}) => {
    const response = await api.post('/FlightSearch/search', searchData);
    return response.data;
};

export const searchAirports = async (searchString: string) => {
    try {
        const response = await api.get(`/Airports/search?searchString=${encodeURIComponent(searchString)}`);
        return response.data.data;
    } catch (error) {
        console.error('Error searching for airports:', error);
        throw error;
    }
};