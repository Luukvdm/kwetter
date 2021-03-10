<script>
export default {};
</script>

<template>
  <header>
    <div class="logo-container">
      <a class="logo">
        <img src="@/assets/logo.png" />
      </a>
    </div>
    <nav>
      <div class="nav-item">
        <BaseGlowyButton
          text="Explore"
          symbol="hashtag"
          includeTextInHover="true"
        />
      </div>
      <div class="nav-item">
        <BaseGlowyButton
          text="Settings"
          symbol="cog"
          includeTextInHover="true"
        />
      </div>
    </nav>
  </header>
</template>

<style>
header {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  -moz-box-align: start;
  align-items: flex-start;
}

.logo-container {
  max-width: 48px;
  max-height: 48px;
  margin: 5px;
  padding: 2px 0;
}

.logo img {
  width: 100%;
  height: 100%;
  padding: 2px;
}

nav {
  margin: 5px 0;
}

nav .nav-item {
  color: var(--text-color-bright);
  padding: 12px;
}

nav .nav-item .symbol-box {
  color: var(--text-color-bright);
  font-size: 17pt;
}
</style>
