const Newsletter = () => {
    return (
      <>
      <div className="bg-black pb-16 pt-20 ">
  <div className="bg-black w-full text-white  p-8 border rounded-tl-[60px] rounded-bl-[60px] flex items-center justify-between max-w-4xl mx-auto">
  <div>
    <h2 className="text-2xl font-bold mb-2">NEWSLETTER SUBSCRIBE</h2>
    <p className="text-gray-400">
      Stay updated with our latest offers, tips, and news. Subscribe now and never miss out!
    </p>
  </div>
  <form className="flex items-center space-x-2">
    <input
      type="email"
      placeholder="Your Email Address"
      className="p-3 rounded-l-md text-black focus:outline-none"
      required
    />
    <button
      type="submit"
      className="bg-blue-600 text-white px-5 py-3 rounded-r-md hover:bg-blue-700 flex items-center"
    >
      <svg
        xmlns="http://www.w3.org/2000/svg"
        fill="none"
        viewBox="0 0 24 24"
        strokeWidth="2"
        stroke="currentColor"
        className="w-5 h-5 mr-2"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          d="M4.75 12h14.5m-6.75 6.75l6-6m-6-6l6 6"
        />
      </svg>
      Send
    </button>
  </form>
</div>
</div>
      
      </>
    );
  }
  export default Newsletter 