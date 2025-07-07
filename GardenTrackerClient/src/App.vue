<template>
  <div class="bg-blue-500 text-white p-4">Test Tailwind</div>
  <div class="min-h-screen bg-gray-100">
    <nav class="bg-green-600 text-white p-4">
      <div class="container mx-auto flex justify-between">
        <h1 class="text-xl font-bold">Garden Tracker</h1>
        <div>
          <router-link to="/" class="mr-4">Dashboard</router-link>
          <router-link to="/add-crop">Add Crop</router-link>
        </div>
      </div>
    </nav>
    <div class="container mx-auto p-4">
      <router-view></router-view>
    </div>
  </div>
</template>

<script setup lang="ts">
</script>