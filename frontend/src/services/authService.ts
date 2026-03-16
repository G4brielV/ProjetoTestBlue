import api from './api';

export const authService = {
    async login(email: string, senha: string) {
        // Envia o LoginRequest esperado pelo seu backend
        const response = await api.post('/Auth/login', { email, senha });
        if (response.data.token) {
            localStorage.setItem('user_token', response.data.token);
        }
        return response.data;
    },
    async cadastro(nome: string, email: string, senha: string) {
        // Envia o CadastroRequest
        return await api.post('/Auth/cadastro', { nome, email, senha });
    },
    logout() {
        localStorage.removeItem('user_token');
    }
};