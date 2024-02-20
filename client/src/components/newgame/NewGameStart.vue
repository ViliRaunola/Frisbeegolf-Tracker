<template>
  <div class="page-container">
    <div :class="['text-h5', 'pa-2']">Starting menu of a new game</div>
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
      <v-btn @click="sendPlayerResults">save</v-btn>
      <v-alert v-if="saveFailed" text="Saving results failed" type="error" style="margin-top: 5rem;"
        ><v-btn @click="confirmSaveResult" style="margin-left: 1rem;">Ok</v-btn></v-alert
      >
      <v-alert v-if="saveComplete" text="Results saved" type="success" style="margin-top: 5rem;">
        <v-btn @click="confirmSaveResult" style="margin-left: 1rem;">Ok</v-btn>
      </v-alert>
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
    data() {
        const userData = useUserStore();
        const mapData = useMapsStore();
        let saveComplete = false;
        let saveFailed = false;
        let selectedMap;
        let isMapSelected = ref(false);

        return {userData, mapData, selectedMap, isMapSelected, saveComplete, saveFailed}
    },
    methods: {
      onSelectedMapUpdate(newValue){
        this.selectedMap = newValue;
        this.userData.currentGameMapId = newValue.id;
        this.isMapSelected = !this.isMapSelected;
      },
      async sendPlayerResults(){

        const userDataToSend = {
          mapId: this.userData.currentGameMapId,
          score: this.userData.currentGameScores
        }

        console.log(JSON.stringify(userDataToSend))

        try{
          const fetchResponse = await fetch(process.env.VUE_APP_API_ADDRESS + `/api/User/game/${this.userData.subject}`, {
          method: 'PUT',
          headers:
              {
              'Authorization': 'Bearer' + sessionStorage.getItem("userToken"),
              'Content-Type': 'application/json'
              },
          body: JSON.stringify(userDataToSend)
          });

          if(fetchResponse.status){
            this.saveComplete = true;
            console.log("here")
          }else {
            this.saveFailed = true;
          }

        }
        catch(error){
          this.saveFailed = true;
          console.log(error)
        }

        // Clearing "pinia"
        this.userData.setCurrentGameMapId("");
        this.userData.nullCurrentGameScore();
      },
      confirmSaveResult() {
        this.isMapSelected = false
        this.saveComplete = false
        this.saveFailed = false
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

  margin: 2rem 0 2rem 0;
  gap: 2rem;
}
</style>
