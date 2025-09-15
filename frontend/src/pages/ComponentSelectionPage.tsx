import React, { useState, useMemo } from 'react';
import { useParams, useNavigate } from 'react-router-dom';

// Mock data - in a real app, this would come from an API
const mockComponents = {
  cpu: [
    { id: 'cpu-1', name: 'AMD Ryzen 7 7800X3D', brand: 'AMD', price: 399.99, power: 120 },
    { id: 'cpu-2', name: 'Intel Core i9-13900K', brand: 'Intel', price: 549.99, power: 150 },
    { id: 'cpu-3', name: 'AMD Ryzen 5 7600X', brand: 'AMD', price: 229.99, power: 105 },
  ],
  motherboard: [
    { id: 'mobo-1', name: 'MSI B650 TOMAHAWK', brand: 'MSI', price: 219.99, power: 50 },
    { id: 'mobo-2', name: 'Gigabyte Z790 AORUS ELITE', brand: 'Gigabyte', price: 259.99, power: 60 },
  ],
  ram: [
      { id: 'ram-1', name: 'Corsair Vengeance 32GB DDR5', brand: 'Corsair', price: 99.99, power: 10 },
      { id: 'ram-2', name: 'G.Skill Ripjaws S5 32GB DDR5', brand: 'G.Skill', price: 94.99, power: 10 },
  ]
  // ... other categories
};

type Component = {
  id: string;
  name: string;
  brand: string;
  price: number;
  power: number;
};

const ComponentSelectionPage: React.FC = () => {
  const { category } = useParams<{ category: keyof typeof mockComponents }>();
  const navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState('');

  const components = category ? mockComponents[category] || [] : [];

  const filteredComponents = useMemo(() => {
    return components.filter(component =>
      component.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
  }, [components, searchTerm]);

  const handleSelect = (component: Component) => {
    // In a real app, you'd use a state management library (like Redux or Zustand)
    // to pass the selected component back to the builder page.
    // For now, we'll use localStorage as a simple mechanism.
    localStorage.setItem(`selected_${category}`, JSON.stringify(component));
    navigate('/builder');
  };

  if (!category) {
    return <div>Invalid component category.</div>;
  }

  return (
    <div className="container mx-auto">
      <h1 className="text-3xl font-bold mb-6">Select a {category}</h1>
      <div className="mb-4">
        <input
          type="text"
          placeholder="Search components..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="w-full p-2 border rounded dark:bg-gray-700 dark:border-gray-600"
        />
      </div>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {filteredComponents.map(component => (
          <div key={component.id} className="bg-white dark:bg-gray-800 shadow-lg rounded-lg p-4 flex flex-col">
            <h2 className="text-xl font-bold">{component.name}</h2>
            <p className="text-gray-500">{component.brand}</p>
            <p className="text-lg font-semibold mt-auto pt-4">${component.price.toFixed(2)}</p>
            <button
              onClick={() => handleSelect(component)}
              className="mt-4 bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded"
            >
              Select
            </button>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ComponentSelectionPage;
