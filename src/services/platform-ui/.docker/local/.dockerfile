# Stage 1: Build the Angular application
FROM node:20 as build

# Set the working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install the dependencies
RUN npm install

# # Copy the rest of the application code
COPY . .

# Build the Angular application
# ENTRYPOINT ["npx nx run game-portal:server --configuration=production"]
