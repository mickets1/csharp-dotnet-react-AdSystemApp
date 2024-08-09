import React, { useState } from 'react';
import axios from 'axios';
import './styles.css';

const FetchSubscriber = ({ subscriptionNumber, setSubscriptionNumber, setAdvertiserInfo }) => {
    const [errorMessage, setErrorMessage] = useState('');

    const handleSubscriptionNumberChange = (event) => {
        setSubscriptionNumber(event.target.value);
    };

    const handleFetchSubscriber = async () => {
        try {
            // Check if subscription number is valid before making a request
            if (!subscriptionNumber) {
                setErrorMessage('Please enter a valid subscription number.');
                return;
            }

            const response = await axios.get(`http://localhost:5001/api/subscriber/${subscriptionNumber}`);
            const subscriberData = response.data;
            console.log(subscriberData);

            setAdvertiserInfo({
                name: subscriberData.name,
                address: subscriberData.address,
                postalCode: subscriberData.postalCode,
                city: subscriberData.city,
                billingAddress: subscriberData.address,
                phoneNumber: subscriberData.phoneNumber,
                subscriptionNumber: parseInt(subscriptionNumber)
            });

            // Clear any previous error message if the fetch is successful
            setErrorMessage('');
        } catch (error) {
            if (error.response && error.response.status === 404) {
                setErrorMessage('Subscriber not found.');
            } else {
                setErrorMessage('An error occurred while fetching subscriber data. Please try again later.');
            }
        }
    };

    return (
        <div className="fetch-subscriber-container">
            <h2>Fetch Subscriber Details</h2>
            <input
                type="number"
                placeholder="Subscription Number"
                value={subscriptionNumber}
                onChange={handleSubscriptionNumberChange}
            />
            <button onClick={handleFetchSubscriber}>Fetch Subscriber</button>
            {errorMessage && <p className="error-message">{errorMessage}</p>}
        </div>
    );
};

export default FetchSubscriber;
