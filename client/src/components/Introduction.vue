<template>
  <img alt="Vue logo" src="../assets/logo.png" />
  <div class="hello">
    <h1>Welcome {{userData.firstName}}, to the Frisbee Golf Tracker App</h1>
    <p>This application is a project for testing out Vue and .NET 6 with Google authentication.</p>
    <p>
      You can track your frisbee golf results in here. Currently the application supports only a few
      fairways.
    </p>
  </div>
</template>

<script>
import { useUserStore } from "../stores/user"
export default {
  // eslint-disable-next-line vue/multi-word-component-names
  name: 'Introduction',
  props: {
  },
  setup() {
    const userData = useUserStore();
    return {
      userData
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
