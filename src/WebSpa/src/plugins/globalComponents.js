export default {
  install: app => {
    // Plugin code goes here
    const requireComponent = require.context(
      "@/components",
      false,
      /_base-[\w-]+\.vue$/
    );

    requireComponent.keys().forEach(fileName => {
      const componentConfig = requireComponent(fileName);
      const componentName = fileName
        // Remove the "./_" from the beginning
        .replace(/^\.\/_/, "")
        // Remove the file extension from the end
        .replace(/\.\w+$/, "")
        // Split up kebabs
        .split("-")
        // Upper case
        .map(kebab => kebab.charAt(0).toUpperCase() + kebab.slice(1))
        // Concatenated
        .join("");

      app.component(componentName, componentConfig.default || componentConfig);
    });
  }
};
