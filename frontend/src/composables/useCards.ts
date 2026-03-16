import { ref, reactive } from 'vue';
import { cardService } from '../services/cardService';

export function useCards(getLists: () => any[], reloadLists: () => Promise<void>) {
  const showCreateCardDialog = ref(false);
  const currentListId = ref<number | null>(null);
  const newCardData = reactive({ titulo: '', description: '' });

  const showEditCardDialog = ref(false);
  const editCardId = ref<number | null>(null);
  const editCardData = reactive({ titulo: '', description: '' });

  const openCreateCardDialog = (listId: number) => {
    currentListId.value = listId;
    newCardData.titulo = '';
    newCardData.description = '';
    showCreateCardDialog.value = true;
  };

  const createCard = async () => {
    if (currentListId.value === null) return;

    const lists = getLists();
    const list = lists.find((l: any) => l.id === currentListId.value);
    const position = list?.cards?.length ?? 0;

    try {
      await cardService.create({
        titulo: newCardData.titulo,
        description: newCardData.description,
        listId: currentListId.value,
        position
      });
      showCreateCardDialog.value = false;
      await reloadLists();
    } catch (err: any) {
      console.error('Erro ao criar card:', err);
    }
  };

  const openEditCardDialog = (card: any) => {
    editCardId.value = card.id;
    editCardData.titulo = card.titulo;
    editCardData.description = card.description;
    showEditCardDialog.value = true;
  };

  const updateCard = async () => {
    if (editCardId.value === null) return;

    const lists = getLists();
    const parentList = lists.find((l: any) => l.cards.some((c: any) => c.id === editCardId.value));

    try {
      await cardService.update(editCardId.value, {
        titulo: editCardData.titulo,
        description: editCardData.description,
        listId: parentList?.id ?? 0,
        position: 0
      });
      showEditCardDialog.value = false;
      await reloadLists();
    } catch (err: any) {
      console.error('Erro ao atualizar card:', err);
    }
  };

  const deleteCard = async (id: number) => {
    try {
      await cardService.delete(id);
      await reloadLists();
    } catch (err: any) {
      console.error('Erro ao excluir card:', err);
    }
  };

  const toggleCard = async (id: number) => {
    try {
      await cardService.toggleStatus(id);
      await reloadLists();
    } catch (err: any) {
      console.error('Erro ao alternar status do card:', err);
      await reloadLists();
    }
  };

  return {
    showCreateCardDialog,
    currentListId,
    newCardData,
    showEditCardDialog,
    editCardId,
    editCardData,
    openCreateCardDialog,
    createCard,
    openEditCardDialog,
    updateCard,
    deleteCard,
    toggleCard
  };
}
