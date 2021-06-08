<script>
import Layout from "@/layouts/default.vue";
import TweetForm from "@/components/tweetForm.vue";
import Timeline from "@/components/timeline.vue";
import SearchBox from "@/components/search.vue";
import VerticalHeader from "@/components/vertical-header.vue";

export default {
  components: {
    Layout,
    TweetForm,
    Timeline,
    SearchBox,
    VerticalHeader
  }
};
</script>

<template>
  <Layout>
    <div class="page-wrapper">
      <div class="hor-container">
        <VerticalHeader class="sub-item" />
        <div class="main-item">
          <div class="main-header bordered">
            <h2>Home</h2>
          </div>
          <TweetForm class="tweet-form" />
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

.main-item .main-header {
  border-top: 0;
  border-bottom: 0;
}

.main-item .tweet-form {
  border-bottom: 0;
}

.tweet-timeline {
  border: 0px solid black;
}
</style>
