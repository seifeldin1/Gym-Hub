import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: 'http://localhost:5291/api',
    headers: {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',  // This is usually added in the response, not the request.
    },
});

export default axiosInstance;
