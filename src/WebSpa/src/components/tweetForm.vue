<script>
import { mapActions } from "vuex";

export default {
  data() {
    return {
      message: ""
    };
  },
  methods: {
    ...mapActions("tweet", ["createTweet"]),
    tweet() {
      if (this.message.length == 0 || this.message.length > 280) {
        console.error("Message length no good");
        // return;
      }
      let tweet = {
        message: this.message
      };
      this.createTweet(tweet);
      this.message = "";
    }
  }
};
</script>

<template>
  <div class="form-container bordered">
    <div class="profile-row profile-pic-container">
      <div class="profile-pic-cast">
        <img class="profile-pic-img" src="@/assets/logo.png" />
      </div>
    </div>
    <div class="input-row">
      <textarea
        class="message-input"
        placeholder="What's happening?"
        v-model.trim="message"
      />
      <div class="icon-container bordered">
        <div class="message-options">
          <div class="message-option">
            <BaseGlowyButton class="option-symbol" symbol="image" />
          </div>
          <div class="message-option">
            <BaseGlowyButton class="option-symbol" symbol="smile" />
          </div>
        </div>
        <div class="message-buttons">
          <button v-on:click="tweet">Tweet</button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.form-container {
  display: flex;
  -moz-box-direction: normal;
  -moz-box-orient: horizontal;
  flex-direction: row;
  padding: 4px 16px;
}

.form-container .profile-row {
  flex-basis: 48px;
  margin-right: 12px;
  padding-top: 4px;
}

.input-row {
  display: flex;
  -moz-box-align: stretch;
  -moz-box-direction: normal;
  -moz-box-orient: vertical;
  flex-direction: column;
  width: 100%;
  padding-top: 4px;
}

.message-input {
  /* flex-grow: 1; */
  background-color: transparent;
  border: none;
  overflow-wrap: anywhere;
  font-size: 16pt;
  color: var(--text-color-bright);
}

.message-input::placeholder {
  color: var(--text-color-faded);
}

.icon-container {
  display: flex;
  -moz-box-pack: justify;
  justify-content: space-between;
  -moz-box-direction: normal;
  -moz-box-orient: horizontal;
  flex-direction: row;
  margin-top: 12px;
  border-width: 1px 0 0;
}

.icon-container .message-options {
  display: flex;
  -moz-box-align: center;
  align-items: center;
}

.icon-container .message-options .message-option {
  padding: 8px;
}

.option-symbol {
  font-size: 16pt;
}
</style>
