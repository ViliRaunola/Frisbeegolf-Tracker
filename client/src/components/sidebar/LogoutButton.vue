<template>
  <span class="logout" @click="logout">
    <font-awesome-icon class="icon" icon="right-from-bracket" size="lg" />
    <span v-if="!collapsed"> Logout </span>
  </span>
</template>

<script>
import { collapsed } from './state'
import { googleLogout } from "vue3-google-login"
import { useUserStore } from "../../stores/user"


export default {
    props: {
    },
    methods: {
        logout() {
            const userData = useUserStore();
            googleLogout();
            userData.setIsAuthenticated(false);
            userData.setName("", "");
            sessionStorage.removeItem("userToken");
            this.$router.push('/login');
        }
    },
    setup(){
        return { collapsed}
    }
}
</script>

<style>
.logout{
    position: relative;
    bottom: 10px;

    color: var(--white);
    display: flex;
    align-items: center;

    cursor: pointer;
    font-weight: 400;
    user-select: none;

    margin: auto 0 0.1em 0;
    padding: 0.4em;
    border-radius: 0.25em;
    height: 2em;
    text-decoration: none;

    background-color: var(--secondary-color);
}

.icon {
    margin-right: 10px;
}
</style>
