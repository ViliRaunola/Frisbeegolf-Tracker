<template>
  <GoogleLogin :callback="callback" />
  <SideBar />
  <router-view :style="{'margin-left': sideBardWidth}"> </router-view>
</template>

<script setup>
import { useRouter } from 'vue-router';
import { useUserStore } from "../stores/user"
import { useMapsStore } from '@/stores/maps';
import { decodeCredential } from 'vue3-google-login'

const router = useRouter();
const userData = useUserStore();
const mapData = useMapsStore();

if(sessionStorage.getItem("userToken")){
    router.push('/');
}

const callback = async (response) => {
    if(!response){
        console.log("Something went wrong")
        userData.setIsAuthenticated(false);
    }else if(response.clientId) {
        const userDecodedData = decodeCredential(response.credential);
        sessionStorage.setItem("userToken", response.credential);
        userData.setIsAuthenticated(true);
        userData.setName(userDecodedData.given_name, userDecodedData.family_name);

        console.log(response.credential)
        console.log(sessionStorage.getItem("userToken"))
        const userDataToSend = {
            subject: userDecodedData.sub,
            name: userDecodedData.given_name + ' ' + userDecodedData.family_name
        }

        // Creating a new user to the server if one doesn't exist yet.

            const fetchResponse = await fetch(process.env.VUE_APP_API_ADDRESS + '/api/User', {
            method: 'POST',
            headers:
                {
                'Authorization': 'Bearer' + sessionStorage.getItem("userToken"),
                'Content-Type': 'application/json'
                },
            body: JSON.stringify(userDataToSend)
        });

        const data = await fetchResponse.json();
        if(data.subject != null){
            userData.setSubject(data.subject)
            await userData.fetchUserGames();
            await mapData.fetchMaps();
        }
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
