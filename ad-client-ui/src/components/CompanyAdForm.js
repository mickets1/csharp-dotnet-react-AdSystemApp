import React, { useState } from 'react';
import axios from 'axios';
import './styles.css';

const CompanyAdForm = () => {
    const [advertiserInfo, setAdvertiserInfo] = useState({
        name: 'Default Company Name',
        organizationNumber: '12345678',
        phoneNumber: '123-456-7890',
        address: '123 Main St',
        postalCode: '12345',
        city: 'Your City',
        billingAddress: '123 Billing St',
        billingPostalCode: '54321',
        billingCity: 'Billing City',
        title: 'Default Ad Title',
        content: 'Default Ad Content',
        itemPrice: 0
    });

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setAdvertiserInfo({ ...advertiserInfo, [name]: value });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            const response = await axios.post(
                'http://localhost:5000/api/ads/company',
                advertiserInfo,
                { headers: { 'Content-Type': 'application/json' } }
            );

            console.log('Advertisement submitted successfully:', response.data);
            // Optionally reset the form or show a success message here
        } catch (error) {
            console.error('Error submitting advertisement:', error);
            // Handle specific error scenarios or show an error message here
        }
    };

    return (
        <div className="company-ad-form">
            <h2>Company Advertisement</h2>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    placeholder="Company Name"
                    name="name"
                    value={advertiserInfo.name}
                    onChange={handleInputChange}
                />
                <input
                    type="text"
                    placeholder="Organization Number"
                    name="organizationNumber"
                    value={advertiserInfo.organizationNumber}
                    onChange={handleInputChange}
                />
                <input
                    type="text"
                    placeholder="Phone Number"
                    name="phoneNumber"
                    value={advertiserInfo.phoneNumber}
                    onChange={handleInputChange}
                />
                <input
                    type="text"
                    placeholder="Address"
                    name="address"
                    value={advertiserInfo.address}
                    onChange={handleInputChange}
                />
                <input
                    type="text"
                    placeholder="Postal Code"
                    name="postalCode"
                    value={advertiserInfo.postalCode}
                    onChange={handleInputChange}
                />
                <input
                    type="text"
                    placeholder="City"
                    name="city"
                    value={advertiserInfo.city}
                    onChange={handleInputChange}
                />
                Billing Address:
                <input
                    type="text"
                    placeholder="Billing Address"
                    name="billingAddress"
                    value={advertiserInfo.billingAddress}
                    onChange={handleInputChange}
                />
                <input
                    type="text"
                    placeholder="Billing Postal Code"
                    name="billingPostalCode"
                    value={advertiserInfo.billingPostalCode}
                    onChange={handleInputChange}
                />
                <input
                    type="text"
                    placeholder="Billing City"
                    name="billingCity"
                    value={advertiserInfo.billingCity}
                    onChange={handleInputChange}
                />
                Ad Details:
                <input
                    type="text"
                    placeholder="Headline"
                    name="title"
                    value={advertiserInfo.title}
                    onChange={handleInputChange}
                />
                <textarea
                    placeholder="Content"
                    name="content"
                    value={advertiserInfo.content}
                    onChange={handleInputChange}
                />
                <input
                    type="number"
                    placeholder="Item Price"
                    name="itemPrice"
                    value={advertiserInfo.itemPrice}
                    onChange={handleInputChange}
                />
                <button type="submit">Submit Advertisement</button>
            </form>
        </div>
    );
};

export default CompanyAdForm;
