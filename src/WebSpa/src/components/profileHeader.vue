<script>
import { mapActions } from "vuex";

export default {
  props: {
    id: String,
    displayName: String,
    username: String,
    profilePicture: String,
    bannerImage: String,
    isCurrentUser: Boolean,
    isFollowing: Boolean,
    followers: {
      type: Array,
      default: () => []
    },
    following: {
      type: Array,
      default: () => []
    }
  },
  methods: {
    ...mapActions("userRelation", ["unFollowUser", "followUser"]),
    follow() {
      this.followUser(this.id);
    },
    unFollow() {
      this.unFollowUser(this.id);
    }
  }
};
</script>

<template>
  <div class="profile-header bordered">
    <img class="banner" :src="bannerImage" />
    <div class="info-box">
      <div class="top-row row">
        <div class="profile-pic">
          <div class="profile-pic-container">
            <div class="profile-pic-cast">
              <img class="profile-pic-img" src="@/assets/logo.png" />
            </div>
          </div>
        </div>
        <div v-if="!isCurrentUser" class="profile-buttons">
          <button @click="unFollow" v-if="isFollowing" class="following-button">
            Unfollow
          </button>
          <button @click="follow" v-else class="following-button">
            Follow
          </button>
        </div>
      </div>
      <div class="name-row row">
        <span class="display-name">{{ displayName }}</span>
        <span class="username">@{{ username }}</span>
      </div>
      <div class="follow-row row">
        <div class="follow-box">
          <span class="follow-amount">{{ following.length }}</span>
          &nbsp;
          <span class="follow-desc">Following</span>
        </div>
        <div class="follow-box">
          <span class="follow-amount">{{ followers.length }}</span>
          &nbsp;
          <span class="follow-desc">Followers</span>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.profile-header {
  display: flex;
  flex-direction: column;
}

.banner {
  width: 100%;
  min-width: 100%;
}

.info-box {
  margin-bottom: 16px;
  padding-top: 12px;
  padding-left: 16px;
  padding-right: 16px;
}

.info-box .row {
  display: flex;
}

.info-box .top-row {
  flex-wrap: wrap;
  justify-content: space-between;
  align-items: flex-end;
  flex-direction: row;
}

.info-box .name-row {
  flex-direction: column;
  margin-top: 4px;
  margin-bottom: 12px;
}

.name-row .display-name {
  color: var(--text-color-bright);
  font-weight: 800;
  line-height: 24px;
}
.name-row .username {
  color: var(--text-color-faded);
  font-weight: 400;
  font-size: 15px;
  line-height: 20px;
}

.profile-pic {
  margin-top: -18%;
  margin-left: -4px;
  margin-right: -4px;

  width: 25%;
  min-width: 48px;
}

.profile-pic .profile-pic-container {
  margin: 0;
}

.profile-pic .profile-pic-container .profile-pic-cast {
  width: 133px;
  height: 133px;
  border-width: 4px;
}

.following-button {
  border-radius: 999px;
  min-width: 97px;

  font-size: 15px;
  font-weight: 700;
}

.follow-box {
  margin-right: 20px;
}

.follow-box .follow-amount {
  font-weight: 700;
  color: var(--text-color-bright);
}

.follow-box .follow-desc {
  color: var(--text-color-faded);
}
</style>
