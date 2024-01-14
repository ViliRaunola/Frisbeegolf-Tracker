<template>
  <div class="page-layout">
    <div>Past Games</div>
    <PastGamesTable></PastGamesTable>
  </div>
</template>

<script>
import PastGamesTable from '@/components/pastgames/PastGamesTable.vue';

export default {
    name: 'PastGames',
    components: {
      PastGamesTable
    }
  }
</script>

<style>
.page-layout{
  margin-left: 56px;
  display: flex;
  flex-direction: column;
  align-items: center;
}

.table-container {
  max-width: 100%;
}
</style>
