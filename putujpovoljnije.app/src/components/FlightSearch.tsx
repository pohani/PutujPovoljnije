import React, { useState } from 'react';
import { searchFlights, searchAirports } from '../services/api';
import Select from 'react-select';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Typography } from '@mui/material';



interface FlightOffer {
    id: string;
    type: string;
    source: string;
    instantTicketingRequired: boolean;
    nonHomogeneous: boolean;
    oneWay: boolean;
    isUpsellOffer: boolean;
    lastTicketingDate: Date; // Use Date if you parse this correctly
    lastTicketingDateTime: Date; // Use Date if you parse this correctly
    numberOfBookableSeats: number;
    itineraries: Itinerary[];
    price: Price | null; // Ensure this can be null
    validatingAirlineCodes: string[];
    flightSearchId: string; // Add this field
}

interface Itinerary {
    id: string;
    duration: string;
    segments: Segment[];
}

interface Segment {
    id: string;
    arrival: Date | null; // Ensure this can be null
    departure: Date | null; // Ensure this can be null
    carrierCode: string;
    number: string;
    duration: string;
    numberOfStops: number;
    blacklistedInEU: boolean;
}

interface Price {
    id: string;
    currency: string;
    total: string;
    base: string;
    grandTotal: string;
}

interface AirportSuggestion {
    iata: string;
    name: string;
    city: string;
    country: string;
}

interface SelectOption {
    value: string;
    label: string;
}

