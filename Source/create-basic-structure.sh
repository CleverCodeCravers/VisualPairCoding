#!/bin/bash

#
# usage: create-basic-structure.sh <nameOfProject>
# 
# creates the necessary folders and projects as well as a suiting dotnet sln
#

mkdir $1
cd $1

dotnet new sln
dotnet new gitignore

mkdir $1.Interfaces
cd $1.Interfaces
dotnet new classlib
cd ..

mkdir $1.BL
cd $1.BL
dotnet new classlib
cd ..

mkdir $1.BL.Tests
cd $1.BL.Tests
dotnet new classlib
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package altcover
cd ..

mkdir $1.Infrastructure
cd $1.Infrastructure
dotnet new classlib
cd ..

dotnet sln "./$1.sln" add "./$1.Interfaces/"
dotnet sln "./$1.sln" add "./$1.BL/"
dotnet sln "./$1.sln" add "./$1.BL.Tests/"
dotnet sln "./$1.sln" add "./$1.Infrastructure/"

