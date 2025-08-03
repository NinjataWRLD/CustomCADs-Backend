#!/bin/bash

DIAGNOSTICS="IDE0130";

dotnet format CustomCADs.sln --exclude-diagnostics "$DIAGNOSTICS"