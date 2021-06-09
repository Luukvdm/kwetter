import { get, getTimeline, create, like } from "@/api/tweet.js";

export const state = {
  hubConnectionIsOpen: false,
  cachedTimelines: []
};

export const getters = {};

export const mutations = {
  SET_CONNECTION_STATE(state, isOpen) {
    console.log("Connection is open: " + isOpen.toString());
    state.hubConnectionIsOpen = isOpen;
  },
  CACHE_TIMELINE(state, timeline) {
    // Remove possible already cached timelines
    state.cachedTimelines = state.cachedTimelines.filter(
      tl => tl.username !== timeline.username
    );

    state.cachedTimelines.push(timeline);
  },
  CREATE_TWEET(state, tweet) {
    const tl = state.cachedTimelines.find(
      e => e.username === tweet.poster.username
    );
    if (tl) state.cachedTimelines[0].tweets.unshift(tweet);
  }
};

export const actions = {
  async getTimeline({ commit, state }, username) {
    const cached = state.cachedTimelines.find(e => e.username === username);
    if (cached) return cached;

    const { data } = await getTimeline(username);
    commit("CACHE_TIMELINE", data);
    return data;
  },
  async createTweet({ commit }, entity) {
    const response = await create(entity);
    commit("START_CREATE_TWEET", response);
    return response.data;
  },
  async likeTweet(_, tweetId) {
    const { data } = await like(tweetId);
    return data;
  },
  async addTweetToTimeLine({ commit }, entity) {
    const { data } = await get(entity.id);
    commit("CREATE_TWEET", data);
  },
  async addLikeToTweet({ state }, notification) {
    const timelines = state.cachedTimelines;
    timelines.forEach(timeline => {
      const tweet = timeline.tweets.find(
        t => t.id == notification.tweetMessageId
      );
      if (tweet) {
        tweet.likes += 1;
      }
    });
  },
  showError(_, message) {
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
