import { defineStore } from "pinia";

export const useMapsStore = defineStore("mapsData", {
  state: () => ({
    maps: [],
  }),

  actions: {
    addToMapsList(map) {
      this.maps.push(map);
    },
    clearMapsList() {
      this.maps = [];
    },
    async fetchMaps() {
      const fetchResponse = await fetch(
        process.env.VUE_APP_API_ADDRESS + "/api/Map/",
        {
          method: "GET",
          headers: {
            Authorization: "Bearer" + sessionStorage.getItem("userToken"),
          },
        }
      );
      const data = await fetchResponse.json();
      if (data[0] !== null) {
        this.clearMapsList();
        data.forEach((map) => {
          this.addToMapsList(map);
        });
      }
    },
  },
});
