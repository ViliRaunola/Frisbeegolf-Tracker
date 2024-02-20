<template>
  <div :class="['text-h3', 'pa-2']">New Game Starts Here</div>
  <NewGameStart></NewGameStart>
</template>

<script>
// eslint-disable-next-line no-unused-vars
import { useMapsStore } from "../stores/maps"
import NewGameStart from '@/components/newgame/NewGameStart.vue';

// const storeMaps = useMapsStore();

export default {
    name: 'NewGame',
    components: {
      NewGameStart
    },
}
</script>

<style></style>
