<script>
import Tweet from "./tweet.vue";
export default {
  components: {
    Tweet
  },
  props: {
    tweets: Array
  },
  data() {
    return {
      poster: Object
    };
  }
};
</script>

<template>
  <div>
    <ul class="tweet-list container">
      <li
        class="item bordered"
        v-for="tweet in tweets"
        :key="`${tweet.id}-${tweet.likes}`"
      >
        <Tweet
          class="tweet"
          :tweetId="tweet.id"
          :tweetMessage="tweet.message"
          :tweetPostTime="tweet.postTime"
          :tweetLikes="tweet.likes"
          :posterId="tweet.poster.id"
          :posterName="tweet.poster.displayName"
          :posterUsername="tweet.poster.username"
          :posterIsVerified="tweet.poster.isVerifified"
          :posterProfilePicture="tweet.poster.profilePicture"
        />
      </li>
    </ul>
  </div>
</template>

<style scoped>
.tweet-list {
  display: flex;
  flex-direction: column;
  margin-bottom: 25px;
}

.tweet-list .item:first-child {
  border-top-width: 1px;
}

.tweet-list .item {
  border-top-width: 0;
}
</style>
