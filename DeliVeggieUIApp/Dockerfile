FROM node:10.16.1-alpine as builder

WORKDIR /deliveggieuiapp
COPY . .

RUN npm i
RUN npm run build --prod

FROM nginx:1.15.8-alpine 

COPY --from=builder /deliveggieuiapp/dist/deliveggieapp /usr/share/nginx/html
EXPOSE 80
CMD ["nginx","-p","daemon off;"]