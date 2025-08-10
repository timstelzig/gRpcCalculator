#!/bin/bash

set -e

# Start server in the background
./GrpcServer &
sleep 1

# Run client and capture output
CLIENT_OUTPUT=$(./GrpcClient add 1 2 -p 5000)

# Check that the output is as expected
if [ "$CLIENT_OUTPUT" == "3" ]; then
    exit 0
else
    echo "Got: $CLIENT_OUTPUT"
    exit 1
fi