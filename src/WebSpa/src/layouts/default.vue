<script>
import Footer from "@/components/footer.vue";
export default {
  components: { Footer }
};
</script>

<template>
  <div class="section-wrapper">
    <div class="dark-toggle">
      <BaseColor />
    </div>
    <div id="page-section" class="main-section">
      <div id="page">
        <slot />
      </div>
    </div>
    <div id="bottom-section" class="bottom-section">
      <Footer />
    </div>
  </div>
</template>

<style scoped>
.section-wrapper {
  display: flex;
  flex-flow: column;
  flex-wrap: nowrap;

  height: inherit;
  min-height: 100%;
}

.main-section {
  flex: 1 1 auto;
}

.bottom-section {
  flex: 0 1 auto;
}

#page {
  height: 100%;
  background-color: var(--c2);
}

.dark-toggle {
  position: absolute;
  right: 200px;
}
</style>
