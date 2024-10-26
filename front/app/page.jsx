'use client'
import React, { useEffect, useState } from 'react';

const Home = () => {
  const [isClient, setIsClient] = useState(false);

  useEffect(() => {
    setIsClient(true);
  }, []);

  return (
    <div>{isClient ? "Home" : null}</div>
  );
}

export default Home;
