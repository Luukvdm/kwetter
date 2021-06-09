<script>
import TweetList from "./tweetList.vue";
import { mapActions } from "vuex";

export default {
  components: {
    TweetList
  },
  props: {
    username: String
  },
  data() {
    return {
      timeline: Array
    };
  },
  methods: {
    ...mapActions("tweet", ["getTimeline"])
  },
  async mounted() {
    this.timeline = await this.getTimeline(this.username);
  }
};
</script>

<template>
  <div>
    <TweetList :tweets="timeline.tweets" />
  </div>
</template>

<style scoped></style>
