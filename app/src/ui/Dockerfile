# Build stage
FROM node:22.14.0-alpine AS builder
WORKDIR /usr/src/app
COPY ./app/src/ui/ /usr/src/app/
RUN echo $(ls -1 .)
RUN npm install -g @angular/cli
RUN npm install
RUN ng build --configuration=production --base-href /ui/


# Runtime stage
FROM nginx:alpine
COPY --from=builder /usr/src/app/dist/esalq-ui/browser /usr/share/nginx/html
RUN echo "antes de alterar: cat /etc/nginx/nginx.conf:" && cat /etc/nginx/nginx.conf
COPY --from=builder /usr/src/app/nginx.conf /etc/nginx
RUN echo "antes de alterar: cat /etc/nginx/conf.d/default.conf:" && cat /etc/nginx/conf.d/default.conf
COPY --from=builder /usr/src/app/default.conf /etc/nginx/conf.d/
RUN echo "Verificando arquivos gerados:" && ls -1 /usr/share/nginx/html
RUN echo "Verificando arquivos gerados:" && ls -1 /etc/nginx/
RUN echo "depois de alterar: cat /etc/nginx/nginx.conf:" && cat /etc/nginx/nginx.conf
RUN echo "depois de alterar: cat /etc/nginx/conf.d/default.conf:" && cat /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]