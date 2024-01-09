<template>
  <div>Past Games</div>
</template>

<script setup>
import { useUserStore } from '@/stores/user';
const userData = useUserStore();

console.log(userData.subject)

const fetchResponse = await fetch(
            process.env.VUE_APP_API_ADDRESS + '/api/User/' + userData.subject, {
            method: 'GET',
            headers:
                {'Authorization': 'Bearer' + sessionStorage.getItem("userToken")}
        });

const data = await fetchResponse.json();
console.log(data)
if(data[0].games !== null) {
  userData.games = data[0].games;
}
// console.log(userData.subject)
console.log(userData.games)
</script>

<script>
export default {
    name: 'PastGames',
    components: {

    },
}
</script>

<style></style>
