# build static files
FROM node:12 AS build-env
COPY . /src
WORKDIR /src

RUN npm i -g @angular/cli
RUN npm i

RUN npm run build

# create runtime image
FROM nginx:alpine as runtime
ENV ENVIRONMENT=test

RUN rm /etc/nginx/conf.d/default.conf
COPY --from=build-env ./src/dist/ng-client-app /usr/share/nginx/html
COPY --from=build-env ./src/nginx.conf.template nginx.conf.template

EXPOSE 80 443

ENTRYPOINT envsubst '${ENVIRONMENT}' < nginx.conf.template > /etc/nginx/conf.d/default.conf && nginx -g 'daemon off;'
