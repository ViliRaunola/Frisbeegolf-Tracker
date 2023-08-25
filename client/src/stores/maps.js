import { defineStore } from "pinia";

export const useMapsStore = defineStore("mapsData", {
  state: () => ({
    id: "",
    name: "",
    location: "",
    fairways: [],
  }),
});
