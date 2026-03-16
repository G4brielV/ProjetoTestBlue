<template>
  <div class="todo-list">
    <div class="list-header">
      <h2>{{ list.titulo }}</h2>
      <div class="list-actions">
      <button class="btn-add" @click="$emit('addCard', list.id)">
        <i class="pi pi-plus" aria-hidden="true"></i>
      </button>
      <button class="btn-edit" @click="$emit('editList', list)">
        <i class="pi pi-pencil" aria-hidden="true"></i>
      </button>
      <button class="btn-delete" @click="$emit('deleteList', list.id)">
        <i class="pi pi-trash" aria-hidden="true"></i>
      </button>
      </div>
    </div>
    <div class="cards">
      <Card
        v-for="card in list.cards"
        :key="card.id"
        :card="card"
        @toggle="$emit('toggleCard', $event)"
        @edit="$emit('editCard', $event)"
        @delete="$emit('deleteCard', $event)"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import Card from './Card.vue';

defineProps<{
  list: any;
}>();

defineEmits<{
  addCard: [listId: number];
  editList: [list: any];
  deleteList: [listId: number];
  toggleCard: [id: number];
  editCard: [card: any];
  deleteCard: [id: number];
}>();
</script>

<style scoped>
.todo-list {
  background: #1e293b;
  color: white;
  padding: 16px;
  margin: 16px;
  border: 1px solid #475569;
  border-radius: 4px;
  width: 320px;
  display: inline-block;
  vertical-align: top;
}

.list-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}

.list-header h2 {
  font-size: 20px;
  font-weight: bold;
}

.list-actions {
  display: flex;
  gap: 8px;
}

.list-actions button {
  padding: 8px;
  border-radius: 6px;
  font-size: 14px;
  color: white;
  border: none;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  justify-content: center;
}

.btn-add {
  background: #16a34a;
}

.btn-edit {
  background: #ca8a04;
}

.btn-delete {
  background: #dc2626;
}

.cards {
  margin-top: 10px;
}
</style>