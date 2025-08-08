#!/bin/bash

set -e

./GrpcServer &
sleep 1

CLIENT_OUTPUT=$(./GrpcClient add 1 2 -p 5000)
if [ "$CLIENT_OUTPUT" == "3" ]; then
    exit 0
else
    echo "Got: $CLIENT_OUTPUT"
    exit 1
fi