#!/bin/bash
cd ./Source/spamfilter/
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:AltCover=true
cd ../../
