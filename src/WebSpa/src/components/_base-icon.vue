<script>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { library as fontAwesomeIconLibrary } from "@fortawesome/fontawesome-svg-core";
import camelCase from "lodash/camelCase";

fontAwesomeIconLibrary.add(
  require("@fortawesome/free-solid-svg-icons/faSync").definition,
  require("@fortawesome/free-solid-svg-icons/faUser").definition,
  require("@fortawesome/free-solid-svg-icons/faSearch").definition,
  require("@fortawesome/free-solid-svg-icons/faUnlockAlt").definition,
  require("@fortawesome/free-solid-svg-icons/faComment").definition,
  require("@fortawesome/free-solid-svg-icons/faRetweet").definition,
  require("@fortawesome/free-solid-svg-icons/faHeart").definition,
  require("@fortawesome/free-solid-svg-icons/faShareSquare").definition,
  require("@fortawesome/free-solid-svg-icons/faHashtag").definition,
  require("@fortawesome/free-solid-svg-icons/faCog").definition
);

export default {
  components: {
    FontAwesomeIcon,
  },
  inheritAttrs: false,
  props: {
    source: {
      type: String,
      default: "font-awesome",
    },
    name: {
      type: String,
      required: true,
    },
  },
  computed: {
    // Gets a CSS module class, e.g. iconCustomLogo
    customIconClass() {
      return this.$style[camelCase("icon-custom-" + this.name)];
    },
  },
};
</script>

<template>
  <FontAwesomeIcon
    v-if="source === 'font-awesome'"
    v-bind="$attrs"
    :icon="name"
  />
  <span
    v-else-if="source === 'custom'"
    v-bind="$attrs"
    :class="customIconClass"
  />
</template>
