#!/usr/bin/env bash

REDOC_PORT=${1:-3100}
npx redoc-cli serve -p $REDOC_PORT --watch spec.yml
