import { defineStore } from "pinia";

let isLoggedIn = false;

if (sessionStorage.getItem("userToken")) {
  isLoggedIn = true;
}

export const useUserStore = defineStore("userData", {
  state: () => ({
    isAuthenticated: isLoggedIn,
    firstName: "",
    lastName: "",
  }),
  actions: {
    setIsAuthenticated(boolean) {
      this.isAuthenticated = boolean;
    },
    setName(firstName, lastName) {
      this.firstName = firstName;
      this.lastName = lastName;
    },
  },
});
