#!/bin/bash


docker run --restart=unless-stopped -p 5000:80 -d --name lingva-app lingva-img
