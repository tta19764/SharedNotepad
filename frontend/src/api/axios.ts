import axios from "axios";
const BASE_URL = window.__APP_CONFIG__?.API_BASE_URL ?? import.meta.env.VITE_API_URL;

const customAxios = axios.create({
    baseURL: BASE_URL
});

export default customAxios;
