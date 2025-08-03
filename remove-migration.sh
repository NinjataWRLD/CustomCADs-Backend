#!/bin/bash

MODULE=$1

if [ -z "$MODULE" ]; then 
  echo "Usage: ./remove-migration.sh Module"
  exit 1
fi

STARTUP_PATH="./src/Tools/Migrations/CustomCADs.Tools.Migrations.csproj"
PROJECT_PATH="./src/Modules/$MODULE/Persistence/CustomCADs.$MODULE.Persistence.csproj"
CONTEXT="${MODULE}Context"

dotnet ef migrations remove -s "$STARTUP_PATH" -p "$PROJECT_PATH" -c "$CONTEXT"
