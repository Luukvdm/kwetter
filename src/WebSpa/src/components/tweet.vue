<script>
export default {
  props: {
    tweetId: {
      type: Number
    },
    tweetMessage: {
      type: String
    },
    tweetPostTime: {
      type: String,
      default: "00-00-00"
    },
    tweetHearts: {
      type: Number,
      default: 0
    },

    posterId: {
      type: String,
      default: ""
    },
    posterName: {
      type: String,
      default: "-"
    },
    posterUsername: {
      type: String,
      default: ""
    },
    posterIsVerified: {
      type: Boolean,
      default: false
    },
    posterProfilePicture: {
      type: String,
      default: ""
    }
  }
};
</script>

<template>
  <div class="tweet">
    <div class="tweet-container">
      <div class="profile-pic-container">
        <div class="profile-pic-cast">
          <img class="profile-pic-img" src="@/assets/logo.png" />
        </div>
      </div>
      <div class="message-container">
        <div class="poster-container">
          <b class="poster-name">{{ posterName }}</b>
          <span class="poster-username">@{{ posterUsername }}</span>
        </div>
        <div class="tweet-content-container">
          <span class="tweet-content">
            {{ tweetMessage }}
          </span>
        </div>
        <div class="symbols-container">
          <div class="symbols-wrapper">
            <BaseGlowyButton text="0" symbol="comment" color="" />
            <BaseGlowyButton text="0" symbol="retweet" color="" />
            <BaseGlowyButton :text="tweetHearts" symbol="heart" color="" />
            <BaseGlowyButton text="" symbol="share-square" color="" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.tweet {
  display: flex;
  margin-top: 15px;
  margin-bottom: 15px;
  width: 100%;
}

.tweet-container {
  display: flex;
  padding-right: 16px;
  padding-left: 16px;
  width: 100%;
}

.tweet-container .message-container {
  flex-basis: 0px;
  flex-grow: 1;
  display: flex;
  flex-direction: column;
  text-align: left;
}

.tweet-container .profile-pic-container,
.message-container {
  float: left;
}

.poster-container {
  text-align: left;
  margin-bottom: 3px;
}

.poster-container .poster-name {
  color: var(--text-color-bright);
  margin-right: 8px;
}

.poster-container .poster-username {
  color: var(--text-color-faded);
}

.tweet-container .tweet-content {
  color: var(--text-color-bright);
  overflow-wrap: anywhere;
}

.symbols-container {
  margin-top: 12px;
}

.symbols-wrapper {
  justify-content: space-between;
  display: flex;
  flex-direction: row;
  max-width: 425px;
}

.symbols-wrapper .symbol-box {
  color: var(--text-color-faded);
}
</style>
