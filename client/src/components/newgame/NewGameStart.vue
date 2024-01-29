<template>
  <div>Starting menu of a new game</div>
  <v-container>
    <v-select
      label="Select"
      :items="['California', 'Colorado', 'Florida', 'Georgia', 'Texas', 'Wyoming']"
    ></v-select>
  </v-container>
</template>

<script>
// import { useUserStore } from '@/stores/user';
// import { useMapsStore } from '@/stores/maps';

export default {
    setup() {
        // const userData = useUserStore();
        // const mapData = useMapsStore();
    }

}
</script>

<style scoped></style>
