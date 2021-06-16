<script>
import TweetList from "./tweetList.vue";
import { mapActions, mapGetters } from "vuex";

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
    ...mapActions("tweet", ["getTimeline"]),
    ...mapGetters("oidcStore", ["oidcUser"])
  },
  async mounted() {
    let username = this.username;
    if (!username) username = this.oidcUser().preferred_username;
    this.timeline = await this.getTimeline(username);
  }
};
</script>

<template>
  <div>
    <TweetList :tweets="timeline.tweets" />
  </div>
</template>

<style scoped></style>
