<template>
  <span v-if="userData.isAuthenticated">
    <SideBar />
  </span>
  <Suspense>
    <router-view> </router-view>
  </Suspense>
</template>

<script>
import { Suspense } from "vue";
import SideBar from "./components/sidebar/SideBar.vue"
import { sideBardWidth } from "./components/sidebar/state";
import { useUserStore } from "./stores/user"


export default {
  name: 'App',
  components: {
    SideBar,
    Suspense
},
  setup() {
    const userData = useUserStore();
    return {sideBardWidth, userData}
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
:root {
  --main-color: #C8E4B2;
  --secondary-color: #9ED2BE;
  --dark-green: #7EAA92;
  --effect-color: #FFD9B7;
  --white: #ffff;
}
</style>
