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
    subject: "",
    games: [],
  }),
  actions: {
    setIsAuthenticated(boolean) {
      this.isAuthenticated = boolean;
    },
    setName(firstName, lastName) {
      this.firstName = firstName;
      this.lastName = lastName;
    },
    setSubject(subject) {
      this.subject = subject;
    },
    setGames(games) {
      this.games = games;
    },
    async fetchUserGames() {
      try {
        let fetchResponse = await fetch(
          process.env.VUE_APP_API_ADDRESS + "/api/User/" + this.subject,
          {
            method: "GET",
            headers: {
              Authorization: "Bearer" + sessionStorage.getItem("userToken"),
            },
          }
        );
        let data = await fetchResponse.json();
        if (data[0].games !== null) {
          this.games = data[0].games;
        }
      } catch (error) {
        console.log(error);
      }
    },
  },
});
