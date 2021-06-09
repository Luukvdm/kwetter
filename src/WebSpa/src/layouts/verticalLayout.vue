<script>
import Layout from "@/layouts/default.vue";
import VerticalHeader from "@/components/vertical-header.vue";
import SearchBox from "@/components/search.vue";

export default {
  components: {
    Layout,
    VerticalHeader,
    SearchBox
  }
};
</script>
<template>
  <Layout>
    <div class="page-wrapper">
      <div class="hor-container">
        <VerticalHeader class="sub-item" />
        <div class="main-item">
          <slot />
        </div>
        <div class="sub-item">
          <SearchBox />
        </div>
      </div>
    </div>
  </Layout>
</template>

<style scoped>
.page-wrapper {
  width: 100%;
  height: 100%;
}

.hor-container {
  display: flex;
  flex-direction: row;
  justify-content: center;

  height: 100%;
}

.sub-item,
.main-item {
  float: left;
}

.sub-item {
  width: 300px;
  height: 100%;
  margin: 5px 20px;
}

.main-item {
  width: 600px;
  height: 100%;
}
</style>
