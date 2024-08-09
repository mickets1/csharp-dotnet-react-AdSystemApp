import React, { useState } from 'react';
import './App.css';
import SubscriberList from './components/SubscriberList';
import AdList from './components/AdList';
import AdvertiserList from './components/CompanyList';
import SubscriberAdForm from './components/SubscriberAdForm';
import CompanyAdForm from './components/CompanyAdForm';

function App() {
  const [isSubscriber, setIsSubscriber] = useState(false);

  return (
    <div className="App">
      <div className="add-advertisement-container">
        <h2>Add Advertisement</h2>
        <div className="radio-buttons">
          <label>
            <input
              type="radio"
              name="adType"
              checked={!isSubscriber}
              onChange={() => setIsSubscriber(false)}
            />
            Company Ad
          </label>
          <label>
            <input
              type="radio"
              name="adType"
              checked={isSubscriber}
              onChange={() => setIsSubscriber(true)}
            />
            Subscriber Ad
          </label>
        </div>
        {isSubscriber ? <SubscriberAdForm /> : <CompanyAdForm />}
      </div>
      <AdList />
      <SubscriberList />
      <AdvertiserList />
    </div>
  );
}

export default App;
