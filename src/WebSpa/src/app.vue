<script>
import "@/design/style.css";

export default {};
</script>

<template>
  <div id="app">
    <RouterView :key="$route.fullPath" />
  </div>
</template>
