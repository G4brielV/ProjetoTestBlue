import { ref, reactive } from 'vue';
import { authService } from '../services/authService';
import { useRouter } from 'vue-router';

export function useAuth() {
    const router = useRouter();

    // Estado comum para formulários de auth
    const form = reactive({
        nome: '',
        email: '',
        senha: ''
    });

    const loading = ref(false);
    const error = ref('');

    // Função para login
    const handleLogin = async () => {
        loading.value = true;
        error.value = '';
        try {
            await authService.login(form.email, form.senha);
            router.push('/dashboard');
        } catch (err: any) {
            error.value = err.response?.data || 'Erro ao realizar login';
        } finally {
            loading.value = false;
        }
    };

    // Função para cadastro
    const handleCadastro = async () => {
        loading.value = true;
        error.value = '';
        try {
            await authService.cadastro(form.nome, form.email, form.senha);
            router.push('/login'); // Ou redirecionar para dashboard se auto-login
        } catch (err: any) {
            error.value = err.response?.data || 'Erro ao realizar cadastro';
        } finally {
            loading.value = false;
        }
    };

    // Função para logout
    const handleLogout = () => {
        authService.logout();
        router.push('/login');
    };

    return {
        form,
        loading,
        error,
        handleLogin,
        handleCadastro,
        handleLogout
    };
}