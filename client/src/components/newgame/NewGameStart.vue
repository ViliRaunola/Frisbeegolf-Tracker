<template>
  <div class="page-container">
    <div>Starting menu of a new game</div>
    <template v-if="!isMapSelected"
      ><LocationSelection @selected-map-update="onSelectedMapUpdate"
    /></template>
    <template v-if="isMapSelected">
      <div class="fairways-container">
        <FairwayScore
          v-for="fairway in selectedMap.fairways"
          :key="fairway.number"
          :number="fairway.number"
          :par="fairway.par"
          :distance="fairway.distance"
        />
      </div>
    </template>
  </div>
</template>

<script>
import { useUserStore } from '@/stores/user';
import { useMapsStore } from '@/stores/maps';
import LocationSelection from './LocationSelection.vue';
import FairwayScore from './FairwayScore.vue';
import { ref } from 'vue';

export default {
  components: { LocationSelection, FairwayScore },
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

.fairways-container{
  display: flex;
  flex-direction: column;
  align-items: center;

  margin: 2rem 0 0 0;
  gap: 2rem;
}
</style>
