import request from "@/util/request.js";

export function get(username) {
  return request({
    url: "/api/web/profile/" + username,
    method: "get"
  });
}

export function me() {
  return request({
    url: "/api/web/profile/me",
    method: "get"
  });
}
