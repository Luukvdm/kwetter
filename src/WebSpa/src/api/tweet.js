import request from "@/util/request.js";
import { hubConnectionBuilder } from "@/util/hubConnection.js";

export function get(id) {
  return request({
    url: "/tweets/" + id.toString(),
    method: "get"
  });
}

export function getTimeline() {
  return request({
    url: "/tweets/timeline",
    method: "get"
  });
}

export function create(entity) {
  return request({
    url: "/tweets",
    method: "post",
    data: entity
  });
}

export function createHubConnection() {
  return hubConnectionBuilder("/hub/tweet");
}
