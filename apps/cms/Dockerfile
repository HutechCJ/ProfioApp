FROM docker.io/node:lts-alpine as deps

RUN apk add --no-cache libc6-compat
WORKDIR /usr/src/app
COPY dist/apps/cms/package*.json ./
RUN npm install --only=production -f

FROM docker.io/node:lts-alpine as runner
RUN apk add --no-cache dumb-init
ENV NODE_ENV production
ENV PORT 3000
ENV HOST 0.0.0.0
ENV NEXT_TELEMETRY_DISABLED 1

WORKDIR /usr/src/app
COPY --from=deps /usr/src/app/node_modules ./node_modules
COPY --from=deps /usr/src/app/package.json ./package.json
COPY dist/apps/cms/public ./public
COPY dist/apps/cms/.next ./.next
RUN chown -R node:node .
USER node
EXPOSE 3000

CMD ["dumb-init", "node_modules/.bin/next", "start"]
