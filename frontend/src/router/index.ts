import { createRouter, createWebHistory } from 'vue-router';
import Login from '../views/Login.vue';
import Cadastro from '../views/Cadastro.vue';
import Dashboard from '../views/DashBoard.vue';

const routes = [
  { 
    path: '/', 
    redirect: '/login' 
  },
  { 
    path: '/login', 
    name: 'Login', 
    component: Login 
  },
  { 
    path: '/cadastro', 
    name: 'Cadastro', 
    component: Cadastro
  },
  { 
    path: '/dashboard', 
    name: 'Dashboard', 
    component: Dashboard,
    meta: { requiresAuth: true }
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

function isTokenValid(token: string | null) {
  if (!token) return false;

  try {
    // Decode JWT payload (base64url)
    const payload = JSON.parse(atob(token.split('.')[1]));
    // exp is in seconds since epoch
    return typeof payload.exp === 'number' && payload.exp * 1000 > Date.now();
  } catch {
    return false;
  }
}

router.beforeEach((to, _, next) => {
  const token = localStorage.getItem('user_token');
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth);

  if (requiresAuth && !isTokenValid(token)) {
    // Ensure no stale/invalid token remains
    localStorage.removeItem('user_token');
    next({ name: 'Login' });
  } else {
    next();
  }
});

export default router;