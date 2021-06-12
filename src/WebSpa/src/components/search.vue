<script>
import { mapActions } from "vuex";

export default {
  data() {
    return {
      isSearching: false,
      results: [
        /*{
          content: "test result",
          category: "trending",
        },
        {
          content: "Also test result",
        },
        {
          content: "Test Person",
          category: "a person",
        },*/
      ],
    };
  },
  methods: {
    ...mapActions("profile", ["searchUsers"]),
    activateSearch() {
      let sWrapper = document.getElementsByClassName("search-wrapper")[0];
      let sIcon = document.getElementsByClassName("search-icon")[0];
      sWrapper.classList.toggle("activated");
      sIcon.classList.toggle("activated");
      this.isSearching = true;
    },
    deactivateSearch(event) {
      // If a list item is clicked, don't lose deactivate the search results list
      if (
        event.relatedTarget &&
        event.relatedTarget.classList.contains("list-item")
      ) {
        return;
      }
      let sWrapper = document.getElementsByClassName("search-wrapper")[0];
      let sIcon = document.getElementsByClassName("search-icon")[0];
      sWrapper.classList.toggle("activated");
      sIcon.classList.toggle("activated");
      this.isSearching = false;
    },

    async search(event) {
      // console.log("Search");
      // console.log(event.target.value);
      const searchTerm = event.target.value;
      const result = await this.searchUsers(searchTerm);
      this.results = [];
      result.forEach((user) => {
        this.results.push({
          content: user.displayName,
          descriptor: "A user",
          category: "user",
          routerLink: "/profile/" + user.username,
        });
      });
    },
    async showHints(event) {
      const searchTerm = event.target.value;
      const result = await this.searchUsers(searchTerm);
      this.results = [];
      result.forEach((user) => {
        this.results.push({
          content: user.displayName,
          descriptor: "A user",
          category: "user",
          routerLink: "/profile/" + user.username,
        });
      });
    },
  },
};
</script>

<template>
  <div class="search-wrapper">
    <div class="search-box-wrapper">
      <div class="search-icon">
        <BaseIcon name="search" />
      </div>
      <input
        class="search-box"
        @input="showHints"
        @change="search"
        @focus="activateSearch"
        @blur="deactivateSearch"
        placeholder="Search Kwetter"
      />
    </div>
    <div v-if="isSearching" class="search-dropdown">
      <div class="list bordered-shadow">
        <router-link
          :to="result.routerLink"
          class="list-item bordered"
          v-for="(result, key) in results"
          v-bind:key="key"
        >
          <span class="item-content">{{ result.content }}</span>
          <span class="item-hint">{{ result.descriptor }}</span>
        </router-link>
      </div>
    </div>
  </div>
</template>

<style scoped>
.search-dropdown {
  position: relative;
  flex: 1 1 0%;
}

.search-dropdown .list {
  overflow-y: auto;
  /* min-height: 100px; */
  max-height: calc(-53px + 80vh);

  position: absolute;
  top: 0px;
  right: 0px;
  left: 0px;
  overscroll-behavior: contain;
}

.search-dropdown .list .list-item {
  display: flex;
  flex-direction: column;
  cursor: pointer;
  padding: 16px;
  line-height: 1.3125;
  overflow-wrap: break-word;
  text-align: left;
}

.search-dropdown .list .list-item .item-content {
  color: var(--text-color-bright);
}

.search-dropdown .list .list-item .item-hint {
  color: var(--text-color-faded);
  font-size: 14px;
}

.search-box-wrapper {
  display: flex;
  flex-direction: row;
}

.search-icon {
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.search-icon svg {
  padding-left: 12px;
}

.search-box {
  flex-grow: 1;
  background-color: transparent;
  border: none;

  overflow-wrap: break-word;
  padding: 14px;
}

.search-box,
.search-icon svg {
  color: var(--text-color-faded);
}

.search-wrapper {
  height: 45px;
  background-color: var(--c3);

  border-radius: 999px;
  border: 1px solid;
  border-color: var(--c3);
}

.search-wrapper.activated {
  background-color: var(--c1);
  border-color: var(--main-highlight-color);
  border-style: solid;
  border-width: 1px;
}

.search-icon.activated svg {
  color: var(--main-highlight-color) !important;
}
</style>
