<script>
export default {
  props: {
    text: String,
    color: String,
    symbol: String,
    includeTextInHover: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    hoverSymbolStart(event) {
      let symbolBox = event.target.closest(".symbol-box");
      if (!symbolBox.classList.contains("highlited"))
        symbolBox.classList.toggle("highlited");
    },
    hoverSymbolEnd(event) {
      let symbolBox = event.target.closest(".symbol-box");
      symbolBox.classList.toggle("highlited");
    }
  }
};
</script>

<template>
  <div
    class="symbol-box"
    @mouseover="hoverSymbolStart"
    @mouseleave="hoverSymbolEnd"
  >
    <div class="symbol">
      <div class="symbol-highlighter" />
      <BaseIcon class="symbol" :name="symbol" />
      <span v-if="text && includeTextInHover" class="symbol-val">{{
        text
      }}</span>
    </div>
    <span v-if="text && !includeTextInHover" class="symbol-val">{{
      text
    }}</span>
  </div>
</template>

<style>
.symbol-box {
  display: flex;
  justify-content: flex-start;
  cursor: pointer;
  color: inherit; /* var(--text-color-faded); */
}

.symbol-box.highlited {
  color: var(--main-highlight-color) !important;
}

.symbol {
  position: relative;
  min-width: 1.4em;
}

.symbol .symbol-highlighter {
  position: absolute;
  top: 0px;
  right: 0px;
  left: 0px;
  bottom: 0px;
  margin: -8px;
  outline-style: none;
  transition-property: background-color, box-shadow;
  transition-duration: 0.2s;
  border-radius: 999px;
}
.symbol-box.highlited .symbol .symbol-highlighter {
  background-color: rgba(255, 169, 48, 0.1);
}
.symbol-val {
  min-width: calc(1em + 5px);
  padding-right: 12px;
  padding-left: 12px;
}
</style>
