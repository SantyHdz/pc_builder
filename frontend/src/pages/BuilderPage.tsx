import React, { useState, useEffect, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';

const componentCategories = [
  { id: 'cpu', name: 'Processor (CPU)' },
  { id: 'motherboard', name: 'Motherboard' },
  { id: 'ram', name: 'Memory (RAM)' },
  { id: 'gpu', name: 'Video Card (GPU)' },
  { id: 'storage', name: 'Storage' },
  { id: 'psu', name: 'Power Supply (PSU)' },
  { id: 'case', name: 'Case' },
];

type SelectedComponent = {
  id: string;
  name: string;
  price: number;
  power: number;
};

const BuilderPage: React.FC = () => {
  const [selectedComponents, setSelectedComponents] = useState<Record<string, SelectedComponent | null>>({});
  const navigate = useNavigate();
  const buildRef = useRef(null);

  const handleExportPDF = () => {
    if (buildRef.current) {
      html2canvas(buildRef.current).then(canvas => {
        const imgData = canvas.toDataURL('image/png');
        const pdf = new jsPDF('p', 'mm', 'a4');
        const pdfWidth = pdf.internal.pageSize.getWidth();
        const pdfHeight = (canvas.height * pdfWidth) / canvas.width;
        pdf.addImage(imgData, 'PNG', 0, 0, pdfWidth, pdfHeight);
        pdf.save('pc-build.pdf');
      });
    }
  };

  const handleShare = () => {
      alert('Share functionality to be implemented. A unique link would be generated here.');
  }

  useEffect(() => {
    const newSelectedComponents: Record<string, SelectedComponent | null> = {};
    componentCategories.forEach(category => {
      const storedComponent = localStorage.getItem(`selected_${category.id}`);
      if (storedComponent) {
        newSelectedComponents[category.id] = JSON.parse(storedComponent);
      }
    });
    setSelectedComponents(newSelectedComponents);
  }, []);

  const handleSelectComponent = (category: string) => {
    navigate(`/builder/${category}`);
  };

  const handleRemoveComponent = (category: string) => {
    localStorage.removeItem(`selected_${category}`);
    setSelectedComponents(prev => {
        const newSelections = {...prev};
        delete newSelections[category];
        return newSelections;
    });
  }

  const totalCost = Object.values(selectedComponents).reduce((acc, component) => {
    return acc + (component?.price || 0);
  }, 0);

  const totalPower = Object.values(selectedComponents).reduce((acc, component) => {
    return acc + (component?.power || 0);
  }, 0);

  return (
    <div className="container mx-auto">
      <h1 className="text-3xl font-bold mb-6">Build Your PC</h1>
      <div ref={buildRef} className="bg-white dark:bg-gray-800 shadow-lg rounded-lg p-6">
        <div className="space-y-4">
          {componentCategories.map(category => (
            <div key={category.id} className="flex items-center justify-between border-b border-gray-200 dark:border-gray-700 py-4">
              <span className="text-lg font-semibold">{category.name}</span>
              <div>
                {selectedComponents[category.id] ? (
                  <div className="text-right flex items-center">
                    <div>
                        <p>{selectedComponents[category.id]?.name}</p>
                        <p className="text-sm text-gray-500">
                            ${selectedComponents[category.id]?.price.toFixed(2)} | {selectedComponents[category.id]?.power}W
                        </p>
                    </div>
                    <button onClick={() => handleRemoveComponent(category.id)} className="ml-4 text-red-500 hover:text-red-700">
                        X
                    </button>
                  </div>
                ) : (
                  <button
                    onClick={() => handleSelectComponent(category.id)}
                    className="bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded"
                  >
                    Choose
                  </button>
                )}
              </div>
            </div>
          ))}
        </div>
        <div className="mt-6 pt-4 border-t border-gray-200 dark:border-gray-700 text-right">
          <h3 className="text-xl font-semibold">Estimated Wattage: {totalPower}W</h3>
          <h2 className="text-2xl font-bold">Total Cost: ${totalCost.toFixed(2)}</h2>
        </div>
      </div>
      <div className="mt-6 flex justify-end space-x-4">
        <button
          onClick={handleShare}
          className="bg-gray-500 hover:bg-gray-600 text-white font-bold py-2 px-4 rounded"
        >
          Share Build
        </button>
        <button
          onClick={handleExportPDF}
          className="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded"
        >
          Export to PDF
        </button>
      </div>
    </div>
  );
};

export default BuilderPage;
