import { createRouter, createWebHistory } from 'vue-router';
import Dashboard from '../views/Dashboard.vue';
import CropForm from '../views/CropForm.vue';

const routes = [
  { path: '/', component: Dashboard },
  { path: '/add-crop', component: CropForm },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;