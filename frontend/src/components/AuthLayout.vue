<template>
  <div class="page-wrapper">
    <Card class="signup-card">
      <template #header>
        <div class="header-section">
          <h1 class="brand-name">ProjBlue</h1>
          <p class="brand-tagline">{{ props.subtitle }}</p>
        </div>
      </template>

      <template #content>
        <slot />
      </template>
    </Card>
  </div>
</template>

<script setup lang="ts">
import Card from 'primevue/card';

// Define o que pode mudar 
const props = defineProps<{
  subtitle: string
}>();
</script>

<style scoped>
.page-wrapper {
  display: flex !important;
  justify-content: center !important;
  align-items: center !important;
  min-height: 100vh !important;
  width: 100vw !important;
  position: fixed;
  top: 0;
  left: 0;
  z-index: 999;
}

.signup-card {
  width: 100%;
  max-width: 480px;
  background-color: #1e1f26 !important;
  border: 1px solid #333644 !important;
  border-radius: 20px !important;
  box-shadow: 0 20px 50px rgba(0,0,0,0.5) !important;
}

.brand-name {
  font-size: 4.5rem !important;
  font-weight: 900 !important;
  color: #3b82f6 !important;
  margin: 0 !important;
  letter-spacing: -3px;
  text-align: center;
}

.brand-tagline {
  font-size: 1.4rem;
  color: #888;
  margin: 10px 0 0;
  text-align: center;
}
</style>