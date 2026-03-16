<template>
  <AuthLayout subtitle="Criar Conta">
    <div class="auth-page-content">
      <div class="input-group">
        <label>Nome Completo</label>
        <InputText v-model="form.nome" class="p-inputtext-lg custom-input" />
      </div>

      <div class="input-group">
        <label>E-mail</label>
        <InputText v-model="form.email" type="email" class="p-inputtext-lg custom-input" />
      </div>

      <div class="input-group">
        <label>Senha</label>
        <Password 
          v-model="form.senha" 
          :feedback="false" 
          toggleMask 
          class="w-full"
          inputClass="p-inputtext-lg custom-input w-full"
        />
      </div>

      <Button 
        label="Registrar" 
        icon="pi pi-user-plus"
        @click="handleCadastro" 
        :loading="loading" 
        class="submit-btn" 
      />
      
      <small class="text-red-500 block mt-2" v-if="error">{{ error }}</small>

      <div class="footer-area">
        <router-link to="/login" class="login-link">Já tem uma conta? Faça login</router-link>
      </div>
    </div>
  </AuthLayout>
</template>

<script setup lang="ts">
import { useAuth } from '../composables/useAuth';
import AuthLayout from '../components/AuthLayout.vue';
import InputText from 'primevue/inputtext';
import Password from 'primevue/password';
import Button from 'primevue/button';

const { form, loading, error, handleCadastro } = useAuth();
</script>

<style scoped>
/* Mantendo o padrão que funcionou no Login */
.auth-page-content {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
  width: 100%;
  max-width: 400px;
  margin: 0 auto;
}

.input-group {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  text-align: center;
}

.input-group label {
  color: #94a3b8;
  font-size: 0.9rem;
}

/* Estilização dos Inputs (Isolada) */
:deep(.custom-input) {
  text-align: center !important;
  background: #1e293b !important; 
  border-color: #334155 !important;
  color: white !important;
  padding: 1rem !important;
}

/* Fix para o componente Password do PrimeVue */
:deep(.p-password) {
  width: 100%;
}

:deep(.p-password input) {
  text-align: center !important;
  background: #1e293b !important;
  border-color: #334155 !important;
  color: white !important;
  width: 100% !important;
}

.submit-btn {
  width: 100% !important;
  padding: 1rem !important;
  font-size: 1.1rem !important;
  background: #3b82f6 !important; /* Azul padrão */
  border: none !important;
}

.footer-area {
  text-align: center;
  margin-top: 1rem;
}

.login-link {
  color: #3b82f6;
  text-decoration: none;
  font-weight: 500;
}

.login-link:hover {
  text-decoration: underline;
}
</style>