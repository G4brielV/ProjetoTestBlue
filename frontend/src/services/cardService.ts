import api from './api';

export const cardService = {
  create: async (payload: {
    titulo: string;
    description: string;
    listId: number;
    position: number;
  }) => {
    return api.post('/api/Card', payload);
  },

  update: async (id: number, payload: { titulo: string; description: string; listId: number; position: number }) => {
    return api.put(`/api/Card/${id}`, payload);
  },

  delete: async (id: number) => {
    return api.delete(`/api/Card/${id}`);
  },

  toggleStatus: async (id: number) => {
    return api.patch(`/api/Card/${id}/toggle`);
  }
};
