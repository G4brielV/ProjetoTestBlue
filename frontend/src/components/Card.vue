<template>
  <div class="card" :class="{ completed: isCompleted }">
    <h3>{{ card.titulo }}</h3>
    <p>{{ card.description }}</p>
    <div class="card-actions">
      <button class="btn-checkbox" :class="{ checked: isCompleted }" @click="handleToggle" :title="isCompleted ? 'Marcar como pendente' : 'Marcar como concluído'">
        <i :class="isCompleted ? 'pi pi-check-square' : 'pi pi-square'" aria-hidden="true"></i>
      </button>
      <button class="btn-edit" @click="$emit('edit', card)">
        <i class="pi pi-pencil" aria-hidden="true"></i>
      </button>
      <button class="btn-delete" @click="$emit('delete', card.id)">
        <i class="pi pi-trash" aria-hidden="true"></i>
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, ref, watch } from 'vue';

const props = defineProps<{
  card: any;
}>();

const emit = defineEmits<{
  toggle: [id: number];
  edit: [card: any];
  delete: [id: number];
}>();

// Estado local para otimistic update (feedback visual imediato)
const localCompleted = ref<boolean | null>(null);

// Garante que o status seja booleano
const cardCompleted = computed(() => {
  const status = props.card?.isCompleted;
  const result = status === true || status === 1 || status === 'true' || status === '1';
  return result;
});

// Usa o estado local se foi alterado, senão usa o do card
const isCompleted = computed(() => {
  return localCompleted.value !== null ? localCompleted.value : cardCompleted.value;
});

// Atualiza o estado local para mostrar mudança imediata
const handleToggle = () => {
  localCompleted.value = !isCompleted.value;
  emit('toggle', props.card.id);
};

// Reseta o estado local quando a prop mudar (resposta do servidor)
watch(cardCompleted, () => {
  if (localCompleted.value !== null) {
    localCompleted.value = null; // Reseta para usar o valor do servidor
  }
});
</script>

<style scoped>
.card {
  background: #334155;
  color: white;
  padding: 16px;
  margin: 8px;
  border: 1px solid #475569;
  border-radius: 4px;
  transition: all 0.3s ease;
}

.card.completed {
  background: #1e3f2f;
  border-color: #16a34a;
  opacity: 0.85;
}

.card.completed h3 {
  text-decoration: line-through;
  color: #a0aec0;
}

.card h3 {
  font-size: 18px;
  font-weight: bold;
  margin-bottom: 8px;
}

.card p {
  font-size: 14px;
  margin-bottom: 8px;
}

.card-actions {
  display: flex;
  gap: 8px;
}

.card-actions button {
  padding: 8px;
  border-radius: 6px;
  font-size: 13px;
  color: white;
  border: none;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  transition: transform 0.2s ease;
  min-width: 36px;
  height: 36px;
}

.card-actions button i {
  font-size: 16px;
  line-height: 1;
}

.card-actions button:hover {
  transform: scale(1.1);
}

.btn-checkbox {
  background: #64748b;
}

.btn-checkbox.checked {
  background: #16a34a;
  color: #fff;
}

.btn-edit {
  background: #ca8a04;
}

.btn-delete {
  background: #dc2626;
}
</style>