<template>
  <GoogleLogin :callback="callback" />
  <SideBar />
  <router-view :style="{'margin-left': sideBardWidth}"> </router-view>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { useUserStore } from "../stores/user"
import { decodeCredential } from 'vue3-google-login'

const router = useRouter();
const userData = useUserStore();

if(sessionStorage.getItem("userToken")){
    router.push('/');
}

const callback = (response) => {
    if(!response){
        console.log("Something went wrong")
        userData.setIsAuthenticated(false);
    }else if(response.clientId) {
        const userDecodedData = decodeCredential(response.credential)
        sessionStorage.setItem("userToken", response.credential)
        userData.setIsAuthenticated(true);
        userData.setName(userDecodedData.given_name, userDecodedData.family_name)
        router.push('/')
    }
}
</script>

<script>
export default {
    name: 'Log-in',
    components: {
    },
}
</script>
