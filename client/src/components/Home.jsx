

export default function Home() {
  return (
    <div className="home-container">
      <h1>Welcome to Bianca Bikes!</h1>
 
      <div className="home-links">
        <a href="/bikes">View Bikes</a>
        <a href="/workorders">View Work Orders</a>
        <a href="/userprofiles">Manage User Profiles</a>
      </div>
    </div>
  );
}
