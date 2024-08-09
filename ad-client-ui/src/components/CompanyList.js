import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './styles.css';

const AdvertiserList = () => {
    const [advertisers, setAdvertisers] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5000/api/advertiser')
            .then(response => {
                const advertisersData = response.data.$values || [];
                console.log(advertisersData)
                setAdvertisers(advertisersData);
            })
            .catch(error => {
                console.error('Error fetching advertisers:', error);
            });
    }, []);

    return (
        <div className="advertiser-list-container">
            <h2>Company Advertisers</h2>
            <ul className="advertiser-list">
                {advertisers.length > 0 ? (
                    advertisers.map(advertiser => (
                        <li key={advertiser.id} className="advertiser-item">
                            <span className="advertiser-name">Name: {advertiser.name}</span>
                            <p className="advertiser-address">
                            Address: {advertiser.address}<br />
                            Postal Code: {advertiser.postalCode}<br />
                            City: {advertiser.city}<br />
                            BillingAddress: {advertiser.billingAddress}<br />
                            BillingPostalCode: {advertiser.billingPostalCode}<br />
                            BillingCity: {advertiser.billingCity}<br />
                            </p>
                        </li>
                    ))
                ) : (
                    <p>No advertisers available.</p>
                )}
            </ul>
        </div>
    );
}

export default AdvertiserList;
