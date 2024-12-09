#!/bin/bash

echo "Running database migrations..."

CONTAINER_ID=$(docker ps -q | head -n 1)

echo "Container ID: $CONTAINER_ID"

if [ -n "$CONTAINER_ID" ]; then
    docker exec $CONTAINER_ID dotnet CustomCADs.Presentation.dll --migrate-only
    if [ $? -eq 0 ]; then
        echo "Migrations applied successfully."
    else
        echo "Migration failed." >&2
        exit 1
    fi
else
    echo "No running container found." >&2
    exit 1
fi
