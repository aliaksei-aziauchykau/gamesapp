# Stage 1: Build the Angular application
FROM node:20 as build

# Set the working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install the dependencies
RUN npm install

# Copy the rest of the application code
COPY . .

ENV NX_DISABLE_DB=true

# !!
# RUN npx nx reset

# Build the Angular application
RUN npx nx run game-portal:build --configuration=production

# Stage 2: Serve the application with Nginx
FROM node:20

# Copy the built Angular application from the build stage
COPY --from=build /app/dist /app/dist
COPY --from=build /app/projects/game-portal/ssl /app/dist/game-portal/server/ssl
