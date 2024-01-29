<template>
  <div class="page-container">
    <div>Starting menu of a new game</div>
    <template v-if="!isMapSelected"
      ><LocationSelection @selected-map-update="onSelectedMapUpdate"
    /></template>
  </div>
</template>

<script>
import { useUserStore } from '@/stores/user';
import { useMapsStore } from '@/stores/maps';
import LocationSelection from './LocationSelection.vue';
import { ref } from 'vue';

export default {
  components: { LocationSelection },
    setup() {
        const userData = useUserStore();
        const mapData = useMapsStore();
        let selectedMap;
        let isMapSelected = ref(false);

        return {userData, mapData, selectedMap, isMapSelected}
    },
    methods: {
      onSelectedMapUpdate(newValue){
        this.selectedMap = newValue;
        this.isMapSelected = !this.isMapSelected;
      }
    },
}
</script>

<style scoped>
.page-container{
  margin: 0 15rem 0 15rem;
}
</style>
