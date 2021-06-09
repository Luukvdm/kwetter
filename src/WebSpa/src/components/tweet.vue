<script>
import { mapActions } from "vuex";

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
    tweetLikes: {
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
  },
  data() {
    return {
      tweet: {
        id: null,
        message: "",
        postTime: null,
        hearts: 0
      },
      poster: {
        id: null,
        name: "",
        username: "",
        isVerified: false,
        profilePicture: ""
      }
    };
  },
  mounted() {
    this.tweet = {
      id: this.tweetId,
      message: this.tweetMessage,
      postTime: this.tweetPostTime,
      hearts: this.tweetLikes
    };
    this.poster = {
      id: this.posterId,
      name: this.posterName,
      username: this.posterUsername,
      isVerified: this.posterIsVerified,
      profilePicture: this.posterProfilePicture
    };
  },
  methods: {
    ...mapActions("tweet", ["likeTweet"]),
    like() {
      // event, text
      this.likeTweet(this.tweet.id);
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
          <router-link
            :to="`/profile/${poster.username}`"
            class="poster-name"
            >{{ poster.name }}</router-link
          >
          <span class="poster-username">@{{ poster.username }}</span>
        </div>
        <div class="tweet-content-container">
          <span class="tweet-content">
            {{ tweet.message }}
          </span>
        </div>
        <div class="symbols-container">
          <div class="symbols-wrapper">
            <BaseGlowyButton
              text="0"
              symbol="comment"
              color="#1da1f2"
              hoverColor="rgba(29, 161, 242, 0.1)"
            />
            <BaseGlowyButton
              text="0"
              symbol="retweet"
              color="#17bf63"
              hoverColor="rgba(23, 191, 99, 0.1)"
            />
            <BaseGlowyButton
              :text="tweet.hearts.toString()"
              @glowyClick:event,text="like"
              symbol="heart"
              color="#e0245e"
              hoverColor="rgba(224, 36, 94, 0.1)"
            />
            <BaseGlowyButton
              text=""
              symbol="share-square"
              color="#1da1f2"
              hoverColor="rgba(29, 161, 242, 0.1)"
            />
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
  font-weight: bold;
  margin-right: 3px;
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
