import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: 'http://localhost:5291/api',
    headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjcmlzdGlhbm8ucm9uYWxkbyIsInJvbGUiOiJPd25lciIsImp0aSI6IjY3MmMxYjFhLTgwM2ItNDZkMS05OTE3LTgzZWFiYTM1ZWYzNiIsIm5iZiI6MTczNTM1MDI4MSwiZXhwIjoxNzM1NDM2NjgxLCJpYXQiOjE3MzUzNTAyODF9.Xy1Y1HPwyWSBZMXh5Ai2RgF71P07Q6RNGsjABrqaCy0`,
        'Access-Control-Allow-Origin': '*',  // This is usually added in the response, not the request.
    },
});

export default axiosInstance;
