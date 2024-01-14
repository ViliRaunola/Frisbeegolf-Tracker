<template>
  <v-table>
    <thead>
      <tr>
        <th class="text-left">Location</th>
        <th class="text-left">Total score</th>
        <th class="text-left">Time</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in userData.games" :key="item.dateTime">
        <td>{{ item.mapName }}</td>
        <td>{{ total_score(item.score) }}</td>
        <td>{{ format_date(item.dateTime) }}</td>
      </tr>
    </tbody>
  </v-table>
</template>

<script>
import { useUserStore } from '@/stores/user';
import { useMapsStore } from '@/stores/maps';
import moment from 'moment'

export default {
    setup() {
    const userData = useUserStore();
    const mapData = useMapsStore();

    // Adding the name of the map to the user's game based on id
    userData.games.forEach(game => {
        mapData.maps.forEach(map => {
            if(game.mapId == map.id){
                game.mapName = map.name
            }
        });
    });

    return {
      userData,
      mapData
    }
  },
  methods: {
      format_date(value){
         if (value) {
           return moment(String(value)).format('DD/MM/YYYY HH:MM')
          }
      },
      total_score(scores){
        return scores.reduce((accumulator, currentValue) => accumulator + currentValue, 0);
      }
   },
}
</script>
