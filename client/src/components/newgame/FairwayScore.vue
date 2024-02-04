<template>
  <div class="fairway-container">
    <div># {{number}}</div>
    <div>Distance: {{distance}}</div>
    <div>Par: {{par}}</div>
    <!-- TODO: add real cross -->
    <v-btn>+</v-btn>
  </div>
</template>

<script>
export default {
    props: ["number", "par", "distance"],
    setup(props){
        console.log(props)
    }

}
</script>

<style>
.fairway-container{
    display: flex;
    gap: 1rem;
}
</style>
