#!/usr/bin/env bash



repoFolder="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd $repoFolder
ls
dotnet --version
dotnet restore
dotnet build
dotnet run &
sleep 10
wget "localhost:5000" --timeout 30 -O - 2>/dev/null

