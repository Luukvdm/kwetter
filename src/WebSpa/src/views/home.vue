<script>
import { mapGetters } from "vuex";

import VerticalLayout from "@/layouts/verticalLayout.vue";
import TweetForm from "@/components/tweetForm.vue";
import Timeline from "@/components/timeline.vue";

export default {
  components: {
    VerticalLayout,
    TweetForm,
    Timeline
  },
  computed: {
    ...mapGetters("oidcStore", ["oidcUser"])
  }
};
</script>

<template>
  <VerticalLayout>
    <div class="main-header bordered">
      <h2>Home</h2>
    </div>
    <TweetForm class="tweet-form" />
    <Timeline :username="oidcUser.preferred_username" class="tweet-timeline" />
  </VerticalLayout>
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

.main-item .main-header {
  border-top: 0;
  border-bottom: 0;
}

.main-item .tweet-form {
  border-bottom: 0;
}

.tweet-timeline {
  border: 1px solid var(--border-color);
}
</style>
