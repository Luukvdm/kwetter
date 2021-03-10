<script>
import LightStyle from "!!raw-loader!@/design/colors-light.css";
import DarkStyle from "!!raw-loader!@/design/colors-dark.css";

export default {
  data() {
    return {
      dark: false,
      styleElement: document.createElement("style")
    };
  },
  mounted() {
    this.styleElement.type = "text/css";
    this.setColorScheme();
    document.head.appendChild(this.styleElement);
  },
  methods: {
    setColorScheme() {
      const isDarkMode = window.matchMedia("(prefers-color-scheme: dark)")
        .matches;
      const isLightMode = window.matchMedia("(prefers-color-scheme: light)")
        .matches;
      const isNotSpecified = window.matchMedia(
        "(prefers-color-scheme: no-preference)"
      ).matches;
      const hasNoSupport = !isDarkMode && !isLightMode && !isNotSpecified;

      window
        .matchMedia("(prefers-color-scheme: dark)")
        .addListener(e => e.matches && this.activateDarkMode());
      window
        .matchMedia("(prefers-color-scheme: light)")
        .addListener(e => e.matches && this.activateLightMode());

      if (isDarkMode) this.activateDarkMode();
      if (isLightMode) this.activateLightMode();
      if (isNotSpecified || hasNoSupport) {
        console.log(
          "You specified no preference for a color scheme or your browser does not support it. I schedule dark mode during night time."
        );
        const now = new Date();
        const hour = now.getHours();
        if (hour < 4 || hour >= 16) {
          this.activateDarkMode();
        }
      }
    },
    activateDarkMode() {
      if (this.dark) return;
      this.styleElement.innerHTML = DarkStyle;
      this.dark = true;
    },
    activateLightMode() {
      if (!this.dark) return;
      this.styleElement.innerHTML = LightStyle;
      this.dark = false;
    },
    toggleDarkMode() {
      if (this.dark) this.activateLightMode();
      else this.activateDarkMode();
    }
  }
};
</script>

<template>
  <button @click="toggleDarkMode">Toggle Dark Mode</button>
  <span></span>
</template>

<style></style>
