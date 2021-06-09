import { createHubConnection } from "@/api/tweet.js";

const socket = createHubConnection();

function registerHandlers(socket, store) {
  socket.on("stateChanged", (oldState, newState) => {
    if (oldState !== newState && newState !== "Connected")
      store.dispatch("tweet/connectionClosed");
    else store.dispatch("tweet/connectionOpened");
  });
  socket.on("failureNotification", function(failure) {
    store.dispatch("tweet/showError", failure.message);
  });
  socket.on("tweetMessageCreated", function(tweetMessage) {
    store.dispatch("tweet/addTweetToTimeLine", tweetMessage);
  });
  socket.on("tweetMessageLiked", function(notification) {
    store.dispatch("tweet/addLikeToTweet", notification);
  });
}

export default function createWebSocketPlugin() {
  return store => {
    registerHandlers(socket, store);
    store.subscribe((mutation, state) => {
      if (
        !state.tweet.connected &&
        mutation.type === "oidcStore/setOidcAuthIsChecked" &&
        socket.connectionState === "Disconnected"
      ) {
        // When user is authenticated, start socket connection
        socket
          .start()
          .then(() => {
            console.log("Socket started!");
          })
          .catch(error => {
            console.log("TODO: Handle this error in the tweet store");
            console.log(error);
          });
      }
      // TODO disconnect on logout
    });
  };
}
