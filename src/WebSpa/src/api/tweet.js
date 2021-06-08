import request from "@/util/request.js";
import { hubConnectionBuilder } from "@/util/hubConnection.js";

export function get(id) {
  return request({
    url: "/api/web/tweets/" + id.toString(),
    method: "get"
  });
}

export function getTimeline() {
  return request({
    url: "/api/web/tweets/timeline",
    method: "get"
  });
}

export function create(entity) {
  return request({
    url: "/api/web/tweets",
    method: "post",
    data: entity
  });
}

export function like(tweetId) {
  return request({
    url: "/api/web/tweets/like/" + tweetId.toString(),
    method: "post"
  });
}

export function createHubConnection() {
  return hubConnectionBuilder("/hub/tweet/tweet");
}
