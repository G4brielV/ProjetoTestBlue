import api from './api';

export const todoListService = {
  getAll: async () => {
    const response = await api.get('/api/TodoList');
    return response.data;
  },

  create: async (titulo: string) => {
    return api.post('/api/TodoList', { titulo });
  },

  update: async (id: number, titulo: string) => {
    return api.put(`/api/TodoList/${id}`, { titulo });
  },

  delete: async (id: number) => {
    return api.delete(`/api/TodoList/${id}`);
  }
};
