import { get, getTimeline, create, like } from "@/api/tweet.js";

export const state = {
  hubConnectionIsOpen: false,
  cachedTimeline: []
};

export const getters = {};

export const mutations = {
  SET_CONNECTION_STATE(state, isOpen) {
    console.log("Connection is open: " + isOpen.toString());
    state.hubConnectionIsOpen = isOpen;
  },
  CACHE_TIMELINE(state, timeline) {
    state.cachedTimeline = timeline;
  },
  CREATE_TWEET(state, tweet) {
    state.cachedTimeline.tweets.push(tweet);
  }
};

export const actions = {
  async getTimeline({ commit /*, state*/ }) {
    const { data } = await getTimeline();
    commit("CACHE_TIMELINE", data);
    return data;
  },
  async createTweet({ commit }, entity) {
    const response = await create(entity);
    commit("START_CREATE_TWEET", response);
    return response.data;
  },
  async likeTweet({ commit }, tweetId) {
    const { data } = await like(tweetId);
    commit("START_LIKE_TWEET", tweetId);
    console.log(data);
  },
  async addTweetToTimeLine({ commit }, entity) {
    const { data } = await get(entity.id);
    commit("CREATE_TWEET", data);
  },
  showError(message) {
    console.log("Received tweet exception:");
    console.log(message);
  },
  connectionOpened({ commit }) {
    commit("SET_CONNECTION_STATE", true);
  },
  connectionClosed({ commit }) {
    commit("SET_CONNECTION_STATE", false);
  }
};
