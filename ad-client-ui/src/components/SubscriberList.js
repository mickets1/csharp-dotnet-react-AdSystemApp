import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './styles.css';

const SubscriberList = () => {
    const [subscribers, setSubscribers] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5001/api/subscriber')
            .then(response => {
                setSubscribers(response.data);
            })
            .catch(error => {
                console.error('Error fetching subscribers:', error);
            });
    }, []);

    return (
        <div className="subscriber-list-container">
            <h2>Subscribers</h2>
            <ul className="subscriber-list">
                {subscribers.map(subscriber => (
                    <li key={subscriber.subscriptionNumber} className="subscriber-item">
                        <span className="subscriber-name">Name: {subscriber.name}</span>
                        <p className="subscriber-address">
                        Address: {subscriber.address}<br />
                        Phone Number: {subscriber.phoneNumber}<br />
                        Postal Code: {subscriber.postalCode}<br />
                        City: {subscriber.city}
                        </p>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default SubscriberList;
