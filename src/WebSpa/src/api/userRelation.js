import request from "@/util/request.js";

export function follow(userId) {
  return request({
    url: "/api/web/userrelations/follow/" + userId,
    method: "post"
  });
}

export function unFollow(userId) {
  return request({
    url: "/api/web/userrelations/unfollow/" + userId,
    method: "post"
  });
}
