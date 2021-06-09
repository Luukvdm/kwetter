import { get, me } from "@/api/profile.js";

export const state = {
  cachedProfiles: [],
  cachedMyProfile: null
};

export const getters = {};

export const mutations = {
  CACHE_PROFILE(state, profile) {
    state.cachedProfiles.push(profile);
  },
  CACHE_MY_PROFILE(state, profile) {
    state.cachedMyProfile = profile;
  }
};

export const actions = {
  async getProfile({ commit, state }, username) {
    const cachedProfile = state.cachedProfiles.find(
      e => e.username === username
    );
    if (cachedProfile) return cachedProfile;

    const { data } = await get(username);
    commit("CACHE_PROFILE", data);
    return data;
  },
  async getMyProfile({ commit, state }) {
    const cachedProfile = state.cachedMyProfile;
    if (cachedProfile) return cachedProfile;

    const { data } = await me();
    commit("CACHE_MY_PROFILE", data);
    return data;
  }
};
