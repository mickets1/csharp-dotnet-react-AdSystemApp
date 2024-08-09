import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './styles.css';

const AdList = () => {
    const [ads, setAds] = useState([]);

    useEffect(() => {
        const fetchAds = async () => {
            try {
                const response = await axios.get('http://localhost:5000/api/ads/ads');
                const adsData = response.data.$values || response.data || [];
                
                setAds(adsData);
            } catch (error) {
                console.error('Error fetching ads:', error);
            }
        };

        fetchAds();
    }, []);

    return (
        <div className="ad-list-container">
            <h2>Advertisements</h2>
            <ul>
                {ads.length > 0 ? (
                    ads.map((ad) => (
                        <li key={ad.id}>
                            <br/>
                            <p>Ad Id: {ad.id}</p>
                            <h3>{ad.title}</h3>
                            <p>{ad.content}</p>
                            <p>Item Price: ${ad.itemPrice}</p>
                            <p>Ad Cost: ${ad.adPrice}</p>
                            <p>Posted By: {ad.advertiserName}</p>
                        </li>
                    ))
                ) : (
                    <p>No advertisements available.</p>
                )}
            </ul>
        </div>
    );
};

export default AdList;
