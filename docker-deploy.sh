#!/bin/bash

# create container and start app server
docker run --restart=unless-stopped -p 5000:80 -d --name lingva-app lingva-img

# set the connectionString as environment variable

# deploy a seed data optional

# restart the container

