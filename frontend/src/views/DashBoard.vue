<template>
  <div class="dashboard">
    <h1 class="dashboard-title">Dashboard</h1>
    <button class="btn-primary" @click="openCreateListDialog">
      <i class="pi pi-plus" aria-hidden="true"></i>
      Adicionar nova lista
    </button>

    <div v-if="loading" class="text-center">Carregando...</div>
    <div v-if="error" class="error">{{ error }}</div>

    <div class="todo-lists">
      <TodoList
        v-for="list in todoLists"
        :key="list.id"
        :list="list"
        @addCard="openCreateCardDialog($event)"
        @editList="openEditListDialog($event)"
        @deleteList="deleteList($event)"
        @toggleCard="toggleCard($event)"
        @editCard="openEditCardDialog($event)"
        @deleteCard="deleteCard($event)"
      />
    </div>

    <!-- Dialog para criar lista -->
    <div v-if="showCreateListDialog" class="dialog">
      <div class="dialog-content">
        <h3>Adicionar nova lista</h3>
        <input v-model="newListTitle" placeholder="Título da lista" />
        <div class="flex">
          <button class="btn-primary" @click="createList">
            <i class="pi pi-plus" aria-hidden="true"></i>
            Adicionar
          </button>
          <button class="btn-secondary" @click="showCreateListDialog = false">Cancelar</button>
        </div>
      </div>
    </div>

    <!-- Dialog para editar lista -->
    <div v-if="showEditListDialog" class="dialog">
      <div class="dialog-content">
        <h3>Editar Lista</h3>
        <input v-model="editListTitle" placeholder="Título da lista" />
        <div class="flex">
          <button @click="updateList">Salvar</button>
          <button @click="showEditListDialog = false">Cancelar</button>
        </div>
      </div>
    </div>

    <!-- Dialog para criar card -->
    <div v-if="showCreateCardDialog" class="dialog">
      <div class="dialog-content">
        <h3>Criar Novo Card</h3>
        <input v-model="newCardData.titulo" placeholder="Título" />
        <textarea v-model="newCardData.description" placeholder="Descrição"></textarea>
        <div class="flex">
          <button @click="createCard">Criar</button>
          <button @click="showCreateCardDialog = false">Cancelar</button>
        </div>
      </div>
    </div>

    <!-- Dialog para editar card -->
    <div v-if="showEditCardDialog" class="dialog">
      <div class="dialog-content">
        <h3>Editar Card</h3>
        <input v-model="editCardData.titulo" placeholder="Título" />
        <textarea v-model="editCardData.description" placeholder="Descrição"></textarea>
        <div class="flex">
          <button @click="updateCard">Salvar</button>
          <button @click="showEditCardDialog = false">Cancelar</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue';
import TodoList from '../components/TodoList.vue';
import { useTodoLists } from '../composables/useTodoLists';
import { useCards } from '../composables/useCards';

const {
  todoLists,
  loading,
  error,
  loadTodoLists,
  showCreateListDialog,
  newListTitle,
  showEditListDialog,
  editListTitle,
  openCreateListDialog,
  openEditListDialog,
  createList,
  updateList,
  deleteList
} = useTodoLists();

const {
  showCreateCardDialog,
  newCardData,
  showEditCardDialog,
  editCardData,
  openCreateCardDialog,
  createCard,
  openEditCardDialog,
  updateCard,
  deleteCard,
  toggleCard
} = useCards(() => todoLists.value, loadTodoLists);

onMounted(() => {
  loadTodoLists();
});
</script>

<style scoped>
.dashboard {
  background: #0f172a;
  color: #f8fafc;
  min-height: 100vh;
  padding: 24px;
}

.dashboard h1 {
  font-size: 3rem;
  font-weight: 800;
  margin-bottom: 16px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

  .btn-primary {
    background: #2563eb;
    color: white;
    padding: 10px 18px;
    border-radius: 8px;
    margin-bottom: 16px;
    display: inline-flex;
    align-items: center;
    gap: 8px;
    border: none;
    cursor: pointer;
  }

  .btn-secondary {
    background: #475569;
    color: white;
    padding: 10px 18px;
    border-radius: 8px;
    border: none;
    cursor: pointer;
  }
.todo-lists {
  display: flex;
  flex-wrap: wrap;
  gap: 16px;
}

.error {
  background: #dc2626;
  color: white;
  padding: 8px;
  border-radius: 4px;
  margin-bottom: 16px;
}

.dialog {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.dialog-content {
  background: #1e293b;
  color: white;
  padding: 24px;
  border-radius: 4px;
  width: 384px;
}

.dialog-content h3 {
  font-size: 20px;
  font-weight: bold;
  margin-bottom: 16px;
}

.dialog-content input,
.dialog-content textarea {
  width: 100%;
  padding: 8px;
  margin-bottom: 8px;
  background: #334155;
  color: white;
  border: 1px solid #475569;
  border-radius: 4px;
}

.dialog-content textarea {
  margin-bottom: 16px;
}

.dialog-content .flex {
  display: flex;
  gap: 8px;
}

.dialog-content button {
  padding: 8px 16px;
  border-radius: 4px;
  color: white;
}

.dialog-content button:first-child { background: #2563eb; }
.dialog-content button:last-child { background: #6b7280; }
</style>
