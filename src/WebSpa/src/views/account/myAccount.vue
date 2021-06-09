<script>
import Layout from "@/layouts/default.vue";
import { mapGetters } from "vuex";

export default {
  components: {
    Layout
  },
  computed: {
    ...mapGetters("oidcStore", ["oidcUser", "oidcIdTokenExp", "oidcScopes"])
  },
  methods: {
    expand(event) {
      let caret = event.target;
      caret.parentElement.querySelector(".nested").classList.toggle("active");
      caret.classList.toggle("caret-down");
    },
    uDateToString(uTime) {
      // let uTime = this.oidcUser.auth_time;
      let date = new Date(uTime);
      const year = date.getFullYear();
      const day = "" + date.getDate();
      const month = 1 + date.getMonth();
      const hours = date.getHours();
      const minutes = "0" + date.getMinutes();
      const seconds = "0" + date.getSeconds();
      return (
        day +
        "-" +
        month +
        "-" +
        year +
        " " +
        hours +
        ":" +
        minutes.substr(-2) +
        ":" +
        seconds.substr(-2)
      );
    }
  }
};
</script>

<template>
  <Layout>
    <div class="myaccount">
      <div class="page-main-section">
        <div class="content">
          <div class="textblock">
            <h1>{{ oidcUser.preferred_username }}</h1>
            <p>
              You are logged in as {{ oidcUser.name }}. Your session started at
              {{ uDateToString(oidcUser.auth_time * 1000) }} and will end at
              {{ uDateToString(oidcIdTokenExp) }}.
            </p>
            <div class="list-row">
              <div class="list-col">
                <h2>Access</h2>
                <ul class="access-list">
                  <li v-for="(val, key) in oidcScopes" v-bind:key="key">
                    {{ val }}
                  </li>
                </ul>
              </div>
              <div class="account-tree list-col">
                <h2 class="caret" @click="expand">Advanced information</h2>
                <ul class="nested">
                  <template v-for="(val, key) in oidcUser" v-bind:key="key">
                    <li>
                      <b>{{ key }}:</b> {{ val }}
                    </li>
                  </template>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </Layout>
</template>

<style scoped>
.list-row {
  margin: auto;
  max-width: 850px;
}

.list-col {
  width: 50%;
  max-width: 300px;
  text-align: left;
}

.access-list {
  list-style: circle;
  text-align: left;
}

.account-tree {
  text-align: left;
  margin: auto;
  max-width: 500px;
}
.caret {
  cursor: pointer;
  user-select: none; /* Prevent text selection */
}
.caret::before {
  content: "\25B6";
  color: black;
  display: inline-block;
  margin-right: 6px;
}
.caret-down::before {
  transform: rotate(90deg);
}
.nested {
  display: none;
  margin-top: 0;
}
.active {
  display: block;
}
</style>
