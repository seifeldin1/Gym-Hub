import { RiMailSendFill } from "react-icons/ri";
const Newsletter = () => {
  return (
    <>
      <div className="bg-transparent pb-16 pt-20 rounded">


        <div className="bg-transparent text-white flex items-center w-full max-w-5xl h-60 p-16 border rounded-bl-[10rem] rounded-tr-[10rem] mx-auto">

          <div className="w-1/2">
            <h2 className="text-3xl font-extrabold mb-2 text-[#b2d438]">NEWSLETTER SUBSCRIBE</h2>
            <p className="text-gray-300">Keep up with our most recent news, advice, and deals. Enter your email address to subscribe now and never miss out!</p>
          </div>

          <div className="basis-40 ml-6">
            <form className="flex items-center w-[100px] space-x-5">
              <input
                type="email"
                placeholder="Your Email Address"
                className="px-7 h-12 rounded-bl-[1.5rem] text-gray-300 font-bold focus:outline-none bg-transparent border-b-2 border-gray-500 hover:border-[#b2d438] transition"
                required
              />

              <button type="submit" className="bg-blue-600 text-white px-6 h-10 flex items-center gap-2 text-xl border-2 border-blue-600 hover:bg-transparent hover:text-blue-500 transition">
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