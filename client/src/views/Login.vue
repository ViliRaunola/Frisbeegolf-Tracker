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

        console.log(response);
        const userDecodedData = decodeCredential(response.credential);

        console.log(userDecodedData);

        sessionStorage.setItem("userToken", response.credential);
        userData.setIsAuthenticated(true);
        userData.setName(userDecodedData.given_name, userDecodedData.family_name);

        // fetch('http://localhost:5236/api/User', {
        //     method: 'GET',
        // })
        //     .then(response => {
        //         response.json().then(res => console.log(res))
        //     })

        router.push('/');
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