const FlightSearch: React.FC = () => {
    const [departureDate, setDepartureDate] = useState<string>('');
    const [returnDate, setReturnDate] = useState<string>('');
    const [adults, setAdults] = useState<number>(1);
    const [children, setChildren] = useState<number>(0);
    const [currency, setCurrency] = useState<string>('EUR');
    const [flightOffers, setFlightOffers] = useState<FlightOffer[]>([]);
    const [error, setError] = useState<string | null>(null);
    const today = new Date().toISOString().split('T')[0];
    const [departureAirport, setDepartureAirport] = useState<SelectOption | null>(null);
    const [destinationAirport, setDestinationAirport] = useState<SelectOption | null>(null);
    const [departureSuggestions, setDepartureSuggestions] = useState<SelectOption[]>([]);
    const [destinationSuggestions, setDestinationSuggestions] = useState<SelectOption[]>([]);
    const [hasSearched, setHasSearched] = useState(false);
    const [searchMetadata, setSearchMetadata] = useState({
        departureAirport: '',
        destinationAirport: '',
        departureDate: '',
        returnDate: '',
        adults: 0,
        children: 0,
        currency: '',
    });

    const handleDepartureChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const selectedDate = e.target.value;
        if (selectedDate < today) {
            alert("Departure date cannot be in the past.");
            return;
        }
        setDepartureDate(selectedDate);
        if (returnDate && selectedDate > returnDate) {
            setReturnDate('');
        }
    };

    const handleReturnChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const selectedDate = e.target.value;
        if (selectedDate && selectedDate < departureDate) {
            alert("Return date must be the same day or after the departure date.");
            return;
        }
        setReturnDate(selectedDate);
    };

    const handleSearch = async () => {

        // Validate required fields
        if (!departureAirport) {
            setError('Please select a departure airport.');
            return;
        }
        if (!destinationAirport) {
            setError('Please select a destination airport.');
            return;
        }
        if (!departureDate) {
            setError('Please select a departure date.');
            return;
        }
        if (adults < 1) {
            setError('There must be at least 1 adult.');
            return;
        }

        try {
            const searchData = {
                departureAirport: departureAirport ? departureAirport.value : '',
                destinationAirport: destinationAirport ? destinationAirport.value : '',
                departureDate,
                returnDate,
                adults,
                children,
                currency,
            };

            const response = await searchFlights(searchData);
            if (response.success) {
                // Here, ensure that response.data.flightOffers is the correct structure for FlightOffer
                setFlightOffers(response.data.flightOffers); // This should work with the defined FlightOffer type


                // Set search metadata
                setSearchMetadata({
                    departureAirport: response.data.departureAirport,
                    destinationAirport: response.data.destinationAirport,
                    departureDate: response.data.departureDate,
                    returnDate: response.data.returnDate,
                    adults: response.data.adults,
                    children: response.data.children,
                    currency: response.data.currency,
                });

                setError(null);
            } else {
                setError(response.message);
                setFlightOffers([]);
            }
        } catch (error) {
            console.error('Error fetching flight data:', error);
            setError('Error fetching flight data');
            setFlightOffers([]);
        } finally {
            setHasSearched(true);
        }
    };

    const handleDepartureInputChange = (inputValue: string) => {
        if (inputValue.length >= 2) {
            searchAirports(inputValue)
                .then((suggestions: AirportSuggestion[]) => {
                    setDepartureSuggestions(suggestions.map((suggestion) => ({
                        value: suggestion.iata,
                        label: `${suggestion.iata} - ${suggestion.name} (${suggestion.city}, ${suggestion.country})`
                    })));
                })
                .catch((error) => {
                    console.error(error);
                });
        } else {
            setDepartureSuggestions([]);
        }
    };

    const handleDestinationInputChange = (inputValue: string) => {
        if (inputValue.length >= 2) {
            searchAirports(inputValue)
                .then((suggestions: AirportSuggestion[]) => {
                    setDestinationSuggestions(suggestions.map((suggestion) => ({
                        value: suggestion.iata,
                        label: `${suggestion.iata} - ${suggestion.name} (${suggestion.city}, ${suggestion.country})`
                    })));
                })
                .catch((error) => {
                    console.error(error);
                });
        } else {
            setDestinationSuggestions([]);
        }
    };

    return (
        <div className="container">
            <h1>Pretraži letove</h1>
            <div className="row mt-3">
                <div className="col-md-6">

                    <Select
                        value={departureAirport}
                        onChange={(selectedOption) => setDepartureAirport(selectedOption as SelectOption)}
                        onInputChange={handleDepartureInputChange}
                        options={departureSuggestions}
                        placeholder="Enter departure airport code or name"
                        isClearable
                        isSearchable
                        styles={{ menu: (base) => ({ ...base, zIndex: 1000 }) }}
                    />
                </div>
                <div className="col-md-6">

                    <Select
                        value={destinationAirport}
                        onChange={(selectedOption) => setDestinationAirport(selectedOption as SelectOption)}
                        onInputChange={handleDestinationInputChange}
                        options={destinationSuggestions}
                        placeholder="Enter destination airport code or name"
                        isClearable
                        isSearchable
                        styles={{ menu: (base) => ({ ...base, zIndex: 1000 }) }}
                    />
                </div>
            </div>
            <div className="row  mt-3">
                {/* Departure Date */}
                <div className="col-md-6">
                    <div className="mb-3">
                        <label className="form-label">Departure Date</label>
                        <input
                            type="date"
                            className="form-control"
                            value={departureDate}
                            onChange={handleDepartureChange}
                            required
                            min={today}
                        />
                    </div>
                </div>

                {/* Return Date */}
                <div className="col-md-6">
                    <div className="mb-3">
                        <label className="form-label">Return Date (Optional)</label>
                        <input
                            type="date"
                            className="form-control"
                            value={returnDate}
                            onChange={handleReturnChange}
                            min={departureDate}
                        />
                    </div>
                </div>
            </div>
            <div className="row">
                {/* Number of Adults */}
                <div className="col-md-4">
                    <div className="mb-3">
                        <label className="form-label">Number of Adults</label>
                        <input
                            type="number"
                            className="form-control"
                            min="1"
                            max="9"
                            value={adults}
                            onChange={(e) => setAdults(Number(e.target.value))}
                            required
                        />
                    </div>
                </div>

                {/* Number of Children */}
                <div className="col-md-4">
                    <div className="mb-3">
                        <label className="form-label">Number of Children</label>
                        <input
                            type="number"
                            className="form-control"
                            min="0"
                            max="9"
                            value={children}
                            onChange={(e) => setChildren(Number(e.target.value))}
                        />
                    </div>
                </div>

                {/* Currency */}
                <div className="col-md-4">
                    <div className="mb-3">
                        <label className="form-label">Currency</label>
                        <select
                            className="form-select"
                            value={currency}
                            onChange={(e) => setCurrency(e.target.value)}
                            required
                        >
                            <option value="USD">USD - US Dollar</option>
                            <option value="EUR">EUR - Euro</option>
                            <option value="HRK">HRK - Croatian Kuna</option>
                        </select>
                    </div>
                </div>
            </div>
            <div className="row mb-3">
                <div className="d-flex justify-content-end">
                    <button className="btn btn-primary" onClick={handleSearch}>Search</button>
                </div>
            </div>
            {!hasSearched && error && (
                <div className="alert alert-danger mt-3">
                    {error}
                </div>
            )}
            {hasSearched && (
                <div>
                    {error ? (
                        <div className="alert alert-danger text-center" role="alert" style={{ fontSize: '1.5rem', fontWeight: 'bold' }}>
                            <i className="fas fa-exclamation-circle"></i> {error}
                        </div>
                    ) : flightOffers.length > 0 ? (
                        <div>
                            <Typography variant="h4" gutterBottom>
                                Dostupni letovi
                            </Typography>
                            <TableContainer component={Paper}>
                                <Table>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell className='fw-bolder'>Polazni aerodrom</TableCell>
                                            <TableCell className='fw-bolder'>Odredišni aerodfrom</TableCell>
                                            <TableCell className='fw-bolder'>Datum polaska</TableCell>
                                            <TableCell className='fw-bolder'>Datum povratka</TableCell>
                                            <TableCell className='fw-bolder'>Broj presjedanja (Odlazni)</TableCell>
                                            <TableCell className='fw-bolder'>Broj presjedanja (Povratni)</TableCell>
                                            <TableCell className='fw-bolder'>Broj putnika</TableCell>
                                            <TableCell className='fw-bolder'>Cijena</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {flightOffers.map((offer) => {
                                            const outwardItinerary = offer.itineraries[0]; // Assuming the first itinerary is outward
                                            const returnItinerary = offer.itineraries[1]; // Assuming the second itinerary is return (if available)

                                            // Get departure date from the first segment of the outward itinerary
                                            const outwardDepartureDate = searchMetadata.departureDate; // Extract the actual departure datetime if it exists
                                            const returnDepartureDate = searchMetadata.returnDate; // Return date

                                            // Count number of stopovers
                                            const outwardStopovers = outwardItinerary.segments.length - 1; // Number of segments - 1 = number of stopovers
                                            const returnStopovers = returnItinerary ? returnItinerary.segments.length - 1 : 0; // Handle case if return itinerary doesn't exist

                                            // Number of people based on metadata
                                            const numberOfPeople = searchMetadata.adults + searchMetadata.children;

                                            return (
                                                <TableRow key={offer.id}>
                                                    <TableCell>{searchMetadata.departureAirport || 'N/A'}</TableCell>
                                                    <TableCell>{searchMetadata.destinationAirport || 'N/A'}</TableCell>
                                                    <TableCell>{outwardDepartureDate ? new Date(outwardDepartureDate).toLocaleString() : 'N/A'}</TableCell>
                                                    <TableCell>{returnDepartureDate ? new Date(returnDepartureDate).toLocaleString() : 'N/A'}</TableCell>
                                                    <TableCell>{outwardStopovers}</TableCell>
                                                    <TableCell>{returnStopovers}</TableCell>
                                                    <TableCell>{numberOfPeople}</TableCell>
                                                    <TableCell>{offer.price ? `${offer.price.currency} ${offer.price.grandTotal}` : 'N/A'}</TableCell>
                                                </TableRow>
                                            );
                                        })}
                                    </TableBody>
                                </Table>
                            </TableContainer>
                        </div>
                    ) : (
                        <div className="alert alert-warning text-center" role="alert" style={{ fontSize: '1.5rem', fontWeight: 'bold' }}>
                            <i className="fas fa-exclamation-circle"></i> No flights found.
                        </div>
                    )}
                </div>
            )}
        </div >
    );
};

export default FlightSearch;
