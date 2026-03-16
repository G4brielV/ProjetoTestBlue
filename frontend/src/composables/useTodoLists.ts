import { ref } from 'vue';
import { todoListService } from '../services/todoListService';

export function useTodoLists() {
  const todoLists = ref<any[]>([]);
  const loading = ref(false);
  const error = ref('');

  // Estado de controle de dialogs
  const showCreateListDialog = ref(false);
  const newListTitle = ref('');

  const showEditListDialog = ref(false);
  const editListId = ref<number | null>(null);
  const editListTitle = ref('');

  const loadTodoLists = async () => {
    loading.value = true;
    error.value = '';

    try {
      todoLists.value = await todoListService.getAll();
    } catch (err: any) {
      console.error('Erro ao carregar listas:', err);
      error.value = err.response?.data || 'Erro ao carregar listas';
    } finally {
      loading.value = false;
    }
  };

  const openCreateListDialog = () => {
    newListTitle.value = '';
    showCreateListDialog.value = true;
  };

  const openEditListDialog = (list: any) => {
    editListId.value = list.id;
    editListTitle.value = list.titulo;
    showEditListDialog.value = true;
  };

  const createList = async () => {
    if (!newListTitle.value) return;
    try {
      await todoListService.create(newListTitle.value);
      showCreateListDialog.value = false;
      newListTitle.value = '';
      await loadTodoLists();
    } catch (err: any) {
      console.error('Erro ao criar lista:', err);
      error.value = err.response?.data || 'Erro ao criar lista';
    }
  };

  const updateList = async () => {
    if (!editListId.value) return;

    try {
      await todoListService.update(editListId.value, editListTitle.value);
      showEditListDialog.value = false;
      await loadTodoLists();
    } catch (err: any) {
      console.error('Erro ao atualizar lista:', err);
      error.value = err.response?.data || 'Erro ao atualizar lista';
    }
  };

  const deleteList = async (id: number) => {
    try {
      await todoListService.delete(id);
      await loadTodoLists();
    } catch (err: any) {
      console.error('Erro ao excluir lista:', err);
      error.value = err.response?.data || 'Erro ao excluir lista';
    }
  };

  return {
    todoLists,
    loading,
    error,
    loadTodoLists,
    showCreateListDialog,
    newListTitle,
    showEditListDialog,
    editListId,
    editListTitle,
    openCreateListDialog,
    openEditListDialog,
    createList,
    updateList,
    deleteList
  };
}
