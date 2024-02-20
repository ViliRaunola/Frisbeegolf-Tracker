<template>
  <v-form v-model="valid" ref="form" @submit.prevent="handle_submit">
    <v-container>
      <v-select
        v-model="selectedField"
        label="Select field"
        item-title="name"
        :items="mapData.maps"
        :rules="requiredRule"
        return-object
      ></v-select>
      <v-btn type="submit">Next</v-btn>
    </v-container>
  </v-form>
</template>

<script>
import { useUserStore } from '@/stores/user';
import { useMapsStore } from '@/stores/maps';

export default {
    setup(props, context) {
        const userData = useUserStore();
        const mapData = useMapsStore();

        if(userData.currentGameMapId !== "") {
          skip_location_selection();
        }

        function skip_location_selection() {
          mapData.maps.forEach(map => {
            if(map.id === userData.currentGameMapId)
              context.emit('selected-map-update', map)
        });

      }

        return {userData, mapData}
    },

    methods: {
      handle_submit() {
        this.$refs.form.validate();
        console.log(this.selectedField)
        this.$emit('selected-map-update', this.selectedField)
      },

    },

    data: () => ({
      valid: false,
      value: '',
      selectedField: null,
      requiredRule: [
        v => !!v || 'Select the location first',
      ],
  }),
}
</script>

<style></style>
