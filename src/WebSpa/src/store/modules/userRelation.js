import { follow, unFollow } from "@/api/userRelation.js";

export const state = {};

export const getters = {};

export const mutations = {};

export const actions = {
  async unFollowUser(_, userId) {
    await unFollow(userId);
  },
  async followUser(_, userId) {
    await follow(userId);
  }
};
