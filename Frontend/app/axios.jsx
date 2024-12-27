import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: 'http://localhost:5291/api', // Replace with the external API base URL
    headers: {
        'Content-Type': 'application/json',
    },
});

export default axiosInstance;
