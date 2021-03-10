<script>
import TweetList from "./tweetList.vue";

export default {
  components: {
    TweetList,
  },
  data() {
    return {
      tweets: [
        {
          id: 1,
          message: "Hello World!",
          postTime: new Date(),
          hearts: "5",
          poster: {
            name: "Allice",
            username: "alice1337",
            isVerifified: true,
            picture:
              "https://pbs.twimg.com/profile_images/1323327102813413380/3sH2E9Yt_200x200.jpg",
          },
        },
        {
          id: 2,
          message: "Bye Bye World!",
          postTime: new Date(),
          hearts: "2",
          poster: {
            name: "Bob",
            username: "king-bob",
            isVerifified: false,
            picture:
              "https://pbs.twimg.com/profile_images/1323327102813413380/3sH2E9Yt_200x200.jpg",
          },
        },
        {
          id: 3,
          message:
            "Bye Bye World! Bye Bye World! Bye Bye World! Bye Bye World! Bye Bye World! Bye Bye World! Bye Bye World! Bye Bye World! Bye Bye World! Bye Bye World! Bye Bye World! Bye Bye World! ",
          postTime: new Date(),
          hearts: "2",
          poster: {
            name: "Charly",
            username: "charly007",
            isVerifified: false,
            picture:
              "https://pbs.twimg.com/profile_images/1323327102813413380/3sH2E9Yt_200x200.jpg",
          },
        },
        {
          id: 4,
          message:
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla varius urna non nisi eleifend, in luctus enim dictum. Cras et vulputate leo. Nulla ut turpis ac diam ultricies placerat at nec justo. Etiam viverra gravida tincidunt. Praesent efficitur, dui eget fringilla hendrerit, ligula ante vestibulum odio, eget sodales turpis nisi egestas libero. Sed malesuada lobortis nisi at porttitor. Maecenas fermentum ex mauris, at pharetra lorem commodo a. Aenean suscipit sagittis mi vel egestas. Cras rutrum ultricies ante. Mauris massa nulla, viverra non facilisis et, molestie quis metus. Cras auctor condimentum nunc a pulvinar. Integer id turpis rutrum, placerat risus vitae, vestibulum ex. Curabitur ut velit orci. Ut tempus magna ex, in porttitor dolor gravida non.",
          postTime: new Date(),
          hearts: "2",
          poster: {
            name: "Don",
            username: "don-don",
            isVerifified: false,
            picture:
              "https://pbs.twimg.com/profile_images/1323327102813413380/3sH2E9Yt_200x200.jpg",
          },
        },
        {
          id: 5,
          message:
            "Pellentesque et pellentesque nisi. Mauris lorem nunc, semper ut orci sit amet, molestie pharetra leo. In gravida lectus ac urna ullamcorper, quis fringilla ex porta. Ut fermentum consequat magna quis consectetur. Mauris in augue erat. Pellentesque pulvinar lorem et leo aliquet finibus. Curabitur sit amet posuere nisl. Nullam accumsan interdum eleifend. Quisque aliquam mauris sit amet libero imperdiet gravida. Nulla condimentum mollis lectus eget scelerisque. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Etiam sed odio non enim iaculis blandit nec non lorem. Pellentesque volutpat, tellus eu molestie posuere, nulla orci blandit velit, eu porttitor diam eros et libero. Suspendisse scelerisque ligula sit amet erat ornare pharetra. Maecenas ut feugiat metus. ",
          postTime: new Date(),
          hearts: "2",
          poster: {
            name: "Erik",
            username: "erik123",
            isVerifified: false,
            picture:
              "https://pbs.twimg.com/profile_images/1323327102813413380/3sH2E9Yt_200x200.jpg",
          },
        },
        {
          id: 6,
          message:
            "Aenean semper fermentum diam non sollicitudin. Donec sodales maximus metus, quis efficitur lacus euismod lobortis. Integer eget blandit purus. Nulla finibus erat vel nibh tincidunt gravida. Sed quis varius tortor. Nulla sit amet sem scelerisque, pharetra urna euismod, tincidunt massa. Nulla facilisi. Ut enim nisl, varius eget felis pulvinar, egestas dapibus sapien. ",
          postTime: new Date(),
          hearts: "2",
          poster: {
            name: "Fsomething",
            username: "ffffffffffff",
            isVerifified: false,
            picture:
              "https://pbs.twimg.com/profile_images/1323327102813413380/3sH2E9Yt_200x200.jpg",
          },
        },
        {
          id: 7,
          message:
            "Quisque tellus urna, ornare id luctus eget, ultricies vel metus. Maecenas id ultricies nisi. Suspendisse sit amet augue ut purus vehicula fermentum sit amet id ipsum. Suspendisse velit metus, elementum a sollicitudin hendrerit, sagittis ut magna. Praesent nec mauris eget nunc rutrum vehicula. Aliquam imperdiet, dolor vel placerat congue, elit tortor sollicitudin risus, non suscipit risus velit dapibus lacus. Nulla fermentum rhoncus nisl sit amet finibus. Nunc ut quam hendrerit, lacinia nunc id, interdum ligula. Sed vulputate faucibus eros, ut euismod erat vulputate sed. Curabitur faucibus vestibulum tellus. Cras nunc mi, congue vitae facilisis vel, luctus at mi. Sed rhoncus neque vel tellus porttitor varius. Curabitur non tristique nibh. Nullam fringilla fermentum blandit. Vivamus aliquam diam libero, ut venenatis lacus cursus at. ",
          postTime: new Date(),
          hearts: "2",
          poster: {
            name: "Gary",
            username: "gary",
            isVerifified: false,
            picture:
              "https://pbs.twimg.com/profile_images/1323327102813413380/3sH2E9Yt_200x200.jpg",
          },
        },
      ],
    };
  },
};
</script>

<template>
  <TweetList :tweets="tweets" />
</template>

<style scoped></style>
