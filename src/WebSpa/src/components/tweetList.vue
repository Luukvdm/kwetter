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
      <li class="item bordered" v-for="(tweet, key) in tweets" v-bind:key="key">
        <Tweet
          class="tweet"
          :tweetId="tweet.id"
          :tweetMessage="tweet.message"
          :tweetPostTime="tweet.postTime"
          :tweetHearts="tweet.hearts"
          :posterId="tweet.poster.id"
          :posterName="tweet.poster.firstName"
          :posterUsername="tweet.poster.userName"
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
