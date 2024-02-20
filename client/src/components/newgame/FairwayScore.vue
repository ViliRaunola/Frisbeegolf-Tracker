<template>
  <div class="fairway-container">
    <div :class="['text-body-1', 'pa-2']"># {{number}}</div>
    <div :class="['text-body-1', 'pa-2']">Distance: {{distance}}</div>
    <div :class="['text-body-1', 'pa-2']">Par: {{par}}</div>
    <v-text-field
      label="Your score"
      v-model="playerResult"
      hide-details
      single-line
      type="number"
      @change="save_player_score"
    ></v-text-field>
  </div>
</template>

<script>
import { useUserStore } from '@/stores/user';
export default {
    props: ["number", "par", "distance"],
    setup(props){
        const userData = useUserStore();
        console.log(props)
        return {userData}
    },
    data() {
      return {
        playerResult: (this.userData.currentGameScores[this.number - 1] === 'undefined') ? undefined : this.userData.currentGameScores[this.number - 1]
      }
    },
    methods: {
      save_player_score() {
        this.userData.setCurrentGameScore(this.number-1, this.playerResult);
      }
    }

}
</script>

<style>
.fairway-container{
    display: flex;
    gap: 1rem;
}
</style>
