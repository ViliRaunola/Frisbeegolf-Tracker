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
      <tr v-for="item in paginate_table" :key="item.dateTime">
        <td>{{ item.mapName }}</td>
        <td>{{ total_score(item.score) }}</td>
        <td>{{ format_date(item.dateTime) }}</td>
      </tr>
    </tbody>
  </v-table>
  <div>
    <v-pagination
      v-model="page"
      :length="page_count"
      prev-icon="mdi-menu-left"
      next-icon="mdi-menu-right"
    ></v-pagination>
  </div>
</template>

<script>
import { useUserStore } from '@/stores/user';
import { useMapsStore } from '@/stores/maps';
import moment from 'moment'

export default {
    data() {
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
      mapData,
      itemsPerPage: 10,
      page: 1
    }
  },
  methods: {
      format_date(value){
         if (value) {
           return moment(value).format('DD/MM/YYYY H:mm')
          }
      },
      total_score(scores){
        return scores.reduce((accumulator, currentValue) => accumulator + currentValue, 0);
      }
   },
   computed: {
    total_records() {
      return this.userData.games.length
    },
    page_count() {
      return Math.round(this.total_records / this.itemsPerPage)
    },
    paginate_table() {
      const start = (this.page - 1) * this.itemsPerPage;
      const end = start + this.itemsPerPage;
      return this.userData.games.slice(start, end)
    },
   }
}
</script>
