<script>
import { mapActions } from "vuex";

import VerticalLayout from "@/layouts/verticalLayout.vue";
import ProfileHeader from "@/components/profileHeader.vue";
import Timeline from "@/components/timeline.vue";

export default {
  components: {
    VerticalLayout,
    ProfileHeader,
    Timeline
  },
  data() {
    return {
      username: this.$route.params.username,
      profile: Object
    };
  },
  async beforeRouteUpdate(to) {
    this.username = to.params.username;
    if (this.username) await this.fetchProfile();
  },
  methods: {
    ...mapActions("profile", ["getProfile", "getMyProfile"]),
    async fetchProfile() {
      if (this.username) this.profile = await this.getProfile(this.username);
      else this.profile = await this.getMyProfile();
    }
  },
  async mounted() {
    await this.fetchProfile();
  }
};
</script>

<template>
  <VerticalLayout>
    <ProfileHeader
      :id="profile.id"
      :displayName="profile.displayName"
      :username="profile.username"
      :profilePicture="profile.profilePicture"
      :bannerImage="profile.bannerImage"
      :isCurrentUser="profile.isCurrentUser"
      :isFollowing="profile.isFollowing"
    >
    </ProfileHeader>
    <Timeline :username="username" class="tweet-timeline" />
  </VerticalLayout>
</template>

<style scoped></style>
