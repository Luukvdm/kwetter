<script>
import appConfig from "@/config/app.config.js";

export default {
  data: function() {
    return {
      currentYear: new Date().getFullYear(),
      appTitle: appConfig.title
    };
  }
};
</script>

<template>
  <footer>
    <div class="content">
      Released under the
      <a
        href="https://www.gnu.org/licenses/gpl-3.0.en.html"
        target="_blank"
        rel="noopener"
        >GPL License</a
      >
      <br />
      Copyright &copy; 2020-{{ currentYear }} {{ appTitle }}
    </div>
  </footer>
</template>

<style scoped>
footer {
  position: relative;
  padding: 40px 0;
  text-align: center;
}
</style>
