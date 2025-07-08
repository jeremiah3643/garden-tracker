<template>
  <div class="max-w-md mx-auto bg-white p-6 rounded shadow">
    <h2 class="text-2xl font-bold mb-4 text-center">Add Crop</h2>
    <div class="space-y-4">
      <div>
        <label class="block text-sm font-medium text-gray-700">Crop Name</label>
        <input v-model="crop.name" type="text" class="w-full border rounded p-2" required />
      </div>
      <div>
        <label class="block text-sm font-medium text-gray-700">Planted Date</label>
        <input v-model="crop.plantedDate" type="date" class="w-full border rounded p-2" required />
      </div>
      <div>
        <label class="block text-sm font-medium text-gray-700">Notes</label>
        <textarea v-model="crop.notes" class="w-full border rounded p-2"></textarea>
      </div>
      <button @click="addCrop" class="bg-green-500 text-white px-4 py-2 rounded">Save Crop</button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';

const router = useRouter();
const crop = ref({
  name: '',
  plantedDate: new Date().toISOString().split('T')[0],
  notes: '',
});

const addCrop = async () => {
  try {
    await axios.post('https://garden-tracker.onrender.com/api/crops', crop.value);
    router.push('/');
  } catch (error) {
    console.error('Error adding crop:', error);
  }
};
</script>