<script>
import Layout from "@/layouts/default.vue";
import Timeline from "@/components/timeline.vue";
import SearchBox from "@/components/search.vue";
import VerticalHeader from "@/components/vertical-header.vue";

export default {
  components: {
    Layout,
    Timeline,
    SearchBox,
    VerticalHeader,
  },
};
</script>

<template>
  <Layout>
    <div class="page-wrapper">
      <div class="hor-container">
        <VerticalHeader class="sub-item" />
        <div class="main-item">
          <Timeline class="tweet-timeline" />
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
}

.hor-container {
  display: flex;
  flex-direction: row;
  justify-content: center;

  height: 100%;
  background: var(--c1);
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

.tweet-timeline {
  border: 0px solid black;
}
</style>
