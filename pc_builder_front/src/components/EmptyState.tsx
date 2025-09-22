export default function EmptyState({ message }: { message: string }) {
  return (
    <div className="flex flex-col items-center justify-center text-center p-12">
      <img src="https://illustrations.popsy.co/gray/computer.svg" alt="empty" className="w-64 mb-6" />
      <p className="text-gray-300">{message}</p>
    </div>
  );
}
