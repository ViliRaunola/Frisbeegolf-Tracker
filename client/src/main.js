import { createApp } from "vue";
import App from "./App.vue";
import { library } from "@fortawesome/fontawesome-svg-core";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import {
  faAnglesLeft,
  faAnglesRight,
  faHouse,
  faPlus,
  faCalendarDays,
  faRightFromBracket,
} from "@fortawesome/free-solid-svg-icons";
import { createRouter, createWebHistory } from "vue-router";
import Home from "./views/Home.vue";
import NewGame from "./views/NewGame.vue";
import PastGames from "./views/PastGames.vue";
import Login from "./views/Login.vue";
import vue3GoogleLogin from "vue3-google-login";
import { createPinia } from "pinia";

library.add(
  faAnglesLeft,
  faAnglesRight,
  faHouse,
  faPlus,
  faCalendarDays,
  faRightFromBracket
);

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/",
      name: "Home",
      component: Home,
      meta: {
        requiresAuth: true,
      },
    },
    {
      path: "/NewGame",
      name: "NewGame",
      component: NewGame,
      meta: {
        requiresAuth: true,
      },
    },
    {
      path: "/PastGames",
      name: "PastGames",
      component: PastGames,
      meta: {
        requiresAuth: true,
      },
    },
    {
      path: "/Login",
      name: "Login",
      component: Login,
      meta: {
        requiresAuth: false,
      },
    },
  ],
});

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (
      sessionStorage.getItem("userToken") == null ||
      sessionStorage.getItem("userToken") === ""
    ) {
      next({
        path: "/login",
        params: { nextUrl: to.fullPath },
      });
    }
  }
  next();
});
const pinia = createPinia();
const app = createApp(App);
app.use(pinia);

app.use(vue3GoogleLogin, {
  clientId: process.env.VUE_APP_GOOGLE_CLIENT_ID,
});

app.component("font-awesome-icon", FontAwesomeIcon).use(router).mount("#app");
