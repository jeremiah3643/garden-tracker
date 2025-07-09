<template>
  <div class="min-h-screen flex justify-center items-center bg-gray-100">
    <div>
      <h2 class="text-2xl font-bold mb-4 text-center">Crop Dashboard</h2>
      <div v-if="crops.length === 0" class="text-gray-500 text-center">No crops added yet.</div>
      <div v-else class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div v-for="crop in crops" :key="crop.id" class="bg-white p-4 rounded shadow text-center">
          <h3 class="text-lg font-semibold">{{ crop.name }}</h3>
          <p>Planted: {{ new Date(crop.plantedDate).toLocaleDateString() }}</p>
          <p>Notes: {{ crop.notes || 'None' }}</p>
          <p>Harvests: {{ crop.harvests.length }}</p>
          <button @click="deleteCrop(crop.id)" class="mt-2 bg-red-500 text-white px-2 py-1 rounded">Delete</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import axios from 'axios';

interface Crop {
  id: number;
  name: string;
  plantedDate: string;
  notes: string | null;
  harvests: any[];
}

const crops = ref<Crop[]>([]);

const fetchCrops = async () => {
  try {
    const response = await axios.get('https://garden-tracker.onrender.com/api/crops');
    crops.value = response.data;
  } catch (error) {
    console.error('Error fetching crops:', error);
  }
};

const deleteCrop = async (id: number) => {
  try {
    await axios.delete(`https://garden-tracker.onrender.com/api/crops/${id}`);
    crops.value = crops.value.filter(crop => crop.id !== id);
  } catch (error) {
    console.error('Error deleting crop:', error);
  }
};

onMounted(fetchCrops);
</script>