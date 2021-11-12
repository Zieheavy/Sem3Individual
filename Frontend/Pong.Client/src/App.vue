<script setup>
// This starter template is using Vue 3 <script setup> SFCs
// Check out https://v3.vuejs.org/api/sfc-script-setup.html#sfc-script-setup
import Pong from './components/Pong.vue'
</script>

<template>
  <!-- <HelloWorld msg="Hello Vue 3 + Vite" /> -->
  <div>
    <!-- <router-link to="/HelloWorld">Go to About</router-link>
    <router-view></router-view> -->
    <Pong msg="Hello Vue 3 + Vite" />
  </div>
</template>

<script>
export default {
name: 'Index',
data: () => ({
    }),
}

</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  margin-top: 60px;
  color: white;
}

html{
  background: black;
}
</style>
