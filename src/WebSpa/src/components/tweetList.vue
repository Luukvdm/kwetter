<script>
import Tweet from "./tweet.vue";
export default {
  components: {
    Tweet,
  },
  props: {
    tweets: Array,
  },
};
</script>

<template>
  <div>
    <ul class="tweet-list container">
      <li
        class="tweet-list-item tweet bordered"
        v-for="(tweet, key) in tweets"
        v-bind:key="key"
      >
        <Tweet class="tweet" :tweet="tweet" :poster="tweet.poster" />
      </li>
    </ul>
  </div>
</template>

<style scoped>
.tweet-list {
  display: flex;
  flex-direction: column;
  margin-top: 25px;
  margin-bottom: 25px;
}

.tweet-list .tweet-list-item:first-child {
  border-top-width: 1px;
}
</style>
