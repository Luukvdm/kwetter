# build stage
FROM node:lts-alpine as build-stage
WORKDIR /app
COPY WebSpa/package*.json ./
RUN yarn install
COPY WebSpa .
ENV NODE_ENV=production
RUN yarn build --mode production

# Package stage
FROM nginx:stable-alpine as production-stage
COPY WebSpa/nginx/nginx.conf /etc/nginx/nginx.conf
# COPY --from=build-stage /app/nginx/default.conf /etc/nginx/conf.d/default.conf
COPY --from=build-stage /app/dist /app
RUN chown nginx:nginx /app
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
