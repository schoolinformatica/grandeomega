#!/usr/bin/env bash



repoFolder="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
cd $repoFolder
ls
dotnet --version
dotnet restore
dotnet build
dotnet run
