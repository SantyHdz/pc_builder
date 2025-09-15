import React from 'react';

// Mock data for the admin panel
const allComponents = [
  { id: 'cpu-1', name: 'AMD Ryzen 7 7800X3D', category: 'CPU', brand: 'AMD', price: 399.99 },
  { id: 'cpu-2', name: 'Intel Core i9-13900K', category: 'CPU', brand: 'Intel', price: 549.99 },
  { id: 'mobo-1', name: 'MSI B650 TOMAHAWK', category: 'Motherboard', brand: 'MSI', price: 219.99 },
  { id: 'ram-1', name: 'Corsair Vengeance 32GB DDR5', category: 'RAM', brand: 'Corsair', price: 99.99 },
  { id: 'gpu-1', name: 'NVIDIA GeForce RTX 4090', category: 'GPU', brand: 'NVIDIA', price: 1599.99 },
];

const AdminPage: React.FC = () => {
  const handleAddComponent = () => {
    alert('Add new component functionality to be implemented.');
  };

  const handleEditComponent = (id: string) => {
    alert(`Edit component ${id} functionality to be implemented.`);
  };

  const handleDeleteComponent = (id: string) => {
    alert(`Delete component ${id} functionality to be implemented.`);
  };

  return (
    <div className="container mx-auto">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-3xl font-bold">Manage Components</h1>
        <button
          onClick={handleAddComponent}
          className="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded"
        >
          Add New Component
        </button>
      </div>
      <div className="bg-white dark:bg-gray-800 shadow-md rounded-lg overflow-hidden">
        <table className="min-w-full leading-normal">
          <thead>
            <tr>
              <th className="px-5 py-3 border-b-2 border-gray-200 dark:border-gray-700 bg-gray-100 dark:bg-gray-900 text-left text-xs font-semibold uppercase tracking-wider">
                Name
              </th>
              <th className="px-5 py-3 border-b-2 border-gray-200 dark:border-gray-700 bg-gray-100 dark:bg-gray-900 text-left text-xs font-semibold uppercase tracking-wider">
                Category
              </th>
              <th className="px-5 py-3 border-b-2 border-gray-200 dark:border-gray-700 bg-gray-100 dark:bg-gray-900 text-left text-xs font-semibold uppercase tracking-wider">
                Brand
              </th>
              <th className="px-5 py-3 border-b-2 border-gray-200 dark:border-gray-700 bg-gray-100 dark:bg-gray-900 text-left text-xs font-semibold uppercase tracking-wider">
                Price
              </th>
              <th className="px-5 py-3 border-b-2 border-gray-200 dark:border-gray-700 bg-gray-100 dark:bg-gray-900 text-left text-xs font-semibold uppercase tracking-wider">
                Actions
              </th>
            </tr>
          </thead>
          <tbody>
            {allComponents.map(component => (
              <tr key={component.id}>
                <td className="px-5 py-5 border-b border-gray-200 dark:border-gray-700 text-sm">
                  {component.name}
                </td>
                <td className="px-5 py-5 border-b border-gray-200 dark:border-gray-700 text-sm">
                  {component.category}
                </td>
                <td className="px-5 py-5 border-b border-gray-200 dark:border-gray-700 text-sm">
                  {component.brand}
                </td>
                <td className="px-5 py-5 border-b border-gray-200 dark:border-gray-700 text-sm">
                  ${component.price.toFixed(2)}
                </td>
                <td className="px-5 py-5 border-b border-gray-200 dark:border-gray-700 text-sm">
                  <button onClick={() => handleEditComponent(component.id)} className="text-indigo-600 hover:text-indigo-900 mr-4">
                    Edit
                  </button>
                  <button onClick={() => handleDeleteComponent(component.id)} className="text-red-600 hover:text-red-900">
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default AdminPage;
