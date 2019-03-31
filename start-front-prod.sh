#!/bin/bash

# Start frontend
echo "Starting frontend"
serve -l tcp://localhost:3000 -s frontend/build
