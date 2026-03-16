import { createApp } from 'vue';
import PrimeVue from 'primevue/config';
import Aura from '@primevue/themes/aura'; // Altere para esta importação

import 'primeicons/primeicons.css'; 
import './style.css';

import App from './App.vue';
import router from './router';

const app = createApp(App);

app.use(router);
app.use(PrimeVue, {
    theme: {
        preset: Aura,
        options: {
            darkModeSelector: '.my-app-dark', 
            cssLayer: {
                name: 'primevue',
                order: 'primevue, tailwind-utilities' 
            }
        }
    }
});

app.mount('#app');