import { RiMailSendFill } from "react-icons/ri";
const Newsletter = () => {
    return (
      <>
      <div className="bg-transparent pb-16 pt-20 rounded">


        <div className="bg-transparent text-white flex items-center w-full max-w-5xl h-60 p-16 border rounded-bl-[10rem] rounded-tr-[10rem] mx-auto">
          
          <div className="w-1/2">
            <h2 className="text-3xl font-bold mb-2">NEWSLETTER SUBSCRIBE</h2>
            <p className="text-gray-400">Keep up with our most recent news, advice, and deals. Enter your email address to subscribe now and never miss out!</p>
          </div>

          <div className="basis-40 ml-16">
            <form className="flex items-center w-[100px] space-x-2">
              <input
                type="email"
                placeholder="Your Email Address"
                className="p-3 rounded-bl-[1.5rem] text-black focus:outline-none"
                required
              />

              <button type="submit" className="bg-blue-600 text-white px-5 py-3 hover:bg-blue-700 flex items-center gap-2 text-lg hover:">
                <RiMailSendFill />
                Send
              </button>
            </form>
          </div>

        </div>


      </div>
      </>
    );
  }
  export default Newsletter 