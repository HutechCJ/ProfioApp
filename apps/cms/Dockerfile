FROM docker.io/node:lts-alpine as deps

RUN apk add --no-cache libc6-compat
WORKDIR /usr/src/app
COPY dist/apps/cms/package*.json ./
RUN npm install --only=production -f

RUN mkdir /temp_copy
COPY dist/apps/cms/.next /temp_copy

FROM docker.io/node:lts-alpine as runner
RUN apk add --no-cache dumb-init
ENV NODE_ENV production
ENV PORT 3000
ENV HOST 0.0.0.0
ENV NEXT_TELEMETRY_DISABLED 1

WORKDIR /usr/src/app
COPY --from=deps /usr/src/app/node_modules ./node_modules
COPY --from=deps /usr/src/app/package.json ./package.json
COPY --from=deps /temp_copy/.next ./.next
RUN chown -R node:node .
USER node
EXPOSE 3000

CMD ["dumb-init", "node_modules/.bin/next", "start"]
