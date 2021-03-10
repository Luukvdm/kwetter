<script>
export default {
  props: {
    tweet: {
      id: Number,
      message: String,
      postTime: String,
      hearts: Number,
    },
    poster: {
      name: String,
      username: String,
      isVerifified: Boolean,
      picture: String,
    },
  },
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
          <b class="poster-name">{{ poster.name }}</b>
          <span class="poster-username">@{{ poster.username }}</span>
        </div>
        <div class="tweet-content-container">
          <span class="tweet-content">
            {{ tweet.message }}
          </span>
        </div>
        <div class="symbols-container">
          <div class="symbols-wrapper">
            <BaseGlowyButton text="0" symbol="comment" color="" />
            <BaseGlowyButton text="0" symbol="retweet" color="" />
            <BaseGlowyButton :text="tweet.hearts" symbol="heart" color="" />
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
.tweet-container .profile-pic-container {
  flex-basis: 49px;
  flex-grow: 0;
  flex-shrink: 1;
  margin-right: 12px;
}

.profile-pic-container .profile-pic-cast {
  position: relative;
  width: 50px;
  height: 50px;
  overflow: hidden;
  border-radius: 50%;
  border-width: 2px;
  border-style: solid;
  border-color: var(--c0);
}

.profile-pic-container .profile-pic-cast .profile-pic-img {
  width: 100%;
  height: auto;
  background: var(--c0_hard);
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
