import React, { useState } from 'react';
import axios from 'axios';
import FetchSubscriber from './FetchSubscriber';
import './styles.css';

const SubscriberAdForm = () => {
    const [subscriptionNumber, setSubscriptionNumber] = useState('');
    const [advertiserInfo, setAdvertiserInfo] = useState({
        name: '',
        address: '',
        postalCode: '',
        city: '',
        phoneNumber: '',
        subscriptionNumber: 0,
    });
    const [adContent, setAdContent] = useState({
        title: '',
        content: '',
        itemPrice: 0
    });

    const handleAdContentChange = (event) => {
        const { name, value } = event.target;
        setAdContent({ ...adContent, [name]: value });
    };

    const handleAdvertiserInfoChange = (event) => {
        const { name, value } = event.target;
        setAdvertiserInfo({ ...advertiserInfo, [name]: value });
    };

    const handleSubmitAd = async (event) => {
        event.preventDefault();

        try {
            const advertisement = {
                title: adContent.title,
                content: adContent.content,
                itemPrice: adContent.itemPrice,
                subscriptionNumber: parseInt(subscriptionNumber),
                subscriber: {
                    ...advertiserInfo,
                    isSubscriber: true,
                }
            };


            const response = await axios.post(
                'http://localhost:5000/api/ads/subscriber',
                advertisement,
                { headers: { 'Content-Type': 'application/json' } }
            );
            console.log(response.data)

            console.log('Advertisement submitted successfully:', response.data);
        } catch (error) {
            console.error('Error submitting advertisement:', error);
        }
    };

    const handleUpdateAdvertiser = async (event) => {
        event.preventDefault();

        try {
            console.log(advertiserInfo)
            const response = await axios.put(
                `http://localhost:5001/api/subscriber/${advertiserInfo.subscriptionNumber}`,
                advertiserInfo,
                { headers: { 'Content-Type': 'application/json' } }
            );

            console.log('Subscriber updated successfully:', response.data);
        } catch (error) {
            console.error('Error updating subscriber:', error);
        }
    };

    return (
        <div className="add-advertisement-container">
            <h2>Add or Update Subscriber Advertisement</h2>
            <FetchSubscriber
                subscriptionNumber={subscriptionNumber}
                setSubscriptionNumber={setSubscriptionNumber}
                setAdvertiserInfo={setAdvertiserInfo}
            />
            <form onSubmit={handleSubmitAd}>
                <input
                    type="text"
                    placeholder="Name"
                    name="name"
                    value={advertiserInfo.name}
                    readOnly
                />
                <input
                    type="text"
                    placeholder="Address"
                    name="address"
                    value={advertiserInfo.address}
                    readOnly
                />
                <input
                    type="text"
                    placeholder="Postal Code"
                    name="postalCode"
                    value={advertiserInfo.postalCode}
                    readOnly
                />
                <input
                    type="text"
                    placeholder="City"
                    name="city"
                    value={advertiserInfo.city}
                    readOnly
                />
                <input
                    type="text"
                    placeholder="Phone Number"
                    name="phoneNumber"
                    value={advertiserInfo.phoneNumber}
                    readOnly
                />
                <input
                    type="text"
                    placeholder="Headline"
                    name="title"
                    value={adContent.title}
                    onChange={handleAdContentChange}
                />
                <textarea
                    placeholder="Content"
                    name="content"
                    value={adContent.content}
                    onChange={handleAdContentChange}
                />
                <input
                    type="number"
                    placeholder="Advertisement Price"
                    name="itemPrice"
                    value={adContent.itemPrice}
                    onChange={handleAdContentChange}
                />
                <button type="submit">Submit Advertisement</button>
            </form>
            <form onSubmit={handleUpdateAdvertiser}>
                <h2>Update Subscriber Information</h2>
                <input
                    type="text"
                    placeholder="Name"
                    name="name"
                    value={advertiserInfo.name}
                    onChange={handleAdvertiserInfoChange}
                />
                <input
                    type="text"
                    placeholder="Address"
                    name="address"
                    value={advertiserInfo.address}
                    onChange={handleAdvertiserInfoChange}
                />
                <input
                    type="text"
                    placeholder="Postal Code"
                    name="postalCode"
                    value={advertiserInfo.postalCode}
                    onChange={handleAdvertiserInfoChange}
                />
                <input
                    type="text"
                    placeholder="City"
                    name="city"
                    value={advertiserInfo.city}
                    onChange={handleAdvertiserInfoChange}
                />
                <input
                    type="text"
                    placeholder="Phone Number"
                    name="phoneNumber"
                    value={advertiserInfo.phoneNumber}
                    onChange={handleAdvertiserInfoChange}
                />
                <button type="submit">Update Information</button>
            </form>
        </div>
    );
};

export default SubscriberAdForm;
