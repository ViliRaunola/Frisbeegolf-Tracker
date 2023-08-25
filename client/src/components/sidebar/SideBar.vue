<template>
  <div class="side-bar" :style="{width: sideBardWidth}">
    <span class="collapse-icon" :class="{'rotate-180' : collapsed}" @click="toggleSidebar">
      <font-awesome-icon :icon="['fas', 'angles-left']" size="lg" />
    </span>

    <div class="side-bar-links">
      <SideBarLink to="/" icon="house">Home</SideBarLink>
      <SideBarLink to="/NewGame" icon="plus">New Game</SideBarLink>
      <SideBarLink to="/PastGames" icon="calendar-days">Past Games</SideBarLink>
    </div>

    <LogoutButtonVue />
  </div>
</template>

<script>
import { collapsed, toggleSidebar, sideBardWidth } from './state';
import SideBarLink from './SideBarLink.vue';
import LogoutButtonVue from './LogoutButton.vue';


export default {
  props: {
  },
  components: {
    SideBarLink,
    LogoutButtonVue
  },
  setup() {
    return {collapsed, toggleSidebar, sideBardWidth}
  }
}
</script>

<style scoped>
.side-bar {
    color: var(--white);
    background-color: var(--main-color);

    float: left;
    position: fixed;
    z-index: 1;
    top: 0;
    left: 0;
    bottom: 0;
    padding: 0.5rem;

    transition: 0.3s ease;

    display: flex;
    flex-direction: column;
}

.collapse-icon {
  position: absolute;
  top: 0;
  padding: 0.75rem;

  color: var(--white);

  transition: 0.2s linear;
}



.rotate-180 {
  transform: rotate(180deg);
  transition: 0.2s linear;
}

.side-bar-links{
  margin-top: 2em;
  display: flex;
  flex-direction: column;
  gap: 0.5em;
}
</style>
